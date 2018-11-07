using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Windows;

namespace MES
{
    /// <summary>
    /// Interaction logic for HumidityHistory.xaml
    /// </summary>
    public partial class HumidityHistory : Window, IObservableChartPoint
    {
        int indexOfArray = 0;
        public HumidityHistory()
        {
            InitializeComponent();
            SeriesCollection = new SeriesCollection

        {new LineSeries
                {
                    Title = "Humidity",
                    Values= new ChartValues<double> { }
                }
            };

            Labels = new string[1000];
            YFormatter = value => value;
            DataContext = this;
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

        public SeriesCollection SeriesCollection { get; set; }

        public string[] Labels { get; set; }

        public Func<double, double> YFormatter { get; set; }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            History history = new History();
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

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Labels[indexOfArray] = DateTime.Now.ToString();
            _value = generateRandomNumber();
            SeriesCollection[0].Values.Add(Value);
            indexOfArray++;

        }
    }
}
