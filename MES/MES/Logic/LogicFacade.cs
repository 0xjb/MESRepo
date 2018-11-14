using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MES.Acquintance;

namespace MES.Logic
{

    public class LogicFacade : ILogic
    {
        private IData data;
        public void InjectData(IData dataLayer)
        {
            data = dataLayer;
        }
    }
}
