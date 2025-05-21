using System.Windows;
using EventManagementSystemUI.ViewModels;

namespace EventManagementSystemUI
{
    public partial class MainWindow : Window
    {
        public MainViewModel ViewModel { get; } = new MainViewModel();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = ViewModel;
        }
    }
}