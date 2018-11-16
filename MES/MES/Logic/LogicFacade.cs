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
        public void InjectData(IData dataLayer)
        {
            data = dataLayer;
        }

        public OpcClient GetOPC()
        {
            return opc;
        }


    }
}
