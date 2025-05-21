using EventManagementSystemUI.ViewModels;
using System.Windows.Controls;
using WPF = System.Windows; // for WPF
namespace EventManagementSystemUI.Views;

public partial class EventManagementView : WPF.Controls.UserControl
{
    private readonly MainViewModel _viewModel = new MainViewModel();
    public EventManagementView()
    {
        InitializeComponent();
        DataContext = _viewModel;
    }

}