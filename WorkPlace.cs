using System;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoworkingMap
{
    public class WorkPlace
    {
        int number;
        bool taked;
        int userid;
        List<CalendarDateRange> takedDates;

        public WorkPlace()
        {
            number = 0;
            taked = false;
            userid = 0;
            takedDates = new List<CalendarDateRange>();
        }
        public int Number
        {
            get { return this.number; }
            set { number = value; }
        }

        public bool Taked
        {
            get { return this.taked; }
            set { this.taked = value; }
        }

        public int UserID
        {
            get { return this.userid; }
            set { this.userid = value; }
        }

        public List<CalendarDateRange> TakedDates
        {
            get { return this.takedDates; }
            set { this.takedDates = value; }
        }
        public bool IsTaked()
        {
            foreach (CalendarDateRange item in takedDates)
            {
                if (DateTime.Today < item.End && DateTime.Today > item.Start)
                {
                    return true;
                }
            }
            return false;
        }

        public void AddTakedDate(CalendarDateRange takedDate)
        {
            if (this.takedDates == null)
                takedDates = new List<CalendarDateRange>();
            foreach (CalendarDateRange item in takedDates)
            {
                if (item == takedDate)
                    throw new Exception("Время брони уже занято.");
                //тут ещё нужно добавлять в базу
            }
            takedDates.Add(takedDate);
        }

        public BitmapImage ChooseImage()
        {
            BitmapImage bit = new BitmapImage();
            string source;
            if (!this.Taked)//проверка занято ли место
            {
                source = "images/places/" + this.number.ToString() + ".PNG";
                bit.BeginInit();
                bit.UriSource = new Uri(source, UriKind.Relative);
                bit.EndInit();
            }
            else
            {
                source = "images/places/" + this.Number.ToString() + "blue.PNG";
                bit.BeginInit();
                bit.UriSource = new Uri(source, UriKind.Relative);
                bit.EndInit();
            }
            return bit;
        }

        public void Take()
        {
           
        }
    }
}
