using MES.Acquintance;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES.Logic {
    public class Batch : INotifyPropertyChanged {
        private string batchID;
        public string BatchID {
            get { return batchID; }
            set {
                batchID = value;
                OnPropertyChanged("BatchID");
            }
        }
        private string beerType;
        public string BeerType {
            get { return beerType; }
            set {
                beerType = value;
                OnPropertyChanged("BeerType");
            }
        }
        private int desiredAmount;
        public int DesiredAmount {
            get { return desiredAmount; }
            set {
                desiredAmount = value;
                OnPropertyChanged("DesiredAmount");
            }
        }
        public Batch(string id, string bt) {
            BatchID = id;
            BeerType = bt;
            DesiredAmount = 10;
        }
        protected void OnPropertyChanged(string name) {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) {
                handler(this, new PropertyChangedEventArgs(name));

            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

}
