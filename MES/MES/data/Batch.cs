using MES.Acquintance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES.Data
{
    public class Batch : IBatch
    {
        private readonly float batchId;
        private readonly float beerId;
        private int acceptableProducts;
        private int defectProducts;
        private readonly string timestampStart;
        private string timestampEnd;
        private IList<IBatchValueSet> batchValues;

        public Batch(float batchId, float beerId, int acceptableProducts,
            int defectProducts, string timestampStart, string timestampEnd)
        {
            this.batchId = batchId;
            this.beerId = beerId;
            this.acceptableProducts = acceptableProducts;
            this.defectProducts = defectProducts;
            this.timestampStart = timestampStart;
            this.timestampEnd = timestampEnd;
            this.batchValues = new List<IBatchValueSet>();
        }

        override
        public string ToString()
        {
            return batchId + ", " + beerId + ", "
            + acceptableProducts + ", " + defectProducts + ", "
            + timestampStart + ", " + timestampEnd;
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

        public string GetTimestampStart()
        {
            return timestampStart;
        }

        public string GetTimestampEnd()
        {
            return timestampEnd;
        }

        public IList<IBatchValueSet> GetBatchValues()
        {
            return batchValues;
        }

        public void AddProducts(int amount, bool acceptable)
        {
            if (acceptable)
            {
                this.acceptableProducts = this.acceptableProducts + amount;
            }
            else
            {
                this.defectProducts = this.defectProducts + amount;
            }
        }

        public void SetTimestampEnd(string timestamp)
        {
            this.timestampEnd = timestamp;
        }

        public bool AddBatchValues(float temperature, float humidity,
            float vibration, string timestamp)
        {
            bool timestampExists = false;
            foreach (IBatchValueSet valueSet in batchValues)
            {
                if (valueSet.GetTimeStamp() == timestamp)
                {
                    timestampExists = true;
                    break;
                }
            }
            if (!timestampExists)
            {
                batchValues.Add(new BatchValueSet(temperature,
                    humidity, vibration, timestamp));
                return true;
            }
            else return false;
        }

        public void SetBatchValueSet(IList<IBatchValueSet> values)
        {
            this.batchValues = values;
        }
    }
}
