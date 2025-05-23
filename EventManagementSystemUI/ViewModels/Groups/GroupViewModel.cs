using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows;

namespace EventManagementSystemUI.ViewModels
{
    public partial class GroupViewModel : ObservableObject
    {
        private readonly HttpClient _httpClient;
        private readonly MainViewModel _vm;

        public GroupViewModel(HttpClient httpClient, MainViewModel vm)
        {
            _httpClient = httpClient;
            _vm = vm;
        }

        [ObservableProperty]
        private ObservableCollection<EventManagementSystem.Models.Group> groups;

        [ObservableProperty]
        private EventManagementSystem.Models.Group selectedGroup;

        [RelayCommand]
        private async Task LoadGroups()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("Loading groups..."); // for debugging
                var groups_loaded = await _httpClient.GetFromJsonAsync<List<EventManagementSystem.Models.Group>>("Group");
                if (groups_loaded != null)
                {
                    groups.Clear();
                    foreach (var group in groups_loaded)
                    {
                        groups.Add(group); // each group is added to the ObservableCollection

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

    }
}