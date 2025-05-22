using EventManagementSystemUI.ViewModels;
using EventManagementSystem.Models;
using System.Windows.Controls;

namespace EventManagementSystemUI.Views
{
    /// <summary>
    /// Interaction logic for NewEvent.xaml
    /// </summary>
    public partial class NewEvent : UserControl
    {
        public NewEvent()
        {
            InitializeComponent();
            DataContextChanged += (s, e) =>
            {
                if (DataContext is MainViewModel vm)
                {
                    GroupsListBox.SelectionChanged += (s2, e2) =>
                    {
                        vm.SelectedGroups.Clear();
                        foreach (Group item in GroupsListBox.SelectedItems)
                            vm.SelectedGroups.Add(item);
                    };
                }
            };
        }
    }
}
