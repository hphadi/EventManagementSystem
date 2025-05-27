using CommunityToolkit.Mvvm.ComponentModel;
using EventManagementSystemUI.Models;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Windows.Media;
using System.Windows;
using CommunityToolkit.Mvvm.Input;
using EventManagementSystemUI.Views;
using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using EventManagementSystem.Models;

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
            DynamicButtons.Add(new NavigationButton { Id = "NewGroup", Title = "New Group", CommandParameter = "NewGroup", Command = NavigateCommand, Background = redBrush, Visibility = Visibility.Collapsed });
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
                //case "GroupDetails":
                //    frame.Content = new GroupDetailsView { DataContext = _vm };
                //    break;
                case "NewGroup":
                    frame.Content = new NewGroup { DataContext = _vm };
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

        [RelayCommand]
        public void GroupDetails(string groupId)
        {
            var frame = Application.Current.MainWindow.FindName("MainFrame") as System.Windows.Controls.Frame;
            var Vm = new EventManagementSystemUI.ViewModels.GroupDetailsViewModel(_httpClient, _vm, groupId);
            frame.Content = new GroupDetailsView { DataContext = Vm};
        }

        [RelayCommand]
        public void EventDetails(string eventId)
        {
            var frame = Application.Current.MainWindow.FindName("MainFrame") as System.Windows.Controls.Frame;
            var Vm = new EventManagementSystemUI.ViewModels.EventDetailsViewModel(_httpClient, _vm, eventId);
            frame.Content = new EventDetailsView { DataContext = Vm };
        }

        [RelayCommand]
        public void AddEventToMenu(EventNavData data)
        {
            _vm.NavVM.DynamicButtons.Add(new NavigationButton
            {
                Id = "e" + data.Id,
                Title = data.Title,
                CommandParameter = data.Id,
                Command = _vm.NavVM.EventDetailsCommand
            });

            _vm.NavVM.EventDetails(data.Id);
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
        [RelayCommand]
        private void ShowNewGroup()
        {
            Navigate("NewGroup");
            ChangeVisibility("NewGroup", true);
        }
    }
}