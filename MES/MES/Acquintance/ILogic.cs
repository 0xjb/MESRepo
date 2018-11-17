using MES.Logic;


namespace MES.Acquintance
{
    public interface ILogic
    {
        Simulation GetSimulation { get; set; }

        OpcClient OPC { get; set; }
        void InjectData(IData dataLayer);
        OpcClient GetOPC();
    }
}