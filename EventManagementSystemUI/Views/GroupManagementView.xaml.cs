using System.Windows.Controls;
using EventManagementSystemUI.ViewModels;
using WPF = System.Windows; // for WPF

namespace EventManagementSystemUI.Views;

public partial class GroupManagementView : WPF.Controls.UserControl
{
    public GroupManagementView()
    {
        InitializeComponent();
    }

    private void ListView_MouseDoubleClick(object sender, WPF.Input.MouseButtonEventArgs e)
    {
        if (sender is ListView listView && listView.SelectedItem != null)
        {
            var viewModel = DataContext as MainViewModel;
            viewModel?.GroupSelectedCommand.Execute(null);
        }
    }
}