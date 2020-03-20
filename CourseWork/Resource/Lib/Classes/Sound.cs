using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Threading;

namespace CourseWork
{
    public class Sound
    {
        private MainWindow _window;

        public Sound(MainWindow window)
        {
            _window = window;
        }

        BufferedWaveProvider bufferedWaveProvider = null;
        Dictionary<string, float> noteBaseFreqs = new Dictionary<string, float>()
            {
                { "C", 16.35f },
                { "C#", 17.32f },
                { "D", 18.35f },
                { "Eb", 19.45f },
                { "E", 20.60f },
                { "F", 21.83f },
                { "F#", 23.12f },
                { "G", 24.50f },
                { "G#", 25.96f },
                { "A", 27.50f },
                { "Bb", 29.14f },
                { "B", 30.87f },
            };


        public void StartDetect(int inputDevice)
        {
           
                WaveInEvent waveIn = new WaveInEvent();

                waveIn.DeviceNumber = inputDevice;
                waveIn.WaveFormat = new WaveFormat(44100, 1);
                waveIn.DataAvailable += WaveIn_DataAvailable;

                bufferedWaveProvider = new BufferedWaveProvider(waveIn.WaveFormat);

                // begin record
                waveIn.StartRecording();

                IWaveProvider stream = new Wave16ToFloatProvider(bufferedWaveProvider);
                Pitch pitch = new Pitch(stream);

                byte[] buffer = new byte[8192];
                int bytesRead;

                do
                {
                    bytesRead = stream.Read(buffer, 0, buffer.Length);

                    float freq = pitch.Get(buffer);

                    if (freq != 0)
                    {
                    //вывод частоты
                    
                    }

                } while (bytesRead != 0);

                // stop recording
                waveIn.StopRecording();
                waveIn.Dispose();
        }

        void WaveIn_DataAvailable(object sender, WaveInEventArgs e)
        {
            if (bufferedWaveProvider != null)
            {
                bufferedWaveProvider.AddSamples(e.Buffer, 0, e.BytesRecorded);
                bufferedWaveProvider.DiscardOnBufferOverflow = true;
            }
        }

        public string GetNote(float freq)
        {
            float baseFreq;

            foreach (var note in noteBaseFreqs)
            {
                baseFreq = note.Value;

                for (int i = 0; i < 9; i++)
                {
                    if ((freq >= baseFreq - 1) && (freq < baseFreq + 1) || (freq == baseFreq))
                    {
                        return note.Key + i;
                    }

                    baseFreq *= 2;
                }
            }

            return null;
        }
    }
}
