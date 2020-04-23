using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace CourseWork.Resource.Pages
{
    /// <summary>
    /// Логика взаимодействия для TunnerPage.xaml
    /// </summary>
    public partial class TunnerPage : Page
    {

        public TunnerPage()
        {
            InitializeComponent();
            ScanSoundCards();
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
    }
}
