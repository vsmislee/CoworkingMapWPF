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

        private void ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Calendar1.SelectedDates.Count == 0)
                    throw new Exception("Ни один день не выран!");
                SelectedDatesCollection Dates = Calendar1.SelectedDates;
                CalendarDateRange TakedDates = new CalendarDateRange(Dates.First(), Dates.Last().AddMinutes(1439));
                place.Take(TakedDates);
                MessageBox.Show("Место номер " + place.Number + " забронировано с " +TakedDates.Start.ToString() + " до " + TakedDates.End.ToString());
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

        /*private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }*/
    }
}
