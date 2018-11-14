using MES.Acquintance;
using MES.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MES.Acquintance;


namespace MES.Data
{
    public class DataFacade : IData
    {
        IDBManager dbManager;

        public DataFacade()
        {
            dbManager = new DBManager();
        }

        public bool SaveBatch(float batchId, float beerId, int acceptableProducts,
            int defectProducts, string timestampStart, string timestampEnd)
        {
            return dbManager.InsertIntoBatchesTable(
                new Batch(batchId, beerId, acceptableProducts,
                defectProducts, timestampStart, timestampEnd));
        }

        public bool SaveBatch(IBatch batch)
        {
            return dbManager.InsertIntoBatchesTable(batch);
        }

        public bool InsertBatchValueSet(float temperature, float humidity,
            float vibration, string timestamp, float batchId)
        {
            return dbManager.InsertBatchValueSet(temperature, humidity,
                vibration, timestamp, batchId);
        }

        public IDictionary<float, IBatch> GetAllBatches()
        {
            return dbManager.GetAllBatches();
        }

        public IDictionary<float, IBatch> GetBatches(string month, string year)
        {
            return dbManager.GetBatches(month, year);
        }

        public IDictionary<float, IBatch> GetBatches(int amount)
        {
            return dbManager.GetBatches(amount);
        }

        public IBatch GetBatch(float batchId)
        {
            return dbManager.GetBatch(batchId);
        }

        public bool DeleteAllBatches()
        {
            return dbManager.DeleteAllBatches();
        }

        public bool DeleteBatch(float batchId)
        {
            return dbManager.DeleteBatch(batchId);
        }

        public bool RunQueries(string[] statements)
        {
            return dbManager.RunQueries(statements);
        }

        public bool RunQuery(string statement)
        {
            string[] s = { statement };
            return dbManager.RunQueries(s);
        }
    }
}
