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
                this._testSimulation = new TestSimulation(opc, this);
            }
        }

        public void CreateBatch(float batchId, float amount, float machineSpeed, float productType)
        {
            //private void Button_Click(object sender, RoutedEventArgs e)
            //{
            //        float batchId = float.Parse(BatchIdTB.Text);
            //        float productType = float.Parse(ProductTypeTB.Text);
            //        float amount = float.Parse(AmountTB.Text);
            //        float machineSpeed = float.Parse(MachineSpeedTB.Text);
            //        c.StartMachine(batchId, productType, amount, machineSpeed);


            //}
        }
    }
}