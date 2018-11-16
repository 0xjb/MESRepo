namespace MES.Acquintance
{
    public interface IPresentation
    {
        ILogic ILogic {
            get;
            set;
        }
        
        void InjectLogic(ILogic logicFacade);
        ILogic GetLogic();
    }
}
