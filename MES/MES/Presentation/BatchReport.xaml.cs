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
        private History history;
        private bool closeApp;

        public BatchReport(IPresentation pf, History history)
        {
            this.presentationFacade = pf;
            this.history = history;
            InitializeComponent();
            Closed += new EventHandler(Window_Closed);
            closeApp = true;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            closeApp = false;
            this.Close();
            history.Show();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (closeApp)
                Application.Current.Shutdown();
        }
    }
}
