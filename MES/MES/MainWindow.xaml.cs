using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Windows;

namespace MES
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "[Ingredients]",
                    Values = new ChartValues<double> { 10, 50, 39, 50, 100 }
                }
            };

            //adding series will update and animate the chart automatically
            //SeriesCollection.Add(new ColumnSeries {
            //    Title = "2016",
            //    Values = new ChartValues<double> { 11, 56, 42, 99 }
            //});
            //also adding values updates and animates the chart automatically
            //SeriesCollection[1].Values.Add(48d);

            Labels = new[] { "Barley", "Hops", "Malt", "Wheat", "Yeast" };
            Formatter = value => value.ToString("N");
            DataContext = this;
        }

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAbort_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAlarms_Click(object sender, RoutedEventArgs e)
        {
            Alarms alarms = new Alarms();
            this.Close();
            alarms.Show();
        }

        private void btnOEE_Click(object sender, RoutedEventArgs e)
        {
            OEE oEE = new OEE();
            this.Close();
            oEE.Show();
        }

        private void btnOptimization_Click(object sender, RoutedEventArgs e)
        {
            Optimization optimization = new Optimization();
            this.Close();
            optimization.Show();
        }

        private void btnHistory_Click(object sender, RoutedEventArgs e)
        {
            History history = new History();
            this.Close();
            history.Show();
        }

        private void btnBatchSetup_Click(object sender, RoutedEventArgs e)
        {
            BatchSetup batchSetup = new BatchSetup();
            this.Close();
            batchSetup.Show();
        }
    }
}
