using MES.Acquintance;
using System;
using System.Windows;

namespace MES.Presentation
{
    /// <summary>
    /// Interaction logic for TestSimulation.xaml
    /// </summary>
    public partial class Simulation : Window
    {
        private IPresentation presentationFacade;
        private MainWindow mw;
        private bool closeApp;

        public Simulation(IPresentation pf, bool isSimulationOn, MainWindow mainWindow)
        {
            this.presentationFacade = pf;
            this.mw = mainWindow;
            InitializeComponent();

            if (isSimulationOn)
            {
                checkBockSimulation.IsChecked = true;
            }

            if (presentationFacade.ILogic.OPC.StateCurrent != 2)
            {
                checkBockSimulation.IsEnabled = false;
            }

            Closed += new EventHandler(Window_Closed);
            closeApp = true;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            closeApp = false;
            this.Close();
            mw.Show();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (presentationFacade.ILogic.OPC.StateCurrent == 2)
            {
                presentationFacade.ILogic.IsSimulationOn = true;
                presentationFacade.ILogic.CreateSimulation();
                Console.WriteLine(" is checked");
            }
        }

        private void CheckBox_OnUnchecked(object sender, RoutedEventArgs e)
        {
            if (presentationFacade.ILogic.OPC.StateCurrent == 2)
            {
                presentationFacade.ILogic.IsSimulationOn = false;
                Console.WriteLine(" is unchecked");
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (closeApp)
                Application.Current.Shutdown();
        }
    }
}
