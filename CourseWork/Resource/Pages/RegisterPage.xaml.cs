using CourseWork.DataBase;
using System.Windows;
using System.Windows.Controls;

namespace CourseWork.Resource.Pages
{
    /// <summary>
    /// Логика взаимодействия для RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : Page
    {
        public MainWindow mWindow;
        private ConnectedClass connection = new ConnectedClass();

        public RegisterPage(MainWindow mWindow)
        {
            InitializeComponent();
            this.mWindow = mWindow;
        }

        private async void signUpButton_Click(object sender, RoutedEventArgs e)
        {
            if (loginTextBox.Text.Length < 4)
            {
                MessageBox.Show("Логин должен содержать не мение 4-х символов!");
                return;
            }
            foreach (var ch in loginTextBox.Text)
            {
                if ((ch >= '0' && ch <= '9') || (ch >= 'a' && ch <= 'z') || (ch >= 'A' && ch <= 'Z'))
                {
                }
                else
                {
                    MessageBox.Show("Логин может содержать только цифры и латиницу!");
                    return;
                }
            }
            if (passwordTextBox.Text.Length < 5)
            {
                MessageBox.Show("Пароль должен содержать не мение 5-и символов!");
                return;
            }
            foreach (var ch in passwordTextBox.Text)
            {
                if ((ch >= '0' && ch <= '9') || (ch >= 'a' && ch <= 'z') || (ch >= 'A' && ch <= 'Z'))
                {
                }
                else
                {
                    MessageBox.Show("Пароль может содержать только цифры и латиницу!");
                    return;
                }
            }

            var success = await connection.SingUp(loginTextBox.Text,passwordTextBox.Text);
            if (success)
            {
                recoveryLabel.Content = "Код для восстановления: "+connection.GetRecovery(loginTextBox.Text);
            }
        }  
    }
}
