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
        private ILogic il;
        public void InjectLogic(ILogic logicFacade)
        {
            this.il = logicFacade;
        }
    }
}
