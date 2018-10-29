using System.Windows;

namespace MES
{
    /// <summary>
    /// Interaction logic for History.xaml
    /// </summary>
    public partial class History : Window
    {
        public History()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            this.Close();
            mainWindow.Show();
        }

        private void btnShowTemperatureHistory_Click(object sender, RoutedEventArgs e)
        {
            TemperatureHistory temperatureHistory = new TemperatureHistory();
            this.Close();
            temperatureHistory.Show();
        }
        private void btnShowHumidityHistory_Click(object sender, RoutedEventArgs e)
        {
            HumidityHistory humidityHistory = new HumidityHistory();
            this.Close();
            humidityHistory.Show();


        }
        private void btnShowVibrationHistory_Click(object sender, RoutedEventArgs e)
        {
            VibrationHistory vibrationHistory = new VibrationHistory();
            this.Close();
            vibrationHistory.Show();

        }
        private void btnShowBatchReport_Click(object sender, RoutedEventArgs e)
        {
            BatchReport batchReport = new BatchReport();
            this.Close();
            batchReport.Show();

        }






    }
}
