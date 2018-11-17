using MES.Acquintance;

namespace MES.Logic
{

    public class LogicFacade : ILogic
    {
        private OpcClient opc;

        private Simulation _simulation;
        public LogicFacade()
        {
            this.opc = new OpcClient();
            this._simulation = new Simulation(opc);

        }

        public Simulation GetSimulation
        {
            get => _simulation;
            set => _simulation = value;
        }


        private IData data;

      

        public OpcClient OPC {
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


    }
}
