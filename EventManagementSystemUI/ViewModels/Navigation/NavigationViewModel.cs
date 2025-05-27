using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EventManagementSystem.Models;
using EventManagementSystemUI.Models;
using EventManagementSystemUI.Views;
using Microsoft.Extensions.Logging;

namespace EventManagementSystemUI.ViewModels
{
    public record NavData(int Id, string Title);
    public record NavDataPrefix(String prefix, int Id, string Title);
    public partial class NavigationViewModel : ObservableObject
    {
        private readonly HttpClient _httpClient;
        private readonly MainViewModel _vm;
        private readonly Brush redBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF32B21"));

        public NavigationViewModel(HttpClient httpClient, MainViewModel vm)
        {
            _httpClient = httpClient;
            _vm = vm;
        }
        [ObservableProperty]
        private FontWeight fontWeight = FontWeights.Normal;

        [ObservableProperty]
        private double fontSize = 14; // default

        [ObservableProperty]
        private Thickness margin = new(5); // default

        public ObservableCollection<NavigationButton> DynamicButtons { get; } = new();

        private void NavigateToView(UserControl view) =>
            (Application.Current.MainWindow.FindName("MainFrame") as Frame)!.Content = view;

        public void LoadButtons()
        {
            AddNavButton("Dashboard", "Dashboard");
            AddNavButton("Events", "Events");
            AddNavButton("NewEvent", "New Event", redBrush, Visibility.Collapsed, 12, new Thickness(20, 2, 0, 2));
            AddNavButton("Groups", "Groups");
            AddNavButton("SignOut", "Sign Out", null, Visibility.Collapsed, 14, null, _vm.UserVM.SignOutCommand);
            AddNavButton("Register", "Register");
            AddNavButton("SignIn", "Sign In");
        }


        private void AddNavButton(
            string id,
            string title,
            Brush? background = null,
            Visibility visibility = Visibility.Visible,
            double fontSize = 14,
            Thickness? margin = null,
            IRelayCommand? command = null)
        {
                    DynamicButtons.Add(new NavigationButton
                    {
                        Id = id,
                        Title = title,
                        Command = command ?? NavigateCommand,
                        CommandParameter = id,
                        Background = background,
                        Visibility = visibility,
                        FontWeight = FontWeights.Normal,
                        FontSize = fontSize,
                        Margin = margin ?? new Thickness(5)
                    });
                }


        [RelayCommand]
        public void Navigate(string page)
        {
            HighlightButton(page);

            NavigateToView(page switch
            {
                "Dashboard" => new DashboardView { DataContext = _vm },
                "Events" => new EventManagementView { DataContext = _vm },
                "Groups" => new GroupManagementView { DataContext = _vm },
                "Profile" => new ProfileView { DataContext = _vm },
                "NewEvent" => new NewEvent { DataContext = _vm },
                "Register" => new SignUpView { DataContext = _vm },
                "SignIn" => new SignIn { DataContext = _vm },
                _ => new DashboardView { DataContext = _vm }
            });
        }

        [RelayCommand]
        public void GroupDetails(string groupId)
        {
            HighlightButton("g" + groupId.ToString());
            var vm = new GroupDetailsViewModel(_httpClient, _vm, groupId);
            NavigateToView(new GroupDetailsView { DataContext = vm });
        }

        [RelayCommand]
        public void EventDetails(string eventId)
        {
            HighlightButton("e" + eventId);
            var vm = new EventDetailsViewModel(_httpClient, _vm, eventId);
            NavigateToView(new EventDetailsView { DataContext = vm });
        }

        [RelayCommand]
        public void AddEventToMenu(NavData data)
        {
            var prefixed = new NavDataPrefix("e", data.Id, data.Title);
            AddToMenuCommand.Execute(prefixed);
            EventDetailsCommand.Execute(data.Id.ToString());
        }

        [RelayCommand]
        public void AddGroupToMenu(NavData data)
        {
            var prefixed = new NavDataPrefix("g", data.Id, data.Title);
            AddToMenuCommand.Execute(prefixed);
            GroupDetailsCommand.Execute(data.Id.ToString());
        }



        [RelayCommand]
        public void AddToMenu(NavDataPrefix data)
        {
            string id_ = data.prefix + data.Id.ToString();
            if (DynamicButtons.Any(b => b.Id == id_))
                return;

            var newButton = new NavigationButton
            {
                Id = id_,
                Title = "  " + data.Title, // Indent for visual grouping
                Command = data.prefix == "g" ? GroupDetailsCommand : EventDetailsCommand,
                CommandParameter = id_,
                FontWeight = FontWeights.Normal,
                FontSize = 12, // Smaller
                Margin = new Thickness(20, 2, 0, 2) // Indented
            };

            string anchorId = data.prefix == "e" ? "Events" : "Groups";
            int insertIndex = DynamicButtons.ToList().FindIndex(b => b.Id == anchorId);

            if (insertIndex != -1)
                DynamicButtons.Insert(insertIndex + 1, newButton);
            else
                DynamicButtons.Add(newButton); // fallback
        }


        public NavigationButton? GetButtonById(string id) =>
            DynamicButtons.FirstOrDefault(b => b.Id == id);

        public void ChangeVisibility(string id, bool visible)
        {
            var button = GetButtonById(id);
            if (button != null)
                button.Visibility = visible ? Visibility.Visible : Visibility.Collapsed;
        }

        [RelayCommand]
        private void ShowNewEvent()
        {
            Navigate("NewEvent");
            ChangeVisibility("NewEvent", true);
        }

        private void HighlightButton(string id)
        {
            foreach (var button in DynamicButtons)
                button.FontWeight = button.Id == id ? FontWeights.Bold : FontWeights.Normal;
        }
    }
}
