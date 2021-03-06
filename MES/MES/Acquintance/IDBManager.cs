﻿using System.Collections.Generic;

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
        /// Inserts batch values into the db
        /// </summary>
        /// <param name="batchValues"></param>
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
        /// Returns all batches from a specific month and year from the db.
        /// The year needs to have a length of 4 characters and the month needs to have a length of 1 or 2 characters.
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
        /// Runs a custom query in the db
        /// </summary>
        /// <param name="statements"></param>
        /// <returns></returns>
        bool RunQueries(string[] statements);

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
        /// Returns the highest Batch id in the db
        /// </summary>
        /// <returns></returns>
        float GetHighestBatchId();

        double GetOptimalSpeed(IRecipe recipe);
    }
}
