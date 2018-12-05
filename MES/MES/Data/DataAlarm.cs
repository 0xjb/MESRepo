using MES.Acquintance;

namespace MES.Data
{
    public class DataAlarm : IAlarmObject
    {
        public int AlarmNumber { get; set; }
        public string Timestamp { get; set; }
        public string StopReason { get; set; }
        public int BatchID { get; set; }
        public int StopID { get; set; }
    }
}
