using System;
using System.Collections.Generic;
using System.Collections;
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
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        int size = 8;
        bool check;
        List<WorkPlace> Places; //думаю будет инициалиироваться из базы 
        public MainPage()///не доделал добавление мест из базы
        {
            check = false;
            Places = new List<WorkPlace>();
            
            InitializeComponent();
            using (var connection = new SqliteConnection("Data Source=Numbers.db"))
            {
                connection.Open();
                string sqlExpression = "SELECT*FROM Numbers";
                SqliteCommand command = new SqliteCommand(sqlExpression, connection);
                command.CommandText = "SELECT*FROM Numbers";
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            int numberPlace = reader.GetInt32(1);
                            int MarginUpPlace = reader.GetInt32(2);
                            int MarginLeftPlace = reader.GetInt32(3);
                            WorkPlace workPlace = new WorkPlace(numberPlace, MarginUpPlace, MarginLeftPlace);
                            Places.Add(workPlace);
                        }
                    }
                }
            }
            using (var connection = new SqliteConnection("Data Source=Users.db"))
            {
                connection.Open();
                string sqlExpression = "SELECT*FROM Users";
                SqliteCommand command = new SqliteCommand(sqlExpression, connection);
                command.CommandText = "SELECT*FROM Users";
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string booking1 = reader.GetString(6);
                            string booking2 = reader.GetString(7);
                            int number = reader.GetInt32(8);
                            string[] str = booking1.Split('.', ' ');
                            string[] str2 = booking2.Split('.',' ');
                            foreach (var item in Places)
                            {
                                if (number == item.Number)
                                { 
                                item.Take(new CalendarDateRange(new DateTime(int.Parse(str[2]), int.Parse(str[1]), int.Parse(str[0])), new DateTime(int.Parse(str2[2]), int.Parse(str2[1]), int.Parse(str2[0]))));
                                }
                            }                         
                        }
                    }
                }
            }
            check = true;
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
        public void TakePlace(WorkPlace place)
        {
            Window PlaceSelect = new WindowSelectPlace(place);
            PlaceSelect.Top = Mouse.GetPosition(this).Y;
            PlaceSelect.Left = Mouse.GetPosition(this).X;
            PlaceSelect.ShowDialog();
        }

        private void ImageMap_Loaded(object sender, RoutedEventArgs e) // тут нужно красиво сделать
        {
            if(check)
            { 
            for (int i =0; i<Places.Count;i++)
            {
                CreateImage(Places[i]);
            }
            }
        }

        private void ImagePlace_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Image im = (Image)e.Source;
                int placeNumber = int.Parse(im.Name.Last().ToString());
                foreach (var item in Places)
                {
                    if (placeNumber== item.Number)
                    { 
                    TakePlace(item);  
                    im.Source = item.ChooseImage();
                    }
                }              
              
            }
            catch (ArgumentOutOfRangeException ex) 
            {
                MessageBox.Show(ex.Message + " " + ex.ParamName);
            }
            //MessageBox.Show(e.Source.ToString());
        }

        private void MenuMapItem_Click(object sender, RoutedEventArgs e)
        {
            Point MousePos = new Point();
            MousePos = Mouse.GetPosition(MainGrid);
            Window PlaceAdd = new WindowAddPlace(this, MousePos);
            PlaceAdd.ShowDialog();
        }

        public void CreateImage(WorkPlace place)
        {
            Image im = new Image();
            im.Margin = new Thickness(place.MarginLeft, place.MarginUp, 0, 0);
            im.Width = place.Width;
            im.Height = place.Height;
            im.HorizontalAlignment = HorizontalAlignment.Left;
            im.VerticalAlignment = VerticalAlignment.Top;
            im.Source = place.ChooseImage();
            im.Cursor = Cursors.Hand;
            im.Name = "ImagePlace" + place.Number.ToString();
            im.MouseLeftButtonUp += ImagePlace_MouseLeftButtonUp;
            MakeContextMenu(im);

          /*  Places.Add(place);*/
            MainGrid.Children.Add(im);
        }

        private void MakeContextMenu(Image im)
        {
            ContextMenu contextMenu = new ContextMenu();
            //изменить
            //переместить
            MenuItem menuItem1 = new MenuItem();
            menuItem1.Header = "Изменить";
            MenuItem menuItem2 = new MenuItem();
            menuItem2.Header = "Удалить";
            menuItem2.Click += ImagePlaceContextMenuDeleteClick;
            contextMenu.Items.Add(menuItem1);
            contextMenu.Items.Add(menuItem2);
            im.ContextMenu = contextMenu;
        }

        public void ImagePlaceContextMenuDeleteClick(object sender, RoutedEventArgs e)
        {
            ContextMenu cm = (e.Source as MenuItem).Parent as ContextMenu;
            Image im = cm.PlacementTarget as Image;
            MainGrid.Children.Remove(im);
            int number = int.Parse(im.Name.Last().ToString());//надо по циклу сделать нормально
            int index=-1;
            foreach (var item in Places)
            {
                if (number== item.Number)
                {
                    index = Places.IndexOf(item);                   
                }
            }
           using (var connection = new SqliteConnection("Data Source=Numbers.db"))//удаление из базы
                      {
                      connection.Open();
                      string sqlExpression = $"DELETE FROM Numbers WHERE place={Places[index].Number}";
                       SqliteCommand command = new SqliteCommand(sqlExpression, connection);
                       command.ExecuteNonQuery();
                     }
           /* using (var connection = new SqliteConnection("Data Source=Users.db"))//удаление из базы
            {
                connection.Open();
                string sqlExpression = $"UPDATE Users SET number =NULL WHERE id={WorkPlace.userid} ";
                SqliteCommand command = new SqliteCommand(sqlExpression, connection);
                command.ExecuteNonQuery();
            }*/
            if (index >= 0)
                Places.RemoveAt(index);
                // тут ещё нужно будет удалять элемент из базы
            }
    }
}
