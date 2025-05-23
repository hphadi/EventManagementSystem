using CommunityToolkit.Mvvm.ComponentModel;
using EventManagementSystemUI.Models;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Windows.Media;
using System.Windows;
using CommunityToolkit.Mvvm.Input;
using EventManagementSystemUI.Views;

namespace EventManagementSystemUI.ViewModels
{
    public partial class NavigationViewModel : ObservableObject
    {
        private readonly HttpClient _httpClient;
        private readonly MainViewModel _vm;

        public NavigationViewModel(HttpClient httpClient, MainViewModel vm)
        {
            _httpClient = httpClient;
            _vm = vm;
        }

        public ObservableCollection<NavigationButton> DynamicButtons { get; } = new();

        public void LoadButtons()
        {
            var redBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF32B21"));
            DynamicButtons.Add(new NavigationButton { Id = "Dashboard", Title = "Dashboard", CommandParameter = "Dashboard", Command = NavigateCommand });
            DynamicButtons.Add(new NavigationButton { Id = "Events", Title = "Events", CommandParameter = "Events", Command = NavigateCommand });
            DynamicButtons.Add(new NavigationButton { Id = "Groups", Title = "Groups", CommandParameter = "Groups", Command = NavigateCommand });
            DynamicButtons.Add(new NavigationButton { Id = "NewEvent", Title = "New Event", CommandParameter = "NewEvent", Command = NavigateCommand, Background = redBrush, Visibility = Visibility.Collapsed });
            DynamicButtons.Add(new NavigationButton { Id = "SignOut", Title = "Sign Out", Command = _vm.UserVM.SignOutCommand, Visibility = Visibility.Collapsed });
            DynamicButtons.Add(new NavigationButton { Id = "Register", Title = "Register", CommandParameter = "Register", Command = NavigateCommand });
            DynamicButtons.Add(new NavigationButton { Id = "SignIn", Title = "Sign In", CommandParameter = "SignIn", Command = NavigateCommand });
        }

        [RelayCommand]
        public void Navigate(string page)
        {
            var frame = Application.Current.MainWindow.FindName("MainFrame") as System.Windows.Controls.Frame;
            if (frame == null) return;

            switch (page)
            {
                case "Dashboard":
                    frame.Content = new DashboardView { DataContext = _vm };
                    break;
                case "Events":
                    frame.Content = new EventManagementView { DataContext = _vm };
                    break;
                case "Groups":
                    frame.Content = new GroupManagementView { DataContext = _vm };
                    break;
                case "GroupDetails":
                    frame.Content = new GroupDetailsView { DataContext = _vm };
                    break;
                case "Profile":
                    frame.Content = new ProfileView { DataContext = _vm };
                    break;
                case "NewEvent":
                    frame.Content = new NewEvent { DataContext = _vm };
                    break;
                case "Register":
                    frame.Content = new SignUpView { DataContext = _vm };
                    break;
                case "SignIn":
                    frame.Content = new SignIn { DataContext = _vm };
                    break;
            }
        }

        public NavigationButton? GetButtonById(string id)
        {
            return DynamicButtons.FirstOrDefault(b => b.Id == id);
        }
        public void ChangeVisibility(string id, bool visible)
        {
            NavigationButton n = GetButtonById(id);
            n.Visibility = visible ? Visibility.Visible : Visibility.Collapsed;
        }

        [RelayCommand]
        private void ShowNewEvent()
        {
            Navigate("NewEvent");
            ChangeVisibility("NewEvent", true);
            //var frame = Application.Current.MainWindow.FindName("MainFrame") as System.Windows.Controls.Frame;
            //if (frame != null)
            //{
            //    frame.Content = new NewEvent { DataContext = _vm };
            //    ChangeVisibility("NewEvent", true);
//}
        }

        [RelayCommand]
        public async Task NavigateToEventDetails(int eventId)
        {
            //await LoadEventDetails(eventId);
            //_navigateAction?.Invoke(this);
        }
    }
}