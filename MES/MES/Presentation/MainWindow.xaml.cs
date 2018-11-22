using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using MES.Acquintance;
using System;
using System.ComponentModel;
using System.Threading;
using System.Windows;

namespace MES.Presentation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged // IObservableChartPoint//,INotifyPropertyChanged
    {
        
        private ILogic iLogic;
        private IPresentation presentationFacade;
        //private Alarms alarms;

        //Level "Barley", "Hops", "Malt", "Wheat", "Yeast" 
        private double levelBarley;
        private double levelHops;
        private double levelMalt;
        private double levelWheat;
        private double levelYeast;

        //
        private double machineSpeed;
        private double temperature;
        private double humidity;
        private double vibration;
        private double batchID;
        private double amount;
        private double produced;
        private double acceptableProducts;

        private double defectProducts;

        //
        private double status;
        private double _value;
        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindow(IPresentation pf)
        {

            Console.WriteLine("\n\n\n HELLO FROM MAIN WINDOW\n\n\n");
            this.presentationFacade = pf;
            //Get logiclayer
            iLogic = presentationFacade.ILogic;
           
            //
            CheckIfSimulationIsOn();

            //Connects to OPC server
            iLogic.OPC.Connect();

            InitializeComponent();
            //alarms = new Alarms(presentationFacade, this);
            //Do stuff when closing window
            this.Closed += new EventHandler(MainWindow_Closed);

            ValuesIngredients = new ChartValues<ObservableValue>
            {
                new ObservableValue(LevelBarley),
                new ObservableValue(LevelHops),
                new ObservableValue(LevelMalt),
                new ObservableValue(LevelWheat),
                new ObservableValue(LevelYeast)
            };
            var columnSeries = new ColumnSeries
            {
                Title = "[Ingredients]",
                Values = ValuesIngredients,
                DataLabels = true
            };
            //Place valuelabel inside the column
            columnSeries.LabelsPosition = (BarLabelPosition) 3;
            SeriesCollection = new SeriesCollection {columnSeries};

            //put label stuff here
            Labels = new[] {"Barley", "Hops", "Malt", "Wheat", "Yeast"};
            Formatter = value => value.ToString("N");
            DataContext = this;
        }

        private void checkForChangesIngredientsLevel(object sender, PropertyChangedEventArgs e)
        {
            levelBarley = iLogic.GetTestSimulation.LevelBarley;
            levelHops = iLogic.GetTestSimulation.LevelHops;
            levelMalt = iLogic.GetTestSimulation.LevelMalt;
            levelWheat = iLogic.GetTestSimulation.LevelWheat;
            levelYeast = iLogic.GetTestSimulation.LevelYeast;

            ValuesIngredients[0].Value = LevelBarley;
            ValuesIngredients[1].Value = LevelHops;
            ValuesIngredients[2].Value = LevelMalt;
            ValuesIngredients[3].Value = LevelWheat;
            ValuesIngredients[4].Value = LevelWheat;
        }


        private void CheckIfSimulationIsOn()
        {
            if (presentationFacade.ILogic.IsSimulationOn)
            {
                this.levelBarley = iLogic.GetTestSimulation.LevelBarley;
                this.levelHops = iLogic.GetTestSimulation.LevelHops;
                this.levelMalt = iLogic.GetTestSimulation.LevelMalt;
                this.levelWheat = iLogic.GetTestSimulation.LevelWheat;
                this.levelYeast = iLogic.GetTestSimulation.LevelYeast;

                iLogic.GetTestSimulation.PropertyChanged += checkForChangesIngredientsLevel;
            }
        }

        public ChartValues<ObservableValue> ValuesIngredients { get; set; }


        public SeriesCollection SeriesCollection { get; set; }

        public string[] Labels { get; set; }

        public Func<double, string> Formatter { get; set; }


        void MainWindow_Closed(object sender, EventArgs e)
        {
            //Put your close code here
            //iLogic.OPC.StopMachine();
        }


        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            //opc.StartMachine(1, 2, 2000, 600);
            iLogic.OPC.StartMachine(1, 2, 200, 600);
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            //opc.StopMachine();
            iLogic.OPC.StopMachine();
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            //opc.ResetMachine();
            iLogic.OPC.ResetMachine();
        }

        private void btnAbort_Click(object sender, RoutedEventArgs e)
        {
            //opc.AbortMachine();
            iLogic.OPC.AbortMachine();
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            //opc.ClearMachine();
            iLogic.OPC.ClearMachine();
        }

        private void btnAlarms_Click(object sender, RoutedEventArgs e)
        {
            Alarms alarms = new Alarms(presentationFacade, this);
            this.Hide();
            alarms.Show();
        }

        private void btnOEE_Click(object sender, RoutedEventArgs e)
        {
            OEE oEE = new OEE(presentationFacade, this);
            this.Hide();
            oEE.Show();
        }

        private void btnOptimization_Click(object sender, RoutedEventArgs e)
        {
            Optimization optimization = new Optimization(presentationFacade, this);
            this.Hide();
            optimization.Show();
        }

        private void btnHistory_Click(object sender, RoutedEventArgs e)
        {
            History history = new History(presentationFacade, this);
            this.Hide();
            history.Show();
        }

        private void btnBatchSetup_Click(object sender, RoutedEventArgs e)
        {
            BatchSetup batchSetup = new BatchSetup(presentationFacade, this);
            this.Hide();
            batchSetup.Show();
        }

        private void btnMachineSettings_Click(object sender, RoutedEventArgs e)
        {
            Simulation simulation = new Simulation(presentationFacade, presentationFacade.ILogic.IsSimulationOn, this);
            this.Hide();
            simulation.Show();
        }

        public double LevelBarley

        {
            get { return levelBarley; }

            set

            {
                levelBarley = value;
                OnPropertyChanged("SeriesCollection");
            }
        }

        public double LevelHops

        {
            get { return levelHops; }

            set

            {
                levelHops = value;
                OnPropertyChanged("SeriesCollection");
            }
        }

        public double LevelMalt

        {
            get { return levelMalt; }

            set

            {
                levelMalt = value;
                OnPropertyChanged("SeriesCollection");
            }
        }

        public double LevelWheat

        {
            get { return levelWheat; }

            set

            {
                levelWheat = value;
                OnPropertyChanged("SeriesCollection");
            }
        }

        public double LevelYeast

        {
            get { return levelYeast; }

            set

            {
                levelYeast = value;
                OnPropertyChanged("SeriesCollection");
            }
        }


        public double ValueMaintenance

        {
            get { return _value; }
            set
            {
                _value = value;
                OnPropertyChanged("ValueMaintenance");
            }
        }

        public double MachineSpeed

        {
            get { return machineSpeed; }

            set

            {
                machineSpeed = value;
                OnPropertyChanged("MachineSpeed");
            }
        }

        public double Temperature

        {
            get { return temperature; }

            set

            {
                temperature = value;
                OnPropertyChanged("Temperature");
            }
        }

        public double Humidity

        {
            get { return humidity; }

            set

            {
                humidity = value;
                OnPropertyChanged("Humidity");
            }
        }

        public double Vibration

        {
            get { return vibration; }

            set

            {
                vibration = value;
                OnPropertyChanged("Vibration");
            }
        }

        public double BatchID

        {
            get { return batchID; }

            set

            {
                batchID = value;
                OnPropertyChanged("Batch_ID");
            }
        }

        public double Amount

        {
            get { return amount; }

            set

            {
                amount = value;
                OnPropertyChanged("Amount");
            }
        }


        public double Produced

        {
            get { return produced; }

            set

            {
                produced = value;
                OnPropertyChanged("Produced");
            }
        }

        public double AcceptableProducts

        {
            get { return acceptableProducts; }

            set

            {
                acceptableProducts = value;
                OnPropertyChanged("AcceptableProducts");
            }
        }

        public double DefectProducts

        {
            get { return defectProducts; }

            set

            {
                defectProducts = value;
                OnPropertyChanged("DefectProducts");
            }
        }

        public double Status

        {
            get { return status; }

            set

            {
                status = value;
                OnPropertyChanged("Status");
            }
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }


        private void button_Click(object sender, RoutedEventArgs e)
        {
            int number = 0;
            Random randomNumber = new Random();
            number = randomNumber.Next(19, 26);
            ValueMaintenance = randomNumber.Next(1, 100);
        }

        public IPresentation PresentationFacade
        {
            get { return presentationFacade; }
            set { presentationFacade = value; }
        }
    }
}