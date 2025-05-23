//using CommunityToolkit.Mvvm.ComponentModel;
//using CommunityToolkit.Mvvm.Input;
//using EventManagementSystemUI.ViewModels.Navigation;
//using System.Collections.ObjectModel;
//using System.Net.Http;
//using System.Net.Http.Json;
//using System.Windows;

//namespace EventManagementSystemUI.ViewModels.Groups
//{
//    public partial class GroupDetailsViewModel : ObservableObject
//    {
//        private readonly HttpClient _httpClient;
//        private readonly NavigationViewModel _navigationViewModel;
//        private readonly DynamicButtonManager _buttonManager;

//        [ObservableProperty]
//        private Group selectedGroup;

//        [ObservableProperty]
//        private ObservableCollection<EventManagementSystem.Models.Event> groupEvents;

//        public GroupDetailsViewModel(HttpClient httpClient, NavigationViewModel navigationViewModel, DynamicButtonManager buttonManager)
//        {
//            _httpClient = httpClient;
//            _navigationViewModel = navigationViewModel;
//            _buttonManager = buttonManager;
//            GroupEvents = new ObservableCollection<EventManagementSystem.Models.Event>();
//        }

//        [RelayCommand]
//        private async Task LoadGroupEvents(int groupId)
//        {
//            try
//            {
//                System.Diagnostics.Debug.WriteLine($"Loading events for group {groupId}...");
//                var events = await _httpClient.GetFromJsonAsync<List<EventManagementSystem.Models.Event>>($"Group/{groupId}/events");
//                if (events != null)
//                {
//                    GroupEvents.Clear();
//                    foreach (var evt in events)
//                    {
//                        GroupEvents.Add(evt);
//                    }
//                }
//                else
//                {
//                    System.Diagnostics.Debug.WriteLine("No group events loaded.");
//                }
//            }
//            catch (Exception ex)
//            {
//                System.Diagnostics.Debug.WriteLine($"Error in LoadGroupEvents: {ex.Message}");
//                MessageBox.Show($"Error loading group events: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
//            }
//        }

//        [RelayCommand]
//        private void OnGroupSelected()
//        {
//            if (SelectedGroup != null)
//            {
//                _navigationViewModel.Navigate("GroupDetails", this);
//                _buttonManager.AddDynamicButton(SelectedGroup.Name, new RelayCommand(() => _navigationViewModel.Navigate("GroupDetails", this)));
//                LoadGroupEvents(SelectedGroup.Id).ConfigureAwait(false);
//            }
//        }
//    }
//}