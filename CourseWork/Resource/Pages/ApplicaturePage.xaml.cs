using NAudio.Wave;
using System;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace CourseWork.Resource.Pages
{
    /// <summary>
    /// Логика взаимодействия для ApplicaturePage.xaml
    /// </summary>
    public partial class ApplicaturePage : Page
    {
        MainWindow mWindow;
        private string[] chordList = new string[] { "A", "Adiez", "B", "C", "Cdiez", "D", "Ddiez", "E", "F", "Fdiez", "G", "Gdiez" };
        private string[] chordList1 = new string[] { "A", "A#", "B", "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#" };
        private string[] typeList = new string[] { "", "m", "5", "7" };
        SoundPlayer chordSound = new SoundPlayer();
        WaveOut outputSound = new WaveOut();
        public ApplicaturePage(MainWindow mainWindow)
        {
            InitializeComponent();
            mWindow = mainWindow;
            typeChordList.SelectedIndex = 0;
            typeChordList.Focus();
            noteList.SelectedIndex = 0;
            noteList.Focus();
            string imagePath = "../Pictures/Aplicatures/A.jpg";
            Uri imageUri = new Uri(imagePath, UriKind.RelativeOrAbsolute);
            AplicatureImage.Source = new BitmapImage(imageUri);
            nameNoteLabel.Content = "A";
        }

        private void typeListDownButton_Click(object sender, RoutedEventArgs e)
        {
            typeChordList.SelectedIndex = typeChordList.SelectedIndex + 1;
            typeChordList.Focus();
            typeChordList.ScrollIntoView(typeChordList.SelectedItem);
        }

        private void noteListDownButton_Click(object sender, RoutedEventArgs e)
        {
            noteList.SelectedIndex = noteList.SelectedIndex + 1;
            noteList.Focus();
            noteList.ScrollIntoView(noteList.SelectedItem);
        }

        private void typeListUpButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                typeChordList.SelectedIndex = typeChordList.SelectedIndex - 1;
                typeChordList.Focus();
                typeChordList.ScrollIntoView(typeChordList.SelectedItem);
            }
            catch (ArgumentException)
            {
            }
        }

        private void noteListUpButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                noteList.SelectedIndex = noteList.SelectedIndex - 1;
                noteList.Focus();
                noteList.ScrollIntoView(noteList.SelectedItem);
            }
            catch (ArgumentException)
            {
            }

        }

        private void noteList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string noteName = $@"{chordList[noteList.SelectedIndex]}{typeList[typeChordList.SelectedIndex]}";
                nameNoteLabel.Content = $@"{chordList1[noteList.SelectedIndex]}{typeList[typeChordList.SelectedIndex]}";
                string imagePath = $@"../Pictures/Aplicatures/{noteName.Trim()}.jpg";
                Uri imageUri = new Uri(imagePath, UriKind.RelativeOrAbsolute);
                BitmapImage bitmap = new BitmapImage(imageUri);
                bitmap.CacheOption = BitmapCacheOption.None;
                AplicatureImage.Source = bitmap;
            }
            catch (IndexOutOfRangeException)
            {
            }
        }

        private void listenButton_Click(object sender, RoutedEventArgs e)
        {
            string noteName = $@"{chordList[noteList.SelectedIndex]}{typeList[typeChordList.SelectedIndex]}";
            chordSound = new SoundPlayer($@"../../Resource/Sounds/Chords/{noteName}.wav");
            chordSound.Play();
        }
    }
}
