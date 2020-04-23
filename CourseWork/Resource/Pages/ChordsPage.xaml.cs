using System;
using System.Collections.Generic;
using System.Data.Entity;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CourseWork.DataBase;
using CourseWork;
using CourseWork.Resource.Lib.Classes;

namespace CourseWork.Resource.Pages
{
    /// <summary>
    /// Логика взаимодействия для ChordsPage.xaml
    /// </summary>
    public partial class ChordsPage : Page
    {
        private int tone = 0;
        private ConnectedClass connection;
        private Dictionary<string, string> songsList;
        public MainWindow mWindow;
        private Transporation trans = new Transporation();

        public ChordsPage(MainWindow mWindow)
        {
            InitializeComponent();
            this.mWindow = mWindow;
            connection = new ConnectedClass();
            connection.SelectSongs();
            songsList = connection.songsList;
            songListBox.ItemsSource = songsList.Keys;
        }

        private void songListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (songListBox.SelectedItem != null)
            {
                string songName = songListBox.SelectedItem.ToString();
                songText.Text = songsList[songName];
            }
        }

        private void delSongButton_Click(object sender, RoutedEventArgs e)
        {

            if (songListBox.SelectedItem != null)
            {
                string songName = songListBox.SelectedItem.ToString();
                connection.DeleteSong(songName);
                connection.SelectSongs();
                songsList = connection.songsList;
                songListBox.ItemsSource = songsList.Keys;
                songText.Text = "";
            }
            else
            {
                MessageBox.Show("Не выбрана песня");
            }


        }

        private void addSongButton_Click(object sender, RoutedEventArgs e)
        {
            mWindow.Frames.Navigate(new AddSongPage(mWindow));
        }

        private void addTone_Click(object sender, RoutedEventArgs e)
        {
            if ((tone < 8) && (tone >= 0))
            {
                tone++;
                toneLabel.Content = "+" + tone;
                songText.Text = trans.TransporateDown(songText.Text);
                return;
            }
            if (tone < -1)
            {
                tone++;
                toneLabel.Content = "-" + tone;
                songText.Text = trans.TransporateDown(songText.Text);
                return;

            }
            if (tone == -1)
            {
                tone++;
                toneLabel.Content = tone;
                songText.Text = trans.TransporateDown(songText.Text);
                return;
            }
        }

        private void minusTone_Click(object sender, RoutedEventArgs e)
        {

            if ((tone <= 8) && (tone >= 0))
            {
                tone--;
                toneLabel.Content = "+" + tone;
                songText.Text = trans.TransporateDown(songText.Text);
                return;
            }
            if (tone < 0)
            {
                tone--;
                toneLabel.Content = "-" + tone;
                songText.Text = trans.TransporateDown(songText.Text);
                return;
            }
            if (tone == 1)
            {
                tone--;
                toneLabel.Content = tone;
                songText.Text = trans.TransporateDown(songText.Text);
                return;
            }
        }


    }
}
