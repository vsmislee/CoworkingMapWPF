using System;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoworkingMap
{
    public class WorkPlace
    {
        int number;
        int userid;
        List<CalendarDateRange> takedDates;
        double marginUp;
        double marginLeft;
        static readonly int width = 19;
        static readonly int height = 19;
        //Margin
        public WorkPlace()
        {
            number = 0;
            userid = 0;
            takedDates = new List<CalendarDateRange>();
            this.marginUp = 100;
            this.marginLeft = 100;
        }

        public WorkPlace(int number)
        {
            this.number = number;
            userid = 0;
            takedDates = new List<CalendarDateRange>();
            this.marginUp = 100;
            this.marginLeft = 100;
        }

        public WorkPlace(int number, double marginUp, double marginLeft)
        {
            this.number = number;
            userid = 0;
            takedDates = new List<CalendarDateRange>();
            this.marginUp = marginUp;
            this.marginLeft = marginLeft;
        }
        public int Number
        {
            get { return this.number; }
            set { number = value; }
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

        public double MarginUp
        {
            get { return this.marginUp; }
        }

        public double MarginLeft
        {
            get { return this.marginLeft; }
        }

        public int Width
        {
            get { return width; }
        }

        public int Height
        {
            get { return height; }
        }
        public bool IsTaked()
        {
            foreach (CalendarDateRange item in takedDates)
                if (DateTime.Today <= item.End && DateTime.Today >= item.Start)
                    return true;
            return false;
        }

        public void Take(CalendarDateRange takedDate, User user)
        {
            if (this.takedDates == null)
                takedDates = new List<CalendarDateRange>();
            foreach (CalendarDateRange item in takedDates)
            {
                if (takedDate.Start < item.Start && takedDate.End > item.Start)
                    throw new Exception("Время брони уже занято.");
            }
            //////////////////////////////////тут ещё нужно добавлять в базу///////////////////////////////////////////////
            takedDates.Add(takedDate);
        }

        public BitmapImage ChooseImage()
        {
            BitmapImage bit = new BitmapImage();
            string source;
            if (!this.IsTaked())
            {
                source = "images/places/" + number.ToString() + ".PNG";
                bit.BeginInit();
                bit.UriSource = new Uri(source, UriKind.Relative);
                bit.EndInit();
            }
            else
            {
                source = "images/places/" + number.ToString() + "blue.PNG";
                bit.BeginInit();
                bit.UriSource = new Uri(source, UriKind.Relative);
                bit.EndInit();
            }
            return bit;
        }
    }
}
