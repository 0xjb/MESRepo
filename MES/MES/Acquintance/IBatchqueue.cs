using MES.Logic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES.Acquintance {
    public class IBatchQueue {
        ObservableCollection<Batch> Batches { get; set; }
        Batch CurrentBatch { get; set; }
    }
}
