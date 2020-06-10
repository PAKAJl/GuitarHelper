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

        public async void LoadDB()
        {
            try
            {
                using (GuitarHelperDBContext context = new GuitarHelperDBContext())
                {
                    await context.Users.ForEachAsync(user => { });
                }
            }
            catch (Exception e)
            {

            }

        }

        public void SelectSongs()
        {
            songsList = new Dictionary<string, string>();
            try
            {
                using (GuitarHelperDBContext context = new GuitarHelperDBContext())
                {
                    foreach (var song in context.Songs)
                    {
                        songsList.Add(song.Name, song.Text);

                    }

                };
            }
            catch (Exception e)
            {

            }
            
            
        }

        public void DeleteSong(string name)
        {
            try
            {
                using (GuitarHelperDBContext context = new GuitarHelperDBContext())
                {
                    Songs song = context.Songs.Where(s => s.Name == name).FirstOrDefault();
                    context.Songs.Remove(song);
                    context.SaveChanges();
                }
                SelectSongs();
            }
            catch (Exception e)
            {

            }
            
        }

        public void AddSong(string name, string text)
        {
            try
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
            catch (Exception e)
            {

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
            try
            {
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
            }
            catch (Exception e)
            {

            }
           
            
            return result;
        }

        public void UpdateTimeInApp(int time, string login)
        {
            try
            {
                using (GuitarHelperDBContext context = new GuitarHelperDBContext())
                {
                    var userInfo = context.Users.SingleOrDefault(user => user.Login == login);
                    userInfo.TimeInApp += time;
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {

            }
            
        }

        public async Task<int> GetTimeInApp(string login)
        {
            int result = 0;
            try
            {
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
            }
            catch (Exception e)
            {

            }
            
            
            return result;
        }
        public string GetRecovery(string login)
        {
            try
            {
                using (GuitarHelperDBContext context = new GuitarHelperDBContext())
                {
                    var userInfo = context.Users.SingleOrDefault(user => user.Login == login);
                    return userInfo.RecoveryCode;
                }
            }
            catch (Exception e)
            {

            }
            return "";
        }

        public async Task<bool> SingUp(string login, string password)
        {
            bool result = true;
            try
            {
                Random rand = new Random();
                Users newUser = new Users();
                newUser.Login = login;
                newUser.Password = GetHash(password);
                newUser.Avatar = @"NoAvatar.png";
                newUser.TimeInApp = 0;
                newUser.FavoritesSongs = "";
                newUser.RecoveryCode = rand.Next(10000, 99999).ToString();
                using (GuitarHelperDBContext context = new GuitarHelperDBContext())
                {
                    foreach (var user in context.Users)
                    {
                        if (login == user.Login)
                        {
                            MessageBox.Show("Пользователь с таким именем уже сущесвует!");
                            result = false;
                        }
                    }
                    if (result)
                    {
                        context.Users.Add(newUser);
                        context.SaveChanges();
                        MessageBox.Show("Пользователь успешно зарегистрирован!");
                        return result;
                    }
                    else
                    {
                        return result;
                    }


                }
            }
            catch (Exception e)
            {

            }
            return result;
            
        }

        public string GetAvatar(string login)
        {
            try
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
            }
            catch (Exception e)
            {

            }
            
            return "";
        }

        public bool RecoveryPass(string login, string recoveryCode, string newPass)
        {
            bool result = false;
            try
            {
                using (GuitarHelperDBContext context = new GuitarHelperDBContext())
                {
                    var userInfo = context.Users.SingleOrDefault(user => user.Login == login);
                    if (recoveryCode == userInfo.RecoveryCode)
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
            }
            catch (Exception e)
            {

            }
            
            
            return result;
        }

        public void UpdateUserLogin(string oldName, string newName)
        {
            try
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
            catch (Exception e)
            {

            }
            
        }

        public void UpdateUserPass(string oldName, string newPass)
        {
            try
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
            catch (Exception e)
            {

            }
            
        }

        public void UpdateUserAvatar(string oldName, string newAvatar)
        {
            try
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
            catch (Exception e)
            {

            }
            
        }

        public string GetPass(string login)
        {
            try
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
            }
            catch (Exception e)
            {

            }
            
            return "";
        }

        public void AddInFavorite(string login, string nameSong)
        {
            try
            {
                using (GuitarHelperDBContext context = new GuitarHelperDBContext())
                {
                    var userInfo = context.Users.SingleOrDefault(user => user.Login == login);
                    var newSong = context.Songs.SingleOrDefault(song => song.Name == nameSong);
                    userInfo.FavoritesSongs += $"{newSong.SongID} ";
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {

            }
            
        }

        public void DeleteFromFavorite(string login, string nameSong)
        {
            try
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
            catch (Exception e)
            {

            }
            
        }

        public List<int> GetFavoriteSongList(string login)
        {
            int[] songsArray = new int[10];
            try
            {
                using (GuitarHelperDBContext context = new GuitarHelperDBContext())
                {
                    var userInfo = context.Users.SingleOrDefault(user => user.Login == login);
                    songsArray = userInfo.FavoritesSongs.Trim().Split(' ').Select(x => int.Parse(x)).ToArray();
                    
                }
            }
            catch (Exception e)
            {

            }
            return songsArray.ToList();
        }

        public Dictionary<string, string> SelectFavoriteSong(string login)
        {
            List<int> favoritesSongsId = GetFavoriteSongList(login);
            Dictionary<string, string> result = new Dictionary<string, string>();
            try
            {
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
            }
            catch (Exception e)
            {

            }
            
            return result;
        }

        public bool CheckOnFavorite(string login, string checkSong)
        {
            bool result = false;
            try
            {
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
            }
            catch (Exception e)
            {

            }
            return result;

        }
    }
}
