using CourseWork.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        private MainWindow mWindow;
        private ConnectedClass connection = new ConnectedClass();

        public LoginPage(MainWindow mWindow)
        {
            InitializeComponent();
            this.mWindow = mWindow;
        }

        private void signUpButton_Click(object sender, RoutedEventArgs e)
        {
            mWindow.Frames.Navigate(new RegisterPage(mWindow));
        }

        private async void signInButton_Click(object sender, RoutedEventArgs e)
        {
            bool success = await connection.SignIn(loginTextBox.Text, passwordTextBox.Text);
            if (success)
            {
                mWindow.accountName.Content = loginTextBox.Text;
            }
        }
    }
}
