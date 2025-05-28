using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
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
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is MainViewModel vm)
            {
                vm.NavVM.NavigateCommand.Execute("Dashboard");
            }
        }
    }

}
