using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES.Acquintance {
   public interface ISimpleBatch {
        float BatchID { get; set; }

        float BeerType { get; set; }

        float DesiredAmount { get; set; }

        string TimestampStart { get; set; }

        string TimestampEnd { get; set; }
    }
}
