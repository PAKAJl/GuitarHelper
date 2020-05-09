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

        private void signUpButton_Click(object sender, RoutedEventArgs e)
        {
            string recovery = "";
            bool success = connection.SingUp(loginTextBox.Text,passwordTextBox.Text,ref recovery);
            if (success)
            {
                recoveryLabel.Content = "Код для восстановления: "+recovery;
            }
        }
    }
}
