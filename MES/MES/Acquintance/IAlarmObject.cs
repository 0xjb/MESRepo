namespace MES.Acquintance
{
    public interface IAlarmObject
    {
        int AlarmNumber { get; set; }

        string Timestamp { get; set; }

        string StopReason { get; set; }

        int BatchID { get; set; }

        int StopID { get; set; }
    }
}
