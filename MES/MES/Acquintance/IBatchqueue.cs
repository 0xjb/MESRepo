using MES.Logic;
using System.Collections.ObjectModel;

namespace MES.Acquintance
{
    public class IBatchQueue
    {
        ObservableCollection<SimpleBatch> Batches { get; set; }
        SimpleBatch CurrentBatch { get; set; }
    }
}
