using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoworkingMap
{
    class WorkPlace
    {
        int number;
        bool busy;
        int userid;

        public int Number
        {
            get { return this.number; }
            set { number = value; }
        }

        public bool Busy
        {
            get { return this.busy; }
            set { this.busy = value; }
        }

        public int UserID
        {
            get { return this.userid; }
            set { this.userid = value; }
        }
        public bool IsBusy()
        {
            return this.busy; 
        }

    }
}
