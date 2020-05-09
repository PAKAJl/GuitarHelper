using CourseWork.DataBase;
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

namespace CourseWork.Resource.Pages
{
    /// <summary>
    /// Логика взаимодействия для RecoveryPage.xaml
    /// </summary>
    public partial class RecoveryPage : Page
    {
        MainWindow mWindow;
        public RecoveryPage(MainWindow mainWindow)
        {
            InitializeComponent();
            mWindow = mainWindow;
        }
        ConnectedClass connection = new ConnectedClass();
        private void recoverButton_Click(object sender, RoutedEventArgs e)
        {
            bool success = connection.RecoveryPass(loginBox.Text, recoveryBox.Text, passwordBox.Text);
            if (success)
            {
                mWindow.Frames.Navigate(new LoginPage(mWindow));
            }
        }
    }
}
