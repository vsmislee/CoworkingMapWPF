using System;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoworkingMap
{
    class WorkPlace
    {
        int number;
        bool taked;
        int userid;
        List <DateInterval> takedDates;
        //время занятости .. сделать структуру для даты: начало брони и конец.. потом сделать список этих структур и отметить их в календаре

        public WorkPlace()
        {
            number = 0;
            taked = false;
            userid = 0;
            takedDates = null;
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

        public List<DateInterval> TakedDates
        {
            get { return this.takedDates; }
            set { this.takedDates = value; }
        }
        public bool IsTaked()
        {
            foreach (DateInterval item in takedDates)
            {
                if (item.IsIn(DateTime.Today))
                {
                    return true;
                }
            }
            return false;
        }

        public void AddTakedDate(DateInterval takedDate)
        {
            if (this.takedDates == null)
                takedDates = new List<DateInterval>();
            foreach (DateInterval item in takedDates)
            {
                if (item == takedDate)
                    throw new Exception("Время брони уже занято.");
                takedDates.Add(takedDate);
                //тут ещё нужно добавлять в базу
            }
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
    }
}
