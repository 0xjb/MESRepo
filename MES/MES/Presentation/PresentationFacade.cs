using MES.Acquintance;

namespace MES.Presentation
{
    class PresentationFacade : IPresentation
    {
        public ILogic iLogic;

        public void InjectLogic(ILogic logicFacade)
        {
            iLogic = logicFacade;
        }


        public ILogic GetLogic()
        {
            return iLogic;
        }


    }
}
