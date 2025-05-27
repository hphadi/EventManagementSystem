using System.Windows.Controls;
using EventManagementSystemUI.ViewModels;
using WPF = System.Windows; // for WPF
//using EventManagementSystemUI.ViewModels.Groups;
using EventManagementSystem.Models;
using System.ComponentModel;
using System.Windows;


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
            var vm = DataContext as MainViewModel;
            if (vm != null && !DesignerProperties.GetIsInDesignMode(new DependencyObject()))
                vm.GroupVM?.GroupSelectedCommand.Execute(null);
        }
    }
}