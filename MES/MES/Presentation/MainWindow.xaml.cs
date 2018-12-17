using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using MES.Acquintance;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
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
        public string[] LabelsIngredients { get; set; }

        private List<string> arrayListLabelsTemperature;
        private List<string> arrayListLabelsVibration;
        private List<string> arrayListLabelsHumidity;
        private System.Timers.Timer timerTrend;

        public MainWindow(IPresentation pf)
        {
            this.presentation = pf;

            //Get logiclayer
            iLogic = presentation.ILogic;

            iLogic.ErrorHandler.Alarms.CollectionChanged += EventHandling;

            if (!presentation.ILogic.IsSimulationOn)
            {
                //Connects to OPC server
                iLogic.OPC.Connect();
            }

            timerTrend = new Timer();
            timerTrend.Interval = 5000;
            timerTrend.AutoReset = true;
            timerTrend.Enabled = true;
            timerTrend.Elapsed += OnTimedEvent;

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
            columnSeries.LabelsPosition = (BarLabelPosition)3;

            SeriesCollectionIngredients = new SeriesCollection { columnSeries };

            //put label stuff here
            LabelsIngredients = new[] { "Barley", "Hops", "Malt", "Wheat", "Yeast" };

            SeriesCollectionTemperature = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Temperature",
                    Fill = Brushes.Chartreuse, Stroke = Brushes.Coral, PointGeometrySize = 5,
                    Values = new ChartValues<double> { }
                }
            };

            SeriesCollectionHumidity = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Humidity",
                    Fill = Brushes.DarkOrange, Stroke = Brushes.DarkOrchid, PointGeometrySize = 5,
                    Values = new ChartValues<double> { }
                }
            };

            SeriesCollectionVibration = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Vibration",
                    Fill = Brushes.Yellow, Stroke = Brushes.DarkSlateGray, PointGeometrySize = 5,
                    Values = new ChartValues<double> { }
                }
            };

            arrayListLabelsTemperature = new List<string>();
            arrayListLabelsHumidity = new List<string>();
            arrayListLabelsVibration = new List<string>();

            CheckStatus();
            CheckIngredientsLevel();
            DataContext = this;
        }

        private void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            double Temperature = iLogic.OPC.TempCurrent;
            double Humidity = iLogic.OPC.HumidityCurrent;
            double Vibration = iLogic.OPC.VibrationCurrent;

            if (arrayListLabelsTemperature.Count >= 50)
            {
                SeriesCollectionTemperature[0].Values.RemoveAt(0);
                arrayListLabelsTemperature.RemoveAt(0);
            }

            SeriesCollectionTemperature[0].Values.Add(Temperature);
            arrayListLabelsTemperature.Add(DateTime.Now.ToString());

            if (arrayListLabelsHumidity.Count >= 50)
            {
                SeriesCollectionHumidity[0].Values.RemoveAt(0);
                arrayListLabelsHumidity.RemoveAt(0);
            }

            SeriesCollectionHumidity[0].Values.Add(Humidity);
            arrayListLabelsHumidity.Add(DateTime.Now.ToString());

            if (arrayListLabelsVibration.Count >= 50)
            {
                SeriesCollectionVibration[0].Values.RemoveAt(0);
                arrayListLabelsVibration.RemoveAt(0);
            }

            SeriesCollectionVibration[0].Values.Add(Vibration);
            arrayListLabelsVibration.Add(DateTime.Now.ToString());
        }

        private void CheckForChangesIngredientsLevel(object sender, PropertyChangedEventArgs e)
        {
            ValuesIngredients[0].Value = LevelBarley;
            ValuesIngredients[1].Value = LevelHops;
            ValuesIngredients[2].Value = LevelMalt;
            ValuesIngredients[3].Value = LevelWheat;
            ValuesIngredients[4].Value = LevelYeast;

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

        private void CheckChangesInStatus(object sender, PropertyChangedEventArgs e)
        {
            int index = (int)iLogic.OPC.StateCurrent;
            StatusString = statusArray[index];
        }

       

        private void CheckStatus()
        {
            int index = (int)iLogic.OPC.StateCurrent;
            txtStatus.Text = statusArray[index];
            iLogic.OPC.PropertyChanged += CheckChangesInStatus;
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

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            presentation.ILogic.StartProduction();
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            iLogic.OPC.StopMachine();
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            iLogic.OPC.ResetMachine();
        }

        private void btnAbort_Click(object sender, RoutedEventArgs e)
        {
            iLogic.OPC.AbortMachine();
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            iLogic.OPC.ClearMachine();
        }

        private void btnAlarms_Click(object sender, RoutedEventArgs e)
        {
            Alarms alarms = new Alarms(presentation, this);
            this.Hide(); 
            alarms.Show();
        }

        private void btnOpt_Click(object sender, RoutedEventArgs e)
        {
            Opt opt = new Opt(presentation, this);
            this.Hide();
            opt.Show();
        }

        private void btnOEE_Click(object sender, RoutedEventArgs e)
        {
            OEE oee = new OEE(presentation, this);
            this.Hide();
            oee.Show();
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

        

        public string StatusString
        {
            get { return statusString; }
            set
            {
                statusString = value;
                OnPropertyChanged("StatusString");
            }
        }

       

        public List<string> ArrayListLabelsTemperature
        {
            get => arrayListLabelsTemperature;
            set => arrayListLabelsTemperature = value;
        }

        public List<string> ArrayListLabelsHumidity
        {
            get => arrayListLabelsHumidity;
            set => arrayListLabelsHumidity = value;
        }

        

        public List<string> ArrayListLabelsVibration
        {
            get => arrayListLabelsVibration;
            set => arrayListLabelsVibration = value;
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
