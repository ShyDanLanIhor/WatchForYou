using Shyryi_WatchForYou.ViewModels;
using Shyryi_WatchForYou.ViewModels.AriaListViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Shyryi_WatchForYou.Views.AriaListView
{
    /// <summary>
    /// Interaction logic for CameraView.xaml
    /// </summary>
    public partial class CameraView : Window
    {
        public CameraView()
        {
            InitializeComponent();
            this.DataContext = new CameraViewModel();
        }
    }
}
