using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EventManagementSystem.Models;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows;

namespace EventManagementSystemUI.ViewModels
{
    public partial class GroupDetailsViewModel : ObservableObject
    {
        private readonly HttpClient _httpClient;
        private readonly MainViewModel _vm;

        public GroupDetailsViewModel(HttpClient httpClient, MainViewModel vm)
        {
            _httpClient = httpClient;
            _vm = vm;
        }

        [ObservableProperty]
        private Group selectedGroup;

        [ObservableProperty]
        private ObservableCollection<Event> groupEvents = new ObservableCollection<EventManagementSystem.Models.Event>();

        [RelayCommand]
        private async Task LoadGroupEvents(int groupId)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"Loading events for group {groupId}...");
                var url = $"Group/{groupId}/events";
                System.Diagnostics.Debug.WriteLine($"Requesting URL: {_httpClient.BaseAddress}{url}");
                var response = await _httpClient.GetAsync(url);
                System.Diagnostics.Debug.WriteLine($"Response Status: {response.StatusCode}");
                var events = await _httpClient.GetFromJsonAsync<List<EventManagementSystem.Models.Event>>(url);
                if (events != null)
                {
                    System.Diagnostics.Debug.WriteLine($"Loaded {events.Count} events.");
                    GroupEvents.Clear();
                    foreach (var evt in events)
                    {
                        GroupEvents.Add(evt);
                    }
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("No group events loaded.");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in LoadGroupEvents: {ex.Message}");
                MessageBox.Show($"Error loading group events: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


    }
}