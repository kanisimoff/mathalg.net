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
using MathAlgNet.Viewer.WriterProvider;

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
            Console.SetOut(new ControlWriter(this.TextBlock));
            Console.WriteLine("Данная программа позволяет выполнить среднеквадратичное приближение по степенному базису для функции, заданной таблично, а также вычислить значение функции при некотором значении параметра x");
            Console.WriteLine("");
        }

        private void Evalute_OnClick(object sender, RoutedEventArgs e)
        {
            if (!(this.DataContext is SquareMethodViewModel dataContext))
                return;
            dataContext.Evalute();
        }

    }
}
