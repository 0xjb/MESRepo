using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MES.Acquintance;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Data;

namespace MES.Logic {

    public class BatchQueue : IBatchQueue, INotifyPropertyChanged {
        private static object _lock = new object();
        private ILogic logic;
        private ISimpleBatch currentBatch;
        public ISimpleBatch CurrentBatch {
            get { return currentBatch; }
            set {
                currentBatch = value;
                OnPropertyChanged("CurrentBatch");
            }
        }
        private ObservableCollection<ISimpleBatch> batches = new ObservableCollection<ISimpleBatch>();

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<ISimpleBatch> Batches {
            get { return batches; }
            set {
                batches = value;
                OnPropertyChanged("Batches");
            }
        }
        public BatchQueue(ILogic l) {
            logic = l;
            l.OPC.PropertyChanged += CheckBatchProdStatus;
            BindingOperations.EnableCollectionSynchronization(Batches, _lock);
        }
        protected void OnPropertyChanged(string name) {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) {
                handler(this, new PropertyChangedEventArgs(name));

            }
        }
        public void MoveUp(ISimpleBatch b) {
            int bIndex = Batches.IndexOf(b);
            if (bIndex != 0) {
                ISimpleBatch temp = b;
                Batches[bIndex] = Batches[bIndex - 1];
                Batches[bIndex - 1] = temp;
            }
        }
        public void MoveDown(ISimpleBatch b) {
            int bIndex = Batches.IndexOf(b);
            if (bIndex != Batches.Count - 1) {
                ISimpleBatch temp = b;
                Batches[bIndex] = Batches[bIndex + 1];
                Batches[bIndex + 1] = temp;
            }
        }
        private void CheckBatchProdStatus(object sender, PropertyChangedEventArgs e) {
            if (e.PropertyName.Equals("StateCurrent")) {
                if ((sender as OpcClient).StateCurrent == 17) {
                    if (CurrentBatch != null) {
                        CurrentBatch.TimestampEnd = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss.fff");
                        OEE currentOEE = new OEE((int)logic.OPC.AcceptableProducts, (int)logic.OPC.DefectProducts, currentBatch.TimestampStart, currentBatch.TimestampEnd, (int)currentBatch.MachineSpeed);
                        currentBatch.OEE = currentOEE.CalculateOEE();
                        logic.SaveBatch(CurrentBatch);
                        if (Batches.Count >= 1) {
                            CurrentBatch = Batches[0];
                            Batches.RemoveAt(0);
                        } else {
                            CurrentBatch = null;
                        }
                    }
                }
            }
        }
    }
}
