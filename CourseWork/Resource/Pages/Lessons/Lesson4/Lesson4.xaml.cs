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
using System.Windows.Navigation;
using System.Windows.Shapes;

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
