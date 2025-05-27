using EventManagementSystemUI.ViewModels;
using System.Windows;
using System.Windows.Controls;
using WPF = System.Windows; // for WPF

namespace EventManagementSystemUI.Views;

public partial class ProfileView : WPF.Controls.UserControl
{
    public ProfileView()
    {
        InitializeComponent();
        this.Loaded += ProfileView_Loaded;
    }

    private void ProfileView_Loaded(object sender, RoutedEventArgs e)
    {
        if (DataContext is MainViewModel vm)
        {
            vm.UserVM?.LoadUserDetailsCommand.Execute(null);
        }
    }
    private void ListView_MouseDoubleClick(object sender, WPF.Input.MouseButtonEventArgs e)
    {
        if (sender is ListView listView && listView.SelectedItem != null)
        {
            var vm = DataContext as MainViewModel;
            vm.UserVM?.EventSelectedCommand.Execute(null);
        }
    }
}