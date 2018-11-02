using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MES.Acquintance;

namespace MES.Logic
{
    class LogicFacade : MES.Acquintance.ILogic
    {
        private IData data;
        public void injectData(IData dataLayer)
        {
            data = dataLayer;
        }
    }
}
