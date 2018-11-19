using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MES.Acquintance;

namespace MES.Presentation
{
    class PresentationFacade : IPresentation
    {
        private ILogic iLogic;
        

        public ILogic ILogic {
            get { return iLogic; }
            set { iLogic = value; }
        }


        public void InjectLogic(ILogic logicFacade)
        {
            this.iLogic = logicFacade;
        }
    }
}
