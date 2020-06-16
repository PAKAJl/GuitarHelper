using System.Windows;
using System.Windows.Controls;

namespace CourseWork.Resource.Pages.Lessons.Lesson2
{
    /// <summary>
    /// Логика взаимодействия для Lesson2.xaml
    /// </summary>
    public partial class Lesson2 : Page
    {
        MainWindow mainWindow;
        public Lesson2(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
        }
        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Frames.Navigate(new LessonsPage(mainWindow));
        }
    }
}
