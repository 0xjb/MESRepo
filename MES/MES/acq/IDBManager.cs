using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES.acq
{
    interface IDBManager
    {
        bool CreateBatchesTable();

        bool DeleteBatchesTable();

        bool InsertIntoBatchesTable(IBatch batch);

        IDictionary<float, IBatch> GetAllBatches();

        IDictionary<float, IBatch> GetBatches(string month, string year);

        IDictionary<float, IBatch> GetBatches(int amount);

        bool DeleteAllBatches();

        IBatch GetBatch(float batchId);
    }
}
