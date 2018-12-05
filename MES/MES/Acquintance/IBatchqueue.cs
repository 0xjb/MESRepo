using MES.Logic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES.Acquintance {
    public class IBatchQueue {
        ObservableCollection<SimpleBatch> Batches { get; set; }
        SimpleBatch CurrentBatch { get; set; }
    }
}
