using MES.Acquintance;

namespace MES.Presentation
{
    class PresentationFacade : IPresentation
    {
        public static ILogic iLogic;

        public void InjectLogic(ILogic logicFacade)
        {
            iLogic = logicFacade;
        }


        public static ILogic GetLogic()
        {
            return iLogic;
        }


    }
}
