﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MES.Acquintance
{
    public interface ILogic 
    {
        void injectData(IData dataLayer);
    }
}
