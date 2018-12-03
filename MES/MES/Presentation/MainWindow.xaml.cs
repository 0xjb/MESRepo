using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using MES.Acquintance;
using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.Remoting.Channels;
using System.Threading;
using System.Windows;

namespace MES.Presentation {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window, INotifyPropertyChanged // IObservableChartPoint//,INotifyPropertyChanged
    {
        private ILogic iLogic;
        private IPresentation presentation;

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

        public ChartValues<ObservableValue> ValuesIngredients { get; set; }
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }

        public MainWindow(IPresentation pf) {
            this.presentation = pf;

            //Get logiclayer
            iLogic = presentation.ILogic;

            
            
            iLogic.ErrorHandler.Alarms.CollectionChanged += EventHandling;

            if (!presentation.ILogic.IsSimulationOn) {
                //Connects to OPC server
                iLogic.OPC.Connect();
            }


            InitializeComponent();
            ////Do stuff when closing window
            this.Closed += new EventHandler(MainWindow_Closed);

            ValuesIngredients = new ChartValues<ObservableValue>
            {
                new ObservableValue(LevelBarley),
                new ObservableValue(LevelHops),
                new ObservableValue(LevelMalt),
                new ObservableValue(LevelWheat),
                new ObservableValue(LevelYeast)
            };
            var columnSeries = new ColumnSeries {
                Title = "[Ingredients]",
                Values = ValuesIngredients,
                DataLabels = true
            };

            //Place valuelabel inside the column
            columnSeries.LabelsPosition = (BarLabelPosition)3;
            SeriesCollection = new SeriesCollection { columnSeries };

            //put label stuff here
            Labels = new[] { "Barley", "Hops", "Malt", "Wheat", "Yeast" };
            Formatter = value => value.ToString("N");

            CheckIngredientsLevel();

            DataContext = this;
        }

        private void checkForChangesIngredientsLevel(object sender, PropertyChangedEventArgs e) {

            ValuesIngredients[0].Value = LevelBarley;
            ValuesIngredients[1].Value = LevelHops;
            ValuesIngredients[2].Value = LevelMalt;
            ValuesIngredients[3].Value = LevelWheat;
            ValuesIngredients[4].Value = LevelWheat;

            LevelBarley = iLogic.OPC.Barley;
            LevelHops = iLogic.OPC.Hops;
            LevelMalt = iLogic.OPC.Malt;
            LevelWheat = iLogic.OPC.Wheat;
            LevelYeast = iLogic.OPC.Yeast;


        }


        private void CheckIngredientsLevel() {
            //if (presentation.ILogic.IsSimulationOn)
            //{
            //    this.levelBarley = iLogic.TestSimulation.LevelBarley;
            //    this.levelHops = iLogic.TestSimulation.LevelHops;
            //    this.levelMalt = iLogic.TestSimulation.LevelMalt;
            //    this.levelWheat = iLogic.TestSimulation.LevelWheat;
            //    this.levelYeast = iLogic.TestSimulation.LevelYeast;

            //    iLogic.TestSimulation.PropertyChanged += checkForChangesIngredientsLevel;
            //}
            //else
            //{
            this.levelBarley = iLogic.OPC.Barley;
            this.levelHops = iLogic.OPC.Hops;
            this.levelMalt = iLogic.OPC.Malt;
            this.levelWheat = iLogic.OPC.Wheat;
            this.levelYeast = iLogic.OPC.Yeast;

            iLogic.OPC.PropertyChanged += checkForChangesIngredientsLevel;
            //}
        }

        private void EventHandling(object sender, NotifyCollectionChangedEventArgs e) {
            IAlarmObject s = e.NewItems[0] as IAlarmObject;
            if (s.StopReason == "Empty inventory" || s.StopReason == "Maintenance") {
                Dispatcher.BeginInvoke(new Action(delegate { ActivateAlarmWindow(s); }));
            }
        }

        private void ActivateAlarmWindow(IAlarmObject alarmObject) {
            PopupAlarm pop = new PopupAlarm(alarmObject);
            pop.Show();
        }

        //Button handling

        #region XAML generated code

        void MainWindow_Closed(object sender, EventArgs e) {
            //Put your close code here
            //iLogic.OPC.StopMachine();
        }


        private void btnStart_Click(object sender, RoutedEventArgs e) {
            //opc.StartMachine(1, 2, 2000, 600);
            iLogic.OPC.StartMachine(1, 2, 200, 100);
            iLogic.ErrorHandler.AddAlarm(10, 11);
        }

        private void btnStop_Click(object sender, RoutedEventArgs e) {
            //opc.StopMachine();
            iLogic.OPC.StopMachine();
        }

        private void btnReset_Click(object sender, RoutedEventArgs e) {
            //opc.ResetMachine();
            iLogic.OPC.ResetMachine();
        }

        private void btnAbort_Click(object sender, RoutedEventArgs e) {
            //opc.AbortMachine();
            iLogic.OPC.AbortMachine();
        }

        private void btnClear_Click(object sender, RoutedEventArgs e) {
            //opc.ClearMachine();
            iLogic.OPC.ClearMachine();
        }

        private void btnAlarms_Click(object sender, RoutedEventArgs e) {
            Alarms alarms = new Alarms(presentation, this);
            this.Hide();
            alarms.Show();
        }

        private void btnOEE_Click(object sender, RoutedEventArgs e) {
            OEE oEE = new OEE(presentation, this);
            this.Hide();
            oEE.Show();
        }

        private void btnOptimization_Click(object sender, RoutedEventArgs e) {
            Optimization optimization = new Optimization(presentation, this);
            this.Hide();
            optimization.Show();
        }

        private void btnHistory_Click(object sender, RoutedEventArgs e) {
            History history = new History(presentation, this);
            this.Hide();
            history.Show();
        }

        private void btnBatchSetup_Click(object sender, RoutedEventArgs e) {
            BatchSetup batchSetup = new BatchSetup(presentation, this);
            this.Hide();
            batchSetup.Show();
        }

        private void btnMachineSettings_Click(object sender, RoutedEventArgs e) {
            Simulation simulation = new Simulation(presentation, presentation.ILogic.IsSimulationOn, this);
            this.Hide();
            simulation.Show();
        }

        #endregion

        public double LevelBarley {
            get { return levelBarley; }

            set {
                levelBarley = value;
                OnPropertyChanged("SeriesCollection");
            }
        }

        public double LevelHops {
            get { return levelHops; }

            set {
                levelHops = value;
                OnPropertyChanged("SeriesCollection");
            }
        }

        public double LevelMalt {
            get { return levelMalt; }

            set {
                levelMalt = value;
                OnPropertyChanged("SeriesCollection");
            }
        }

        public double LevelWheat {
            get { return levelWheat; }

            set {
                levelWheat = value;
                OnPropertyChanged("SeriesCollection");
            }
        }

        public double LevelYeast {
            get { return levelYeast; }

            set {
                levelYeast = value;
                OnPropertyChanged("SeriesCollection");
            }
        }


        public double ValueMaintenance {
            get { return _value; }
            set {
                _value = value;
                OnPropertyChanged("ValueMaintenance");
            }
        }

        public double MachineSpeed {
            get { return machineSpeed; }

            set {
                machineSpeed = value;
                OnPropertyChanged("MachineSpeed");
            }
        }

        public double Temperature {
            get { return temperature; }

            set {
                temperature = value;
                OnPropertyChanged("Temperature");
            }
        }

        public double Humidity {
            get { return humidity; }

            set {
                humidity = value;
                OnPropertyChanged("Humidity");
            }
        }

        public double Vibration {
            get { return vibration; }

            set {
                vibration = value;
                OnPropertyChanged("Vibration");
            }
        }

        public double BatchID {
            get { return batchID; }

            set {
                batchID = value;
                OnPropertyChanged("Batch_ID");
            }
        }

        public double Amount {
            get { return amount; }

            set {
                amount = value;
                OnPropertyChanged("Amount");
            }
        }


        public double Produced {
            get { return produced; }

            set {
                produced = value;
                OnPropertyChanged("Produced");
            }
        }

        public double AcceptableProducts {
            get { return acceptableProducts; }

            set {
                acceptableProducts = value;
                OnPropertyChanged("AcceptableProducts");
            }
        }

        public double DefectProducts {
            get { return defectProducts; }

            set {
                defectProducts = value;
                OnPropertyChanged("DefectProducts");
            }
        }

        public double Status {
            get { return status; }

            set {
                status = value;
                OnPropertyChanged("Status");
            }
        }

        public IPresentation PresentationFacade {
            get { return presentation; }
            set { presentation = value; }
        }

        protected void OnPropertyChanged(string name) {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        private void button_Click(object sender, RoutedEventArgs e) {
            int number = 0;
            Random randomNumber = new Random();
            number = randomNumber.Next(19, 26);
            ValueMaintenance = randomNumber.Next(1, 100);
        }
    }
}