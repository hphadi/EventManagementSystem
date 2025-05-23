//using CommunityToolkit.Mvvm.ComponentModel;
//using CommunityToolkit.Mvvm.Input;
//using EventManagementSystemUI.Views;
//using System.Windows.Controls;

//namespace EventManagementSystemUI.ViewModels.Navigation
//{
//    public partial class NavigationViewModel : ObservableObject
//    {
//        private readonly Frame _mainFrame;
//        private readonly DynamicButtonManager _buttonManager;

//        public NavigationViewModel(Frame mainFrame, DynamicButtonManager buttonManager)
//        {
//            _mainFrame = mainFrame;
//            _buttonManager = buttonManager;
//        }

//        [RelayCommand]
//        public void Navigate(string page, object? dataContext = null) // Fix for CS8625: Allow dataContext to be nullable
//        {
//            if (_mainFrame == null) return;

//            switch (page)
//            {
//                case "Dashboard":
//                    _mainFrame.Content = new DashboardView { DataContext = dataContext };
//                    _buttonManager.RemoveDynamicButtons();
//                    break;
//                case "Events":
//                    _mainFrame.Content = new EventManagementView { DataContext = dataContext };
//                    _buttonManager.RemoveDynamicButtons();
//                    break;
//                case "Groups":
//                    _mainFrame.Content = new GroupManagementView { DataContext = dataContext };
//                    _buttonManager.RemoveDynamicButtons();
//                    break;
//                case "Profile":
//                    _mainFrame.Content = new ProfileView { DataContext = dataContext };
//                    _buttonManager.RemoveDynamicButtons();
//                    break;
//                case "NewEvent":
//                    _mainFrame.Content = new NewEvent { DataContext = dataContext };
//                    _buttonManager.RemoveDynamicButtons();
//                    break;
//                case "GroupDetails":
//                    _mainFrame.Content = new GroupDetailsView { DataContext = dataContext };
//                    break;
//            }
//        }
//    }
//}