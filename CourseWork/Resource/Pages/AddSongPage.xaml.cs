using CourseWork.DataBase;
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
using System.Windows.Shapes;

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
    }
}
