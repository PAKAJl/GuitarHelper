using System.Windows;
using System.Windows.Controls;

namespace CourseWork.Resource.Pages.Lessons.Lesson4
{
    /// <summary>
    /// Логика взаимодействия для Lesson4.xaml
    /// </summary>
    public partial class Lesson4 : Page
    {
        MainWindow mainWindow;
        public Lesson4(MainWindow mainWindow)
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
