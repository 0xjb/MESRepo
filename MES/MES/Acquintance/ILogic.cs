using MES.Logic;


namespace MES.Acquintance
{
    public interface ILogic
    {
        OpcClient OPC {
            get;
            set;
        }
        void InjectData(IData dataLayer);
        OpcClient GetOPC();

        //SubscribeThread GetSubscribeThread();

    }



}
