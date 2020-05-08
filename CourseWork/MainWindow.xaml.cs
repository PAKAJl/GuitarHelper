using CourseWork.DataBase;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace CourseWork
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static bool[] checkOnClick;
        private static Button[] buttonsList;
        private Sound sound = new Sound();
        private DispatcherTimer timerInApp = new DispatcherTimer();
        private int timeInSessoin = 0;
        private ConnectedClass connection = new ConnectedClass();

        public void TimerT()
        {
            timerInApp.Tick += new EventHandler(incTimer);
            timerInApp.Interval = new TimeSpan(0, 0, 0, 1);
            timerInApp.Start();
        }
        private void incTimer(object sender, EventArgs e)
        {
            timeInSessoin++;
        }
        public MainWindow()
        {
            InitializeComponent();
            
            checkOnClick = new bool[3];
            buttonsList = new Button[3];
            buttonsList[0] = TunnerButton;
            buttonsList[1] = LessonButton;
            buttonsList[2] = ChordsButton;
            for (int i = 0; i < checkOnClick.Length; i++)
            {
                checkOnClick[i] = false;
            }
            Frames.Navigate(new Resource.Pages.LoginPage(this));
            string imagePath = $"../../Resource/Pictures/Avatars/NoAvatar.png";
            Uri imageUri = new Uri(imagePath, UriKind.RelativeOrAbsolute);
            avatarImage.ImageSource = new BitmapImage(imageUri);
            
        }
        
        
        private static void ButtonAccsess(int numberButton)
        {
            for (int i = 0; i < checkOnClick.Length; i++)
            {
                checkOnClick[i] = false;
            }
            checkOnClick[numberButton] = true;
            for (int i = 0; i < buttonsList.Length; i++)
            {
                if (!checkOnClick[i])
                {
                    buttonsList[i].Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF323232"));
                }
                else
                {
                    buttonsList[i].Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#525252"));
                }
            }
        }

        private void TunnerButton_Click(object sender, RoutedEventArgs e)
        {
            ButtonAccsess(0);

            Frames.Navigate(new Resource.Pages.TunnerPage());
        }

        private void LessonButton_Click(object sender, RoutedEventArgs e)
        {
            ButtonAccsess(1);
            Frames.Navigate(new Resource.Pages.LessonsPage());
        }

        private void ChordsButton_Click(object sender, RoutedEventArgs e)
        {
            ButtonAccsess(2);
            Frames.Navigate(new Resource.Pages.ChordsPage(this));
        }

        private void toProfileButton_Click(object sender, RoutedEventArgs e)
        {
            Frames.Navigate(new Resource.Pages.ProfilePage(this));
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (accountName.Content.ToString() != "Гость")
            {
                connection.UpdateTimeInApp(timeInSessoin, accountName.Content.ToString());
            }
            timerInApp.Stop();
        }
    }
}
