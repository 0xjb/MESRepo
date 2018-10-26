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

        DataTable GetAllBatches();

        DataTable GetBatch(float batchId);

    }
}
