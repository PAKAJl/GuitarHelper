using System.Windows;
using System.Windows.Controls;

namespace CourseWork.Resource.Pages
{
    /// <summary>
    /// Логика взаимодействия для LessonsPage.xaml
    /// </summary>
    public partial class LessonsPage : Page
    {
        MainWindow mainWindow;
        public LessonsPage(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
        }

        private void lesOneButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Frames.Navigate(new Lessons.Lesson1.Lesson1Page1(mainWindow));
        }

        private void lesTwoButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Frames.Navigate(new Lessons.Lesson2.Lesson2(mainWindow));
        }

        private void lesThreeButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Frames.Navigate(new Lessons.Lesson3.Lesson3(mainWindow));
        }

        private void lesFourButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Frames.Navigate(new Lessons.Lesson4.Lesson4(mainWindow));
        }

        private void lesFiveButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Frames.Navigate(new Lessons.Lesson5.Lesson5(mainWindow));
        }

        private void netButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Frames.Navigate(new Lessons.netResources.netResources(mainWindow));
        }
    }
}
