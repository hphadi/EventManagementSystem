//using CommunityToolkit.Mvvm.ComponentModel;
//using CommunityToolkit.Mvvm.Input;
//using System.Collections.ObjectModel;
//using System.Net.Http;
//using System.Net.Http.Json;
//using System.Windows;

//namespace EventManagementSystemUI.ViewModels.Groups
//{
//    public partial class GroupViewModel : ObservableObject
//    {
//        private readonly HttpClient _httpClient;

//        [ObservableProperty]
//        private ObservableCollection<EventManagementSystem.Models.Group> groups;

//        public GroupViewModel(HttpClient httpClient)
//        {
//            _httpClient = httpClient;
//            Groups = new ObservableCollection<EventManagementSystem.Models.Group>();
//            LoadGroupsCommand.Execute(null);
//        }

//        [RelayCommand]
//        private async Task LoadGroups()
//        {
//            try
//            {
//                System.Diagnostics.Debug.WriteLine("Loading groups...");
//                var groups = await _httpClient.GetFromJsonAsync<List<EventManagementSystem.Models.Group>>("Group");
//                if (groups != null)
//                {
//                    Groups.Clear();
//                    foreach (var group in groups)
//                    {
//                        Groups.Add(group);
//                    }
//                }
//                else
//                {
//                    System.Diagnostics.Debug.WriteLine("No groups loaded.");
//                }
//            }
//            catch (Exception ex)
//            {
//                System.Diagnostics.Debug.WriteLine($"Error in LoadGroups: {ex.Message}");
//                MessageBox.Show($"Error loading groups: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
//            }
//        }
//    }
//}