using MES.Acquintance;
using System;
using System.Collections.Generic;

namespace MES.Logic
{
    public class LogicFacade : ILogic
    {
        private IData data;
        private ErrorHandler errorHandler;
        private OpcClient opc;
        private BatchQueue batches;
        private TestSimulation _testSimulation;
        private bool isSimulationON;

        public LogicFacade()
        {
            this.opc = new OpcClient(this);
            Batches = new BatchQueue(this);
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

        public OpcClient OPC
        {
            get { return opc; }
            set { opc = value; }
        }

        public IData Data
        {
            get => data;
            set => data = value;
        }

        public ErrorHandler ErrorHandler
        {
            get => errorHandler;
            set => errorHandler = value;
        }

        public bool IsSimulationOn
        {
            get => isSimulationON;
            set => isSimulationON = value;
        }

        public void InjectData(IData dataLayer)
        {
            data = dataLayer;
            CreateErrorHandler();
        }

        public void CreateSimulation()
        {
            if (isSimulationON)
            {
                Console.WriteLine("Simulation ON");
                this._testSimulation = new TestSimulation(opc, this);
            }
        }

        public void CreateErrorHandler()
        {
            this.errorHandler = new ErrorHandler(this);
        }

        public void CreateBatch(float batchId, float amount, IRecipe recipe)
        {
            SimpleBatch b = new SimpleBatch(batchId, recipe, amount);
            if (Batches.CurrentBatch == null)
            {
                Batches.CurrentBatch = b;
            }
            else
            {
                Batches.Batches.Add(new SimpleBatch(batchId, recipe, amount));
            }
        }

        public IDictionary<float, IRecipe> GetAllRecipes()
        {
            return data.GetAllRecipes();
        }

        public float GetHighestBatchId()
        {
            return data.GetHighestBatchId();
        }

        public void StartProduction()
        {
            OPC.StartMachine(Batches.CurrentBatch.BatchID, Batches.CurrentBatch.BeerType,
                Batches.CurrentBatch.DesiredAmount, Batches.CurrentBatch.MachineSpeed);
        }

        public bool AuthenticateUserInformation(string username, string password)
        {
            return data.AuthenticateUserInformation(username, password);
        }
        public void SaveBatch(ISimpleBatch s)
        {
            ISet<IList<IBatchValue>> set = new HashSet<IList<IBatchValue>>();
            set.Add(OPC.TempList);
            set.Add(OPC.HumidityList);
            set.Add(OPC.VibrationList);

            Data.SaveBatch(s.BatchID, s.BeerType, (int)OPC.AcceptableProducts,
                (int)OPC.DefectProducts, s.TimestampStart, s.TimestampEnd, s.OEE, set);
        }

        public ISimpleBatch GetCurrentBatch()
        {
            return Batches.CurrentBatch;
        }
        public IDictionary<float,IBatch> GetAllBatches() {
            return data.GetAllBatches();
        }
    }
}
