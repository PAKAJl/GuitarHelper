using System.Windows;
using System.Windows.Controls;

namespace CourseWork.Resource.Pages.Lessons.Lesson5
{
    /// <summary>
    /// Логика взаимодействия для Lesson5.xaml
    /// </summary>
    public partial class Lesson5 : Page
    {
        MainWindow mainWindow;
        public Lesson5(MainWindow mainWindow)
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
