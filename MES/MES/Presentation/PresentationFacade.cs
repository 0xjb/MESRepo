using MES.Acquintance;

namespace MES.Presentation
{
    public class PresentationFacade : IPresentation
    {
        private ILogic iLogic;
        public ILogic ILogic
        {
            get { return iLogic; }
            set { iLogic = value; }
        }

        public void InjectLogic(ILogic logicFacade)
        {
            ILogic = logicFacade;
        }

        // TODO deprecate
        public ILogic GetLogic()
        {
            return iLogic;
        }


    }
}
