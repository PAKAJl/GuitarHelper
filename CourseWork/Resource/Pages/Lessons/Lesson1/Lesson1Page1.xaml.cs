using System.Windows;
using System.Windows.Controls;

namespace CourseWork.Resource.Pages.Lessons.Lesson1
{
    /// <summary>
    /// Логика взаимодействия для Lesson1Page1.xaml
    /// </summary>
    public partial class Lesson1Page1 : Page
    {
        MainWindow mainWindow;
        public Lesson1Page1(MainWindow mainWindow)
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
