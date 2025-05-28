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
            var group = await _httpClient.GetFromJsonAsync<EventManagementSystem.Models.GroupWithEventsDto>($"group/{groupId}");
            if(group!=null)
            {
                SelectedGroup = group;
            }
        }
        [RelayCommand]
        private void Close()
        {
            _vm.NavVM.DeleteNavButtonCommand.Execute("g" + Id);
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