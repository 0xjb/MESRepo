using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MES.Acquintance
{
    public interface IData
    {
        /// <summary>
        /// Saves a batch in the db
        /// </summary>
        /// <param name="batchId"></param>
        /// <param name="beerId"></param>
        /// <param name="acceptableProducts"></param>
        /// <param name="defectProducts"></param>
        /// <param name="timeStampStart"></param>
        /// <param name="timeStampEnd"></param>
        /// <returns></returns>
        bool SaveBatch(float batchId, float beerId, int acceptableProducts,
            int defectProducts, string timestampStart, string timestampEnd, double oee);

        /// <summary>
        /// Saves a batch in the db
        /// </summary>
        /// <param name="batchId"></param>
        /// <param name="beerId"></param>
        /// <param name="acceptableProducts"></param>
        /// <param name="defectProducts"></param>
        /// <param name="timeStampStart"></param>
        /// <param name="timeStampEnd"></param>
        /// <param name="batchValues"></param>
        /// <returns></returns>
        bool SaveBatch(float batchId, float beerId, int acceptableProducts,
           int defectProducts, string timestampStart, string timestampEnd, double oee,
           ISet<IList<IBatchValue>> batchValues);

        /// <summary>
        /// Saves a batch in the db
        /// </summary>
        /// <param name="batch"></param>
        /// <returns></returns>
        bool SaveBatch(IBatch batch);

        /// <summary>
        /// Inserts batch values into the db
        /// </summary>
        /// <param name="batchValues"></param>
        /// <param name="batchId"></param>
        /// <returns></returns>
        bool InsertBatchValueSet(ISet<IList<IBatchValue>> batchValues, float batchId);

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

        /// <summary>
        /// Adds recipes to the db
        /// </summary>
        /// <param name="recipes"></param>
        /// <returns></returns>
        bool AddRecipes(IRecipe[] recipes);

        /// <summary>
        /// Authenticates the user information
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        bool AuthenticateUserInformation(string username, string password);

        ObservableCollection<IAlarmObject> ReadFile();
        void WriteToFile(string s);

    }
}
