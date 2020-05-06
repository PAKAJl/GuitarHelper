using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Data.Entity;

namespace CourseWork.DataBase
{
    class ConnectedClass
    {
        public Dictionary<string, string> songsList;

        public void SelectSongs()
        {
            songsList = new Dictionary<string, string>();
            using (mainConnectedDB context = new mainConnectedDB())
            {
                foreach (var song in context.Songs)
                {
                    songsList.Add(song.Name, song.Text);

                }

            };
        }

        public void DeleteSong(string name)
        {
            using (mainConnectedDB context = new mainConnectedDB())
            {
                Songs song = context.Songs.Where(s => s.Name == name).FirstOrDefault();
                context.Songs.Remove(song);
                context.SaveChanges();
            }
            SelectSongs();
        }

        public void AddSong(string name, string text)
        {
            Songs newSong = new Songs();
            newSong.Name = name;
            newSong.Text = text;
            using (mainConnectedDB context = new mainConnectedDB())
            {
                context.Songs.Add(newSong);
                context.SaveChanges();
            }
        }

        private string GetHash(string input)
        {
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(input));

            return Convert.ToBase64String(hash);
        }

        public async Task<bool> SignIn(string login, string password)
        {
            bool result = false;
            using (mainConnectedDB context = new mainConnectedDB())
            {
                await context.Users.ForEachAsync(user =>
                {
                    if ((user.Login == login) & (user.Password == GetHash(password)))
                    {

                        result = true;
                    } });
            }
            if (result)
            {
                MessageBox.Show("Вход выполнен успешно!");
            }
            else
            {
                MessageBox.Show("Неправильный логин или пароль!");
            }
            return result;
        }


        public bool SingUp(string login, string password)
        {
            Users newUser = new Users();
            newUser.Login = login;
            newUser.Password = GetHash(password);
            newUser.Avatar = @"\\Resource\\Pictures\\noavatar.png";
            using (mainConnectedDB context = new mainConnectedDB())
            {
                foreach (var user in context.Users)
                {
                    if (login == user.Login)
                    {
                        MessageBox.Show("Пользователь с таким именем уже сущесвует!");
                        return false;
                    }
                }
                context.Users.Add(newUser);
                context.SaveChanges();
                MessageBox.Show("Пользователь успешно зарегистрирован!");
                return true;

            }
        }

        public string GetAvatar(string login)
        {
            using (mainConnectedDB context = new mainConnectedDB())
            {
                foreach (var user in context.Users)
                {
                    if (login == user.Login)
                    {
                        return user.Avatar;
                    }
                }
            }
            return @"\\Resource\\Pictures\\noavatar.png";
        }
    }
}
