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
        private History history;
        int indexOfArray = 0;
        IBatch batch;

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
            InsertHumidityData();
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
            this.Close();
            history.Show();
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
            foreach (var batchvalue in batch.GetBatchHumidities())
            {
                LabelsHumidity[indexOfArray] = batchvalue.Timestamp;
                _value = batchvalue.Value;
                SeriesCollectionHumidity[0].Values.Add(Value);
                indexOfArray++;
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            LabelsHumidity[indexOfArray] = DateTime.Now.ToString();
            _value = generateRandomNumber();
            SeriesCollectionHumidity[0].Values.Add(Value);
            indexOfArray++;
        }
    }
}