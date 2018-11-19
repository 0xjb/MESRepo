﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MES.Acquintance;

namespace MES.Presentation
{
    /// <summary>
    /// Interaction logic for TestSimulation.xaml
    /// </summary>
    public partial class Simulation : Window
    {
        private IPresentation presentationFacade;
        public Simulation(IPresentation pf, bool isSimulationOn)
        {

            this.presentationFacade = pf;
            InitializeComponent();
            if (isSimulationOn)
            {
                checkBockSimulation.IsChecked = true;
            }

           
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(presentationFacade);
            this.Close();
            mainWindow.Show();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (presentationFacade.ILogic.OPC.StateCurrent != 6)
            {
                presentationFacade.ILogic.IsSimulationOn = true;
                presentationFacade.ILogic.CreateSimulation();
                Console.WriteLine(" is checked");
            }
            else
            {
                checkBockSimulation.IsChecked = false;
                txtBlock.Text = "Machine is running...Can't turn on simulation....";
            }
         
        }

        private void CheckBockSimulation_OnUnchecked(object sender, RoutedEventArgs e)
        {
            presentationFacade.ILogic.IsSimulationOn = false;
            Console.WriteLine(" is unchecked");
        }
    }
}