using CourseWork.DataBase;
using System.Windows;
using System.Windows.Controls;

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
