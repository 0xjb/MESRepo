using MES.Acquintance;
using System.ComponentModel;

namespace MES.Logic
{
    public class SimpleBatch : INotifyPropertyChanged, ISimpleBatch
    {
        private float batchID;
        public float BatchID
        {
            get { return batchID; }
            set
            {
                batchID = value;
                OnPropertyChanged("BatchID");
            }
        }
        private float beerType;
        public float BeerType
        {
            get { return beerType; }
            set
            {
                beerType = value;
                OnPropertyChanged("BeerType");
            }
        }
        private float desiredAmount;
        public float DesiredAmount
        {
            get { return desiredAmount; }
            set
            {
                desiredAmount = value;
                OnPropertyChanged("DesiredAmount");
            }
        }
        public string TimestampStart { get; set; }
        public string TimestampEnd { get; set; }
        public double OEE { get; set; }

        public SimpleBatch(float id, float bt, float amount)
        {
            BatchID = id;
            BeerType = bt;
            DesiredAmount = amount;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }

}
