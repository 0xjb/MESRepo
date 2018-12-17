using MES.Logic;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MES.Acquintance
{
    public interface ILogic
    {
        TestSimulation TestSimulation { get; set; }

        BatchQueue Batches { get; set; }

        void CreateSimulation();

        IData Data { get; set; }

        OpcClient OPC { get; set; }

        ErrorHandler ErrorHandler { get; set; }

        void InjectData(IData dataLayer);

        bool IsSimulationOn { get; set; }

        void CreateBatch(float amount, float speed, IRecipe recipe);

        void StartProduction();

        /// <summary>
        /// Returns all recipes from the db
        /// </summary>
        /// <returns></returns>
        IDictionary<float, IRecipe> GetAllRecipes();

        /// <summary>
        /// Returns the highest Batch id in the db
        /// </summary>
        /// <returns></returns>
        float GetHighestBatchId();

        /// <summary>
        /// Authenticates the user information
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        bool AuthenticateUserInformation(string username, string password);

        void CreateErrorHandler();

        bool addOEEFromBatch(int batchId);

        void SearchNewestBatches(int number);

        void SearchDateYearBatches(string month, string year);

        ObservableCollection<IBatch> OEeList { get; set; }

        void SaveBatch(ISimpleBatch s);

        ISimpleBatch GetCurrentBatch();

        IDictionary<float, IBatch> GetAllBatches();

        double GetOptimalSpeed(IRecipe recipe);
    }
}
