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
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Page
    {   

        public Authorization()
        {
            InitializeComponent(); 
        }
        public void AuthClick(object sender, RoutedEventArgs e)//пока нет вывода ошибки неверного ввода
        {
            // try
            // {
                bool CheckLogPass = false;
                using (var connection = new SqliteConnection("Data Source=Users.db"))
                {
                connection.Open();
                    string sqlExpression = "SELECT*FROM Users";
                    SqliteCommand command = new SqliteCommand(sqlExpression, connection);
                     //command.CommandText = "SELECT*FROM Users";
                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        string login = LoginBox.Text.Trim();
                        string password = PasswordBox.Password.Trim();
                       // int id = -1;
                      //  string l = "";
                       // string p = "";
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                int id = reader.GetInt32(0);
                               string  l = reader.GetString(1);
                                string p = reader.GetString(2);

                                if (login == l && password == p)
                                {
                                    MessageBox.Show("Вход успешный!");
                                    WorkPlace.userid = id;
                                    CheckLogPass = true;
                                    NavigationService.Navigate(new MainPage());
                                    CheckLogPass = true;
                                    break;
                                }                               
                            }                       
                        }
                    if (CheckLogPass == false)
                    {
                        MessageBox.Show("Ввод неверный!");
                    }                        
                    }
                }
                
        }    
          //  catch (Exception ex)
           // {
          ///      MessageBox.Show(ex.Message);
           // }
       // }
    }
}

