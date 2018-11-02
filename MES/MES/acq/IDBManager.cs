﻿using System;
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

        bool DeleteAllBatches();

        IBatch GetBatch(float batchId);

    }
}
