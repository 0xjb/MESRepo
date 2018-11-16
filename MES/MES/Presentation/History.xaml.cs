using MES.Acquintance;
using System.Windows;

namespace MES.Presentation
{
    /// <summary>
    /// Interaction logic for History.xaml
    /// </summary>
    public partial class History : Window
    {

        private IPresentation presentationFacade;
        public History(IPresentation pf)
        {
            this.presentationFacade = pf;
            InitializeComponent();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(presentationFacade);
            this.Close();
            mainWindow.Show();
        }

        private void btnShowTemperatureHistory_Click(object sender, RoutedEventArgs e)
        {
            TemperatureHistory temperatureHistory = new TemperatureHistory(presentationFacade);
            this.Close();
            temperatureHistory.Show();
        }
        private void btnShowHumidityHistory_Click(object sender, RoutedEventArgs e)
        {
            HumidityHistory humidityHistory = new HumidityHistory(presentationFacade);
            this.Close();
            humidityHistory.Show();


        }
        private void btnShowVibrationHistory_Click(object sender, RoutedEventArgs e)
        {
            VibrationHistory vibrationHistory = new VibrationHistory(presentationFacade);
            this.Close();
            vibrationHistory.Show();

        }
        private void btnShowBatchReport_Click(object sender, RoutedEventArgs e)
        {
            BatchReport batchReport = new BatchReport(presentationFacade);
            this.Close();
            batchReport.Show();

        }






    }
}
