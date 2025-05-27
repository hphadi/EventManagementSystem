using EventManagementSystemUI.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EventManagementSystemUI.Views
{
    /// <summary>
    /// Interaction logic for GroupDetailsView.xaml
    /// </summary>
    public partial class GroupDetailsView : UserControl
    {
        //public GroupDetailsViewModel GroupDetailsVM { get; set; }

        public GroupDetailsView()
        {
            InitializeComponent();
        }

        private void ListView_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (sender is ListView listView && listView.SelectedItem != null)
            {
                var vm = DataContext as GroupDetailsViewModel;
                if (vm != null && !DesignerProperties.GetIsInDesignMode(new DependencyObject()))
                    vm.EventSelectedCommand.Execute(null);
            }
        }
    }
}
