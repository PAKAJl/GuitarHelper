using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Threading;
using System.IO;
using System.Threading.Tasks;

namespace CourseWork
{
    public class Sound
    {
        public BufferedWaveProvider bufferedWaveProvider = null;
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

       
        public void WaveIn_DataAvailable(object sender, WaveInEventArgs e)
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
