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