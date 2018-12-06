using MES.Acquintance;
using System.Collections.Generic;
using System.Windows;

namespace MES.Presentation
{
    /// <summary>
    /// Interaction logic for History.xaml
    /// </summary>
    public partial class History : Window
    {
        private IPresentation presentationFacade;
        private ICollection<IBatch> batches;
        private MainWindow mw;

        public History(IPresentation pf, MainWindow mainWindow)
        {
            DataContext = this;
            this.presentationFacade = pf;
            batches = pf.ILogic.GetAllBatches().Values;

            this.mw = mainWindow;
            InitializeComponent();
            comboBox.ItemsSource = batches;
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
            TemperatureHistory temperatureHistory = new TemperatureHistory(comboBox.SelectedItem as IBatch, this);
            this.Hide();
            temperatureHistory.Show();
        }

        private void btnShowHumidityHistory_Click(object sender, RoutedEventArgs e)
        {
            HumidityHistory humidityHistory = new HumidityHistory(comboBox.SelectedItem as IBatch, this);
            this.Hide();
            humidityHistory.Show();
        }

        private void btnShowVibrationHistory_Click(object sender, RoutedEventArgs e)
        {
            VibrationHistory vibrationHistory = new VibrationHistory(comboBox.SelectedItem as IBatch, this);
            this.Hide();
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