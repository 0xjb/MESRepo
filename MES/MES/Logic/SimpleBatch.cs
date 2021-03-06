﻿using MES.Acquintance;
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
        public float Amount
        {
            get { return desiredAmount; }
            set
            {
                desiredAmount = value;
                OnPropertyChanged("Amount");
            }
        }
        public string TimeStart { get; set; }
        public string TimeEnd { get; set; }
        public double OEE { get; set; }
        public float Speed { get; set; }

        public SimpleBatch(float id, float amount, float speed, IRecipe recipe)
        {
            BatchID = id;
            BeerType = recipe.BeerId;
            Speed = speed;
            Amount = amount;
            OEE = 0;
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
