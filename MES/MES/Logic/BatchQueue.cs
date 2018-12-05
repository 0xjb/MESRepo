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
        private SimpleBatch currentBatch;
        public SimpleBatch CurrentBatch {
            get { return currentBatch; }
            set { currentBatch = value;
                OnPropertyChanged("CurrentBatch"); }
        }
        private ObservableCollection<SimpleBatch> batches = new ObservableCollection<SimpleBatch>();

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<SimpleBatch> Batches {
            get { return batches; }
            set { batches = value;
                OnPropertyChanged("Batches");
            }
        }
        public BatchQueue(OpcClient c) {
           c.PropertyChanged += CheckBatchProdStatus;
            BindingOperations.EnableCollectionSynchronization(Batches, _lock);
        }
        protected void OnPropertyChanged(string name) {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) {
                handler(this, new PropertyChangedEventArgs(name));

            }
        }
        public void MoveUp(SimpleBatch b) {
            int bIndex = Batches.IndexOf(b);
            if(bIndex != 0) {
                SimpleBatch temp = b;
                Batches[bIndex] = Batches[bIndex - 1];
                Batches[bIndex - 1] = temp;
            }
        }
        public void MoveDown(SimpleBatch b) {
            int bIndex = Batches.IndexOf(b);
            if (bIndex != Batches.Count-1) {
                SimpleBatch temp = b;
                Batches[bIndex] = Batches[bIndex + 1];
                Batches[bIndex + 1] = temp;
            }
        }
        private void CheckBatchProdStatus(object sender, PropertyChangedEventArgs e) {
           if(e.PropertyName.Equals("StateCurrent")) {
                if((sender as OpcClient).StateCurrent == 17) {
                    if(CurrentBatch != null) {

                    }
                    try {
                        CurrentBatch = Batches[0];
                        Batches.RemoveAt(0);
                    } catch(ArgumentOutOfRangeException ex) {
                        Console.WriteLine(":)");
                    }
                        
                    

                }
            }
        }
    }
}
