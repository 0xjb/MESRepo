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
        private Thread thread3;
        private IPresentation presentationFacade;

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
            this.presentationFacade = pf;
            //Get logiclayer
            iLogic = presentationFacade.GetLogic();
            //Connects to OPC server
            iLogic.GetOPC().Connect();

            InitializeComponent();

            //Do stuff when closing window
            this.Closed += new EventHandler(MainWindow_Closed);

            ValuesIngredients = new ChartValues<ObservableValue>
            {
                new ObservableValue(levelBarley),
                new ObservableValue(levelHops),
                new ObservableValue(levelMalt),
                new ObservableValue(levelWheat),
                new ObservableValue(levelYeast)
            };
            var columnSeries = new ColumnSeries {
                Title = "[Ingredients]",
                Values = ValuesIngredients,
                DataLabels = true
            };

            SeriesCollection = new SeriesCollection { columnSeries };

            //put label stuff here

            Labels = new[] { "Barley", "Hops", "Malt", "Wheat", "Yeast" };
            Formatter = value => value.ToString("N");
            DataContext = this;

            thread3 = new Thread(ThreadReadProcessedProducts);
            thread3.IsBackground = true;
            thread3.Start();

        }

        public ChartValues<ObservableValue> ValuesIngredients { get; set; }


        public SeriesCollection SeriesCollection { get; set; }

        public string[] Labels { get; set; }

        public Func<double, string> Formatter { get; set; }

        public void ThreadReadProcessedProducts()
        {
            while (true) {
                Produced = iLogic.GetOPC().processedProducts;
                Thread.Sleep(500);
            }

        }

        void MainWindow_Closed(object sender, EventArgs e)
        {
            //Put your close code here
            //opc.StopMachine();
            iLogic.GetOPC().StopMachine();
        }


        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            //opc.StartMachine(1, 2, 2000, 600);
            iLogic.GetOPC().StartMachine(1, 2, 200, 600);
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            //opc.StopMachine();
            iLogic.GetOPC().StopMachine();
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            //opc.ResetMachine();
            iLogic.GetOPC().ResetMachine();
        }

        private void btnAbort_Click(object sender, RoutedEventArgs e)
        {
            //opc.AbortMachine();
            iLogic.GetOPC().AbortMachine();
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            //opc.ClearMachine();
            iLogic.GetOPC().ClearMachine();
        }

        private void btnAlarms_Click(object sender, RoutedEventArgs e)
        {
            Alarms alarms = new Alarms(presentationFacade);
            this.Hide();
            alarms.Show();
        }

        private void btnOEE_Click(object sender, RoutedEventArgs e)
        {
            OEE oEE = new OEE(presentationFacade);
            this.Hide();
            oEE.Show();
        }

        private void btnOptimization_Click(object sender, RoutedEventArgs e)
        {
            Optimization optimization = new Optimization(presentationFacade);
            this.Hide();
            optimization.Show();
        }

        private void btnHistory_Click(object sender, RoutedEventArgs e)
        {
            History history = new History(presentationFacade);
            this.Hide();
            history.Show();
        }

        private void btnBatchSetup_Click(object sender, RoutedEventArgs e)
        {
            BatchSetup batchSetup = new BatchSetup(presentationFacade);
            this.Hide();
            batchSetup.Show();
        }

        public double Value

        {
            get { return _value; }

            set

            {
                _value = value;
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
                OnPropertyChanged("BatchID");
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
            if (handler != null) {
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


        private void button1_Click(object sender, RoutedEventArgs e)
        {
            int number = 0;
            Random randomNumber = new Random();
            number = randomNumber.Next(19, 26);
            Value = randomNumber.Next(1, 100);
            //Values for testing purposes.
            MachineSpeed = Value;
            Temperature = Value;
            Humidity = Value;
            Vibration = Value;
            BatchID = Value;
            Amount = Value;
            //Produced = Value;
            //Produced = opc.ReadCurrentProdProcessed();
            //Produced = iLogic.GetSubscribeThread().ReadCurrentProductsProcessed();
            //Produced = iLogic.GetOPC().processedProducts;
            AcceptableProducts = Value;
            DefectProducts = Value;
            Status = Value;


            ValuesIngredients.RemoveAt(4);

            ValuesIngredients.Add(new ObservableValue(Value));

            ValuesIngredients.RemoveAt(3);

            ValuesIngredients.Add(new ObservableValue(Value));
            ValuesIngredients.RemoveAt(2);

            ValuesIngredients.Add(new ObservableValue(Value));
            ValuesIngredients.RemoveAt(1);

            ValuesIngredients.Add(new ObservableValue(Value));

            ValuesIngredients.RemoveAt(0);

            ValuesIngredients.Add(new ObservableValue(Value));
        }


        ///TODO SKAL FJERNES
        private void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
        }
    }
}
