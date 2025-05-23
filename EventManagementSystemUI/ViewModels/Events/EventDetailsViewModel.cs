using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows;
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
            LoadEventDetailsCommand.Execute(Int32.Parse(Id));
        }

        [ObservableProperty]
        private EventManagementSystem.Models.EventWithGroupsDto selectedEvent = new();

        [RelayCommand]
        private async Task LoadEventDetails(int eventId)
        {
            SelectedEvent = await _httpClient.GetFromJsonAsync<EventManagementSystem.Models.EventWithGroupsDto>($"event/{eventId}");
        }
        [RelayCommand]
        private async Task Close()
        {
            _vm.NavVM.DynamicButtons.Remove(_vm.NavVM.DynamicButtons.FirstOrDefault(b => b.Id == "e" + Id));
            _vm.NavVM.NavigateCommand.Execute("Events");
        }

        //[RelayCommand]
        //public async Task NavigateToEventDetails(int eventId)
        //{
        //    await LoadEventDetails(eventId);
        //    _navigateAction?.Invoke(this); 
        //}
    }
}