using MES.Acquintance;

namespace MES.Data
{
    public class BatchTemperature : IBatchValue
    {
        private readonly float temperature;
        private readonly string timestamp;

        public BatchTemperature(float temperature, string timestamp)
        {
            this.temperature = temperature;
            this.timestamp = timestamp;
        }

        public float GetValue()
        {
            return temperature;
        }

        public string GetTimeStamp()
        {
            return timestamp;
        }
    }
}
