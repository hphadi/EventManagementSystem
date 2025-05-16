using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows;
using EventManagementSystem.Models; 
namespace EventManagementSystem.UI.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<Event> events;

        private readonly HttpClient _httpClient;

        public MainViewModel()
        {
            System.Diagnostics.Debug.WriteLine("MainViewModel initialized"); // for debugging
            _httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:5144/api/") };
            Events = new ObservableCollection<Event>();
            LoadEventsCommand.Execute(null);
        }

        [RelayCommand]
        private async Task LoadEvents()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("Loading events..."); // for debugging
                var events = await _httpClient.GetFromJsonAsync<List<Event>>("Event");
                if (events != null)
                {
                    Events.Clear();
                    foreach (var evt in events)
                    {
                        Events.Add(evt); // each event is added to the ObservableCollection
                    }
                    System.Diagnostics.Debug.WriteLine($"Loaded {events.Count} events."); // for debugging
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
                    frame.Navigate(new Uri("Views/DashboardView.xaml", UriKind.Relative));
                    break;
                case "Events":
                    frame.Navigate(new Uri("Views/EventManagementView.xaml", UriKind.Relative));
                    break;
                case "Groups":
                    frame.Navigate(new Uri("Views/GroupManagementView.xaml", UriKind.Relative));
                    break;
                case "Profile":
                    frame.Navigate(new Uri("Views/ProfileView.xaml", UriKind.Relative));
                    break;
            }
        }
    }
}