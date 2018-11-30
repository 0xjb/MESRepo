using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using MES.Acquintance;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.Remoting.Channels;
using System.Timers;
using System.Windows;
using System.Windows.Media;

namespace MES.Presentation
{
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

        private int indexOfArrayTemp = 0;
        private int indexOfArrayHumid = 0;
        private int indexOfArrayVibra = 0;

        private string statusString;

        private string[] statusArray =
        {
            "Deactivated", "Clearing", "Stopped", "Starting", "Idle", "Suspended", "Execute", "Stopping", "Aborting",
            "Aborted", "Holding", "Held", "none", "none", "none", "Resetting", "Completing", "Complete", "Deactivating",
            "Activating"
        };

        //
        private double status;
        private double _valueMaintenance;
        public event PropertyChangedEventHandler PropertyChanged;

        public ChartValues<ObservableValue> ValuesIngredients { get; set; }
        public SeriesCollection SeriesCollectionIngredients { get; set; }
        public SeriesCollection SeriesCollectionTemperature { get; set; }
        public SeriesCollection SeriesCollectionHumidity { get; set; }
        public SeriesCollection SeriesCollectionVibration { get; set; }
        public string[] LabelsTemperature { get; set; }
        public string[] LabelsHumidity { get; set; }
        public string[] LabelsIngredients { get; set; }
        public string[] LabelsVibration { get; set; }

        //public ArrayList LabelsTemperature { get; set; }
        private List<string> ArrayListLabelsTemperature;
        private List<string> ArrayListLabelsVibration;
        private List<string> ArrayListLabelsHumidity;


        public MainWindow(IPresentation pf)
        {
            this.presentation = pf;

            //Get logiclayer
            iLogic = presentation.ILogic;

            if (!presentation.ILogic.IsSimulationOn)
            {
                //Connects to OPC server
                iLogic.OPC.Connect();
            }


            InitializeComponent();


            DataGridQuedBatches.ItemsSource = presentation.ILogic.Batches.Batches;

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
            var columnSeries = new ColumnSeries
            {
                Title = "[Ingredients]",
                Values = ValuesIngredients,
                DataLabels = true
            };

            //Place valuelabel inside the column
            columnSeries.LabelsPosition = (BarLabelPosition) 3;

            SeriesCollectionIngredients = new SeriesCollection {columnSeries};

            //put label stuff here
            LabelsIngredients = new[] {"Barley", "Hops", "Malt", "Wheat", "Yeast"};

            SeriesCollectionTemperature = new SeriesCollection

            {
                new LineSeries
                {
                    Title = "Temperature",
                    Fill = Brushes.Chartreuse, Stroke = Brushes.Coral, PointGeometrySize = 1,
                    Values = new ChartValues<double> { }
                }
            };

            SeriesCollectionHumidity = new SeriesCollection

            {
                new LineSeries
                {
                    Title = "Humidity",
                    Fill = Brushes.DarkOrange, Stroke = Brushes.DarkOrchid, PointGeometrySize = 1,
                    Values = new ChartValues<double> { }
                }
            };

            SeriesCollectionVibration = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Vibration",
                    Fill = Brushes.Yellow, Stroke = Brushes.DarkSlateGray, PointGeometrySize = 1,
                    Values = new ChartValues<double> { }
                }
            };

            LabelsHumidity = new string[50];
            LabelsTemperature = new string[50];
            LabelsVibration = new string[100];

            ArrayListLabelsTemperature = new List<string>();
            ArrayListLabelsHumidity = new List<string>();
            ArrayListLabelsVibration = new List<string>();

            CheckStatus();
            CheckIngredientsLevel();
            CheckTemperature();
            CheckHumidity();
            CheckVibration();

            DataContext = this;
        }

        private void CheckForChangesIngredientsLevel(object sender, PropertyChangedEventArgs e)
        {
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


        private void CheckIngredientsLevel()
        {
            this.levelBarley = iLogic.OPC.Barley;
            this.levelHops = iLogic.OPC.Hops;
            this.levelMalt = iLogic.OPC.Malt;
            this.levelWheat = iLogic.OPC.Wheat;
            this.levelYeast = iLogic.OPC.Yeast;

            iLogic.OPC.PropertyChanged += CheckForChangesIngredientsLevel;
        }

        private void CheckChangesInTemperature(object sender, PropertyChangedEventArgs e)
        {
            Temperature = iLogic.OPC.TempCurrent;

            ArrayListLabelsTemperature.Add(DateTime.Now.ToString());

            if (e.PropertyName.ToString().Equals("TempCurrent"))
            {
                if (indexOfArrayTemp >= 49)
                {
                    SeriesCollectionTemperature[0].Values.RemoveAt(0);
                    ArrayListLabelsTemperature.RemoveAt(0);
                }

                LabelsTemperature = ArrayListLabelsTemperature.ToArray();

                SeriesCollectionTemperature[0].Values.Add(Temperature);

                indexOfArrayTemp++;
            }
        }

        private void CheckChangesInStatus(object sender, PropertyChangedEventArgs e)
        {
            int index = (int) iLogic.OPC.StateCurrent;
            StatusString = statusArray[index];
        }

        private void CheckChangesInHumidity(object sender, PropertyChangedEventArgs e)
        {
            Humidity = iLogic.OPC.HumidityCurrent;

            ArrayListLabelsHumidity.Add(DateTime.Now.ToString());

            if (e.PropertyName.ToString().Equals("HumidityCurrent"))
            {
                if (indexOfArrayHumid >= 49)
                {
                    SeriesCollectionHumidity[0].Values.RemoveAt(0);
                    ArrayListLabelsHumidity.RemoveAt(0);
                }

                LabelsHumidity = ArrayListLabelsHumidity.ToArray();

                SeriesCollectionHumidity[0].Values.Add(Humidity);
                indexOfArrayHumid++;
            }
        }

        private void CheckChangesInVibration(object sender, PropertyChangedEventArgs e)
        {
            Vibration = iLogic.OPC.VibrationCurrent;

            //ArrayListLabelsHumidity.Add(DateTime.Now.ToString());


            if (e.PropertyName.ToString().Equals("VibrationCurrent"))
            {
                ArrayListLabelsVibration.Add(DateTime.Now.ToString());
                if (indexOfArrayVibra >= 49)
                {
                    SeriesCollectionVibration[0].Values.RemoveAt(0);
                    ArrayListLabelsVibration.RemoveAt(0);
                }

                LabelsVibration = ArrayListLabelsVibration.ToArray();
                //LabelsVibration[0] = ArrayListLabelsVibration[0];

                SeriesCollectionVibration[0].Values.Add(Vibration);

                Console.WriteLine("\n\n * " + ArrayListLabelsVibration[indexOfArrayVibra] + "\n\n" + " *" +
                                  ArrayListLabelsVibration.Count);

                Console.WriteLine("\n\n * " + ArrayListLabelsVibration[indexOfArrayVibra] + "\n\n" );
                indexOfArrayVibra++;
            }
        }


        private void CheckTemperature()
        {
            temperature = iLogic.OPC.TempCurrent;
            iLogic.OPC.PropertyChanged += CheckChangesInTemperature;
        }

        private void CheckStatus()
        {
            int index = (int) iLogic.OPC.StateCurrent;
            txtStatus.Text = statusArray[index];
            iLogic.OPC.PropertyChanged += CheckChangesInStatus;
        }

        private void CheckHumidity()
        {
            humidity = iLogic.OPC.HumidityCurrent;
            iLogic.OPC.PropertyChanged += CheckChangesInHumidity;
        }

        private void CheckVibration()
        {
            vibration = iLogic.OPC.VibrationCurrent;
            iLogic.OPC.PropertyChanged += CheckChangesInVibration;
        }

        private void EventHandling(object sender, NotifyCollectionChangedEventArgs e)
        {
            IAlarmObject s = e.NewItems[0] as IAlarmObject;
            if (s.StopReason == "Empty inventory" || s.StopReason == "Maintenance")
            {
                Dispatcher.BeginInvoke(new Action(delegate { ActivateAlarmWindow(s); }));
            }
        }

        private void ActivateAlarmWindow(IAlarmObject alarmObject)
        {
            PopupAlarm pop = new PopupAlarm(alarmObject);
            pop.Show();
        }

        //Button handling

        #region XAML generated code

        void MainWindow_Closed(object sender, EventArgs e)
        {
            //Put your close code here
            //iLogic.OPC.StopMachine();
        }


        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            //opc.StartMachine(1, 2, 2000, 600);
            iLogic.OPC.StartMachine(1, 2, 200, 100);
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
            Alarms alarms = new Alarms(presentation, this);
            this.Hide();
            alarms.Show();
        }

        private void btnOEE_Click(object sender, RoutedEventArgs e)
        {
            OEE oEE = new OEE(presentation, this);
            this.Hide();
            oEE.Show();
        }

        private void btnOptimization_Click(object sender, RoutedEventArgs e)
        {
            Optimization optimization = new Optimization(presentation, this);
            this.Hide();
            optimization.Show();
        }

        private void btnHistory_Click(object sender, RoutedEventArgs e)
        {
            History history = new History(presentation, this);
            this.Hide();
            history.Show();
        }

        private void btnBatchSetup_Click(object sender, RoutedEventArgs e)
        {
            BatchSetup batchSetup = new BatchSetup(presentation, this);
            this.Hide();
            batchSetup.Show();
        }

        private void btnMachineSettings_Click(object sender, RoutedEventArgs e)
        {
            Simulation simulation = new Simulation(presentation, presentation.ILogic.IsSimulationOn, this);
            this.Hide();
            simulation.Show();
        }

        #endregion

        public double LevelBarley
        {
            get { return levelBarley; }

            set
            {
                levelBarley = value;
                OnPropertyChanged("SeriesCollectionIngredients");
            }
        }

        public double LevelHops
        {
            get { return levelHops; }

            set
            {
                levelHops = value;
                OnPropertyChanged("SeriesCollectionIngredients");
            }
        }

        public double LevelMalt
        {
            get { return levelMalt; }

            set
            {
                levelMalt = value;
                OnPropertyChanged("SeriesCollectionIngredients");
            }
        }

        public double LevelWheat
        {
            get { return levelWheat; }

            set
            {
                levelWheat = value;
                OnPropertyChanged("SeriesCollectionIngredients");
            }
        }

        public double LevelYeast
        {
            get { return levelYeast; }

            set
            {
                levelYeast = value;
                OnPropertyChanged("SeriesCollectionIngredients");
            }
        }


        public double ValueMaintenance
        {
            get { return _valueMaintenance; }
            set
            {
                _valueMaintenance = value;
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

        public string StatusString
        {
            get { return statusString; }
            set
            {
                statusString = value;
                OnPropertyChanged("StatusString");
            }
        }


        public IPresentation PresentationFacade
        {
            get { return presentation; }
            set { presentation = value; }
        }

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