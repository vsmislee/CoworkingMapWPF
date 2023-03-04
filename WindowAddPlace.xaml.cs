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
using System.Windows.Shapes;
using Microsoft.Data.Sqlite;
namespace CoworkingMap
{
    /// <summary>
    /// Логика взаимодействия для WindowAddPlace.xaml
    /// </summary>
    public partial class WindowAddPlace : Window
    {
        MainPage mainPage;
        Point mousePosition;
        public WindowAddPlace()
        {
            InitializeComponent();
        }

        public WindowAddPlace(MainPage mainPage, Point mousePosition)
        {
            InitializeComponent();
            this.mainPage = mainPage;
            this.mousePosition = mousePosition;
        }

        public void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            // место должно загружаться из базы, если нет места в базе, т е добавляем новое - по расположению мыши
            WorkPlace addPlace;
            int number;
            double marginUp;
            double marginLeft;
            number = int.Parse(TextBoxNumber.Text);
            bool check = true;
            using (var connection = new SqliteConnection("Data Source=Numbers.db"))///если место есть в базе
            {
            string sqlExpression = "INSERT INTO Numbers (place) VALUES (@place)";
                connection.Open();
                SqliteCommand command = new SqliteCommand(sqlExpression, connection);
                command.CommandText = "SELECT*FROM Numbers";
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                           int numberPlace= reader.GetInt32(1);
                            if (numberPlace == number)
                            {
                                MessageBox.Show("Такое место уже есть!");//выводим сообщение
                                check = false;
                                break;
                            }
                        }
                    }
                }
            }
            if(check==true) // если нет места в базе
            {
               
                marginUp = mousePosition.Y - 12;
                marginLeft = mousePosition.X - 14;
                
                using (var connection = new SqliteConnection("Data Source=Numbers.db"))
                { 
                    string sqlExpression = "INSERT INTO Numbers (place,MarginUp,MarginLeft,Width,Height) VALUES (@place,@MarginUp,@MarginLeft,@Width,@Height)";
                     connection.Open();
                     SqliteCommand command = new SqliteCommand(sqlExpression, connection);//добавляем
                     SqliteParameter sqliteParameter = new SqliteParameter("@place", number);
                    SqliteParameter sqliteParameter1 = new SqliteParameter("@MarginUp", marginUp);
                    SqliteParameter sqliteParameter2 = new SqliteParameter("@MarginLeft", marginLeft);
                    SqliteParameter sqliteParameter3 = new SqliteParameter("@Width", 19);
                    SqliteParameter sqliteParameter4 = new SqliteParameter("@Height", 19);
                    command.Parameters.Add(sqliteParameter);
                    command.Parameters.Add(sqliteParameter1);
                    command.Parameters.Add(sqliteParameter2);
                    command.Parameters.Add(sqliteParameter3);
                    command.Parameters.Add(sqliteParameter4);
                    command.ExecuteNonQuery();
                }

                addPlace = new WorkPlace(number, marginUp, marginLeft);
                mainPage.CreateImage(addPlace);
            }                        
            this.DialogResult = true;
        }
    }
}
