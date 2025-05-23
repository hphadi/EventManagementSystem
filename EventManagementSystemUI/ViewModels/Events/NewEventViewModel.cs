//using CommunityToolkit.Mvvm.ComponentModel;
//using CommunityToolkit.Mvvm.Input;
//using EventManagementSystemUI.ViewModels.Navigation;
//using System.Collections.ObjectModel;
//using System.ComponentModel;
//using System.Net.Http;
//using System.Net.Http.Json;
//using System.Windows;

//namespace EventManagementSystemUI.ViewModels.Events
//{
//    public partial class NewEventViewModel : ObservableObject
//    {
//        private readonly HttpClient _httpClient;
//        private readonly NavigationViewModel _navigationViewModel;

//        [ObservableProperty]
//        private string eventTitle = "";

//        [ObservableProperty]
//        private string eventDescription = "";

//        [ObservableProperty]
//        private DateTime? eventStartDateTime;

//        [ObservableProperty]
//        private DateTime? eventEndDateTime;

//        [ObservableProperty]
//        private string eventLocation = "";

//        [ObservableProperty]
//        private ObservableCollection<EventManagementSystem.Models.Group> selectedGroups;

//        [ObservableProperty]
//        private Visibility newEventButtonVisibility = Visibility.Hidden;

//        public NewEventViewModel(HttpClient httpClient, NavigationViewModel navigationViewModel, ObservableCollection<EventManagementSystem.Models.Group> groups)
//        {
//            _httpClient = httpClient;
//            _navigationViewModel = navigationViewModel;
//            SelectedGroups = new ObservableCollection<EventManagementSystem.Models.Group>(groups);
//            NewEventButtonVisibility = Visibility.Visible;
//        }

//        [RelayCommand]
//        private async Task SubmitNewEvent()
//        {
//            if (string.IsNullOrWhiteSpace(EventTitle) || EventTitle == "Enter Title" ||
//            string.IsNullOrWhiteSpace(EventDescription) || EventDescription == "Enter Description" ||
//                EventEndDateTime == null || EventStartDateTime == null ||
//                string.IsNullOrWhiteSpace(EventLocation) || EventLocation == "Enter Location")
//            {
//                MessageBox.Show("Please fill in all fields.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
//                return;
//            }

//            var startDateUtc = EventStartDateTime.Value.ToUniversalTime();
//            var endDateUtc = EventEndDateTime.Value.ToUniversalTime();

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

//            try
//            {
//                var response = await _httpClient.PostAsJsonAsync("Event", newEvent);
//                if (response.IsSuccessStatusCode)
//                {
//                    MessageBox.Show("Event created successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
//                    CancelNewEventCommand.Execute(null);
//                }
//                else
//                {
//                    MessageBox.Show($"Failed to create event: {response.StatusCode}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show($"Error submitting event: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
//            }
//        }

//        [RelayCommand]
//        private void CancelNewEvent()
//        {
//            NewEventButtonVisibility = Visibility.Hidden;
//            _navigationViewModel.Navigate("Events", null);
//            EventTitle = "";
//            EventDescription = "";
//            EventStartDateTime = null;
//            EventEndDateTime = null;
//            EventLocation = "";
//        }
//    }
//}