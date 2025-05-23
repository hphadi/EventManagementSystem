using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EventManagementSystem.Models;
using EventManagementSystemUI.Models;
using EventManagementSystemUI.Views;
using Microsoft.IdentityModel.Tokens;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows;

namespace EventManagementSystemUI.ViewModels
{
    public partial class NewEventViewModel : ObservableObject
    {
        private readonly HttpClient _httpClient;
        private readonly MainViewModel _vm;

        public ObservableCollection<Event> Events { get; } = new();
        public ObservableCollection<Event> FutureEvents { get; } = new();

        public NewEventViewModel(HttpClient httpClient, MainViewModel vm)
        {
            _httpClient = httpClient;
            _vm = vm;
        }

        [ObservableProperty]
        private NewEventDto newEventDraft = new();


        [ObservableProperty]
        private ObservableCollection<EventManagementSystem.Models.Group>? selectedGroups;

        [RelayCommand]
        private async Task SubmitNewEvent()
        {

            if (newEventDraft.IsValid())
            {
                MessageBox.Show("Please fill in all fields.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var startDateUtc = NewEventDraft.StartDateTime.Value.ToUniversalTime();
            var endDateUtc = newEventDraft.EndDateTime.Value.ToUniversalTime();

            var newEvent = new EventDto
            {
                Title = newEventDraft.Title,
                Description = newEventDraft.Description,
                StartDate = startDateUtc,
                EndDate = endDateUtc,
                Location = newEventDraft.Location,
                CreatedAt = DateTime.Now.ToUniversalTime(),
                GroupIds = SelectedGroups.IsNullOrEmpty()? null : SelectedGroups.Select(g => g.Id).ToList()
            };


            try
            {
                var response = await _httpClient.PostAsJsonAsync("Event", newEvent);
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Event created successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    await _vm.EventVM.LoadEvents();
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
            _vm.NavVM.ChangeVisibility("NewEvent", false);
            newEventDraft.Clear();
            _vm.NavVM.Navigate("Dashboard");
        }
    }
}