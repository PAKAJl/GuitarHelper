using NAudio.Wave;
using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace CourseWork.Resource.Pages
{
    /// <summary>
    /// Логика взаимодействия для TunnerPage.xaml
    /// </summary>
    public partial class TunnerPage : Page
    {

        private float freq;
        public WaveInEvent waveIn;
        Sound sound;
        bool recordStatus = false;
        private int inputDevice = 0;
        private int manualNumber = -1;
        private DispatcherTimer timerFrame = new DispatcherTimer();
        Border[] indicat = new Border[11];
        string[] notesName = { "E2", "A", "D", "G", "B", "E4" };
        double[][] frames =
            {
            new double[3] {68.615,82.41,96.205},
            new double[3] {96.205,110,128.415},
            new double[3] {128.415,146.83,171.415},
            new double[3] {171.415,196,221.47},
            new double[3] { 221.47, 246.94,288.285},
            new double[3] { 288.285, 329.63,370.975},
            };

        public void SetInDevice(int index)
        {
            inputDevice = index;
        }


        public TunnerPage()
        {
            InitializeComponent();
            ScanSoundCards();
            sound = new Sound();
            indicat = FillIndicators();
            foreach (var item in indicat)
            {
                indicators.Children.Add(item);
            }
        }

        private Border[] FillIndicators()
        {
            Thickness thickness = new Thickness();
            thickness.Left = 0;
            thickness.Left = 0;
            thickness.Left = 0;
            thickness.Left = 0;
            Border[] ind = new Border[11];
            for (int i = 0; i < ind.Length; i++)
            {
                if (i == 0)
                {
                    CornerRadius cornerRadius = new CornerRadius();
                    cornerRadius.TopLeft = 3;
                    cornerRadius.TopRight = 0;
                    cornerRadius.BottomRight = 0;
                    cornerRadius.BottomLeft = 3;
                    ind[i] = new Border
                    {
                        Opacity = 0.5,
                        Height = 40,
                        Width = 20,
                        BorderThickness = thickness,
                        Background = System.Windows.Media.Brushes.Red,
                    };
                }
                else if (i == 10)
                {
                    CornerRadius cornerRadius = new CornerRadius();
                    cornerRadius.TopLeft = 0;
                    cornerRadius.TopRight = 3;
                    cornerRadius.BottomRight = 3;
                    cornerRadius.BottomLeft = 0;
                    ind[i] = new Border
                    {
                        Opacity = 0.5,
                        Height = 40,
                        Width = 20,
                        BorderThickness = thickness,
                        Background = System.Windows.Media.Brushes.Red,
                    };
                }
                else
                {
                    CornerRadius cornerRadius = new CornerRadius();
                    cornerRadius.TopLeft = 0;
                    cornerRadius.TopRight = 0;
                    cornerRadius.BottomRight = 0;
                    cornerRadius.BottomLeft = 0;
                    if ((i == 1) || (i == 9))
                    {
                        ind[i] = new Border
                        {
                            Opacity = 0.5,
                            Height = 40,
                            Width = 20,
                            BorderThickness = thickness,
                            Background = System.Windows.Media.Brushes.OrangeRed,
                        };
                    }
                    if ((i == 2) || (i == 8))
                    {
                        ind[i] = new Border
                        {
                            Opacity = 0.5,
                            Height = 40,
                            Width = 20,
                            BorderThickness = thickness,
                            Background = System.Windows.Media.Brushes.Orange
                        };
                    }
                    if ((i == 3) || (i == 7))
                    {
                        ind[i] = new Border
                        {
                            Opacity = 0.5,
                            Height = 40,
                            Width = 20,
                            BorderThickness = thickness,
                            Background = System.Windows.Media.Brushes.Yellow
                        };
                    }
                    if ((i == 4) || (i == 6))
                    {
                        ind[i] = new Border
                        {
                            Opacity = 0.5,
                            Height = 40,
                            Width = 20,
                            BorderThickness = thickness,
                            Background = System.Windows.Media.Brushes.YellowGreen
                        };
                    }
                    if (i == 5)
                    {
                        ind[i] = new Border
                        {
                            Opacity = 0.5,
                            Height = 40,
                            Width = 20,
                            BorderThickness = thickness,
                            Background = System.Windows.Media.Brushes.Lime
                        };
                    }
                }
            }
            return ind;
        }


        private void ScanSoundCards()
        {
            cdDevice.Items.Clear();
            for (int i = 0; i < WaveIn.DeviceCount; i++)
                cdDevice.Items.Add(WaveIn.GetCapabilities(i).ProductName);
            if (cdDevice.Items.Count > 0)
                cdDevice.SelectedIndex = 0;
            else
                MessageBox.Show("ERROR: no recording devices available");
        }

        public void StartDetect(int inputDevice)
        {
            this.Dispatcher.BeginInvoke((ThreadStart)delegate ()
            {
                waveIn = new WaveInEvent();

                waveIn.DeviceNumber = inputDevice;
                waveIn.WaveFormat = new WaveFormat(44100, 1);
                waveIn.DataAvailable += sound.WaveIn_DataAvailable;

                sound.bufferedWaveProvider = new BufferedWaveProvider(waveIn.WaveFormat);

                // begin record
                waveIn.StartRecording();

                IWaveProvider stream = new Wave16ToFloatProvider(sound.bufferedWaveProvider);
                Pitch pitch = new Pitch(stream);

                byte[] buffer = new byte[8192];
                int bytesRead;


                bytesRead = stream.Read(buffer, 0, buffer.Length);
                freq = pitch.Get(buffer);

                if (freq != 0)
                {
                    ReturnFreq();
                }
            });
        }
        string lastNote = "None";
        private void ReturnFreq()
        {
            this.Dispatcher.BeginInvoke((ThreadStart)delegate ()
            {
                if (manualNumber == -1)
                {
                    noteLabel.Content = GetNote(freq);
                    lastNote = noteLabel.Content.ToString();
                    freqLabel.Content = $"{freq:0.00}";
                }
                //В разработке
                //else
                //{
                //    noteLabel.Content = GetNote(manualNumber,freq);
                //    lastNote = noteLabel.Content.ToString();
                //    freqLabel.Content = $"{freq:0.00}";
                //}
            });


        }
        int lastind = 0;
        public string GetNote(double freq)
        {
            indicat[lastind].Opacity = 0.5;
            int curNote = 0;

            for (int i = 0; i < frames.Length; i++)
            {
                if ((freq >= frames[i][0]) && (freq >= frames[i][0]))
                {
                    curNote = i;
                    lastNote = notesName[i];

                }
            }

            if ((frames[curNote][1] - 5 < freq) && (frames[curNote][1] + 5 > freq))
            {
                indicat[5].Opacity = 1;
                lastind = 5;
            }
            else
            {
                double step = 0;
                if (freq < (frames[curNote][1] - 5))
                {
                    step = (frames[curNote][1] - frames[curNote][0]) / 5;
                    double curFreq = frames[curNote][0];
                    int ind = 0;

                    for (int j = 0; j < 5; j++)
                    {
                        if ((freq >= curFreq) && (freq <= curFreq + step))
                        {
                            indicat[ind].Opacity = 1;
                            lastind = ind;
                        }
                        else
                        {
                            ind++;
                            curFreq += step;
                        }
                    }
                }
                else if (freq > (frames[curNote][1] + 5))
                {
                    step = (frames[curNote][2] - frames[curNote][1]) / 5;
                    double curFreq = frames[curNote][2];
                    int ind = 10;
                    for (int j = 0; j < 5; j++)
                    {
                        if ((freq <= curFreq) && (freq >= curFreq - step))
                        {
                            indicat[ind].Opacity = 1;
                            lastind = ind;
                        }
                        else
                        {
                            ind--;
                            curFreq -= step;
                        }
                    }
                }
            }
            return lastNote;
        }



        private void StartFrame(object sender, EventArgs e)
        {
            Dispatcher.BeginInvoke((ThreadStart)delegate ()
            {
                StartDetect(inputDevice);
            });
        }

        private void startRecord_Click(object sender, RoutedEventArgs e)
        {
            if (!recordStatus)
            {
                startRecord.Content = "Выключть";
                recordStatus = true;
                timerFrame.Tick += new EventHandler(StartFrame);
                timerFrame.Interval = new TimeSpan(0, 0, 0, 0, 300);
                timerFrame.Start();
            }
            else
            {
                startRecord.Content = "Включить";
                recordStatus = false;
                waveIn.StopRecording();
                waveIn.Dispose();
                timerFrame.Stop();
            }
        }

        //В разработке
        //private void AutoMode_MouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    if (chromaticMode)
        //    {
        //        string imagePath = $@"../../Resource/Pictures/Vkl.png";
        //        Uri imageUri = new Uri(imagePath, UriKind.RelativeOrAbsolute);
        //        AutoMode.Source = new BitmapImage(imageUri);
        //        chromaticMode = false;
        //        recordStatus = true;
        //        startRecord_Click(sender, e);
        //    }
        //    else
        //    {
        //        string imagePath = $@"../../Resource/Pictures/Vikl.png";
        //        Uri imageUri = new Uri(imagePath, UriKind.RelativeOrAbsolute);
        //        AutoMode.Source = new BitmapImage(imageUri);
        //        chromaticMode = true;
        //        recordStatus = true;
        //        startRecord_Click(sender, e);
        //        manualNumber = -1;
        //    }
        //}

        //private string GetNote(int stringNumber,double freq)
        //{
        //    indicat[lastind].Opacity = 0.5;
        //    int curNote = stringNumber;
        //    if ((frames[curNote][1] - 5 < freq) && (frames[curNote][1] + 5 > freq))
        //    {
        //        indicat[5].Opacity = 1;
        //        lastind = 5;
        //    }
        //    else
        //    {
        //        double step = 0;
        //        if (freq < (frames[curNote][1] - 5))
        //        {
        //            step = (frames[curNote][1] - frames[curNote][0]) / 5;
        //            double curFreq = frames[curNote][0];
        //            int ind = 0;

        //            for (int j = 0; j < 5; j++)
        //            {
        //                if ((freq >= curFreq) && (freq <= curFreq + step))
        //                {
        //                    indicat[ind].Opacity = 1;
        //                    lastind = ind;
        //                }
        //                else
        //                {
        //                    ind++;
        //                    curFreq += step;
        //                }
        //            }
        //        }
        //        else if (freq > (frames[curNote][1] + 5))
        //        {
        //            step = (frames[curNote][2] - frames[curNote][1]) / 5;
        //            double curFreq = frames[curNote][2];
        //            int ind = 10;
        //            for (int j = 0; j < 5; j++)
        //            {
        //                if ((freq <= curFreq) && (freq >= curFreq - step))
        //                {
        //                    indicat[ind].Opacity = 1;
        //                    lastind = ind;
        //                }
        //                else
        //                {
        //                    ind--;
        //                    curFreq -= step;
        //                }
        //            }
        //        }
        //    }
        //    return notesName[stringNumber];
        //}

        //private void firstString_Click(object sender, RoutedEventArgs e)
        //{
        //    manualNumber = 5;
        //    chromaticMode = false;
        //    AutoMode_MouseDown(sender, (MouseButtonEventArgs)e);
        //}

        //private void secondString_Click(object sender, RoutedEventArgs e)
        //{
        //    manualNumber = 4;
        //    chromaticMode = false;
        //    AutoMode_MouseDown(sender, (MouseButtonEventArgs)e);
        //}

        //private void thirdString_Click(object sender, RoutedEventArgs e)
        //{
        //    manualNumber = 3;
        //    chromaticMode = false;
        //    AutoMode_MouseDown(sender, (MouseButtonEventArgs)e);
        //}

        //private void sixString_Click(object sender, RoutedEventArgs e)
        //{
        //    manualNumber = 0;
        //    chromaticMode = false;
        //    AutoMode_MouseDown(sender, (MouseButtonEventArgs)e);
        //}

        //private void fiveString_Click(object sender, RoutedEventArgs e)
        //{
        //    manualNumber = 1;
        //    chromaticMode = false;
        //    AutoMode_MouseDown(sender, (MouseButtonEventArgs)e);
        //}

        //private void fourString_Click(object sender, RoutedEventArgs e)
        //{
        //    manualNumber = 2;
        //    chromaticMode = false;
        //    AutoMode_MouseDown(sender, (MouseButtonEventArgs)e);
        //}
    }
}
