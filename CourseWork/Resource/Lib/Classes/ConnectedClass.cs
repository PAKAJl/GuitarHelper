using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Data.Entity;
using System.Windows.Forms;
using MessageBox = System.Windows.Forms.MessageBox;

namespace CourseWork.DataBase
{
    class ConnectedClass
    {
        public Dictionary<string, string> songsList;

        public void SelectSongs()
        {
            songsList = new Dictionary<string, string>();
            using (GuitarHelperDBContext context = new GuitarHelperDBContext())
            {
                foreach (var song in context.Songs)
                {
                    songsList.Add(song.Name, song.Text);

                }

            };
        }

        public void DeleteSong(string name)
        {
            using (GuitarHelperDBContext context = new GuitarHelperDBContext())
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
            using (GuitarHelperDBContext context = new GuitarHelperDBContext())
            {
                context.Songs.Add(newSong);
                context.SaveChanges();
            }
        }

        public string GetHash(string input)
        {
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(input));

            return Convert.ToBase64String(hash);
        }

        public async Task<bool> SignIn(string login, string password)
        {
            bool result = false;
            using (GuitarHelperDBContext context = new GuitarHelperDBContext())
            {
                await context.Users.ForEachAsync(user =>
                {
                    if ((user.Login == login) & (user.Password == GetHash(password)))
                    {

                        result = true;
                    }
                });
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

        public void UpdateTimeInApp(int time, string login)
        {
            using (GuitarHelperDBContext context = new GuitarHelperDBContext())
            {
                var userInfo = context.Users.SingleOrDefault(user => user.Login == login);
                userInfo.TimeInApp += time;
                context.SaveChanges();
            }
        }

        public async Task<int> GetTimeInApp(string login)
        {
            int result = 0;
            using (GuitarHelperDBContext context = new GuitarHelperDBContext())
            {
                await context.Users.ForEachAsync(user =>
                {
                    if (user.Login == login)
                    {
                        result = (int)user.TimeInApp;
                    }
                });
            }
            return result;
        }

        public bool SingUp(string login, string password, ref string recoveryCode)
        {
            Random rand = new Random();
            Users newUser = new Users();
            newUser.Login = login;
            newUser.Password = GetHash(password);
            newUser.Avatar = @"NoAvatar.png";
            newUser.TimeInApp = 0;
            newUser.FavoritesSongs = "";
            recoveryCode = rand.Next(10000, 99999).ToString();
            newUser.RecoveryCode = GetHash(recoveryCode.ToString());
            using (GuitarHelperDBContext context = new GuitarHelperDBContext())
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
            using (GuitarHelperDBContext context = new GuitarHelperDBContext())
            {
                foreach (var user in context.Users)
                {
                    if (login == user.Login)
                    {
                        return user.Avatar;
                    }
                }
            }
            return "";
        }

        public bool RecoveryPass(string login, string recoveryCode, string newPass)
        {
            bool result = false;
            using (GuitarHelperDBContext context = new GuitarHelperDBContext())
            {
                var userInfo = context.Users.SingleOrDefault(user => user.Login == login);
                if (GetHash(recoveryCode) == userInfo.RecoveryCode)
                {
                    MessageBox.Show("Пароль успешно изменен");
                    userInfo.Password = GetHash(newPass);
                }
                else
                {
                    result = false;
                    MessageBox.Show("Не верный кол воостановления, поворите попытку!");
                }
                context.SaveChanges();
            }
            return result;
        }

        public void UpdateUserLogin(string oldName, string newName)
        {
            using (GuitarHelperDBContext context = new GuitarHelperDBContext())
            {
                foreach (var user in context.Users)
                {
                    if (oldName == user.Login)
                    {
                        user.Login = newName;
                    }
                }
                context.SaveChanges();
            }
        }

        public void UpdateUserPass(string oldName, string newPass)
        {
            using (GuitarHelperDBContext context = new GuitarHelperDBContext())
            {
                foreach (var user in context.Users)
                {
                    if (oldName == user.Login)
                    {
                        user.Password = GetHash(newPass);
                    }
                }
                context.SaveChanges();
            }
        }

        public void UpdateUserAvatar(string oldName, string newAvatar)
        {
            using (GuitarHelperDBContext context = new GuitarHelperDBContext())
            {
                foreach (var user in context.Users)
                {
                    if (oldName == user.Login)
                    {
                        user.Avatar = newAvatar;
                    }
                }
                context.SaveChanges();
            }
        }

        public string GetPass(string login)
        {
            using (GuitarHelperDBContext context = new GuitarHelperDBContext())
            {
                foreach (var user in context.Users)
                {
                    if (login == user.Login)
                    {
                        return user.Password;
                    }
                }
            }
            return "";
        }
        
        public void AddInFavorite(string login, string nameSong)
        {
            using (GuitarHelperDBContext context = new GuitarHelperDBContext())
            {
                var userInfo = context.Users.SingleOrDefault(user => user.Login == login);
                var newSong = context.Songs.SingleOrDefault(song => song.Name == nameSong);
                userInfo.FavoritesSongs += $"{newSong.SongID} ";
                context.SaveChanges();
            }
        }

        public void DeleteFromFavorite(string login, string nameSong)
        {
            using (GuitarHelperDBContext context = new GuitarHelperDBContext())
            {
                var userInfo = context.Users.SingleOrDefault(user => user.Login == login);
                var curentSong = context.Songs.SingleOrDefault(song => song.Name == nameSong);
                List<int> favoritesSongs = GetFavoriteSongList(login);
                favoritesSongs.Remove(curentSong.SongID);
                string favInDB = "";
                foreach (var songId in favoritesSongs)
                {
                    favInDB += songId + " ";
                }
                userInfo.FavoritesSongs = favInDB;
                context.SaveChanges();
            }
        }

        public List<int> GetFavoriteSongList(string login)
        {
            using (GuitarHelperDBContext context = new GuitarHelperDBContext())
            {
                var userInfo = context.Users.SingleOrDefault(user => user.Login == login);
                int[] songsArray = userInfo.FavoritesSongs.Trim().Split(' ').Select(x => int.Parse(x)).ToArray();
                return songsArray.ToList();
            }
        }

        public Dictionary<string,string> SelectFavoriteSong(string login)
        {
            List<int> favoritesSongsId = GetFavoriteSongList(login);
            Dictionary<string, string> result = new Dictionary<string, string>();
            using (GuitarHelperDBContext context = new GuitarHelperDBContext())
            {
                foreach (var songId in favoritesSongsId)
                {
                    foreach (var song in context.Songs)
                    {
                        if (song.SongID == songId)
                        {
                            result.Add(song.Name, song.Text);
                        }
                    }
                }
               
            }
            return result;
        }

        public bool CheckOnFavorite(string login, string checkSong)
        {
            bool result = false;
            using (GuitarHelperDBContext context = new GuitarHelperDBContext())
            {
                var userInfo = context.Users.SingleOrDefault(user => user.Login == login);
                var curentSong = context.Songs.SingleOrDefault(song => song.Name == checkSong);
                List<int> favoriteSongList = GetFavoriteSongList(login);
                foreach (var songId in favoriteSongList)
                {
                    if (curentSong.SongID == songId)
                    {
                        result = true;
                        break;
                    }
                }
            }
            return result;
        }
    }
}
