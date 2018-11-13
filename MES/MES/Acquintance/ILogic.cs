using MES.Logic;


namespace MES.Acquintance
{
    public interface ILogic
    {
        void InjectData(IData dataLayer);
        OpcClient getOPC();

    }



}
