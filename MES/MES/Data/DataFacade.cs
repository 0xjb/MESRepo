using MES.acq;
using MES.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES.Data
{
    class DataFacade : IData
    {

        IDBManager dbManager;

        public DataFacade()
        {
            dbManager = new DBManager();
        }

        public bool SaveBatch(float batchId, float beerId, int acceptableProducts,
            int defectProducts, float temperature, float humidity,
            float vibration, string timeStamp)
        {
            return dbManager.InsertIntoBatchesTable(
                new Batch(batchId, beerId, acceptableProducts,
                defectProducts, temperature, humidity,
                vibration, timeStamp));
        }

        public bool SaveBatch(IBatch batch)
        {
            return dbManager.InsertIntoBatchesTable(batch);
        }
    }
}
