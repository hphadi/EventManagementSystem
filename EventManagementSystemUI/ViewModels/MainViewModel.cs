using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EventManagementSystemUI.Views;
using EventManagementSystem.Models;
using EventManagementSystemUI.Models;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json; 
using System.Windows;
using EventManagementSystemUI.Tools;
using System.Net;


namespace EventManagementSystemUI.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<EventManagementSystem.Models.Event> events;

        [ObservableProperty]
        private ObservableCollection<EventManagementSystem.Models.Event> futureEvents;

        [ObservableProperty]
        private Visibility newEventButtonVisibility = Visibility.Collapsed;
        
        [ObservableProperty]
        private Visibility signInButtonVisibility = Visibility.Visible;
        
        [ObservableProperty]
        private Visibility signOutButtonVisibility = Visibility.Collapsed;

        [ObservableProperty]
        private ObservableCollection<EventManagementSystem.Models.Group> groups;
        public ObservableCollection<Group> SelectedGroups { get; set; } = new();

        [ObservableProperty]
        private string eventTitle = "";

        [ObservableProperty]
        private string eventDescription = "";

        [ObservableProperty]
        private DateTime? eventStartDateTime;

        [ObservableProperty]
        private DateTime? eventEndDateTime;

        [ObservableProperty]
        private LoginUser loginUser = new();

        [ObservableProperty]
        private string eventLocation = "";

        [ObservableProperty]
        private NewUser newUserDraft = new();

        private readonly HttpClient _httpClient;

        [ObservableProperty]
        private EventManagementSystem.Models.Person currentUser = new();

        public MainViewModel()
        {
            System.Diagnostics.Debug.WriteLine("MainViewModel initialized"); // for debugging
            _httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:5144/api/") };
            Events = new ObservableCollection<EventManagementSystem.Models.Event>();
            FutureEvents = new ObservableCollection<EventManagementSystem.Models.Event>();
            Groups = new ObservableCollection<EventManagementSystem.Models.Group>();
            LoadGroupsCommand.Execute(null);
        }

        [RelayCommand]
        private async Task LoadEvents()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("Loading events..."); // for debugging
                var events = await _httpClient.GetFromJsonAsync<List<EventManagementSystem.Models.Event>>("Event");
                if (events != null)
                {
                    Events.Clear();
                    FutureEvents.Clear();
                    foreach (var evt in events)
                    {
                        Events.Add(evt); // each event is added to the ObservableCollection
                        if (evt.StartDate > DateTime.Now)
                        {
                            FutureEvents.Add(evt); // each future event is added to the FutureEvents ObservableCollection
                        }
                    }
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("No events loaded."); // for debugging
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in LoadEvents: {ex.Message}"); // for debugging
                MessageBox.Show($"Error loading events: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        [RelayCommand]
        private async Task LoadGroups()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("Loading groups..."); // for debugging
                var groups = await _httpClient.GetFromJsonAsync<List<EventManagementSystem.Models.Group>>("Group");
                if (groups != null)
                {
                    Groups.Clear();
                    foreach (var group in groups)
                    {
                        Groups.Add(group); // each group is added to the ObservableCollection

                    }
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("No groups loaded."); // for debugging
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in LoadGroups: {ex.Message}"); // for debugging
                MessageBox.Show($"Error loading groups: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        [RelayCommand]
        private void Navigate(string page)
        {
            var frame = Application.Current.MainWindow.FindName("MainFrame") as System.Windows.Controls.Frame;
            if (frame == null) return;

            switch (page)
            {
                case "Dashboard":
                    frame.Content = new DashboardView { DataContext = this };
                    break;
                case "Events":
                    frame.Content = new EventManagementView { DataContext = this };
                    break;
                case "Groups":
                    frame.Content = new GroupManagementView { DataContext = this };
                    break;
                case "Profile":
                    frame.Content = new ProfileView { DataContext = this };
                    break;
                case "NewEvent":
                    frame.Content = new NewEvent { DataContext = this };
                    NewEventButtonVisibility = Visibility.Visible;
                    break;
                case "Register":
                    frame.Content = new SignUpView { DataContext = this };
                    break;
                case "SignIn":
                    frame.Content = new SignIn{ DataContext = this };
                    break;
            }
        }

        [RelayCommand]
        private void ShowNewEvent()
        {
            var frame = Application.Current.MainWindow.FindName("MainFrame") as System.Windows.Controls.Frame;
            if (frame != null)
            {
                frame.Content = new NewEvent { DataContext = this };
                NewEventButtonVisibility = Visibility.Visible;
            }
        }

        [RelayCommand]
        private async Task SubmitNewEvent()
        {

            if (string.IsNullOrWhiteSpace(EventTitle) || EventTitle == "Enter Title" ||
                string.IsNullOrWhiteSpace(EventDescription) || EventDescription == "Enter Description" ||
                EventEndDateTime == null || EventStartDateTime == null ||
                string.IsNullOrWhiteSpace(EventLocation) || EventLocation == "Enter Location")
            {
                MessageBox.Show("Please fill in all fields.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var startDateUtc = EventStartDateTime.Value.ToUniversalTime();
            var endDateUtc = EventEndDateTime.Value.ToUniversalTime();

            var newEvent = new EventManagementSystem.Models.EventDto
            {
                Title = EventTitle,
                Description = EventDescription,
                StartDate = startDateUtc,
                EndDate = endDateUtc,
                Location = EventLocation,
                CreatedAt = DateTime.Now.ToUniversalTime(),
                GroupIds = SelectedGroups.Select(g => g.Id).ToList()
            };


            try
            {
                var response = await _httpClient.PostAsJsonAsync("Event", newEvent);
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Event created successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    await LoadEvents();
                    CancelNewEventCommand.Execute(null);
                }
                else
                {
                    MessageBox.Show($"Failed to create event: {response.StatusCode}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error submitting event: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        [RelayCommand]
        private void CancelNewEvent()
        {
            NewEventButtonVisibility = Visibility.Collapsed; 
            var frame = Application.Current.MainWindow.FindName("MainFrame") as System.Windows.Controls.Frame;
            if (frame != null)
            {
                frame.Content = new EventManagementView { DataContext = this };
                EventTitle = "";
                EventDescription = "";
                EventStartDateTime = null;
                EventEndDateTime = null;
                EventLocation = "";
            }
        }

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
                    var person = await response.Content.ReadFromJsonAsync<Person>();

                    // Optionally store logged-in user
                    CurrentUser = person;

                    MessageBox.Show("Login successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    CloseLogInCommand?.Execute(null);
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
        private void CloseLogIn()
        {
            var frame = Application.Current.MainWindow.FindName("MainFrame") as System.Windows.Controls.Frame;
            if (frame != null)
            {
                frame.Content = new DashboardView { DataContext = this };
            }
            if(CurrentUser.Name != string.Empty)
            {
                SignInButtonVisibility = Visibility.Collapsed;
                SignOutButtonVisibility = Visibility.Visible;
            }
            else
            {
                SignInButtonVisibility = Visibility.Visible;
                SignOutButtonVisibility = Visibility.Collapsed;
            }
        }

        [RelayCommand]
        private void SignOut()
        {
            CurrentUser = new();
            CloseLogInCommand?.Execute(null);
        }

    }
}