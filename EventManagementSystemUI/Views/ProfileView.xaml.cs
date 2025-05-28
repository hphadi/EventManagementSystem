using EventManagementSystem.Models;
using EventManagementSystemUI.ViewModels;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
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

public class GroupListToStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var groups = value as List<SimpleGroupDto>;
        if (groups == null || !groups.Any())
            return "Groups:";

        return "Groups: " + string.Join(", ", groups.Select(g => g.Name));
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}