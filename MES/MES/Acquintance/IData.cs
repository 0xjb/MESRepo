using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES.Acquintance
{
    public interface IData
    {
        /// <summary>
        /// Saves a batch in the db
        /// </summary>
        /// <param name="batch"></param>
        /// <returns></returns>
        bool SaveBatch(float batchId, float beerId, int acceptableProducts,
            int defectProducts, string timeStampStart, string timeStampEnd);

        /// <summary>
        /// Saves a batch in the db
        /// </summary>
        /// <param name="batch"></param>
        /// <returns></returns>
        bool SaveBatch(IBatch batch);

        /// <summary>
        /// Inserts a set of batch values into the db
        /// </summary>
        /// <param name="temperature"></param>
        /// <param name="humidity"></param>
        /// <param name="vibration"></param>
        /// <param name="timestamp"></param>
        /// <param name="batchId"></param>
        /// <returns></returns>
        bool InsertBatchValueSet(float temperature, float humidity,
            float vibration, string timestamp, float batchId);

        /// <summary>
        /// Updates a batch in the db
        /// </summary>
        /// <param name="batch"></param>
        /// <returns></returns>
        bool UpdateBatch(IBatch batch);

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

        /// <summary>
        /// Run custom queries in the db
        /// </summary>
        /// <param name="statements"></param>
        /// <returns></returns>
        bool RunQueries(string[] statements);

        /// <summary>
        /// Runs a custom query in the db
        /// </summary>
        /// <param name="statements"></param>
        /// <returns></returns>
        bool RunQuery(string statement);

        /// <summary>
        /// Returns all recipes from the db
        /// </summary>
        /// <returns></returns>
        IDictionary<float, IRecipe> GetAllRecipes();
    }
}
