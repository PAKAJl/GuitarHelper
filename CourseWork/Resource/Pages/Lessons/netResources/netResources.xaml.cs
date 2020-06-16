using System.Windows;
using System.Windows.Controls;

namespace CourseWork.Resource.Pages.Lessons.netResources
{
    /// <summary>
    /// Логика взаимодействия для netResources.xaml
    /// </summary>
    public partial class netResources : Page
    {
        MainWindow mainWindow;
        public netResources(MainWindow mainWindow)
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
