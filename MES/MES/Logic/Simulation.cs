using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using MES.Annotations;
using UnifiedAutomation.UaClient;

namespace MES.Logic
{
    public class Simulation : INotifyPropertyChanged

    {
        private OpcClient opc;

        //Level "Barley", "Hops", "Malt", "Wheat", "Yeast" 
        private double levelBarley;
        private double levelHops;
        private double levelMalt;
        private double levelWheat;
        private double levelYeast;

        public Simulation(OpcClient opcClient)
        {
            this.opc = opcClient;
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
            LevelBarley -= 0.43;
            LevelHops -= 0.15;
            LevelMalt -= 0.22;
            LevelWheat -= 0.23;
            LevelYeast -= 0.33;

            Console.WriteLine("Barley sim " + LevelBarley);
            Console.WriteLine("Hops sim " + LevelHops);
            Console.WriteLine("Malt sim " + LevelMalt);
            Console.WriteLine("Wheat sim " + LevelWheat);
            Console.WriteLine("Yeast sim " + LevelYeast);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    
    }
}