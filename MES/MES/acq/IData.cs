using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES.acq
{
    interface IData
    {
        bool SaveBatch(float batchId, float beerId, int acceptableProducts,
            int defectProducts, float temperature, float humidity,
            float vibration, string timeStamp);

        bool SaveBatch(IBatch batch);

        IDictionary<float, IBatch> GetAllBatches();

        IDictionary<float, IBatch> GetBatches(string month, string year);

        IDictionary<float, IBatch> GetBatches(int amount);

        IBatch GetBatch(float batchId);

        bool DeleteAllBatches();

        bool DeleteBatch(float batchId);
    }
}
