using EventManagementSystemUI.ViewModels;
using System.Windows.Controls;
using WPF = System.Windows; // for WPF
namespace EventManagementSystemUI.Views;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;

public partial class EventManagementView : WPF.Controls.UserControl
{
    private readonly MainViewModel _viewModel = new MainViewModel();
    public EventManagementView()
    {
        InitializeComponent();
        DataContext = _viewModel;
    }

    private void ListView_MouseDoubleClick(object sender, WPF.Input.MouseButtonEventArgs e)
    {
        //if (sender is ListView listView && listView.SelectedItem != null)
        //{
        //    if (WPF.Application.Current.MainWindow.DataContext is MainViewModel mainViewModel)
        //    {
        //        var selectedEvent = listView.SelectedItem as EventManagementSystem.Models.Event;
        //        mainViewModel.EventDetailsViewModel.SelectedEvent = selectedEvent;
        //        mainViewModel.EventDetailsViewModel.NavigateToEventDetailsCommand.Execute(selectedEvent.Id);
        //    }
        //}
    }

}
