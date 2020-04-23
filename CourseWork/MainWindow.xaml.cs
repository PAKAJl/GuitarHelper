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
    }
}
