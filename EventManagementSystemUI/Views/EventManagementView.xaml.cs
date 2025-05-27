using EventManagementSystemUI.ViewModels;
using System.Windows.Controls;
using WPF = System.Windows; // for WPF
namespace EventManagementSystemUI.Views;
using System.ComponentModel;

using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;

public partial class EventManagementView : WPF.Controls.UserControl
{
    public EventManagementView()
    {
        InitializeComponent();
    }

    private void ListView_MouseDoubleClick(object sender, WPF.Input.MouseButtonEventArgs e)
    {
        if (sender is ListView listView && listView.SelectedItem != null)
        {
            var vm = DataContext as MainViewModel;
            if (vm != null && !DesignerProperties.GetIsInDesignMode(new DependencyObject()))
                vm.EventVM?.EventSelectedCommand.Execute(null);
        }
    }

}
