using System.Windows.Controls;
using System.Windows.Forms;
using NAudio.Wave;
using System.Windows.Media.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Threading;
using System;

namespace CourseWork.Resource.Pages
{
    /// <summary>
    /// Логика взаимодействия для RecorderPage.xaml
    /// </summary>
    public partial class RecorderPage : Page
    {

        [DllImport("winmm.dll", EntryPoint = "mciSendStringA", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        private static extern int record(string lpstrCommand, string lpstrReturnString, int uReturnLength, int hwndCallback);
        private MainWindow mWindow;
        private string filePath = "";
        private bool recordStarted = false;
        private DispatcherTimer timerFrame = new DispatcherTimer();
        public int recordTime = 0;

        public RecorderPage(MainWindow mainWindow)
        {
            InitializeComponent();
            mWindow = mainWindow;
            string imagePath = $"../../Resource/Pictures/Unrecord.png";
            Uri imageUri = new Uri(imagePath, UriKind.RelativeOrAbsolute);
            recordImage.ImageSource = new BitmapImage(imageUri);
            ScanSoundCards();
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

        private void UpdateRecordTimer(object sender, EventArgs e)
        {
            recordTime++;
            if (recordTime < 60)
            {
                if (recordTime < 10)
                {
                    secLabel.Content = "0" + recordTime.ToString();
                }
                else
                {
                    secLabel.Content = recordTime.ToString();
                }

            }
            else
            {
                secLabel.Content = "0";
                recordTime = 0;
                if (int.Parse(minLabel.Content.ToString()) < 10)
                {
                    minLabel.Content = $"0{int.Parse(minLabel.Content.ToString()) + 1}";
                }
                else
                {
                    minLabel.Content = $"{int.Parse(minLabel.Content.ToString()) + 1}";
                }
            }
        }


        private void recordButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (!recordStarted)
            {
                SaveFileDialog saveDialog = new SaveFileDialog();
                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = saveDialog.FileName;
                }
                string imagePath = $"../../Resource/Pictures/Record.png";
                Uri imageUri = new Uri(imagePath, UriKind.RelativeOrAbsolute);
                recordImage.ImageSource = new BitmapImage(imageUri);
                recordStarted = true;

                record("open new Type waveaudio Alias recsound", "", 0, 0);
                record("record recsound", "", 0, 0);
                recordButton.Content = "Закончить запись";
                timerFrame.Tick += new EventHandler(UpdateRecordTimer);
                timerFrame.Interval = new TimeSpan(0, 0, 0, 1);
                timerFrame.Start();
            }
            else
            {
                string imagePath = $"../../Resource/Pictures/Unrecord.png";
                Uri imageUri = new Uri(imagePath, UriKind.RelativeOrAbsolute);
                recordImage.ImageSource = new BitmapImage(imageUri);
                record($"save recsound {filePath}.wav", "", 0, 0);
                record("close recsound", "", 0, 0);
                timerFrame.Stop();
                recordTime = 0;
                secLabel.Content = "00";
                minLabel.Content = "00";
                recordButton.Content = "Начать запись";
                recordStarted = false;
            }
        }
    }
}
