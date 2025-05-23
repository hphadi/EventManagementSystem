using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows;
using EventManagementSystem.Models;

namespace EventManagementSystemUI.ViewModels
{
    public partial class EventDetailsViewModel : ObservableObject
    {
        private readonly HttpClient _httpClient;
        MainViewModel _vm;
        //private readonly Action<object> _navigateAction; 

        [ObservableProperty]
        private Event selectedEvent;

        [ObservableProperty]
        private ObservableCollection<Group> eventGroups;

        public EventDetailsViewModel(HttpClient httpClient, MainViewModel vm)
        {
            _httpClient = httpClient;
            _vm = vm;
            
            EventGroups = new ObservableCollection<Group>();
        }

        [RelayCommand]
        public async Task LoadEventDetails(int eventId)
        {
            try
            {
                var evt = await _httpClient.GetFromJsonAsync<Event>($"Event/{eventId}");
                if (evt != null)
                {
                    SelectedEvent = evt;
                    EventGroups.Clear();
                    foreach (var eventGroup in evt.EventGroups)
                    {
                        EventGroups.Add(eventGroup.Group);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in LoadEventDetails: {ex.Message}");
                MessageBox.Show($"Error loading event details: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //[RelayCommand]
        //public async Task NavigateToEventDetails(int eventId)
        //{
        //    await LoadEventDetails(eventId);
        //    _navigateAction?.Invoke(this); 
        //}
    }
}