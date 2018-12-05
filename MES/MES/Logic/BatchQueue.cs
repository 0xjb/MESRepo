﻿using System;
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
        private ILogic _logic;
        private static object _lock = new object();
        private Batch currentBatch;
        public Batch CurrentBatch {
            get { return currentBatch; }
            set {
                currentBatch = value;
                OnPropertyChanged("CurrentBatch");
            }
        }
        private ObservableCollection<Batch> batches = new ObservableCollection<Batch>();

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Batch> Batches {
            get { return batches; }
            set {
                batches = value;
                OnPropertyChanged("Batches");
            }
        }
        public BatchQueue(ILogic logic) {
            _logic = logic;
            _logic.OPC.PropertyChanged += CheckBatchProdStatus;
            BindingOperations.EnableCollectionSynchronization(Batches, _lock);
        }
        protected void OnPropertyChanged(string name) {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) {
                handler(this, new PropertyChangedEventArgs(name));

            }
        }
        public void MoveUp(Batch b) {
            int bIndex = Batches.IndexOf(b);
            if (bIndex != 0) {
                Batch temp = b;
                Batches[bIndex] = Batches[bIndex - 1];
                Batches[bIndex - 1] = temp;
            }
        }
        public void MoveDown(Batch b) {
            int bIndex = Batches.IndexOf(b);
            if (bIndex != Batches.Count - 1) {
                Batch temp = b;
                Batches[bIndex] = Batches[bIndex + 1];
                Batches[bIndex + 1] = temp;
            }
        }
        private void CheckBatchProdStatus(object sender, PropertyChangedEventArgs e) {
            if (e.PropertyName.Equals("StateCurrent")) {
                if ((sender as OpcClient).StateCurrent == 17 && CurrentBatch != null) {
                    if(CurrentBatch != null) {
                        _logic.WriteBatchData();
                        if(Batches.Count >= 1) {
                            CurrentBatch = Batches[0];
                            Batches.RemoveAt(0);
                            _logic.OPC.ResetMachine();
                            System.Threading.Thread.Sleep(3000);
                            _logic.StartProduction();
                        } else {
                            CurrentBatch = null;
                        }
                    }


                }
            }
        }
    }
}
