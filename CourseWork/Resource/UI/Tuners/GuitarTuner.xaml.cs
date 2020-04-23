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
using NAudio;
using NAudio.Wave;

namespace CourseWork.Resource.UI.Tuners
{
    /// <summary>
    /// Логика взаимодействия для GuitarTuner.xaml
    /// </summary>
    public partial class GuitarTuner : UserControl
    {
        private float freq;
        public WaveInEvent waveIn;
        Sound sound;
        bool recordStatus = false;
        private int inputDevice = 0;
        private DispatcherTimer timerFrame = new DispatcherTimer();
        

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

        public GuitarTuner()
        {
            InitializeComponent();
            sound = new Sound();
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
            this.Dispatcher.BeginInvoke((ThreadStart)delegate () {
                noteLabel.Content = GetNote(freq);
                lastNote = noteLabel.Content.ToString();
                freqLabel.Content = $"{freq:0.00}";
            });


        }

        public string GetNote(float freq)
        {
            double baseFreq;
            double[] frames = { 0,0 };
            for (int i = 0; i < notesFreq.Length; i++)
            {
                baseFreq = notesFreq[i];
                if ((i>=1)&& (i <= 4))
                {
                    frames = new double[] { ((notesFreq[i] - notesFreq[i - 1]) / 2) - notesFreq[i], (((notesFreq[i] - notesFreq[i + 1]) / 2) + notesFreq[i]) };
                }
                else if( i == 0)
                {
                    frames = new double[] { 0, ((notesFreq[i] - notesFreq[i + 1]) / 2) + notesFreq[i] };
                }
                else if(i == 5)
                {
                    frames = new double[]  { ((notesFreq[i] - notesFreq[i - 1]) / 2) - notesFreq[i], 9999 };
                }
               
                if ((freq > frames[0]) && (freq < frames[1]) || (freq == baseFreq))
                {
                    return notesName[i].ToString();
                }
            }

            return lastNote;
        }
                
        


        /* public string GetNote(float freq)
         {
             double baseFreq;

             foreach (var note in noteBaseFreqs)
             {
                 baseFreq = note.Value;

                 for (int i = 0; i < 9; i++)
                 {
                     if ((freq >= baseFreq - 0.5) && (freq < baseFreq + 0.485) || (freq == baseFreq))
                     {
                         return note.Key + i;
                     }

                     baseFreq *= 2;
                 }
             }

             return lastNote;
         }*/

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
