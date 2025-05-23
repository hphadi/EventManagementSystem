using CommunityToolkit.Mvvm.ComponentModel;
using System.Net.Http;

namespace EventManagementSystemUI.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly HttpClient _httpClient;

        public EventViewModel EventVM { get; }
        public NewEventViewModel NewEventVM { get; }
        public EventDetailsViewModel EventDetailsVM { get; }
        public GroupViewModel GroupVM { get; }
        public NavigationViewModel NavVM { get; }
        public UserViewModel UserVM { get; }

        //public ObservableCollection<NavigationButton> DynamicButtons { get; } = new();

        //[ObservableProperty]
        //private ObservableCollection<EventManagementSystem.Models.Event> events;

        //[ObservableProperty]
        //private ObservableCollection<EventManagementSystem.Models.Event> futureEvents;

        //[ObservableProperty]
        //private ObservableCollection<EventManagementSystem.Models.Group> groups;

        //public ObservableCollection<Group> SelectedGroups { get; set; } = new();

        //[ObservableProperty]
        //private Group selectedGroup;

        //[ObservableProperty]
        //private ObservableCollection<Event> groupEvents;

        //[ObservableProperty]
        //private LoginUser loginUser = new();

        //[ObservableProperty]
        //private NewUser newUserDraft = new();

        //[ObservableProperty]
        //private EventManagementSystem.Models.Person currentUser = new();

        //[ObservableProperty]
        //private Button dynamicGroupButton;

        //[ObservableProperty]
        //private Button dynamicButton;



        public MainViewModel()
        {
            _httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:5144/api/") };
            
            EventVM = new EventManagementSystemUI.ViewModels.EventViewModel(_httpClient, this);
            NewEventVM = new EventManagementSystemUI.ViewModels.NewEventViewModel(_httpClient, this);
            //EventDetailsVM = new EventManagementSystemUI.ViewModels.EventDetailsViewModel(_httpClient, this);
            GroupVM = new EventManagementSystemUI.ViewModels.GroupViewModel(_httpClient, this);
            //GroupDetailsVM = new EventManagementSystemUI.ViewModels.GroupDetailsViewModel(_httpClient, this);
            NavVM = new EventManagementSystemUI.ViewModels.NavigationViewModel(_httpClient, this);
            UserVM = new EventManagementSystemUI.ViewModels.UserViewModel(_httpClient, this);
            
            NavVM.LoadButtons();
            //Events = new ObservableCollection<EventManagementSystem.Models.Event>();
            //FutureEvents = new ObservableCollection<EventManagementSystem.Models.Event>();
            //Groups = new ObservableCollection<EventManagementSystem.Models.Group>();
            //GroupEvents = new ObservableCollection<EventManagementSystem.Models.Event>();
            //_eventDetailsViewModel = new Events.EventDetailsViewModel(_httpClient, NavigateToView);
            //EventVM.LoadEventsCommand.Execute(null);
            //LoadGroupsCommand.Execute(null);

        }

        //private void LoadButtons()
        //{
        //    var redBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF32B21"));
        //    DynamicButtons.Add(new NavigationButton { Id = "Dashboard", Title = "Dashboard", CommandParameter = "Dashboard", Command = NavigateCommand });
        //    DynamicButtons.Add(new NavigationButton { Id = "Events", Title = "Events", CommandParameter = "Events", Command = NavigateCommand });
        //    DynamicButtons.Add(new NavigationButton { Id = "Groups", Title = "Groups", CommandParameter = "Groups", Command = NavigateCommand });
        //    DynamicButtons.Add(new NavigationButton { Id = "NewEvent", Title = "New Event", CommandParameter = "NewEvent", Command = NavigateCommand, Background = redBrush, Visibility = Visibility.Collapsed });
        //    DynamicButtons.Add(new NavigationButton { Id = "SignOut", Title = "Sign Out", Command = SignOutCommand, Visibility = Visibility.Collapsed});
        //    DynamicButtons.Add(new NavigationButton { Id = "Register", Title = "Register", CommandParameter = "Register", Command = NavigateCommand });
        //    DynamicButtons.Add(new NavigationButton { Id = "SignIn", Title = "Sign In", CommandParameter = "SignIn", Command = NavigateCommand });
        //}


        //public Events.EventDetailsViewModel EventDetailsViewModel => _eventDetailsViewModel;

        //////private void NavigateToView(object viewModel)
        //////{
        //////    var frame = Application.Current.MainWindow.FindName("MainFrame") as System.Windows.Controls.Frame;
        //////    if (frame == null) return;

        //////    if (viewModel is Events.EventDetailsViewModel)
        //////    {
        //////       // frame.Content = new EventDetailsView { DataContext = viewModel };
        //////    }
        //////}

        //[RelayCommand]
        //private async Task LoadEvents()
        //{
        //    try
        //    {
        //        System.Diagnostics.Debug.WriteLine("Loading events..."); // for debugging
        //        var events = await _httpClient.GetFromJsonAsync<List<EventManagementSystem.Models.Event>>("Event");
        //        if (events != null)
        //        {
        //            Events.Clear();
        //            FutureEvents.Clear();
        //            foreach (var evt in events)
        //            {
        //                Events.Add(evt); // each event is added to the ObservableCollection
        //                if (evt.StartDate > DateTime.Now)
        //                {
        //                    FutureEvents.Add(evt); // each future event is added to the FutureEvents ObservableCollection
        //                }
        //            }
        //        }
        //        else
        //        {
        //            System.Diagnostics.Debug.WriteLine("No events loaded."); // for debugging
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        System.Diagnostics.Debug.WriteLine($"Error in LoadEvents: {ex.Message}"); // for debugging
        //        MessageBox.Show($"Error loading events: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        //    }
        //}

        //[RelayCommand]
        //private async Task LoadGroups()
        //{
        //    try
        //    {
        //        System.Diagnostics.Debug.WriteLine("Loading groups..."); // for debugging
        //        var groups = await _httpClient.GetFromJsonAsync<List<EventManagementSystem.Models.Group>>("Group");
        //        if (groups != null)
        //        {
        //            Groups.Clear();
        //            foreach (var group in groups)
        //            {
        //                Groups.Add(group); // each group is added to the ObservableCollection

        //            }
        //        }
        //        else
        //        {
        //            System.Diagnostics.Debug.WriteLine("No groups loaded."); // for debugging
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        System.Diagnostics.Debug.WriteLine($"Error in LoadGroups: {ex.Message}"); // for debugging
        //        MessageBox.Show($"Error loading groups: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        //    }
        //}

        //[RelayCommand]
        //private async Task LoadGroupEvents(int groupId)
        //{
        //    try
        //    {
        //        System.Diagnostics.Debug.WriteLine($"Loading events for group {groupId}...");
        //        var url = $"Group/{groupId}/events";
        //        System.Diagnostics.Debug.WriteLine($"Requesting URL: {_httpClient.BaseAddress}{url}");
        //        var response = await _httpClient.GetAsync(url); 
        //        System.Diagnostics.Debug.WriteLine($"Response Status: {response.StatusCode}");
        //        var events = await _httpClient.GetFromJsonAsync<List<EventManagementSystem.Models.Event>>(url);
        //        if (events != null)
        //        {
        //            System.Diagnostics.Debug.WriteLine($"Loaded {events.Count} events.");
        //            GroupEvents.Clear();
        //            foreach (var evt in events)
        //            {
        //                GroupEvents.Add(evt);
        //            }
        //        }
        //        else
        //        {
        //            System.Diagnostics.Debug.WriteLine("No group events loaded.");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        System.Diagnostics.Debug.WriteLine($"Error in LoadGroupEvents: {ex.Message}");
        //        MessageBox.Show($"Error loading group events: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        //    }
        //}

        //////[RelayCommand]
        //////private void OnGroupSelected()
        //////{
        //////    if (SelectedGroup != null)
        //////    {
        //////        AddDynamicButton();
        //////        NavigateCommand.Execute("GroupDetails");
        //////    }
        //////}

        //private void AddDynamicButton()
        //{
        //    if (SelectedGroup != null && DynamicButton == null)
        //    {
        //        DynamicButton = new Button
        //        {
        //            Content = SelectedGroup.Name,
        //            Margin = new Thickness(5),
        //            Padding = new Thickness(10, 5, 10, 5),
        //            Style = (Style)Application.Current.Resources["MaterialDesignRaisedButton"],
        //            Background = System.Windows.Media.Brushes.Red,
        //            Foreground = System.Windows.Media.Brushes.White
        //        };
        //        var stackPanel = Application.Current.MainWindow.FindName("MainButtonPanel") as StackPanel;
        //        if (stackPanel != null)
        //        {
        //            stackPanel.Children.Add(DynamicButton);
        //        }
        //    }
        //}

        //private void RemoveDynamicButton()
        //{
        //    if (DynamicGroupButton != null)
        //    {
        //        var stackPanel = Application.Current.MainWindow.FindName("MainButtonPanel") as StackPanel;
        //        if (stackPanel != null)
        //        {
        //            stackPanel.Children.Remove(DynamicGroupButton);
        //            DynamicGroupButton = null;
        //        }
        //    }
        //}

        //[RelayCommand]
        //private void Navigate(string page)
        //{
        //    var frame = Application.Current.MainWindow.FindName("MainFrame") as System.Windows.Controls.Frame;
        //    if (frame == null) return;

        //    switch (page)
        //    {
        //        case "Dashboard":
        //            frame.Content = new DashboardView { DataContext = this };
        //            break;
        //        case "Events":
        //            frame.Content = new EventManagementView { DataContext = this };
        //            break;
        //        case "Groups":
        //            frame.Content = new GroupManagementView { DataContext = this };
        //            break;
        //        case "GroupDetails":
        //            frame.Content = new GroupDetailsView { DataContext = this };
        //            break;
        //        case "Profile":
        //            frame.Content = new ProfileView { DataContext = this };
        //            break;
        //        case "NewEvent":
        //            frame.Content = new NewEvent { DataContext = this };
        //            break;
        //        case "Register":
        //            frame.Content = new SignUpView { DataContext = this };
        //            break;
        //        case "SignIn":
        //            frame.Content = new SignIn { DataContext = this };
        //            break;
        //    }
        //}

        //[RelayCommand]
        //private void ShowNewEvent()
        //{
        //    var frame = Application.Current.MainWindow.FindName("MainFrame") as System.Windows.Controls.Frame;
        //    if (frame != null)
        //    {
        //        frame.Content = new NewEvent { DataContext = this };
        //        ChangeVisibility("NewEvent", true);
        //    }
        //}

        //public void ChangeVisibility(string id, bool visible)
        //{
        //    NavigationButton n = GetButtonById(id);
        //    n.Visibility = visible?Visibility.Visible: Visibility.Collapsed;
        //}

        //public NavigationButton? GetButtonById(string id)
        //{
        //    return DynamicButtons.FirstOrDefault(b => b.Id == id);
        //}

        //[RelayCommand]
        //private async Task SubmitNewEvent()
        //{

        //    if (string.IsNullOrWhiteSpace(EventTitle) || EventTitle == "Enter Title" ||
        //        string.IsNullOrWhiteSpace(EventDescription) || EventDescription == "Enter Description" ||
        //        EventEndDateTime == null || EventStartDateTime == null ||
        //        string.IsNullOrWhiteSpace(EventLocation) || EventLocation == "Enter Location")
        //    {
        //        MessageBox.Show("Please fill in all fields.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
        //        return;
        //    }

        //    var startDateUtc = EventStartDateTime.Value.ToUniversalTime();
        //    var endDateUtc = EventEndDateTime.Value.ToUniversalTime();

        //    var newEvent = new EventManagementSystem.Models.EventDto
        //    {
        //        Title = EventTitle,
        //        Description = EventDescription,
        //        StartDate = startDateUtc,
        //        EndDate = endDateUtc,
        //        Location = EventLocation,
        //        CreatedAt = DateTime.Now.ToUniversalTime(),
        //        GroupIds = SelectedGroups.Select(g => g.Id).ToList()
        //    };


        //    try
        //    {
        //        var response = await _httpClient.PostAsJsonAsync("Event", newEvent);
        //        if (response.IsSuccessStatusCode)
        //        {
        //            MessageBox.Show("Event created successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        //            await LoadEvents();
        //            CancelNewEventCommand.Execute(null);
        //        }
        //        else
        //        {
        //            MessageBox.Show($"Failed to create event: {response.StatusCode}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Error submitting event: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        //    }
        //}
        //[RelayCommand]
        //private void CancelNewEvent()
        //{
        //    ChangeVisibility("NewEvent", false);
        //    var frame = Application.Current.MainWindow.FindName("MainFrame") as System.Windows.Controls.Frame;
        //    if (frame != null)
        //    {
        //        frame.Content = new EventManagementView { DataContext = this };
        //        EventTitle = "";
        //        EventDescription = "";
        //        EventStartDateTime = null;
        //        EventEndDateTime = null;
        //        EventLocation = "";
        //    }
        //}

        //[RelayCommand]
        //private async Task SubmitRegistration()
        //{
        //    if (NewUserDraft.IsValid() == false)
        //    {
        //        MessageBox.Show("Please fill in all fields.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
        //        return;
        //    }
        //    else if (NewUserDraft.Password != NewUserDraft.RepeatPassword)
        //    {
        //        MessageBox.Show("Passwords do not match.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
        //        return;
        //    }

        //    var newUser = new PersonDto
        //    {
        //        Name = NewUserDraft.Name,
        //        Username = NewUserDraft.UserName.ToLower(),
        //        Password = PasswordHasher.Hash(NewUserDraft.Password)
        //    };

        //    try
        //    {
        //        var response = await _httpClient.PostAsJsonAsync("person/register", newUser);

        //        if (response.IsSuccessStatusCode)
        //        {
        //            MessageBox.Show("Registration successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        //            CloseLogInCommand?.Execute(null);
        //        }
        //        else if (response.StatusCode == HttpStatusCode.Conflict)
        //        {
        //            MessageBox.Show("Username already exists.", "Conflict", MessageBoxButton.OK, MessageBoxImage.Warning);
        //        }
        //        else
        //        {
        //            MessageBox.Show($"Registration failed: {response.StatusCode}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Error: {ex.Message}", "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
        //    }
        //}


        //[RelayCommand]
        //private async Task SubmitSignIn()
        //{
        //    if (LoginUser.IsValid() == false)
        //    {
        //        MessageBox.Show("Please fill in all fields.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
        //        return;
        //    }

        //    var loginRequest = new EventManagementSystem.Models.LoginDto
        //    {
        //        Username = LoginUser.UserName.ToLower(),
        //        Password = PasswordHasher.Hash(LoginUser.Password)
        //    };

        //    try
        //    {
        //        var response = await _httpClient.PostAsJsonAsync("person/login", loginRequest);

        //        if (response.IsSuccessStatusCode)
        //        {   
        //            var person = await response.Content.ReadFromJsonAsync<Person>();

        //            // Optionally store logged-in user
        //            CurrentUser = person;

        //            MessageBox.Show("Login successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        //            CloseLogInCommand?.Execute(null);
        //        }
        //        else if (response.StatusCode == HttpStatusCode.Unauthorized)
        //        {
        //            MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Warning);
        //        }
        //        else
        //        {
        //            MessageBox.Show($"Login error: {response.StatusCode}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Login exception: {ex.Message}", "Exception", MessageBoxButton.OK, MessageBoxImage.Error);
        //    }
        //}


        //[RelayCommand]
        //private void CloseLogIn()
        //{
        //    var frame = Application.Current.MainWindow.FindName("MainFrame") as System.Windows.Controls.Frame;
        //    if (frame != null)
        //    {
        //        frame.Content = new DashboardView { DataContext = this };
        //    }
        //    if(CurrentUser.Name != string.Empty)
        //    {
        //        ChangeVisibility("SignIn", false);
        //        ChangeVisibility("Register", false);
        //        ChangeVisibility("SignOut", true);
        //    }
        //    else
        //    {
        //        ChangeVisibility("SignIn", true);
        //        ChangeVisibility("Register", true);
        //        ChangeVisibility("SignOut", false);
        //    }
        //}

        //[RelayCommand]
        //private void SignOut()
        //{
        //    CurrentUser = new();
        //    CloseLogInCommand?.Execute(null);
        //}

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