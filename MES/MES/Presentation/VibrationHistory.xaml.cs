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
        private IBatch batch;
        private History history;
        private int indexOfArray = 0;
        private bool closeApp;

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

        public SeriesCollection SeriesCollectionVibration { get; set; }

        public string[] LabelsVibration { get; set; }

        public Func<double, double> FormatterVibration { get; set; }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            closeApp = false;
            this.Close();
            this.history.Show();
        }

        private void InsertVibrationData()
        {
            try
            {
                foreach (var batchvalue in batch.GetBatchVibrations())
                {
                    LabelsVibration[indexOfArray] = batchvalue.Timestamp;
                    _value = batchvalue.Value;
                    SeriesCollectionVibration[0].Values.Add(Value);
                    indexOfArray++;
                }
            }
            catch (NullReferenceException) { }
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            if (closeApp)
                Application.Current.Shutdown();
        }
    }
}
