using EventManagementSystemUI.ViewModels;
using WPF = System.Windows; // for WPF


namespace EventManagementSystemUI.Views;

public partial class DashboardView : WPF.Controls.UserControl
{
    public DashboardView()
    {
        InitializeComponent();
        this.Loaded += DashboardView_Loaded;
    }

    private void DashboardView_Loaded(object sender, WPF.RoutedEventArgs e)
    {
        if (DataContext is MainViewModel vm)
        {
            vm.EventVM?.LoadEventsCommand.Execute(null);
        }
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