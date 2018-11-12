using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES.Acquintance
{
    public interface IDBManager
    {
        /// <summary>
        /// Inserts a batch into the db
        /// </summary>
        /// <param name="batch"></param>
        /// <returns></returns>
        bool InsertIntoBatchesTable(IBatch batch);

        /// <summary>
        /// Returns all batches from the db
        /// </summary>
        /// <returns></returns>
        IDictionary<float, IBatch> GetAllBatches();

        /// <summary>
        /// Returns all batches from a specific month and year from the db
        /// </summary>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        IDictionary<float, IBatch> GetBatches(string month, string year);

        /// <summary>
        /// Returns the newest batches from the db
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        IDictionary<float, IBatch> GetBatches(int amount);

        /// <summary>
        /// Returns a batch with a specific batch id from the db
        /// </summary>
        /// <param name="batchId"></param>
        /// <returns></returns>
        IBatch GetBatch(float batchId);

        /// <summary>
        /// Deletes all batches from the db
        /// </summary>
        /// <returns></returns>
        bool DeleteAllBatches();

        /// <summary>
        /// Deletes a specific batch from the db
        /// </summary>
        /// <param name="batchId"></param>
        /// <returns></returns>
        bool DeleteBatch(float batchId);

        bool RunQueries(string[] statements);
    }
}
