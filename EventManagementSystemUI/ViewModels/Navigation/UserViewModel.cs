using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EventManagementSystem.Models;
using EventManagementSystemUI.Models;
using EventManagementSystemUI.Tools;
using EventManagementSystemUI.Views;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows;
using System.Windows.Data;

namespace EventManagementSystemUI.ViewModels
{
    public partial class UserViewModel : ObservableObject
    {
        private readonly HttpClient _httpClient;
        private readonly MainViewModel _vm;

        public UserViewModel(HttpClient httpClient, MainViewModel vm)
        {
            _httpClient = httpClient;
            _vm = vm;
        }

        [ObservableProperty]
        private LoginUser loginUser = new();

        [ObservableProperty]
        private NewUser newUserDraft = new();

        public int userId = 0;

        [ObservableProperty]
        private EventManagementSystem.Models.PersonWithDetailsDto currentUser = new();

        [ObservableProperty]
        private EventManagementSystem.Models.SimpleEventDto selectedEvent = new();

        [RelayCommand]
        private async Task SubmitRegistration()
        {
            if (NewUserDraft.IsValid() == false)
            {
                MessageBox.Show("Please fill in all fields.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            else if (NewUserDraft.Password != NewUserDraft.RepeatPassword)
            {
                MessageBox.Show("Passwords do not match.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var newUser = new PersonDto
            {
                Name = NewUserDraft.Name,
                Username = NewUserDraft.UserName.ToLower(),
                Password = PasswordHasher.Hash(NewUserDraft.Password)
            };

            try
            {
                var response = await _httpClient.PostAsJsonAsync("person/register", newUser);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Registration successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    CloseLogInCommand?.Execute(null);
                }
                else if (response.StatusCode == HttpStatusCode.Conflict)
                {
                    MessageBox.Show("Username already exists.", "Conflict", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    MessageBox.Show($"Registration failed: {response.StatusCode}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        [RelayCommand]
        private async Task SubmitSignIn()
        {
            if (LoginUser.IsValid() == false)
            {
                MessageBox.Show("Please fill in all fields.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var loginRequest = new EventManagementSystem.Models.LoginDto
            {
                Username = LoginUser.UserName.ToLower(),
                Password = PasswordHasher.Hash(LoginUser.Password)
            };

            try
            {
                var response = await _httpClient.PostAsJsonAsync("person/login", loginRequest);

                if (response.IsSuccessStatusCode)
                {
                    var person = await response.Content.ReadFromJsonAsync<Person_>();
                    if(person != null)
                    {
                        MessageBox.Show("Login successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        CloseLogInCommand?.Execute(true);
                        userId = person.Id;
                        LoadUserDetailsCommand.Execute(null);
                        _vm.NavVM.NavigateCommand.Execute("Profile");
                    }
                    
                }
                else if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    MessageBox.Show($"Login error: {response.StatusCode}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Login exception: {ex.Message}", "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        [RelayCommand]
        private void CloseLogIn(bool loggedIn = false)
        {
            if (loggedIn)
            {
                _vm.NavVM.ChangeVisibility("SignIn", false);
                _vm.NavVM.ChangeVisibility("Register", false);
                _vm.NavVM.ChangeVisibility("SignOut", true);
                _vm.NavVM.ChangeVisibility("Profile", true);
                _vm.UserVM?.LoadUserDetailsCommand.Execute(null);
                _vm.NavVM.Navigate("Profile");
                _vm.UserButtonsVisibility = Visibility.Visible;
            }
            else
            {
                _vm.NavVM.ChangeVisibility("SignIn", true);
                _vm.NavVM.ChangeVisibility("Register", true);
                _vm.NavVM.ChangeVisibility("SignOut", false);
                _vm.NavVM.ChangeVisibility("Profile", false);
                _vm.NavVM.Navigate("Dashboard");
                _vm.UserButtonsVisibility = Visibility.Collapsed;
            }
        }

        [RelayCommand]
        private void SignOut()
        {
            CurrentUser = new();
            CloseLogInCommand?.Execute(false);
        }

        [RelayCommand]
        private async Task LoadUserDetails()
        {
            if (userId == 0) return;
            var user = await _httpClient.GetFromJsonAsync<EventManagementSystem.Models.PersonWithDetailsDto>($"person/{userId.ToString()}");
            if (user != null)
            {
                CurrentUser = user;
            }
        }
        [RelayCommand]
        private void EventSelected()
        {
            if (SelectedEvent is not null)
            {
                var data = new NavData(SelectedEvent.Id, SelectedEvent.Title);
                _vm.NavVM.AddEventToMenu(data);
            }
        }
    }
}