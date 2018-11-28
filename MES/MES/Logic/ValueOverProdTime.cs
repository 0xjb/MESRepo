using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES
{
    public class ValueOverProdTime
    {
        public string Time { get; set; }
        public double Value { get; set; }

        public ValueOverProdTime(double val) {
            Value = val;
            Time = DateTime.Now.ToString();
        }
    }
}