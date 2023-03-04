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
    /// Логика взаимодействия для History.xaml
    /// </summary>
    public partial class History : Page
    {           
        public History()
        {
            InitializeComponent();

            List<string> DateList = new List<string>();
            List<string> BookingList = new List<string>();
            List<int> PlaceList = new List<int>();
            using (var connection = new SqliteConnection("Data Source=History.db"))
            {
                connection.Open();
                string sql = "INSERT INTO History (Date, Booking,Place) VALUES (@Date, @Booking,@Place)";
                SqliteCommand command = new SqliteCommand(sql, connection);
                command.CommandText = $"SELECT*FROM History WHERE UserID={WorkPlace.userid}";
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {                          
                            string date = reader.GetString(1);
                            string booking = reader.GetString(2);
                            int place = reader.GetInt32(3);
                            DateList.Add(date);
                            BookingList.Add(booking);
                            PlaceList.Add(place);
                        }
                    }
                }
            }
            DateList.Reverse();
            BookingList.Reverse();
            PlaceList.Reverse();
            listOfDate.ItemsSource = DateList;
            listOfBooking.ItemsSource = BookingList;
            listOfPlace.ItemsSource = PlaceList;
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

        private void contacts(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Contacts());
        }

        private void Historyy(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new History());
        }
    }
}
