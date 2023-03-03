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
    /// Логика взаимодействия для WindowSelectPlace.xaml
    /// </summary>
    public partial class WindowSelectPlace : Window
    {
        WorkPlace place;
        public WindowSelectPlace()
        {
            InitializeComponent();
        }

        public WindowSelectPlace(WorkPlace place)
        {
            InitializeComponent();
            this.place = place;
        }

        public void ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Calendar1.SelectedDates.Count == 0)
                {
                    throw new Exception("Ни один день не выбран!");
                }

                SelectedDatesCollection Dates = Calendar1.SelectedDates;
                CalendarDateRange TakedDates = new CalendarDateRange(Dates.First(), Dates.Last().AddMinutes(1439));
                place.Take(TakedDates);
                using (var connection = new SqliteConnection("Data Source=Users.db"))
                {
                    connection.Open();
                    string sqlExpression = $"UPDATE Users SET booking1='{TakedDates.Start}' WHERE id={WorkPlace.userid}";
                    string sqlExpression2 = $"UPDATE Users SET booking2='{TakedDates.End}' WHERE id={WorkPlace.userid}";
                    string sqlExpression3 = $"UPDATE Users SET number='{place.Number}' WHERE id={WorkPlace.userid}";
                    SqliteCommand command = new SqliteCommand(sqlExpression, connection);
                    SqliteCommand command2 = new SqliteCommand(sqlExpression2, connection);
                    SqliteCommand command3 = new SqliteCommand(sqlExpression3, connection);
                    command.ExecuteNonQuery();
                    command2.ExecuteNonQuery();
                    command3.ExecuteNonQuery();
                }
                using (var connection = new SqliteConnection("Data Source=History.db"))//здесь нужно сделать так, чтобы для каждого сотрудника была своя история брони
                {
                    connection.Open();
                    string sql = "INSERT INTO History (Date, Booking,Place,UserID) VALUES (@Date, @Booking,@Place,@UserID)";
                    SqliteCommand command = new SqliteCommand(sql, connection);
                    DateTime now = DateTime.Now;
                    SqliteParameter sqliteParameter = new SqliteParameter("@Date", $"{ now:g}");
                    command.Parameters.Add(sqliteParameter);
                    SqliteParameter sqliteParameter1 = new SqliteParameter("Booking", TakedDates.Start+" - "+TakedDates.End);
                    command.Parameters.Add(sqliteParameter1);
                    SqliteParameter sqliteParameter2 = new SqliteParameter("@Place", place.Number);
                    command.Parameters.Add(sqliteParameter2);
                    SqliteParameter sqliteParameter3 = new SqliteParameter("UserID", WorkPlace.userid);
                    command.Parameters.Add(sqliteParameter3);
                  int nd=  command.ExecuteNonQuery();
                }
                    MessageBox.Show("Место номер " + place.Number + " забронировано с " + TakedDates.Start.ToString() + " до " + TakedDates.End.ToString());
                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Calendar1_Loaded(object sender, RoutedEventArgs e)
        {

            Calendar1.DisplayDateStart = new DateTime(2023, 1, 1); // чтобы в календаре был только 23 год
            Calendar1.DisplayDateEnd = new DateTime(2023, 12, 31);
            Calendar1.FirstDayOfWeek = DayOfWeek.Monday;

            //закрашивание дней до сегодня
            CalendarDateRange InactiveDates = new CalendarDateRange();
            InactiveDates.Start = (DateTime)Calendar1.DisplayDateStart;
            InactiveDates.End = DateTime.Today.AddDays(-1);
            Calendar1.BlackoutDates.Add(InactiveDates);

            //закрашивание занятых дней
            foreach (CalendarDateRange item in place.TakedDates)
            {
                Calendar1.BlackoutDates.Add(item);
            }
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
