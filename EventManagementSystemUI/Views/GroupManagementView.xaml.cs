using System.Windows.Controls;
using EventManagementSystemUI.ViewModels;
using WPF = System.Windows; // for WPF
//using EventManagementSystemUI.ViewModels.Groups;
using EventManagementSystem.Models;


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
        //if (DataContext is GroupViewModel groupViewModel)
        //{
            //groupViewModel.GroupDetailsViewModel.SelectedGroup = listView.SelectedItem as EventManagementSystem.Models.Group;
            //groupViewModel.GroupDetailsViewModel.OnGroupSelectedCommand.Execute(null);
        //}

            var viewModel = DataContext as MainViewModel;
            viewModel?.GroupSelectedCommand.Execute(null);
        }
    }
}