using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Timers;
using MES.Annotations;
using UnifiedAutomation.UaClient;
using System;
using System.Timers;
using Timer = System.Timers.Timer;

namespace MES.Logic
{
    public class TestSimulation : INotifyPropertyChanged

    {
        private OpcClient opc;
        private System.Timers.Timer timerTemp;

        //Level "Barley", "Hops", "Malt", "Wheat", "Yeast" 
        private double levelBarley;
        private double levelHops;
        private double levelMalt;
        private double levelWheat;
        private double levelYeast;

        public TestSimulation(OpcClient opcClient)
        {
            timerTemp = new Timer();
            timerTemp.Interval = 5000;
            timerTemp.AutoReset = true;
            timerTemp.Enabled = true;
            timerTemp.Elapsed += OnTimedEvent;

            this.opc = opcClient;
            opc.TempCurrent = 35;

            levelBarley = 100;
            levelHops = 100;
            levelMalt = 100;
            levelWheat = 100;
            levelYeast = 100;
            opc.PropertyChanged += CheckForChangesIngredientsLevel;
        }

        public double LevelBarley
        {
            get => levelBarley;
            set
            {
                if (value.Equals(levelBarley)) return;
                levelBarley = value;
                OnPropertyChanged();
            }
        }

        public double LevelHops
        {
            get => levelHops;
            set
            {
                if (value.Equals(levelHops)) return;
                levelHops = value;
                OnPropertyChanged();
            }
        }

        public double LevelMalt
        {
            get => levelMalt;
            set
            {
                if (value.Equals(levelMalt)) return;
                levelMalt = value;
                OnPropertyChanged();
            }
        }

        public double LevelWheat
        {
            get => levelWheat;
            set
            {
                if (value.Equals(levelWheat)) return;
                levelWheat = value;
                OnPropertyChanged();
            }
        }

        public double LevelYeast
        {
            get => levelYeast;
            set
            {
                if (value.Equals(levelYeast)) return;
                levelYeast = value;
                OnPropertyChanged();
            }
        }

        private void CheckForChangesIngredientsLevel(object sender, PropertyChangedEventArgs e)
        {
            if (opc.StateCurrent == 6)
            {
                LevelBarley -= 0.43;
                LevelHops -= 0.15;
                LevelMalt -= 0.22;
                LevelWheat -= 0.23;
                LevelYeast -= 0.33;
            }
            
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        bool b = false;

        private void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            Random random = new Random();
            
            double number = random.NextDouble()/8;
            number = Math.Round(number, 2);

            if (b == false)
            {
                opc.TempCurrent += number;
                b = true;
            }
            else
            {
                opc.TempCurrent -=  number;
                b = false;
            }
        }
    }
}