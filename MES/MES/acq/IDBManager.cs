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
        /*
         * Insert a batch into the db
        */
        bool InsertIntoBatchesTable(IBatch batch);

        /*
         * Return all batches from the db
        */
        IDictionary<float, IBatch> GetAllBatches();

        /*
         * Return all batches from a specific month and year from the db
        */
        IDictionary<float, IBatch> GetBatches(string month, string year);

        /*
         * Return the newest batches from the db
        */
        IDictionary<float, IBatch> GetBatches(int amount);

        /*
         * Return a batch with a specific batch id from the db
        */
        IBatch GetBatch(float batchId);

        /*
         * Deletes all batches from the db
        */
        bool DeleteAllBatches();

        /*
         * Delete a specific batch from the db
        */
        bool DeleteBatch(float batchId);
    }
}
