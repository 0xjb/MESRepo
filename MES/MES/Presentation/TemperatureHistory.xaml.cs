using LiveCharts;
using LiveCharts.Wpf;
using MES.Acquintance;
using System;
using System.Windows;

namespace MES.Presentation
{
    /// <summary>
    /// Interaction logic for TemperatureHistory.xaml
    /// </summary>
    public partial class TemperatureHistory : Window, IObservableChartPoint
    {
        //TODO Størrelse af array i constructor Temperature History
        IBatch batch;
        private IPresentation presentationFacade;
        private History history;
        private int indexOfArray = 0;
        private bool closeApp;

        public TemperatureHistory(IBatch b, History history)
        {

            this.history = history;
            InitializeComponent();
            batch = b;
            SeriesCollectionTemperature = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Temperature",
                    Values = new ChartValues<double> { }
                }
            };

            LabelsTemperature = new string[1000];
            FormatterTemperature = value => value;
            DataContext = this;
            InsertTemperatureData();
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
            if (PointChanged != null)
                PointChanged.Invoke();
        }

        public SeriesCollection SeriesCollectionTemperature { get; set; }

        public string[] LabelsTemperature { get; set; }

        public Func<double, double> FormatterTemperature { get; set; }

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
        private void InsertTemperatureData()
        {
            foreach (var batchvalue in batch.GetBatchTemperatures())
            {
                LabelsTemperature[indexOfArray] = batchvalue.Timestamp;
                _value = batchvalue.Value;
                SeriesCollectionTemperature[0].Values.Add(Value);
                indexOfArray++;
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            foreach (var batchvalue in batch.GetBatchTemperatures())
            {
                LabelsTemperature[indexOfArray] = batchvalue.Timestamp;
                _value = batchvalue.Value;
                SeriesCollectionTemperature[0].Values.Add(Value);
                indexOfArray++;
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (closeApp)
                Application.Current.Shutdown();
        }
    }
}
