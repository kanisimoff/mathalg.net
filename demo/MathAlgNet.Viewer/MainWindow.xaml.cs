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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MathAlgNet.Viewer.ViewModel;

namespace MathAlgNet.Viewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new SquareMethodViewModel();
        }

        private void Evalute_OnClick(object sender, RoutedEventArgs e)
        {
            if (!(this.DataContext is SquareMethodViewModel dataContext))
                return;
            dataContext.Evalute();
        }

    }
}
