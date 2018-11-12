using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System;
using System.ComponentModel;
using System.Threading;
using System.Windows;

namespace MES
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged// IObservableChartPoint//,INotifyPropertyChanged
    {
        public OpcClient opc = new OpcClient();
        Thread thread;
        //ChartValues<ObservableValue> Values;
        //Level "Barley", "Hops", "Malt", "Wheat", "Yeast" 
        private double levelBarley;
        private double levelHops;
        private double levelMalt;
        private double levelWheat;
        private double levelYeast;

        private double _value;

        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindow(double value)

        {

            ValueMaintenance = value;

        }

        public MainWindow(OpcClient opc) : this()
        {
            this.opc = opc;
        }
        public MainWindow()
        {
            //Connects to OPC server
            opc.Connect();

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
            //{
            //new
            //ColumnSeries
            //{
            //    Title = "[Ingredients]",
            //    //Values = new ChartValues<double> { levelBarley, levelHops, levelMalt, levelWheat, levelYeast }
            //     Values = new ChartValues<ObservableValue> {new ObservableValue(ValueMaintenance)}

            //}
            //};




            //ThreadStart childref = new ThreadStart(CallToChildThread);
            //Console.WriteLine("In Main: Creating the Child thread");
            //thread = new Thread(new ThreadStart(CallToChildThread));
            //thread.Start();

            //put label stuff here

            Labels = new[] { "Barley", "Hops", "Malt", "Wheat", "Yeast" };
            Formatter = value => value.ToString("N");
            DataContext = this;
        }

        public ChartValues<ObservableValue> ValuesIngredients { get; set; }


        public SeriesCollection SeriesCollection { get; set; }

        public string[] Labels { get; set; }

        public Func<double, string> Formatter { get; set; }

        void MainWindow_Closed(object sender, EventArgs e)
        {
            //Put your close code here
            opc.StopMachine();
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            opc.StartMachine(1, 2, 2000, 600);
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            //thread.Abort();
            opc.StopMachine();
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            opc.ResetMachine();
        }

        private void btnAbort_Click(object sender, RoutedEventArgs e)
        {
            opc.AbortMachine();

        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            opc.ClearMachine();
        }

        private void btnAlarms_Click(object sender, RoutedEventArgs e)
        {
            Alarms alarms = new Alarms(this.opc);
            this.Hide();
            alarms.Show();
        }

        private void btnOEE_Click(object sender, RoutedEventArgs e)
        {
            OEE oEE = new OEE();
            this.Hide();
            oEE.Show();
        }

        private void btnOptimization_Click(object sender, RoutedEventArgs e)
        {
            Optimization optimization = new Optimization();
            this.Hide();
            optimization.Show();
        }

        private void btnHistory_Click(object sender, RoutedEventArgs e)
        {
            History history = new History();
            this.Hide();
            history.Show();
        }

        private void btnBatchSetup_Click(object sender, RoutedEventArgs e)
        {
            BatchSetup batchSetup = new BatchSetup();
            this.Hide();
            batchSetup.Show();
        }

        //public event PropertyChangedEventHandler PropertyChanged;

        //protected virtual void OnPropertyChanged(string propertyName = null)
        //{
        //    var handler = PropertyChanged;
        //    if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        //}

        //public event Action PointChanged;

        //public double BarleyValue
        //{
        //    get { return levelBarley; }
        //    set
        //    {
        //        levelBarley = value;
        //        //OnPointChanged();
        //        OnPropertyChanged();
        //    }
        //}
        //protected void OnPointChanged()
        //{
        //    if (PointChanged != null) PointChanged.Invoke();
        //}

        //public double Barley
        //{
        //    get { return levelBarley; }
        //    set
        //    {
        //        levelBarley = value;
        //        // Call OnPropertyChanged whenever the property is updated
        //        OnPropertyChanged("LevelBarley");
        //    }
        //}

        // Create the OnPropertyChanged method to raise the event
        //protected void OnPropertyChanged(string name)
        //{
        //    PropertyChangedEventHandler handler = PropertyChanged;
        //    if (handler != null) {
        //        handler(this, new PropertyChangedEventArgs(name));
        //    }
        //}
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

        protected virtual void OnPropertyChanged(string propertyName = null)

        {
            if (PropertyChanged != null) PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void CallToChildThread()
        {
            //while (true) {
            //    for (int i = 0; i < 60; i++) {
            //        levelBarley++;
            //        levelHops++;
            //        levelMalt++;
            //        levelWheat++;
            //        levelYeast++;
            //        Console.WriteLine("****" + levelYeast + " *" + levelHops + " *" + levelMalt + " *" + levelWheat + " *" + levelBarley);
            //    }
            //    for (int i = 0; i < 60; i++) {
            //        levelBarley--;
            //        levelHops--;
            //        levelMalt--;
            //        levelWheat--;
            //        levelYeast--;
            //    }
            //}


        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            int number = 0;
            Random randomNumber = new Random();
            number = randomNumber.Next(19, 26);
            ValueMaintenance = randomNumber.Next(1, 100);
            //levelBarley = number;
            //Console.WriteLine("***" + levelBarley);
            //SeriesCollection[0].Values.Add(new ObservableValue(Value));
            //SeriesCollection[0].Values.Add(new ObservableValue(BarleyValue));
            //Values.Add(new ObservableValue(BarleyValue));
            //Console.WriteLine("*************" + BarleyValue);
        }



        private void button1_Click(object sender, RoutedEventArgs e)
        {
            int number = 0;
            Random randomNumber = new Random();
            number = randomNumber.Next(19, 26);
            Value = randomNumber.Next(1, 100);


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

            //var r = new Random();

            //var c = SeriesCollection[0].Values.Count;

            //var val = new ChartValues<ObservableValue>();

            //for (int i = 0; i < c; i++) {
            //    val.Add(new ObservableValue(r.Next(2, 20)));
            //}
            //SeriesCollection.Add(new ColumnSeries {
            //    Title = "[Ingredients]",
            //    Values = val,
            //    DataLabels = true
            //});
        }
    }
}
