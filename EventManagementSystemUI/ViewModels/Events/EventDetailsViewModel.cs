using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Net.Http;
using System.Net.Http.Json;
using EventManagementSystem.Models;

namespace EventManagementSystemUI.ViewModels
{
    public partial class EventDetailsViewModel : ObservableObject
    {
        private readonly HttpClient _httpClient;
        private readonly MainViewModel _vm;
        private readonly string Id;

        public EventDetailsViewModel(HttpClient httpClient, MainViewModel vm, string id)
        {
            _httpClient = httpClient;
            _vm = vm;
            Id = id;
            LoadEventDetailsCommand.Execute(Id);
        }

        [ObservableProperty]
        private EventManagementSystem.Models.EventWithGroupsDto selectedEvent = new();

        [RelayCommand]
        private async Task LoadEventDetails(string eventId)
        {
            var event_ = await _httpClient.GetFromJsonAsync<EventWithGroupsDto>($"event/{eventId}");
            if (event_ != null)
            {
                SelectedEvent = event_;
            }
        }
        [RelayCommand]
        private void Close()
        {
            _vm.NavVM.DeleteNavButtonCommand.Execute("e" + Id);
            _vm.NavVM.NavigateCommand.Execute("Events");
        }
    }
}