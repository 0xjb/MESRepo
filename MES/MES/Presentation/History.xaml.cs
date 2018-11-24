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
        private MainWindow mw;

        public History(IPresentation pf, MainWindow mainWindow)
        {
            this.presentationFacade = pf;
            this.mw = mainWindow;
            InitializeComponent();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(presentationFacade);
            this.Close();
            //mainWindow.Show();
            mw.Show();
        }

        private void btnShowTemperatureHistory_Click(object sender, RoutedEventArgs e)
        {
            TemperatureHistory temperatureHistory = new TemperatureHistory(presentationFacade, mw);
            this.Close();
            temperatureHistory.Show();
        }

        private void btnShowHumidityHistory_Click(object sender, RoutedEventArgs e)
        {
            HumidityHistory humidityHistory = new HumidityHistory(presentationFacade, mw);
            this.Close();
            humidityHistory.Show();
        }

        private void btnShowVibrationHistory_Click(object sender, RoutedEventArgs e)
        {
            VibrationHistory vibrationHistory = new VibrationHistory(presentationFacade, mw);
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