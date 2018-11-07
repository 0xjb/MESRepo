using MES.Acquintance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES.Data
{
    class Batch : IBatch
    {

        private readonly float batchId;
        private readonly float beerId;
        private readonly int acceptableProducts;
        private readonly int defectProducts;
        private readonly float temperature;
        private readonly float humidity;
        private readonly float vibration;
        private readonly string timeStamp;

        public Batch(float batchId, float beerId, int acceptableProducts,
            int defectProducts, float temperature, float humidity,
            float vibration, string timeStamp)
        {
            this.batchId = batchId;
            this.beerId = beerId;
            this.acceptableProducts = acceptableProducts;
            this.defectProducts = defectProducts;
            this.temperature = temperature;
            this.humidity = humidity;
            this.vibration = vibration;
            this.timeStamp = timeStamp;
        }

        override
        public string ToString()
        {
            return batchId + ", " + beerId + ", "
            + acceptableProducts + ", " + defectProducts + ", "
            + temperature + ", " + humidity + ", "
            + vibration + ", " + timeStamp;
        }

        public float GetBatchId()
        {
            return batchId;
        }

        public float GetBeerId()
        {
            return beerId;
        }

        public int GetAcceptableProducts()
        {
            return acceptableProducts;
        }

        public int GetDefectProducts()
        {
            return defectProducts;
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

        public string GetTimestamp()
        {
            return timeStamp;
        }
    }
}
