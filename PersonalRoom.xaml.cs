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
using Microsoft.Data.Sqlite;
using System.IO;
using System.Resources;
using System.Collections;
using System.Drawing;
using System.Globalization;
using Microsoft.Win32;
namespace CoworkingMap
{
    /// <summary>
    /// Логика взаимодействия для PersonalRoom.xaml
    /// </summary>
    public partial class PersonalRoom : Page
    {
        public PersonalRoom()
        {
            InitializeComponent();
            //MessageBox.Show(MainPage.User.login);
            List<string> UserList = new List<string>();
            using (var connection = new SqliteConnection("Data Source=Users.db"))//нужно проверять текущего пользователя
            {
                string sqlExpression = "INSERT INTO Users (FIO,telephone,email,booking1,booking2,number) VALUES (@FIO,@telephone,@email,@booking1,@booking2,@number)";//тут данные пользователя
                connection.Open();
                SqliteCommand command = new SqliteCommand(sqlExpression, connection);
                command.CommandText = $"SELECT*FROM Users WHERE id = {WorkPlace.userid}";
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string fio = reader.GetString(3);
                            string telephone = reader.GetString(4);
                            string email = reader.GetString(5);
                            string booking1 = reader.GetString(6);
                            string booking2 = reader.GetString(7);;
                            int number = reader.GetInt32(8);
                            UserList.Add("ФИО: "+fio);
                            UserList.Add("Телефон: "+telephone);
                            UserList.Add("Email:" + email);
                            UserList.Add("Продолжительность брони: " + booking1 + " - " + booking2);
                            UserList.Add("Номер места: " + number.ToString());
                        }
                    }
                }
            }
            listUser.ItemsSource = UserList;

            List<Image> images = new List<Image>();//тут загрузка аватарки 
            string sql = $"SELECT * FROM Users WHERE id={WorkPlace.userid}";
            using (var connection = new SqliteConnection("Data Source=Users.db"))//сначала из бд в файл 
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(sql, connection);
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string filename = "C:\\Users\\User\\source\\repos\\coworking\\images\\"+reader.GetString(9);
                            string title = reader.GetString(10);
                            byte[] data = (byte[])reader.GetValue(11);
                            Image image = new Image(id, filename, title, data);
                            images.Add(image);
                        }
                    }
                }
                if (images.Count > 0)
                {
                    if(!File.Exists(images[0].FileName))
                    { 
                    using (FileStream fs = new FileStream(images[0].FileName, FileMode.OpenOrCreate))
                    {
                        
                         fs.Write(images[0].Data, 0, images[0].Data.Length);                      
                    } 
                    }
                    Photo.Source = new BitmapImage(new Uri(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, images[0].FileName)));//потом из файла в рамку
                }
            }
        }
        public class Image
        {
            public Image(int id, string filename, string title, byte[] data)
            {
                Id = id;
                FileName = filename;
                Title = title;
                Data = data;
            }
            public int Id { get; private set; }
            public string FileName { get; private set; }
            public string Title { get; private set; }
            public byte[] Data { get; private set; }
        }
        private void main(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainPage());
        }

        private void Maintoroom(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PersonalRoom());
        }

        private void Stats(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Stats());
        }

        private void History(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new History());
        }

        private void contacts(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Contacts());
        }

        private void FioUsers_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
