using MES.Logic;


namespace MES.Acquintance
{
    public interface ILogic
    {
        void InjectData(IData dataLayer);
        OpcClient GetOPC();

        //SubscribeThread GetSubscribeThread();

    }



}
