using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MES.Acquintance;

namespace MES {
    public class ValueOverProdTime : IBatchValue {
        private string timestamp;
        private float value;
    
        public ValueOverProdTime(float _value, string _timestamp, int _type) {
            value = _value;
            timestamp = _timestamp;
            Type = _type;
        }


        public string Timestamp {
            get { return timestamp; }
            set { timestamp = value; }
        }

        public float Value {
            get { return value; }
            set { this.value = value; }
        }

        public int Type { get; set; }
    }
}