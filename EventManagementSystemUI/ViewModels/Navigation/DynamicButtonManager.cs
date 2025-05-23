//using System.Collections.ObjectModel;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Media;
//using CommunityToolkit.Mvvm.Input;

//namespace EventManagementSystemUI.ViewModels.Navigation
//{
//    public class DynamicButtonManager
//    {
//        private readonly StackPanel _buttonPanel;
//        private readonly ObservableCollection<Button> _dynamicButtons;

//        public DynamicButtonManager(StackPanel buttonPanel)
//        {
//            _buttonPanel = buttonPanel;
//            _dynamicButtons = new ObservableCollection<Button>();
//        }

//        public void AddDynamicButton(string buttonText, RelayCommand command)
//        {
//            var button = new Button
//            {
//                Content = buttonText,
//                Margin = new Thickness(5),
//                Padding = new Thickness(10, 5, 10, 5),
//                Style = (Style)Application.Current.Resources["MaterialDesignRaisedButton"],
//                Background = Brushes.Red,
//                Foreground = Brushes.White,
//                Command = command,
//                CommandParameter = "GroupDetails"
//            };

//            _dynamicButtons.Add(button);
//            _buttonPanel?.Children.Add(button);
//        }

//        public void RemoveDynamicButtons()
//        {
//            if (_buttonPanel != null)
//            {
//                foreach (var button in _dynamicButtons)
//                {
//                    _buttonPanel.Children.Remove(button);
//                }
//            }
//            _dynamicButtons.Clear();
//        }
//    }
//}