using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using CourseWork.DataBase;

namespace CourseWork.Resource.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Page
    {
        private MainWindow mainWindow;
        private ConnectedClass connection = new ConnectedClass();
        private int[] numbersAvatars = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };

        public ProfilePage(MainWindow mainWindow)
        {
            InitializeComponent();
            newAvatarList.Visibility = Visibility.Hidden;
            ListUpButton.Visibility = Visibility.Hidden;
            ListDownButton.Visibility = Visibility.Hidden;
            CancelButton.Visibility = Visibility.Hidden;
            newNameBox.Visibility = Visibility.Hidden;
            newNameLabel.Visibility = Visibility.Hidden;
            newPassBox.Visibility = Visibility.Hidden;
            newPassLabel.Visibility = Visibility.Hidden;
            oldPassBox.Visibility = Visibility.Hidden;
            oldPassLabel.Visibility = Visibility.Hidden;
            this.mainWindow = mainWindow;
            changedLogOrAvatar = false;
            string imagePath = "../../Resource/Pictures/Avatars/" + connection.GetAvatar(mainWindow.accountName.Content.ToString());
            Uri imageUri = new Uri(imagePath, UriKind.RelativeOrAbsolute);
            avatarImage.ImageSource = new BitmapImage(imageUri);
            accountName.Content = mainWindow.accountName.Content.ToString();
            var listSongs = connection.GetFavoriteSongList(mainWindow.accountName.Content.ToString());
            int countList = listSongs != null && listSongs.Count > 0 ? listSongs.Count : 0;
            countFavorite.Content = $"Песен в Избранном: { countList }";
        }

        public async Task<bool> TimeInLabel()
        {
            double timeInApp = await connection.GetTimeInApp(accountName.Content.ToString());
            secInApp.Content = timeInApp.ToString() + " секунд";
            minInApp.Content = $"{(timeInApp / 60):0.00} минут";
            hourInApp.Content = $"{(timeInApp / 3600):0.00} часов";
            return true;
        }

        private async void secInApp_Loaded(object sender, RoutedEventArgs e)
        {
            while (secInApp.Content.ToString() != "Нет данных")
            {
                await TimeInLabel();
                if (secInApp.Content.ToString() != "Нет данных")
                {
                    break;
                }
            }
        }
        private bool CheckOnEqual(string oldPass, string newPass)
        {
            if (oldPass == newPass)
                return true;
            else
                return false;
        }

        bool mode = false;
        bool changedLogOrAvatar = false;
        private void editProfileButton_Click(object sender, RoutedEventArgs e)
        {
            if (accountName.Content.ToString() == "Гость")
            {
                MessageBox.Show("Для выполнения этого действия войдите в аккаунт");
                return;
            }
            if (editProfileButton.Content.ToString() == "Редактировать профиль")
            {
                mode = false;
            }
            if (editProfileButton.Content.ToString() == "Сохранить изменения")
            {
                mode = true;
            }
            if (!mode)
            {
                newAvatarList.Visibility = Visibility.Visible;
                ListUpButton.Visibility = Visibility.Visible;
                ListDownButton.Visibility = Visibility.Visible;
                CancelButton.Visibility = Visibility.Visible;
                newNameBox.Visibility = Visibility.Visible;
                newNameLabel.Visibility = Visibility.Visible;
                newPassBox.Visibility = Visibility.Visible;
                newPassLabel.Visibility = Visibility.Visible;
                oldPassBox.Visibility = Visibility.Visible;
                oldPassLabel.Visibility = Visibility.Visible;
                editProfileButton.Content = "Сохранить изменения";
            }
            if (mode)
            {
                if (!((oldPassBox.Password == "") && (newPassBox.Password == "")))
                {
                    if (((oldPassBox.Password == "") || (newPassBox.Password == "")))
                    {
                        MessageBox.Show("Поле для нового или старого пароля пусто!");
                        return;
                    }
                    else
                    {
                        if (CheckOnEqual(connection.GetHash(oldPassBox.Password), connection.GetPass(accountName.Content.ToString())))
                        {
                            connection.UpdateUserPass(accountName.Content.ToString(), newPassBox.Password);

                        }
                        else
                        {
                            MessageBox.Show("Старый пароль введен не верно");
                            return;
                        }
                    }
                }
                if (newNameBox.Text != "")
                {
                    connection.UpdateUserLogin(accountName.Content.ToString(), newNameBox.Text);
                    changedLogOrAvatar = true;
                    accountName.Content = newNameBox.Text;
                }
                if (newAvatarList.SelectedIndex != -1)
                {
                    connection.UpdateUserAvatar(accountName.Content.ToString(), $"{numbersAvatars[newAvatarList.SelectedIndex]}.png");
                    changedLogOrAvatar = true;
                }

                MessageBox.Show("Все возможные изменения применены");

                if (changedLogOrAvatar)
                {
                    mainWindow.accountName.Content = accountName.Content;
                    string imagePath = "../../Resource/Pictures/Avatars/" + connection.GetAvatar(accountName.Content.ToString());
                    Uri imageUri = new Uri(imagePath, UriKind.RelativeOrAbsolute);
                    mainWindow.avatarImage.ImageSource = new BitmapImage(imageUri);


                    imagePath = "../../Resource/Pictures/Avatars/" + connection.GetAvatar(mainWindow.accountName.Content.ToString());
                    imageUri = new Uri(imagePath, UriKind.RelativeOrAbsolute);
                    avatarImage.ImageSource = new BitmapImage(imageUri);
                }

                newAvatarList.Visibility = Visibility.Hidden;
                ListUpButton.Visibility = Visibility.Hidden;
                ListDownButton.Visibility = Visibility.Hidden;
                CancelButton.Visibility = Visibility.Hidden;
                newNameBox.Visibility = Visibility.Hidden;
                newNameLabel.Visibility = Visibility.Hidden;
                newPassBox.Visibility = Visibility.Hidden;
                newPassLabel.Visibility = Visibility.Hidden;
                oldPassBox.Visibility = Visibility.Hidden;
                oldPassLabel.Visibility = Visibility.Hidden;
                editProfileButton.Content = "Редактировать профиль";
            }

        }

        private void ListDownButton_Click(object sender, RoutedEventArgs e)
        {
            newAvatarList.SelectedIndex = newAvatarList.SelectedIndex + 1;
            newAvatarList.Focus();
            newAvatarList.ScrollIntoView(newAvatarList.SelectedItem);
        }

        private void ListUpButton_Click(object sender, RoutedEventArgs e)
        {
            newAvatarList.SelectedIndex = newAvatarList.SelectedIndex - 1;
            newAvatarList.Focus();
            newAvatarList.ScrollIntoView(newAvatarList.SelectedItem);
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            newAvatarList.Visibility = Visibility.Hidden;
            ListUpButton.Visibility = Visibility.Hidden;
            ListDownButton.Visibility = Visibility.Hidden;
            CancelButton.Visibility = Visibility.Hidden;
            newNameBox.Text = "";
            newNameBox.Visibility = Visibility.Hidden;
            newNameLabel.Visibility = Visibility.Hidden;
            newPassBox.Password = "";
            newPassBox.Visibility = Visibility.Hidden;
            newPassLabel.Visibility = Visibility.Hidden;
            oldPassBox.Password = "";
            oldPassBox.Visibility = Visibility.Hidden;
            oldPassLabel.Visibility = Visibility.Hidden;
            editProfileButton.Content = "Редактировать профиль";
        }
    }
}
