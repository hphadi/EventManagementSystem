﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EventManagementSystem.Models;
using EventManagementSystemUI.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows;

namespace EventManagementSystemUI.ViewModels
{
    public partial class NewGroupViewModel : ObservableObject
    {
        private readonly HttpClient _httpClient;
        private readonly MainViewModel _vm;

        public ObservableCollection<GroupBase> Groups { get; } = new();

        public NewGroupViewModel(HttpClient httpClient, MainViewModel vm)
        {
            _httpClient = httpClient;
            _vm = vm;
        }

        [ObservableProperty]
        private DraftGroupDto newGroupDraft = new();

        [RelayCommand]
        private async Task SubmitNewGroup()
        {
            if (!NewGroupDraft.ShowValidationErrorsIfInvalid())
                return;

            var newGroup = new NewGroupDto
            {
                Name = NewGroupDraft.Name,
                Description = NewGroupDraft.Description,
                City = NewGroupDraft.City,
                CreatedAt = DateTime.Now.ToUniversalTime(),
            };


            try
            {
                var response = await _httpClient.PostAsJsonAsync("Group", newGroup);
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Group created successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    await _vm.GroupVM.LoadGroups();
                    if (!DesignerProperties.GetIsInDesignMode(new DependencyObject()))
                        CancelNewGroupCommand.Execute(null);
                }
                else
                {
                    MessageBox.Show($"Failed to create group: {response.StatusCode}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error submitting group: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        [RelayCommand]
        private void CancelNewGroup()
        {
            _vm.NavVM.ChangeVisibility("NewGroup", false);
            NewGroupDraft.Clear();
            _vm.NavVM.Navigate("Dashboard");
        }
    }
}
