using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EventManagementSystem.Models;
using EventManagementSystemUI.Models;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows;

namespace EventManagementSystemUI.ViewModels
{
    public partial class NewGroupViewModel : ObservableObject
    {
        private readonly HttpClient _httpClient;
        private readonly MainViewModel _vm;

        public ObservableCollection<GroupBase> Groups { get; } = new();

        public NewGroupViewModel(HttpClient httpClient, MainViewModel vm)
        {
            _httpClient = httpClient;
            _vm = vm;
        }

        [ObservableProperty]
        private DraftGroupDto newGroupDraft = new();

        //        [ObservableProperty]
        //        private DateTime? eventEndDateTime;

        //[ObservableProperty]
        //private ObservableCollection<EventManagementSystem.Models.EventBase>? selectedEvents;

        [RelayCommand]
        private async Task SubmitNewGroup()
        {

            if (!NewGroupDraft.IsValid())
            {
                MessageBox.Show("Please fill in all fields.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var newGroup = new NewGroupDto
            {
                Name = NewGroupDraft.Name,
                Description = NewGroupDraft.Description,
                City = NewGroupDraft.City,
                CreatedAt = DateTime.Now.ToUniversalTime(),
                //EventsIds = SelectedEvents.IsNullOrEmpty() ? null : SelectedEvents.Select(g => g.Id).ToList()
            };

            //            var newEvent = new EventManagementSystem.Models.EventDto
            //            {
            //                Title = EventTitle,
            //                Description = EventDescription,
            //                StartDate = startDateUtc,
            //                EndDate = endDateUtc,
            //                Location = EventLocation,
            //                CreatedAt = DateTime.Now.ToUniversalTime(),
            //                GroupIds = SelectedGroups.Select(g => g.Id).ToList()
            //            };

            try
            {
                var response = await _httpClient.PostAsJsonAsync("Group", newGroup);
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Group created successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    await _vm.GroupVM.LoadGroups();
                    CancelNewGroupCommand.Execute(null);
                }
                else
                {
                    MessageBox.Show($"Failed to create group: {response.StatusCode}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error submitting group: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        [RelayCommand]
        private void CancelNewGroup()
        {
            _vm.NavVM.ChangeVisibility("NewGroup", false);
            NewGroupDraft.Clear();
            _vm.NavVM.Navigate("Dashboard");
        }
    }
}
