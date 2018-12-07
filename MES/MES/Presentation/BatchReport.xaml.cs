using MES.Acquintance;
using System;
using System.Windows;

namespace MES.Presentation
{
    /// <summary>
    /// Interaction logic for BatchReport.xaml
    /// </summary>
    public partial class BatchReport : Window
    {
        private IPresentation presentationFacade;
        private History hw;
        private bool closeApp;

        public BatchReport(IPresentation pf, History hw)
        {
            this.presentationFacade = pf;
            this.hw = hw;
            InitializeComponent();
            Closed += new EventHandler(Window_Closed);
            closeApp = true;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            closeApp = false;
            this.Close();
            hw.Show();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (closeApp)
                Application.Current.Shutdown();
        }
    }
}
