using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES.Acquintance
{
    public interface IAlarmObject
    {
         int AlarmNumber { get; set; }
         string Timestamp { get; set; }

         string StopReason { get; set; }

         int BatchID { get; set; }
    }
}
