using System.Windows;
using System.Windows.Controls;
using EventManagementSystemUI.ViewModels;

namespace EventManagementSystemUI
{
    public partial class MainWindow : Window
    {
        private readonly MainViewModel _viewModel = new MainViewModel();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = _viewModel;

        }
    }
}