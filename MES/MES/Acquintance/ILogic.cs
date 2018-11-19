using MES.Logic;


namespace MES.Acquintance
{
    public interface ILogic
    {
        TestSimulation GetTestSimulation { get; set; }

        void CreateSimulation();

        OpcClient OPC { get; set; }

        void InjectData(IData dataLayer);

        bool IsSimulationOn { get; set; }

        void CreateBatch(float batchId, float amount, float machineSpeed, float productType);
    }
}