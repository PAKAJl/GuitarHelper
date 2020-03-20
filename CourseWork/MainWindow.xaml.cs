using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace CourseWork
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static bool[] checkOnClick;
        private static Button[] buttonsList;
        public double freq;
        public MainWindow()
        {
            InitializeComponent();
            ScanSoundCards();
            checkOnClick = new bool[2];
            buttonsList = new Button[2];
            buttonsList[0] = button0;
            buttonsList[1] = button1;
            for (int i = 0; i < checkOnClick.Length; i++)
            {
                checkOnClick[i] = false;
            }
        }
        
        private void ScanSoundCards()
        {
            cdDevice.Items.Clear();
            for (int i = 0; i < NAudio.Wave.WaveIn.DeviceCount; i++)
                cdDevice.Items.Add(NAudio.Wave.WaveIn.GetCapabilities(i).ProductName);
            if (cdDevice.Items.Count > 0)
                cdDevice.SelectedIndex = 0;
            else
                MessageBox.Show("ERROR: no recording devices available");
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            Sound sound = new Sound(this);
            this.Dispatcher.BeginInvoke(new ThreadStart(delegate
            {
                sound.StartDetect(cdDevice.SelectedIndex);
            }));
        }
        private static void ButtonAccsess(int numberButton)
        {
            for (int i = 0; i < checkOnClick.Length; i++)
            {
                checkOnClick[i] = false;
            }
            checkOnClick[numberButton] = true;
            for (int i = 0; i < buttonsList.Length; i++)
            {
                if (!checkOnClick[i])
                {
                    buttonsList[i].Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF323232"));
                }
                else
                {
                    buttonsList[i].Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#525252"));
                }
            }
        }  
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            ButtonAccsess(0);
            OxyPlot.Wpf.PlotView plotView = new OxyPlot.Wpf.PlotView();
            plotView.Margin = new Thickness(0, 0, 82, 10);
        }
        private void button2_Click(object sender, RoutedEventArgs e)
        {
            ButtonAccsess(1);
        }

    }
}
