using System;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
namespace CoworkingMap
{
    public class WorkPlace
    {
        int number;
       public static int userid;
        List<CalendarDateRange> takedDates;
        double marginUp;
        double marginLeft;
        static readonly int width = 19;
        static readonly int height = 19;
        //Margin
        public WorkPlace()
        {
            number = 0;
            takedDates = new List<CalendarDateRange>();
            this.marginUp = 100;
            this.marginLeft = 100;
        }

        public WorkPlace(int number)
        {
            this.number = number;
            takedDates = new List<CalendarDateRange>();
            this.marginUp = 100;
            this.marginLeft = 100;
        }

        public WorkPlace(int number, double marginUp, double marginLeft)
        {
            this.number = number;
            takedDates = new List<CalendarDateRange>();
            this.marginUp = marginUp;
            this.marginLeft = marginLeft;
        }

        public int Number
        {
            get { return this.number; }
            set { number = value; }
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

        public void Take(CalendarDateRange takedDate)
        {
            if (this.takedDates == null)
                takedDates = new List<CalendarDateRange>();
            foreach (CalendarDateRange item in takedDates)
            {
                if (takedDate.Start < item.Start && takedDate.End > item.Start)
                    throw new Exception("Время брони уже занято.");
            }
            //////////////////////////////////тут ещё нужно добавлять в базу///////////////////////////////////////////////
            //в WindowSelect.Place прописал
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
