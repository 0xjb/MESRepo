using MES.Acquintance;
using System;
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
        private bool closeApp;

        public History(IPresentation pf, MainWindow mainWindow)
        {
            this.presentationFacade = pf;
            this.mw = mainWindow;
            InitializeComponent();
            Closed += new EventHandler(Window_Closed);
            closeApp = true;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            closeApp = false;
            this.Close();
            mw.Show();
        }

        private void btnShowTemperatureHistory_Click(object sender, RoutedEventArgs e)
        {
            TemperatureHistory temperatureHistory = new TemperatureHistory(presentationFacade, this);
            this.Hide();
            temperatureHistory.Show();
        }

        private void btnShowHumidityHistory_Click(object sender, RoutedEventArgs e)
        {
            HumidityHistory humidityHistory = new HumidityHistory(presentationFacade, this);
            this.Hide();
            humidityHistory.Show();
        }

        private void btnShowVibrationHistory_Click(object sender, RoutedEventArgs e)
        {
            VibrationHistory vibrationHistory = new VibrationHistory(presentationFacade, this);
            this.Hide();
            vibrationHistory.Show();
        }

        private void btnShowBatchReport_Click(object sender, RoutedEventArgs e)
        {
            closeApp = false;
            BatchReport batchReport = new BatchReport(presentationFacade, this);
            this.Hide();
            batchReport.Show();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (closeApp)
                Application.Current.Shutdown();
        }
    }
}
