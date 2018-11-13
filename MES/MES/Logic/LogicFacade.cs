using MES.Acquintance;

namespace MES.Logic
{

    public class LogicFacade : ILogic
    {
        OpcClient opc = new OpcClient();

        private IData data;
        public void InjectData(IData dataLayer)
        {
            data = dataLayer;
        }

        public OpcClient getOPC()
        {
            return opc;
        }
    }
}
