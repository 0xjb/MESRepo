using MES.Acquintance;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
        private bool closeApp;

        public History(IPresentation pf, MainWindow mainWindow)
        {
            DataContext = this;
            this.presentationFacade = pf;
            
            batches = pf.ILogic.GetAllBatches().Values;
            this.mw = mainWindow;
            InitializeComponent();
            comboBox.ItemsSource = batches;
            this.Closed += new EventHandler(Window_Closed);
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
            // Throws a win32Exception if file is not found.
            // Why doesn't it throw a FileNotFound exception?
            try
            {
                string path = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
                path = Directory.GetParent(path).FullName;
                path = Directory.GetParent(Directory.GetParent(path).FullName).FullName;
                path += @"\MES\Data\BatchReports\";

                Process.Start(path+ "BatchReport" +
                              (comboBox.SelectedItem as IBatch).GetBatchId() + ".xlsx");
            }
            catch (System.ComponentModel.Win32Exception exs)
            {
                MessageBox.Show("The selected batch does not have a batch report.");
            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (closeApp)
                Application.Current.Shutdown();
        }
    }
}
