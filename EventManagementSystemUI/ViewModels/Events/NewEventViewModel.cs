﻿using CommunityToolkit.Mvvm.ComponentModel;
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
    public partial class NewEventViewModel : ObservableObject
    {
        private readonly HttpClient _httpClient;
        private readonly MainViewModel _vm;

        public ObservableCollection<EventBase> Events { get; } = new();
        public ObservableCollection<EventBase> FutureEvents { get; } = new();

        public NewEventViewModel(HttpClient httpClient, MainViewModel vm)
        {
            _httpClient = httpClient;
            _vm = vm;
        }

        [ObservableProperty]
        private DraftEventDto newEventDraft = new();


        [ObservableProperty]
        private ObservableCollection<EventManagementSystem.Models.GroupBase>? selectedGroups;

        [RelayCommand]
        private async Task SubmitNewEvent()
        {

            if (!NewEventDraft.ShowValidationErrorsIfInvalid())
                return;

            if (NewEventDraft.StartDateTime == null || NewEventDraft.EndDateTime == null)
            {
                MessageBox.Show("Please select both start and end date/time.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var startDateUtc = NewEventDraft.StartDateTime.Value.ToUniversalTime();
            var endDateUtc = NewEventDraft.EndDateTime.Value.ToUniversalTime();

            var newEvent = new NewEventDto
            {
                Title = NewEventDraft.Title,
                Description = NewEventDraft.Description,
                StartDate = startDateUtc,
                EndDate = endDateUtc,
                Location = NewEventDraft.Location,
                CreatedAt = DateTime.UtcNow,
                GroupIds = SelectedGroups?.Any() == true
                    ? SelectedGroups.Select(g => g.Id).ToList()
                    : null,
                PersonIds = new List<int> { _vm.UserVM.CurrentUser.Id }
            };


            try
            {
                var response = await _httpClient.PostAsJsonAsync("Event", newEvent);
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Event created successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    await _vm.EventVM.LoadEvents();
                    if (!DesignerProperties.GetIsInDesignMode(new DependencyObject()))
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
            NewEventDraft.Clear();
            _vm.NavVM.Navigate("Dashboard");
        }
    }
}