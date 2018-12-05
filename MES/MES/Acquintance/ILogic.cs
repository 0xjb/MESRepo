using MES.Logic;


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

        void CreateBatch(float batchId, float amount, float productType);

        void StartProduction();

        /// <summary>
        /// Authenticates the user information
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        bool AuthenticateUserInformation(string username, string password);

        void CreateErrorHandler();
        void SaveBatch(ISimpleBatch s);
        ISimpleBatch GetCurrentBatch();
    }
}
