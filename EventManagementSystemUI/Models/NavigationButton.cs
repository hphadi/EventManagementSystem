using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace EventManagementSystemUI.Models
{
    public partial class NavigationButton : ObservableObject
    {
        [ObservableProperty]
        private string id = string.Empty;

        [ObservableProperty]
        private string title = string.Empty;

        [ObservableProperty]
        private string commandParameter = string.Empty;

        [ObservableProperty]
        private ICommand command = null!;

        [ObservableProperty]
        private Brush background = Brushes.LightGray;

        [ObservableProperty]
        private Visibility visibility = Visibility.Visible;

        [ObservableProperty]
        private FontWeight fontWeight = FontWeights.Normal;

        [ObservableProperty]
        private double fontSize = 14; // ✅ default size for static buttons

        [ObservableProperty]
        private Thickness margin = new(5);
    }
}