using System;
using System.Collections.Generic;
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

        ISet<IBatch> GetAllBatches();

        IBatch GetBatch(float batchId);

    }
}
