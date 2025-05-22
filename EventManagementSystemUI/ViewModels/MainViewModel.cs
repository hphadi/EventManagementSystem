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

        [ObservableProperty]
        private ObservableCollection<EventManagementSystem.Models.Group> groups;

        [ObservableProperty]
        private string eventTitle = "";

        [ObservableProperty]
        private string eventDescription = "";

        [ObservableProperty]
        private DateTime? eventStartDateTime;

        [ObservableProperty]
        private DateTime? eventEndDateTime;

        [ObservableProperty]
        private string eventLocation = "";

        private readonly HttpClient _httpClient;

        public MainViewModel()
        {
            System.Diagnostics.Debug.WriteLine("MainViewModel initialized"); // for debugging
            _httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:5144/api/") };
            Events = new ObservableCollection<EventManagementSystem.Models.Event>();
            FutureEvents = new ObservableCollection<EventManagementSystem.Models.Event>();
            Groups = new ObservableCollection<EventManagementSystem.Models.Group>();
            LoadGroupsCommand.Execute(null);
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
        private async Task LoadGroups()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("Loading groups..."); // for debugging
                var groups = await _httpClient.GetFromJsonAsync<List<EventManagementSystem.Models.Group>>("Group");
                if (groups != null)
                {
                    Groups.Clear();
                    foreach (var group in groups)
                    {
                        Groups.Add(group); // each group is added to the ObservableCollection

                    }
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("No groups loaded."); // for debugging
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in LoadGroups: {ex.Message}"); // for debugging
                MessageBox.Show($"Error loading groups: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
                    NewEventButtonVisibility = Visibility.Visible;
                    break;
            }
        }

        //[RelayCommand]
        //private void NewEvent()
        //{
        //    var frame = Application.Current.MainWindow.FindName("MainFrame") as System.Windows.Controls.Frame;
        //    if (frame != null && frame.Content is NewEvent)
        //    {
        //        // 
        //    }
        //}

        [RelayCommand]
        private void ShowNewEvent()
        {
            var frame = Application.Current.MainWindow.FindName("MainFrame") as System.Windows.Controls.Frame;
            if (frame != null)
            {
                frame.Content = new NewEvent { DataContext = this };
                NewEventButtonVisibility = Visibility.Visible;
            }
        }

        //[RelayCommand]
        //private void HideNewEvent()
        //{
        //    NewEventButtonVisibility = Visibility.Collapsed;
        //    var frame = Application.Current.MainWindow.FindName("MainFrame") as System.Windows.Controls.Frame;
        //    frame.Content = new EventManagementView { DataContext = this };
        //}

        [RelayCommand]
        private async Task SubmitNewEvent()
        {

            if (string.IsNullOrWhiteSpace(EventTitle) || EventTitle == "Enter Title" ||
                string.IsNullOrWhiteSpace(EventDescription) || EventDescription == "Enter Description" ||
                EventEndDateTime == null || EventStartDateTime == null ||
                string.IsNullOrWhiteSpace(EventLocation) || EventLocation == "Enter Location")
            {
                MessageBox.Show("Please fill in all fields.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var startDateUtc = EventStartDateTime.Value.ToUniversalTime();
            var endDateUtc = EventEndDateTime.Value.ToUniversalTime();

            var newEvent = new EventManagementSystem.Models.Event
            {
                Title = EventTitle,
                Description = EventDescription,
                StartDate = startDateUtc,
                EndDate = endDateUtc,
                Location = EventLocation,
                CreatedAt = DateTime.Now.ToUniversalTime()
            };


            try
            {
                var response = await _httpClient.PostAsJsonAsync("Event", newEvent);
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Event created successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    await LoadEvents();
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
            NewEventButtonVisibility = Visibility.Hidden; 
            var frame = Application.Current.MainWindow.FindName("MainFrame") as System.Windows.Controls.Frame;
            if (frame != null)
            {
                frame.Content = new EventManagementView { DataContext = this };
                EventTitle = "";
                EventDescription = "";
                EventStartDateTime = null;
                EventEndDateTime = null;
                EventLocation = "";
            }
        }

    }
}