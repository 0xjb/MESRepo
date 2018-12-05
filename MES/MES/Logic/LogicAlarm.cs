using MES.Acquintance;

namespace MES.Logic
{
    public class LogicAlarm : IAlarmObject
    {
        public int AlarmNumber { get; set; }
        public string Timestamp { get; set; }
        public string StopReason { get; set; }
        public int BatchID { get; set; }
        public int StopID { get; set; }

        public LogicAlarm(int alarmNumber, string timestamp,
            string stopReason, int batchID, int stopID)
        {
            this.AlarmNumber = alarmNumber;
            this.Timestamp = timestamp;
            this.StopReason = stopReason;
            this.BatchID = batchID;
            this.StopID = stopID;
        }
    }
}
