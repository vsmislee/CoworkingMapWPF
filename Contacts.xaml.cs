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
    /// Логика взаимодействия для Contacts.xaml
    /// </summary>
    public partial class Contacts : Page
    {
        public Contacts()
        {
            InitializeComponent();
            List<string> HRList = new List<string>();
            using (var connection = new SqliteConnection("Data Source=Users.db"))///подключение и чтение 
            {
                string sqlExpression = "INSERT INTO Users (FIO, email,telephone,status) VALUES (@FIO, @email,@telephone,@status)";
                connection.Open();
                SqliteCommand command = new SqliteCommand(sqlExpression, connection);
                command.CommandText = "SELECT*FROM Users";
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string status = reader.GetString(12);
                            if (status == "HR")
                            {
                                string HR_Info = "";
                                string fio = reader.GetString(3);
                                string telephone = reader.GetString(4);
                                string email = reader.GetString(5);
                                HR_Info = "HR-менеджер:" + "\n"+"ФИО: "+fio+"\n" + "Телефон: " + telephone + "\n" + "Email: " + email+"\n\n\n\n";
                                HRList.Add(HR_Info);
                            }
                        }
                    }
                }
            }
            listOfHR.ItemsSource = HRList;
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

        private void listOfBooking_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
