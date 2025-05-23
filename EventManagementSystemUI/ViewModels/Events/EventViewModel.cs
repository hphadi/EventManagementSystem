using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows;

namespace EventManagementSystemUI.ViewModels
{
    public partial class EventViewModel : ObservableObject
    {
        private readonly HttpClient _httpClient;
        private readonly MainViewModel _vm;

        public ObservableCollection<EventManagementSystem.Models.Event> Events { get; } = new();
        public ObservableCollection<EventManagementSystem.Models.Event> FutureEvents { get; } = new();

        public EventViewModel(HttpClient httpClient, MainViewModel vm)
        {
            _httpClient = httpClient;
            _vm = vm;
        }

        [RelayCommand]
        public async Task LoadEvents()
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
    }
}