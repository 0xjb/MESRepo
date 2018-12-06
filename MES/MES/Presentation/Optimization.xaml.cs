﻿using System;
using MES.Acquintance;
using System.Windows;

namespace MES.Presentation
{//TODO Ændre OEE til Optimization , og bytte Optimization til OEE
    /// <summary>
    /// Interaction logic for Optimization.xaml
    /// </summary>
    public partial class Optimization : Window
    {
        private IPresentation presentationFacade;
        private MainWindow mw;

        public Optimization(IPresentation pf, MainWindow mainWindow)
        {
            this.presentationFacade = pf;
            this.mw = mainWindow;
            InitializeComponent();
            DataGridShowBatches.ItemsSource = pf.ILogic.OEeList;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(presentationFacade);
            this.Close();
            //mainWindow.Show();
            mw.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int batchId = Int32.Parse(txtSearchBatchId.Text);
            presentationFacade.ILogic.addOEEFromBatch(batchId);
            Console.WriteLine("\n\n ********************************************   " + presentationFacade.ILogic.OEeList.Count+" \n\n");
            
        }

        private void ComboYear_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
           
        }
    }
}