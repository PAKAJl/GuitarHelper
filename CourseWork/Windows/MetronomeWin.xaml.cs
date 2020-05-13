using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Media;
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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace CourseWork.Windows
{
    /// <summary>
    /// Логика взаимодействия для MetronomeWin.xaml
    /// </summary>
    public partial class MetronomeWin : Window
    {
        public MetronomeWin()
        {
            InitializeComponent();
            margin.Left = 10;
            margin.Bottom = 10;
            margin.Top = 10;
            margin.Right = 10;

        }

        DispatcherTimer metronome = new DispatcherTimer();
        int beatCount = 0;
        double beatTime = 0;
        int beatCounter = 0;
        Brush brush = new SolidColorBrush(Color.FromRgb(0, 114, 0));
        Brush brush1 = new SolidColorBrush(Color.FromRgb(0, 255, 74));
        Thickness margin = new Thickness();
        Ellipse metrElipse;
        Ellipse[] indic;

        SoundPlayer metronomeHighTick = new SoundPlayer("../../Resource/Sounds/HighTick.wav");
        SoundPlayer metronomeLowTick = new SoundPlayer("../../Resource/Sounds/LowTick.wav");
        //Metronome ticking sound

        private async void startButton_Click(object sender, RoutedEventArgs e)
        {
            if (startButton.Content.ToString() == "Запустить")
            {
                metronome = new DispatcherTimer();
                indicators.Children.Clear();
                beatCount = int.Parse(ticksInTackBox.Text);
                beatTime = (double)60 / (double)int.Parse(beatsInMinBox.Text);
                indic = new Ellipse[beatCount];
                for (int i = 0; i < beatCount; i++)
                {
                    metrElipse = new Ellipse()
                    {
                        Fill = brush,
                        Height = 30,
                        Width = 30,
                        Margin = margin,
                        Name = $"ind{i}"
                    };
                    indic[i] = metrElipse;
                    indicators.Children.Add(indic[i]);
                }
                startButton.Content = "Остановить";
                await Task.Factory.StartNew(() =>
                {
                    metronome.Interval = TimeSpan.FromMilliseconds(beatTime * 1000);
                    metronome.Tick += new EventHandler(StartMetronom);
                    metronome.Start();

                });

            }
            else
            {
                indicators.Children.Clear();
                startButton.Content = "Запустить";
                metronome.Stop();
                beatTime = 0;
            }

        }
        int lastindic = 0;
        private void StartMetronom(object sender, EventArgs e)
        {
            indic[lastindic].Fill = brush;
            if (beatCounter == beatCount)
            {
                beatCounter = 0;
            }
            indic[beatCounter].Fill = brush1;

            if (beatCounter == 0)
            {
                metronomeHighTick.PlaySync();
            }
            else
            {
                metronomeLowTick.PlaySync();
            }
            lastindic = beatCounter;
            beatCounter++;
        }


        private void plusTickButton_Click(object sender, RoutedEventArgs e)
        {
            beatsInMinBox.Text = $"{int.Parse(beatsInMinBox.Text) + 1}";
        }

        private void minusTickButton_Click(object sender, RoutedEventArgs e)
        {
            beatsInMinBox.Text = $"{int.Parse(beatsInMinBox.Text) - 1}";
        }

        private void plusTackButton_Click(object sender, RoutedEventArgs e)
        {
            ticksInTackBox.Text = $"{int.Parse(ticksInTackBox.Text) + 1}";
        }

        private void minusTackButton_Click(object sender, RoutedEventArgs e)
        {
            ticksInTackBox.Text = $"{int.Parse(ticksInTackBox.Text) - 1}";
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            metronome.Stop();
        }

        private void beatsInMinBox_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (int.Parse(beatsInMinBox.Text) < 30)
                {
                    beatsInMinBox.Text = "30";
                }
                if (int.Parse(beatsInMinBox.Text) > 180)
                {
                    beatsInMinBox.Text = "180";
                }
            }
            catch (Exception ex)
            {
                beatsInMinBox.Text = "30";
            }
                

        }

        private void ticksInTackBox_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (int.Parse(ticksInTackBox.Text) < 3)
                {
                    ticksInTackBox.Text = "3";
                }
                if (int.Parse(ticksInTackBox.Text) > 10)
                {
                    ticksInTackBox.Text = "10";
                }
            }
            catch (Exception ex)
            {
                ticksInTackBox.Text = "3";
            }

        }
    }
}
