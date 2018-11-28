using MES.Logic;


namespace MES.Acquintance
{
    public interface ILogic
    {
        TestSimulation TestSimulation { get; set; }

        BatchQueue Batches { get; set; }

        void CreateSimulation();

        OpcClient OPC { get; set; }

        void InjectData(IData dataLayer);

        bool IsSimulationOn { get; set; }

        void CreateBatch(float batchId, float amount, float productType);

        void AddBatch(string batchID, string productType, string amount);

        void StartProduction();

        /// <summary>
        /// Authenticates the user information
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        bool AuthenticateUserInformation(string username, string password);
    }
}
