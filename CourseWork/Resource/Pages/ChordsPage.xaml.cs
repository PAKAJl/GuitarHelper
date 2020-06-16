using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using CourseWork.DataBase;
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
        private Dictionary<string, string> songsFavotiteList;
        bool currentSongIsFavorite;

        public ChordsPage(MainWindow mWindow)
        {
            InitializeComponent();
            this.mWindow = mWindow;
            connection = new ConnectedClass();
            connection.SelectSongs();
            songsList = connection.songsList;
            songListBox.ItemsSource = songsList.Keys;
            string imagePath = $"../Pictures/Unfavorite.png";
            Uri imageUri = new Uri(imagePath, UriKind.RelativeOrAbsolute);
            favoriteImage.Source = new BitmapImage(imageUri);
            currentSongIsFavorite = false;
        }

        private void songListBox_SelectionChanged(object кsender, SelectionChangedEventArgs e)
        {
            tone = 0;
            toneLabel.Content = "0";
            if (songListBox.SelectedItem != null)
            {
                string songName = songListBox.SelectedItem.ToString();
                songText.Text = songsList[songName];
            }
            if (mWindow.accountName.Content.ToString() != "Гость")
            {
                if (songListBox.SelectedItem != null)
                {
                    if (connection.CheckOnFavorite(mWindow.accountName.Content.ToString(), songListBox.SelectedItem.ToString()))
                    {
                        string imagePath = $"../Pictures/Favorite.png";
                        Uri imageUri = new Uri(imagePath, UriKind.RelativeOrAbsolute);
                        favoriteImage.Source = new BitmapImage(imageUri);
                        currentSongIsFavorite = true;
                    }
                    else
                    {
                        string imagePath = $"../Pictures/Unfavorite.png";
                        Uri imageUri = new Uri(imagePath, UriKind.RelativeOrAbsolute);
                        favoriteImage.Source = new BitmapImage(imageUri);
                        currentSongIsFavorite = false;
                    }
                }  
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
                songText.Text = trans.TransporateUp(songText.Text);
                return;
            }
            if (tone < -1)
            {
                tone++;
                toneLabel.Content = tone;
                songText.Text = trans.TransporateUp(songText.Text);
                return;

            }
            if (tone == -1)
            {
                tone++;
                toneLabel.Content = tone;
                songText.Text = trans.TransporateUp(songText.Text);
                return;
            }
        }

        private void minusTone_Click(object sender, RoutedEventArgs e)
        {
            if (tone == 1)
            {
                tone--;
                toneLabel.Content = tone;
                songText.Text = trans.TransporateDown(songText.Text);
                return;
            }
            if ((tone >= 2) && (tone <= 8))
            {
                tone--;
                toneLabel.Content = "+" + tone;
                songText.Text = trans.TransporateDown(songText.Text);
                return;
            }
            if ((tone <= 0) && (tone > -8))
            {
                tone--;
                toneLabel.Content = tone;
                songText.Text = trans.TransporateDown(songText.Text);
                return;
            }
        }

        private void favoriteImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (mWindow.accountName.Content.ToString() != "Гость")
            {
                if (songListBox.SelectedItem == null)
                {
                    MessageBox.Show("Для действия выберите песню");
                    return;
                }
                else
                {
                    if (!currentSongIsFavorite)
                    {
                        connection.AddInFavorite(mWindow.accountName.Content.ToString(), songListBox.SelectedItem.ToString());
                        currentSongIsFavorite = true;
                        string imagePath = $"../Pictures/Favorite.png";
                        Uri imageUri = new Uri(imagePath, UriKind.RelativeOrAbsolute);
                        favoriteImage.Source = new BitmapImage(imageUri);
                    }
                    else
                    {
                        connection.DeleteFromFavorite(mWindow.accountName.Content.ToString(), songListBox.SelectedItem.ToString());
                        currentSongIsFavorite = false;
                        string imagePath = $"../Pictures/Unfavorite.png";
                        Uri imageUri = new Uri(imagePath, UriKind.RelativeOrAbsolute);
                        favoriteImage.Source = new BitmapImage(imageUri);
                    }
                }
            }
            else
            {
                MessageBox.Show("Для действия войдите в аккаунт!");
            }
        }

        private void favoriteButton_Click(object sender, RoutedEventArgs e)
        {
            if (mWindow.accountName.Content.ToString() != "Гость")
            {
                if (favoriteButton.Content.ToString() == "Избранное")
                {
                    favoriteButton.Content = "Все песни";
                    songsFavotiteList = connection.SelectFavoriteSong(mWindow.accountName.Content.ToString());
                    songListBox.ItemsSource = songsFavotiteList.Keys;
                }
                else
                {
                    favoriteButton.Content = "Избранное";
                    songListBox.ItemsSource = songsList.Keys;
                }
            }
            else
            {
                MessageBox.Show("Для действия войдите в аккаунт!");
            }

        }
    }
}
