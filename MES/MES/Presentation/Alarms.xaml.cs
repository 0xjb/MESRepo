using System;
using System.Collections;
using System.Collections.ObjectModel;
using MES.Acquintance;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using MES.Logic;

namespace MES.Presentation
{
    /// <summary>
    /// Interaction logic for Alarms.xaml
    /// </summary>
    public partial class Alarms : Window
    {
        private IPresentation presentationFacade;
        private MainWindow mw;


        public Alarms(IPresentation pf,MainWindow mainWindow)
        {


            this.presentationFacade = pf;

            this.mw = mainWindow;

            InitializeComponent();

            listViewAlarms.ItemsSource = presentationFacade.ILogic.OPC.ErrorHandler.Alarms;
            this.DataContext = this;

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {

            //MainWindow mainWindow = new MainWindow(presentationFacade);

            this.Close();
            //mainWindow.Show();
            mw.Show();
        }

 
    }
}
