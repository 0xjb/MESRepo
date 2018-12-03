using MES.Acquintance;

namespace MES.Data
{
    class BatchVibration : IBatchValue
    {
        private readonly float vibration;
        private readonly string timestamp;

        public BatchVibration(float vibration, string timestamp)
        {
            this.vibration = vibration;
            this.timestamp = timestamp;
        }

        public float GetValue()
        {
            return vibration;
        }

        public string GetTimeStamp()
        {
            return timestamp;
        }
    }
}
