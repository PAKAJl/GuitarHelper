using CourseWork.DataBase;
using System.Windows;
using System.Windows.Controls;

namespace CourseWork.Resource.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddSongPage.xaml
    /// </summary>
    public partial class AddSongPage :  Page
    {
        ConnectedClass connection;
        private MainWindow mWindow;

        public AddSongPage(MainWindow mainWindow)
        {
            mWindow = mainWindow;
            InitializeComponent();
            connection = new ConnectedClass();
        }

        private void addSongButton_Click(object sender, RoutedEventArgs e)
        {
            connection.AddSong(nameSong.Text,textSong.Text);
            mWindow.Frames.Navigate(new ChordsPage(mWindow));
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            mWindow.Frames.Navigate(new ChordsPage(mWindow));
        }
    }
}
