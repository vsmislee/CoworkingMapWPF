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
        int placeNumber;
        public WindowSelectPlace()
        {
            InitializeComponent();
        }

        public WindowSelectPlace(int placeNumber)
        {
            InitializeComponent();
            this.placeNumber = placeNumber;
        }

        private void ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Calendar1.SelectedDates.Count == 0)
                    throw new Exception("Ни один день не выран!");
                SelectedDatesCollection dates = Calendar1.SelectedDates;
                DateTime lastday = dates.Last().AddDays(1);
                MessageBox.Show(dates.First().ToString() + "  " + lastday.ToString());
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

            //закрашивание дней которые заняты
            /*CalendarDateRange datePlaceTaked = new CalendarDateRange();
            datePlaceTaked.Start = new DateTime(2023, 1, 7);
            datePlaceTaked.End = new DateTime(2023, 1, 10);
            Calendar1.BlackoutDates.Add(datePlaceTaked);*/
        }

        /*private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }*/
    }
}
