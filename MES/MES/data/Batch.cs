﻿using MES.Acquintance;
using System.Collections.Generic;

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
        private IList<IBatchValue> batchTemperatures;
        private IList<IBatchValue> batchHumidities;
        private IList<IBatchValue> batchVibrations;

        public Batch(float batchId, float beerId, int acceptableProducts,
            int defectProducts, string timestampStart, string timestampEnd)
        {
            this.batchId = batchId;
            this.beerId = beerId;
            this.acceptableProducts = acceptableProducts;
            this.defectProducts = defectProducts;
            this.timestampStart = timestampStart;
            this.timestampEnd = timestampEnd;
            this.batchTemperatures = new List<IBatchValue>();
            this.batchHumidities = new List<IBatchValue>();
            this.batchVibrations = new List<IBatchValue>();
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

        public IList<IBatchValue> GetBatchTemperatures()
        {
            return batchTemperatures;
        }

        public IList<IBatchValue> GetBatchHumidities()
        {
            return batchHumidities;
        }

        public IList<IBatchValue> GetBatchVibrations()
        {
            return batchVibrations;
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

        public void AddBatchValue(IBatchValue value)
        {
            if (value.Type > 1)
            {
                this.batchTemperatures.Add(value);
            }
            else if (value.Type == 1)
            {
                this.batchHumidities.Add(value);
            }
            else if (value.Type == 1)
            {
                this.batchVibrations.Add(value);
            }
            else { }
        }

        public void AddBatchValue(float value, string timestamp, int type)
        {
            IBatchValue bValue = new BatchValue(value, timestamp, type);

            if (bValue.Type < 0)
            {
                this.batchTemperatures.Add(bValue);
            }
            else if (bValue.Type == 0)
            {
                this.batchHumidities.Add(bValue);
            }
            else if (bValue.Type > 0)
            {
                this.batchVibrations.Add(bValue);
            }
            else { }
        }

        public void AddBatchValues(IList<IBatchValue> values)
        {
            foreach (IBatchValue value in values)
            {
                AddBatchValue(value);
            }
        }
    }
}
