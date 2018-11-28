using MES.Acquintance;
using System;

namespace MES.Logic
{
    public class LogicFacade : ILogic
    {
        private IData data;
        private OpcClient opc;
        private BatchQueue batches;
        private TestSimulation _testSimulation;
        private bool isSimulationON;

        public OpcClient OPC
        {
            get { return opc; }
            set { opc = value; }
        }

        public BatchQueue Batches
        {
            get { return batches; }
            set { batches = value; }
        }

        public TestSimulation TestSimulation
        {
            get => _testSimulation;
            set => _testSimulation = value;
        }

        public bool IsSimulationOn
        {
            get => isSimulationON;
            set => isSimulationON = value;
        }

        public LogicFacade()
        {
            this.opc = new OpcClient();
            Batches = new BatchQueue(OPC);
            //this._testSimulation = new TestSimulation(opc);
        }

        public void InjectData(IData dataLayer)
        {
            data = dataLayer;
        }

        public void CreateSimulation()
        {
            if (isSimulationON)
            {
                Console.WriteLine("Simulation ON");
                this._testSimulation = new TestSimulation(opc, this);
            }
        }

        public void CreateBatch(float batchId, float amount, float productType)
        {
            Batch b = new Batch(batchId, productType, amount);
            if (Batches.CurrentBatch == null)
            {
                Batches.CurrentBatch = b;
            }
            else
            {
                Batches.Batches.Add(new Batch(batchId, productType, amount));
            }

        }

        public void AddBatch(string batchID, string productType, string amount)
        {
            Console.WriteLine("yeet");
        }

        public void StartProduction()
        {
            OPC.StartMachine(Batches.CurrentBatch.BatchID, Batches.CurrentBatch.BeerType, Batches.CurrentBatch.DesiredAmount, 60);
        }

        public IUser AuthenticateUserInformation(string username, string password)
        {
            return data.AuthenticateUserInformation(username, password);
        }
    }
}
