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
using Microsoft.Data.Sqlite;
namespace CoworkingMap
{
    /// <summary>
    /// Логика взаимодействия для Stats.xaml
    /// </summary>
    public partial class Stats : Page
    {
        public Stats()
        {
            InitializeComponent();
            List<string> UsersList = new List<string>();
            List<string> EmailList = new List<string>();
            List<string> BookingList = new List<string>();
            List<string> NumberList = new List<string>();
            using (var connection = new SqliteConnection("Data Source=Users.db"))///подключение и чтение 
            {
                string sqlExpression = "INSERT INTO Users (FIO, email,booking1,booking2,number) VALUES (@FIO, @email,@booking1,@booking2,@number)";
                connection.Open();
                SqliteCommand command = new SqliteCommand(sqlExpression, connection);
                command.CommandText = "SELECT*FROM Users";
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string fio = reader.GetString(3);
                            UsersList.Add(fio);
                            string email = reader.GetString(5);
                            EmailList.Add(email);
                            string booking1 = reader.GetString(6);
                            string booking2 = reader.GetString(7);
                            BookingList.Add(booking1+" - "+booking2);
                            int number = reader.GetInt32(8);
                            NumberList.Add(number.ToString());                          
                        }
                    }
                }
                listOfUsers.ItemsSource = UsersList;///вывод статистики
                listOfEmail.ItemsSource = EmailList;
                listOfBooking.ItemsSource = BookingList;
                listOfNumbers.ItemsSource = NumberList;
            }
        }
            private void main(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainPage());
        }

        private void Maintoroom(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PersonalRoom());
        }


        private void History(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new History());
        }

        private void contacts(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Contacts());
        }

        private void Statss(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Stats());
        }
    }
}
