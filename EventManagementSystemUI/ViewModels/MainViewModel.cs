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
        public NewGroupViewModel NewGroupVM { get; }
        public NavigationViewModel NavVM { get; }
        public UserViewModel UserVM { get; }

        public MainViewModel()
        {
            _httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:5144/api/") };
            
            EventVM = new EventManagementSystemUI.ViewModels.EventViewModel(_httpClient, this);
            NewEventVM = new EventManagementSystemUI.ViewModels.NewEventViewModel(_httpClient, this);
            GroupVM = new EventManagementSystemUI.ViewModels.GroupViewModel(_httpClient, this);
            NewGroupVM = new EventManagementSystemUI.ViewModels.NewGroupViewModel(_httpClient, this);
            //GroupDetailsVM = new EventManagementSystemUI.ViewModels.GroupDetailsViewModel(_httpClient, this);
            NavVM = new EventManagementSystemUI.ViewModels.NavigationViewModel(_httpClient, this);
            UserVM = new EventManagementSystemUI.ViewModels.UserViewModel(_httpClient, this);
            
            NavVM.LoadButtons();
        }
    }
}