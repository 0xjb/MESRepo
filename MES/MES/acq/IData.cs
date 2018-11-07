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
    }
}
