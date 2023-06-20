using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        BackgroundWorker backgroundWorker;
        public MainWindow()
        {
            InitializeComponent();
            backgroundWorker = (BackgroundWorker)this.Resources["worker"];
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window1 window1 = new Window1();
            if (window1.ShowDialog() != true) return;
            integral = window1.integral;
            //MessageBox.Show(integral.ToString());
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
            if (integral == null) return;
            double h = (integral.B - integral.A) / integral.N;
            var step = Math.Round((double)integral.N / 100);
            double S = 0;
            for (int i = 0; i < integral.N; i++)
            {
                double x = integral.A + h * i;
                S += integral.fx(x) * h;
                if(i% step == 0)
                {
                    Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => pBar.Value = i / step));
                }
                Dispatcher.BeginInvoke(DispatcherPriority.Normal, 
                    new Action(() => textBlock.Text = String.Format($"x={x:0.00} S = {S:0.00000}\n")));
                Thread.Sleep(100);
            }
        }



        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            buttonD.IsEnabled = false;
            buttonW.IsEnabled = false;
            backgroundWorker.RunWorkerAsync();
        }

        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if(integral == null) return;
            double h = (integral.B - integral.A) / integral.N;
            var step = Math.Round((double)integral.N / 100);
            double S = 0;
            for (int i = 0; i < integral.N; i++)
            {
                double x = integral.A + h * i;
                S += integral.fx(x) * h;
                if (i % step == 0)
                {
                    if (backgroundWorker != null && backgroundWorker.WorkerReportsProgress)
                        backgroundWorker.ReportProgress((int)(i / step));
                }
                Dispatcher.BeginInvoke(DispatcherPriority.Normal, 
                    new Action(() => textBlock.Text =  String.Format($"x ={x:0.00} S = {S:0.00000}\n")));
                Thread.Sleep(100);
            }
        }

        private void BackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pBar.Value = e.ProgressPercentage;
        }

        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            buttonD.IsEnabled = true;
            buttonW.IsEnabled = true;
        }
    }
}
