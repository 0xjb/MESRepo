namespace MES.Acquintance
{
    public interface IPresentation
    {
        void InjectLogic(ILogic logicFacade);
        ILogic GetLogic();
    }
}
