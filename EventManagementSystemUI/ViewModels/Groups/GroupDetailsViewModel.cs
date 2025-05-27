using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EventManagementSystem.Models;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.RegularExpressions;
using System.Windows;

namespace EventManagementSystemUI.ViewModels
{
    public partial class GroupDetailsViewModel : ObservableObject
    {
        private readonly HttpClient _httpClient;
        private readonly MainViewModel _vm;
        private readonly string Id;

        public GroupDetailsViewModel(HttpClient httpClient, MainViewModel vm, string id)
        {
            _httpClient = httpClient;
            _vm = vm;
            Id =  id;
            LoadGroupDetailsCommand.Execute(Id);
        }

        [ObservableProperty]
        private EventManagementSystem.Models.GroupWithEventsDto selectedGroup;

        [ObservableProperty]
        private EventManagementSystem.Models.SimpleEventDto selectedEvent;


        [RelayCommand]
        private async Task LoadGroupDetails(string groupId)
        {
            SelectedGroup = await _httpClient.GetFromJsonAsync<EventManagementSystem.Models.GroupWithEventsDto>($"group/{groupId.ToString()}");
        }
        [RelayCommand]
        private async Task Close()
        {
            _vm.NavVM.DynamicButtons.Remove(_vm.NavVM.DynamicButtons.FirstOrDefault(b => b.Id == "g" + Id.ToString()));
            _vm.NavVM.NavigateCommand.Execute("Groups");
        }
        [RelayCommand]
        private async Task EventSelected()
        {
            if (SelectedEvent is not null)
            {
                var data = new NavData(SelectedEvent.Id, SelectedEvent.Title);
                _vm.NavVM.AddEventToMenu(data);
            }
        }

    }
}