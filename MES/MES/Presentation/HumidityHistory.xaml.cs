using LiveCharts;
using LiveCharts.Wpf;
using MES.Acquintance;
using System;
using System.Windows;

namespace MES.Presentation
{
    /// <summary>
    /// Interaction logic for HumidityHi//TODO Størrelse af array i constructor Humidity Historystory.xaml
    /// </summary>
    public partial class HumidityHistory : Window, IObservableChartPoint
    {
        //TODO Størrelse af array i constructor Humidity History
        private IBatch batch;
        private History history;
        private int indexOfArray = 0;
        private bool closeApp;

        public HumidityHistory(IBatch b, History history)
        {
            this.history = history;
            InitializeComponent();
            batch = b;
            SeriesCollectionHumidity = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Humidity",
                    Values = new ChartValues<double> { }
                }
            };

            LabelsHumidity = new string[1000];
            FormatterHumidity = value => value;
            DataContext = this;
            try
            {
                foreach (var humi in batch.GetBatchHumidities())
                {
                    Console.WriteLine(humi.Value);
                }
            }
            catch (NullReferenceException) { }
            InsertHumidityData();
            Closed += new EventHandler(Window_Closed);
            closeApp = true;
        }

        private double _value;

        public event Action PointChanged;

        public double Value
        {
            get { return _value; }
            set
            {
                _value = value;
                OnPointChanged();
            }
        }

        protected void OnPointChanged()
        {
            if (PointChanged != null) PointChanged.Invoke();
        }

        public SeriesCollection SeriesCollectionHumidity { get; set; }

        public string[] LabelsHumidity { get; set; }

        public Func<double, double> FormatterHumidity { get; set; }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            closeApp = false;
            this.Close();
            this.history.Show();
        }

        //Skal fjernes bare til Test
        int generateRandomNumber()
        {
            int number = 0;
            Random randomNumber = new Random();
            number = randomNumber.Next(19, 26);
            return number;
        }
        private void InsertHumidityData()
        {
            try
            {
                foreach (var batchvalue in batch.GetBatchHumidities())
                {
                    LabelsHumidity[indexOfArray] = batchvalue.Timestamp;
                    _value = batchvalue.Value;
                    SeriesCollectionHumidity[0].Values.Add(Value);
                    indexOfArray++;
                }
            }
            catch (NullReferenceException) { }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            LabelsHumidity[indexOfArray] = DateTime.Now.ToString();
            _value = generateRandomNumber();
            SeriesCollectionHumidity[0].Values.Add(Value);
            indexOfArray++;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (closeApp)
                Application.Current.Shutdown();
        }
    }
}
