//using CommunityToolkit.Mvvm.ComponentModel;
//using CommunityToolkit.Mvvm.Input;
//using System.Collections.ObjectModel;
//using System.Net.Http;
//using System.Net.Http.Json;
//using System.Windows;

//namespace EventManagementSystemUI.ViewModels.Events
//{
//    public partial class EventViewModel : ObservableObject
//    {
//        private readonly HttpClient _httpClient;

//        [ObservableProperty]
//        private ObservableCollection<EventManagementSystem.Models.Event> events;

//        [ObservableProperty]
//        private ObservableCollection<EventManagementSystem.Models.Event> futureEvents;

//        public EventViewModel(HttpClient httpClient)
//        {
//            _httpClient = httpClient;
//            Events = new ObservableCollection<EventManagementSystem.Models.Event>();
//            FutureEvents = new ObservableCollection<EventManagementSystem.Models.Event>();
//            LoadEventsCommand.Execute(null);
//        }

//        [RelayCommand]
//        private async Task LoadEvents()
//        {
//            try
//            {
//                System.Diagnostics.Debug.WriteLine("Loading events...");
//                var events = await _httpClient.GetFromJsonAsync<List<EventManagementSystem.Models.Event>>("Event");
//                if (events != null)
//                {
//                    Events.Clear();
//                    FutureEvents.Clear();
//                    foreach (var evt in events)
//                    {
//                        Events.Add(evt);
//                        if (evt.StartDate > DateTime.Now)
//                        {
//                            FutureEvents.Add(evt);
//                        }
//                    }
//                }
//                else
//                {
//                    System.Diagnostics.Debug.WriteLine("No events loaded.");
//                }
//            }
//            catch (Exception ex)
//            {
//                System.Diagnostics.Debug.WriteLine($"Error in LoadEvents: {ex.Message}");
//                MessageBox.Show($"Error loading events: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
//            }
//        }
//    }
//}