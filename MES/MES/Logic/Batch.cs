using MES.Acquintance;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES.Logic {
    public class Batch : INotifyPropertyChanged {
        private double batchID;
        public double BatchID {
            get { return batchID; }
            set {
                batchID = value;
                OnPropertyChanged("BatchID");
            }
        }
        private double beerType;
        public double BeerType {
            get { return beerType; }
            set {
                beerType = value;
                OnPropertyChanged("BeerType");
            }
        }
        private double desiredAmount;
        public double DesiredAmount {
            get { return desiredAmount; }
            set {
                desiredAmount = value;
                OnPropertyChanged("DesiredAmount");
            }
        }
        public Batch(double id, double bt, double amount) {
            BatchID = id;
            BeerType = bt;
            DesiredAmount = amount;
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
