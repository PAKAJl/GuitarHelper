using System.Windows;
using System.Windows.Controls;

namespace CourseWork.Resource.Pages.Lessons.Lesson3
{
    /// <summary>
    /// Логика взаимодействия для Lesson3.xaml
    /// </summary>
    public partial class Lesson3 : Page
    {
        MainWindow mainWindow;
        public Lesson3(MainWindow mainWindow)
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
