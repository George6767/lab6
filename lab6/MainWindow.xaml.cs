using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace lab6
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml 
    /// /// </summary>
    public partial class MainWindow : Window
    {
        public Integral integral;
        public MainWindow()
        {
            InitializeComponent();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window1 window1 = new Window1();
            if (window1.ShowDialog() != true) return;
            integral = window1.integral;
            MessageBox.Show(integral.ToString());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (integral == null) return;
            //MessageBox.Show(integral.Calculate().ToString());
            Thread thread = new Thread(Calculate);
            thread.Start();
        }

        public void Calculate()
        {
            double h = (integral.B - integral.A) / integral.N;
            double S = 0;
            for (int i = 0; i < integral.N; i++)
            {
                double x = integral.A + h * i;
                S += integral.fx(x) * h;
                Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => textBlock.Text = S.ToString()));
                Thread.Sleep(100);
            }
        }


        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }
    }
}
