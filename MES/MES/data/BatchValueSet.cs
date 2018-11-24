using MES.Acquintance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES.Data
{
    public class BatchValueSet : IBatchValueSet
    {
        private readonly float temperature;
        private readonly float humidity;
        private readonly float vibration;
        private readonly string timestamp;

        public BatchValueSet(float temperature, float humidity,
            float vibration, string timestamp)
        {
            this.temperature = temperature;
            this.humidity = humidity;
            this.vibration = vibration;
            this.timestamp = timestamp;
        }

        public float GetTemperature()
        {
            return temperature;
        }

        public float GetHumidity()
        {
            return humidity;
        }

        public float GetVibration()
        {
            return vibration;
        }

        public string GetTimeStamp()
        {
            return timestamp;
        }
    }
}