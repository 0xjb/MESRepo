using LiveCharts;
using LiveCharts.Wpf;
using MES.Acquintance;
using System;
using System.Windows;

namespace MES.Presentation
{
    /// <summary>
    /// Interaction logic for VibrationHistory.xaml
    /// </summary>
    public partial class VibrationHistory : Window, IObservableChartPoint
    {
        //TODO Størrelse af array i constructor Vibration History
        private History history;
        int indexOfArray = 0;
        IBatch batch;

        public VibrationHistory(IBatch b, History history)
        {
            this.history = history;
            InitializeComponent();
            batch = b;
            SeriesCollectionVibration = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Vibration",
                    Values = new ChartValues<double> { }
                }
            };

            LabelsVibration = new string[1000];
            FormatterVibration = value => value;
            DataContext = this;
            InsertVibrationData();
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

        public SeriesCollection SeriesCollectionVibration { get; set; }

        public string[] LabelsVibration { get; set; }

        public Func<double, double> FormatterVibration { get; set; }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            history.Show();
        }

        //TODO Skal fjernes bare til Test
        int generateRandomNumber()
        {
            int number = 0;
            Random randomNumber = new Random();
            number = randomNumber.Next(19, 26);
            return number;
        }
        private void InsertVibrationData()
        {
            foreach (var batchvalue in batch.GetBatchVibrations())
            {
                LabelsVibration[indexOfArray] = batchvalue.Timestamp;
                _value = batchvalue.Value;
                SeriesCollectionVibration[0].Values.Add(Value);
                indexOfArray++;
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            LabelsVibration[indexOfArray] = DateTime.Now.ToString();
            _value = generateRandomNumber();
            SeriesCollectionVibration[0].Values.Add(Value);
            indexOfArray++;
        }
    }
}