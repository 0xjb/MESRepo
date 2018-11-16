using MES.Acquintance;

namespace MES.Logic
{

    public class LogicFacade : ILogic
    {
        private OpcClient opc;
        //private SubscribeThread subscribeThread;
        public LogicFacade()
        {
            this.opc = new OpcClient();
            //this.subscribeThread = new SubscribeThread();
            
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
