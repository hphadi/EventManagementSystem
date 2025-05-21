using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EventManagementSystemUI.Views;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json; 
using System.Windows; 


namespace EventManagementSystemUI.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<EventManagementSystem.Models.Event> events;

        [ObservableProperty]
        private ObservableCollection<EventManagementSystem.Models.Event> futureEvents;

        [ObservableProperty]
        private Visibility newEventButtonVisibility = Visibility.Hidden;

        private readonly HttpClient _httpClient;

        public MainViewModel()
        {
            System.Diagnostics.Debug.WriteLine("MainViewModel initialized"); // for debugging
            _httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:5144/api/") };
            Events = new ObservableCollection<EventManagementSystem.Models.Event>();
            FutureEvents = new ObservableCollection<EventManagementSystem.Models.Event>();
            LoadEventsCommand.Execute(null);
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
                    break;
            }
        }

        [RelayCommand]
        private void NewEvent()
        {
                NewEventButtonVisibility = Visibility.Visible;
                var frame = Application.Current.MainWindow.FindName("MainFrame") as System.Windows.Controls.Frame;
                frame.Content = new NewEvent { DataContext = this };
        }
        [RelayCommand]
        private void HideNewEvent()
        {
            NewEventButtonVisibility = Visibility.Collapsed;
            var frame = Application.Current.MainWindow.FindName("MainFrame") as System.Windows.Controls.Frame;
            frame.Content = new EventManagementView { DataContext = this };
        }
    }
}