using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EventManagementSystem.Models;
using EventManagementSystemUI.Views;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows;
using System.Windows.Controls;

namespace EventManagementSystemUI.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly HttpClient _httpClient;
        private readonly Events.EventDetailsViewModel _eventDetailsViewModel;

        [ObservableProperty]
        private ObservableCollection<EventManagementSystem.Models.Event> events;

        [ObservableProperty]
        private ObservableCollection<EventManagementSystem.Models.Event> futureEvents;

        [ObservableProperty]
        private ObservableCollection<EventManagementSystem.Models.Group> groups;

        public ObservableCollection<Group> SelectedGroups { get; set; } = new();

        [ObservableProperty]
        private Group selectedGroup;

        [ObservableProperty]
        private ObservableCollection<Event> groupEvents;

        [ObservableProperty]
        private Button dynamicGroupButton;

        [ObservableProperty]
        private Visibility newEventButtonVisibility = Visibility.Collapsed;

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

        public MainViewModel()
        {
            System.Diagnostics.Debug.WriteLine("MainViewModel initialized"); // for debugging
            _httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:5144/api/") };
            Events = new ObservableCollection<EventManagementSystem.Models.Event>();
            FutureEvents = new ObservableCollection<EventManagementSystem.Models.Event>();
            Groups = new ObservableCollection<EventManagementSystem.Models.Group>();
            GroupEvents = new ObservableCollection<EventManagementSystem.Models.Event>();
            _eventDetailsViewModel = new Events.EventDetailsViewModel(_httpClient, NavigateToView);
            LoadEventsCommand.Execute(null);
            LoadGroupsCommand.Execute(null);
        }

        public Events.EventDetailsViewModel EventDetailsViewModel => _eventDetailsViewModel;

        private void NavigateToView(object viewModel)
        {
            var frame = Application.Current.MainWindow.FindName("MainFrame") as System.Windows.Controls.Frame;
            if (frame == null) return;

            if (viewModel is Events.EventDetailsViewModel)
            {
                frame.Content = new EventDetailsView { DataContext = viewModel };
            }
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
        private async Task LoadGroupEvents(int groupId)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"Loading events for group {groupId}..."); // for debugging
                var events = await _httpClient.GetFromJsonAsync<List<EventManagementSystem.Models.Event>>($"Group/{groupId}/events");
                if (events != null)
                {
                    GroupEvents.Clear();
                    foreach (var evt in events)
                    {
                        GroupEvents.Add(evt); // each event is added to the ObservableCollection
                    }
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("No group events loaded."); // for debugging
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in LoadGroupEvents: {ex.Message}"); // for debugging
                MessageBox.Show($"Error loading group events: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        [RelayCommand]
        private void OnGroupSelected()
        {
            if (SelectedGroup != null)
            {
                NavigateCommand.Execute("GroupDetails");
            }
        }

        private void AddDynamicButton()
        {
            if (SelectedGroup != null && DynamicGroupButton == null)
            {
                DynamicGroupButton = new Button
                {
                    Content = SelectedGroup.Name,
                    Command = new RelayCommand(() => Navigate("GroupDetails")),
                    Background = System.Windows.Media.Brushes.Red, 
                    Foreground = System.Windows.Media.Brushes.White,
                    DataContext = this
                };
                var frame = Application.Current.MainWindow.FindName("MainFrame") as System.Windows.Controls.Frame;
                if (frame != null)
                {
                    var button = new Button
                    {
                        Content = "Group Details",
                        Margin = new Thickness(5),
                        Padding = new Thickness(10, 5, 10, 5),
                        Style = (Style)Application.Current.Resources["MaterialDesignRaisedButton"],
                        Background = System.Windows.Media.Brushes.Red,
                        Foreground = System.Windows.Media.Brushes.White,
                        Command = NavigateCommand,
                        CommandParameter = "GroupDetails",
                        DataContext = this
                    };
                    var stackPanel = Application.Current.MainWindow.FindName("MainButtonPanel") as StackPanel;
                    if (stackPanel != null)
                    {
                        stackPanel.Children.Add(DynamicGroupButton);
                    }
                }
            }
        }

        private void RemoveDynamicButton()
        {
            if (DynamicGroupButton != null)
            {
                var stackPanel = Application.Current.MainWindow.FindName("MainButtonPanel") as StackPanel;
                if (stackPanel != null)
                {
                    stackPanel.Children.Remove(DynamicGroupButton);
                    DynamicGroupButton = null;
                }
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
                    RemoveDynamicButton();
                    break;
                case "Events":
                    frame.Content = new EventManagementView { DataContext = this };
                    RemoveDynamicButton();
                    break;
                case "Groups":
                    frame.Content = new GroupManagementView { DataContext = this };
                    RemoveDynamicButton();
                    break;
                case "Profile":
                    frame.Content = new ProfileView { DataContext = this };
                    RemoveDynamicButton();
                    break;
                case "NewEvent":
                    frame.Content = new NewEvent { DataContext = this };
                    NewEventButtonVisibility = Visibility.Visible;
                    RemoveDynamicButton();
                    break;
                case "GroupDetails":
                    if (SelectedGroup != null)
                    {
                        frame.Content = new GroupDetailsView { DataContext = this };
                        AddDynamicButton();
                        LoadGroupEvents(SelectedGroup.Id).ConfigureAwait(false);
                    }
                    break;
            }
        }

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

            var newEvent = new EventManagementSystem.Models.EventDto
            {
                Title = EventTitle,
                Description = EventDescription,
                StartDate = startDateUtc,
                EndDate = endDateUtc,
                Location = EventLocation,
                CreatedAt = DateTime.Now.ToUniversalTime(),
                GroupIds = SelectedGroups.Select(g => g.Id).ToList()
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
            NewEventButtonVisibility = Visibility.Collapsed;
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

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

//using CommunityToolkit.Mvvm.ComponentModel;
//using CommunityToolkit.Mvvm.Input;
//using EventManagementSystemUI.ViewModels.Navigation;
//using System.Net.Http;
//using System.Windows;
//using System.Windows.Controls;

//namespace EventManagementSystemUI.ViewModels
//{
//    public partial class MainViewModel : ObservableObject
//    {
//        private readonly HttpClient _httpClient;
//        private readonly NavigationViewModel _navigationViewModel;
//        private readonly DynamicButtonManager _buttonManager;
//        private readonly Events.EventViewModel _eventViewModel;
//        private readonly Groups.GroupViewModel _groupViewModel;
//        private readonly Groups.GroupDetailsViewModel _groupDetailsViewModel;

//        public MainViewModel()
//        {
//            _httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:5144/api/") };
//            var mainFrame = Application.Current.MainWindow.FindName("MainFrame") as Frame;
//            var buttonPanel = Application.Current.MainWindow.FindName("MainButtonPanel") as StackPanel;

//            _buttonManager = new DynamicButtonManager(buttonPanel);
//            _navigationViewModel = new NavigationViewModel(mainFrame, _buttonManager);
//            _eventViewModel = new Events.EventViewModel(_httpClient);
//            _groupViewModel = new Groups.GroupViewModel(_httpClient);
//            _groupDetailsViewModel = new Groups.GroupDetailsViewModel(_httpClient, _navigationViewModel, _buttonManager);

//            NavigateToDashboard();
//        }

//        public Events.EventViewModel EventViewModel => _eventViewModel;
//        public Groups.GroupViewModel GroupViewModel => _groupViewModel;
//        public Groups.GroupDetailsViewModel GroupDetailsViewModel => _groupDetailsViewModel;

//        [RelayCommand]
//        private void NavigateToDashboard()
//        {
//            _navigationViewModel.Navigate("Dashboard", this);
//        }

//        [RelayCommand]
//        private void NavigateToEvents()
//        {
//            _navigationViewModel.Navigate("Events", _eventViewModel);
//        }

//        [RelayCommand]
//        private void NavigateToGroups()
//        {
//            _navigationViewModel.Navigate("Groups", _groupViewModel);
//        }

//        [RelayCommand]
//        private void NavigateToProfile()
//        {
//            _navigationViewModel.Navigate("Profile", this);
//        }

//        [RelayCommand]
//        private void ShowNewEvent()
//        {
//            var newEventViewModel = new Events.NewEventViewModel(_httpClient, _navigationViewModel, _groupViewModel.Groups);
//            _navigationViewModel.Navigate("NewEvent", newEventViewModel);
//        }
//    }
//}