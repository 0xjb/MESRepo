using MES.Acquintance;
using System;
using System.Windows;
using System.Windows.Controls;

namespace MES.Presentation
{
    /// <summary>
    /// Interaction logic for Alarms.xaml
    /// </summary>
    public partial class Alarms : Window
    {
        private IPresentation presentationFacade;
        private MainWindow mw;
        private bool closeApp;

        public Alarms(IPresentation pf, MainWindow mainWindow)
        {
            this.presentationFacade = pf;
            this.mw = mainWindow;
            InitializeComponent();
            listViewAlarms.ItemsSource = presentationFacade.ILogic.ErrorHandler.Alarms;
            this.DataContext = this;
            Closed += new EventHandler(Window_Closed);
            closeApp = true;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            closeApp = false;
            this.Close();
            mw.Show();
        }

        private void listViewAlarms_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if(listViewAlarms.Ge)
        }

        private void ShowPopUp()
        {
            //listViewAlarms.
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (closeApp)
                Application.Current.Shutdown();
        }
    }
}
