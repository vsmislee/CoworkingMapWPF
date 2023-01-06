using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoworkingMap
{
    class DateInterval
    {
        DateTime begin;
        DateTime end;

        public DateInterval(DateTime begin, DateTime end)
        {
            this.begin = begin;
            this.end = end;
        }

        public DateTime Begin
        {
            get { return this.begin; }
            set { this.begin = value; }
        }

        public DateTime End
        {
            get { return this.end; }
            set { this.end = value; }
        }

        public bool IsIn(DateTime date) // находится ли дата в промежутке
        {
            if(date < end && date > begin)
                return true;
            return false;
        }
    }
}
