using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES
{
    class ValueOverProdTime
    {
        private  DateTime time;

        public DateTime Time
        {
            get { return  time; }
            set {  time = value; }
        }


        private int value;

        public int Value 
        {
            get { return value; }
            set { this.value = value; }
        }



    }
}
