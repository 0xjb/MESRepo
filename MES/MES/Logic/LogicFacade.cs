using System;
using MES.Acquintance;

namespace MES.Logic
{
    public class LogicFacade : ILogic
    {
        private OpcClient opc;

        private TestSimulation _testSimulation;
        private bool isSimulationON;

        public LogicFacade()
        {
            this.opc = new OpcClient();
            //this._testSimulation = new TestSimulation(opc);
        }

        public TestSimulation GetTestSimulation
        {
            get => _testSimulation;
            set => _testSimulation = value;
        }

        private IData data;

        public OpcClient OPC
        {
            get { return opc; }
            set { opc = value; }
        }

        public void InjectData(IData dataLayer)
        {
            data = dataLayer;
        }

        // TODO deprecate
        public OpcClient GetOPC()
        {
            return opc;
        }

        public bool IsSimulationOn
        {
            get => isSimulationON;
            set => isSimulationON = value;
        }

        public void CreateSimulation()
        {
            if (isSimulationON)
            {
                Console.WriteLine("Simulation ON");
                this._testSimulation = new TestSimulation(opc);
            }
        }
    }
}