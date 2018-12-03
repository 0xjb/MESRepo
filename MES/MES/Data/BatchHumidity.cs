using MES.Acquintance;

namespace MES.Data
{
    class BatchHumidity : IBatchValue
    {
        private readonly float humidity;
        private readonly string timestamp;

        public BatchHumidity(float humidity, string timestamp)
        {
            this.humidity = humidity;
            this.timestamp = timestamp;
        }

        public float GetValue()
        {
            return humidity;
        }

        public string GetTimeStamp()
        {
            return timestamp;
        }
    }
}
