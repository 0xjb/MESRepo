using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES.Acquintance
{
    public interface IBatch
    {
        float GetBatchId();

        float GetBeerId();

        int GetAcceptableProducts();

        int GetDefectProducts();

        string GetTimestampStart();

        string GetTimestampEnd();

        IList<IBatchValueSet> GetBatchValues();

        void AddProducts(int amount, bool acceptable);

        void SetTimestampEnd(string timestamp);

        bool AddBatchValues(float temperature, float humidity,
            float vibration, string timestamp);

        void SetBatchValueSet(IList<IBatchValueSet> values);
    }
}
