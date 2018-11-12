using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MES.Acquintance
{
    public interface ILogic 
    {
        void InjectData(IData dataLayer);
        void CreateBatch(float batchId, float amount, float machineSpeed, float productType);
    }
}
