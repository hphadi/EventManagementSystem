using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EventManagementSystem.Models;
using EventManagementSystemUI.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows;

namespace EventManagementSystemUI.ViewModels
{
    public partial class EventViewModel : ObservableObject
    {
        private readonly HttpClient _httpClient;
        private readonly MainViewModel _vm;

        [ObservableProperty]
        public ObservableCollection<EventManagementSystem.Models.Event> events = [];

        [ObservableProperty]
        public ObservableCollection<EventManagementSystem.Models.Event> futureEvents = [];

        public EventViewModel(HttpClient httpClient, MainViewModel vm)
        {
            _httpClient = httpClient;
            _vm = vm;
            if (!DesignerProperties.GetIsInDesignMode(new DependencyObject()))
                LoadEventsCommand.Execute(null);
        }

        [ObservableProperty]
        private EventManagementSystem.Models.Event selectedEvent;

        [RelayCommand]
        public async Task LoadEvents()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("Loading events..."); // for debugging
                var events_loaded = await _httpClient.GetFromJsonAsync<List<EventManagementSystem.Models.Event>>("Event");
                if (events_loaded != null)
                {
                    Events.Clear();
                    FutureEvents.Clear();
                    foreach (var evt in events_loaded)
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
        private async Task EventSelected()
        {
            var selected = _vm.EventVM.SelectedEvent;

            if (selected is not null)
            {
                var data = new NavData(selected.Id, selected.Title);
                _vm.NavVM.AddEventToMenu(data);
            }
        }
    }
}