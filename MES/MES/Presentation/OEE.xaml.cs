using MES.Acquintance;
using System;
using System.Windows;
using System.Windows.Input;

namespace MES.Presentation
{ //TODO Ændre OEE til Optimization , og bytte Optimization til OEE
    /// <summary>
    /// Interaction logic for Optimization.xaml
    /// </summary>
    public partial class OEE : Window
    {
        private IPresentation presentationFacade;
        private MainWindow mw;
        private bool closeApp;

        public OEE(IPresentation pf, MainWindow mainWindow)
        {
            this.presentationFacade = pf;
            this.mw = mainWindow;
            InitializeComponent();
            DataGridShowBatches.ItemsSource = pf.ILogic.OEeList;
            DataContext = this;
            Closed += new EventHandler(Window_Closed);
            closeApp = true;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            closeApp = false;
            this.Close();
            mw.Show();
        }

        private void SearchBatch_Click(object sender, RoutedEventArgs e)
        {
            presentationFacade.ILogic.OEeList.Clear();
            try
            {
                int batchId = Int32.Parse(txtSearchBatchId.Text);

                if (!presentationFacade.ILogic.addOEEFromBatch(batchId))
                {
                    lblInfo.Content = "Batch does not exist... ";
                }
                else
                {
                    lblInfo.Content = "";
                }
            }
            catch (FormatException exception)
            {
                Console.WriteLine(exception);
                lblInfo.Content = "Incorrect input..";
            }
        }

        private void ComboYear_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
        }

        private void SearchNewestBatches_Click(object sender, RoutedEventArgs e)
        {
            presentationFacade.ILogic.OEeList.Clear();

            try
            {
                int number = Int32.Parse(txtSearchNewestBacthId.Text);
                presentationFacade.ILogic.SearchNewestBatches(number);
            }
            catch (FormatException exception)
            {
                Console.WriteLine(exception);
                lblInfo.Content = "Incorrect input..";
            }
        }

        private void MonthYear_Click(object sender, RoutedEventArgs e)
        {
            lblInfo.Content = "";
            presentationFacade.ILogic.OEeList.Clear();

            string month = (ComboMonth.SelectedIndex + 1).ToString();
            string year = ComboYear.Text;

            //Console.WriteLine("\n\nis focused " + ComboMonth.+ "\n\n");

            if (ComboMonth.SelectedItem != null && ComboYear.SelectedItem != null)
            {
                presentationFacade.ILogic.SearchDateYearBatches(month, year);

                if (presentationFacade.ILogic.OEeList.Count.Equals(0))
                {
                    lblInfo.Content = "No batches found";
                }
            }
            else
            {
                lblInfo.Content = "Select month and year..";
            }
        }


        private void TxtSearchNewestBacthId_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                lblInfo.Content = "";
                presentationFacade.ILogic.OEeList.Clear();
                try
                {
                    int number = Int32.Parse(txtSearchNewestBacthId.Text);
                    presentationFacade.ILogic.SearchNewestBatches(number);
                }
                catch (FormatException exception)
                {
                    Console.WriteLine(exception);
                    lblInfo.Content = "Incorrect input..";
                }
            }
        }

        private void TxtSearchBatchId_OnKeyDown(object sender, KeyEventArgs e)
        {
            presentationFacade.ILogic.OEeList.Clear();
            try
            {
                int batchId = Int32.Parse(txtSearchBatchId.Text);

                if (!presentationFacade.ILogic.addOEEFromBatch(batchId))
                {
                    lblInfo.Content = "Batch does not exist... ";
                }
                else
                {
                    lblInfo.Content = "";
                }
            }
            catch (FormatException exception)
            {
                Console.WriteLine(exception);
                lblInfo.Content = "Incorrect input..";
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (closeApp)
                Application.Current.Shutdown();
        }
    }
}
