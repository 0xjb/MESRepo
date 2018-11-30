using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MES.Acquintance;

namespace MES.Data
{
    public class AlarmObject:IAlarmObject
    {
        public int AlarmNumber { get; set; }
        public string Timestamp { get; set; }
        public string StopReason { get; set; }
        public int BatchID { get; set; }

        public int StopID { get; set; }
    }
}
