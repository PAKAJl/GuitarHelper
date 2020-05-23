using NAudio.Wave;
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
        private DispatcherTimer timerFrame = new DispatcherTimer();
        Border[] indicat = new Border[11];


        Dictionary<string, double> noteBaseFreqs = new Dictionary<string, double>()
            {
                { "E4", 329.63 },
                { "B", 246.94 },
                { "G", 196},
                { "D", 146.83 },
                { "A", 110 },
                { "E2", 82.41},
            };

        double[] notesFreq = { 82.41, 110, 146.83, 196, 246.94, 329.63 };
        string[] notesName = { "E2", "A", "D", "G", "B", "E4" };

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
                        Background = Brushes.Red,
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
                        Background = Brushes.Red,
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
                            Background = Brushes.OrangeRed,
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
                            Background = Brushes.Orange
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
                            Background = Brushes.Yellow
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
                            Background = Brushes.YellowGreen
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
                            Background = Brushes.Lime
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
                noteLabel.Content = GetNote(freq);
                lastNote = noteLabel.Content.ToString();
                freqLabel.Content = $"{freq:0.00}";
            });


        }

        private void ClearIndicators(Border[] ind)
        {
            for (int i = 0; i < ind.Length; i++)
            {
                ind[i].Opacity = 0.5;
            }
        }

        public string GetNote(double freq)
        {
            ClearIndicators(indicat);
            double baseFreq;
            double[] frames = { 0, 0 };
            for (int i = 0; i < notesFreq.Length; i++)
            {
                baseFreq = notesFreq[i];
                if ((i >= 1) && (i <= 4))
                {
                    frames = new double[] { ((notesFreq[i] - notesFreq[i - 1]) / 2) - notesFreq[i], (((notesFreq[i] - notesFreq[i + 1]) / 2) + notesFreq[i]) };
                }
                else if (i == 0)
                {
                    frames = new double[] { 0, ((notesFreq[i] - notesFreq[i + 1]) / 2) + notesFreq[i] };
                }
                else if (i == 5)
                {
                    frames = new double[] { ((notesFreq[i] - notesFreq[i - 1]) / 2) - notesFreq[i], 9999 };
                }

                if ((freq > frames[0]) && (freq < frames[1]) || (freq == baseFreq))
                {
                   
                    double step = (frames[1] - frames[0]) / 11;
                    double[][] indicFrames = new double[11][];
                    double stepSum = frames[0];
                    for (int j = 0; j < indicFrames.Length; j++)
                    {
                        indicFrames[j] = new double[2] { stepSum, stepSum + step };
                        stepSum += step;

                    }
                    for (int j = 0; j < indicat.Length; j++)
                    {
                        if ((freq >= indicFrames[j][0]) && (freq <= indicFrames[j][1]))
                        {
                            indicat[j].Opacity = 1;

                        }
                    }
                    return notesName[i - 1].ToString();
                }
            }
           
            return lastNote;
        }


        private void StartFrame(object sender, EventArgs e)
        {
            this.Dispatcher.BeginInvoke((ThreadStart)delegate ()
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
                timerFrame.Interval = new TimeSpan(0, 0, 0, 0, 150);
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
    }
}
