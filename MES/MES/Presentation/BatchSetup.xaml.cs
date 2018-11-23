using System.IO;
using MES.Acquintance;
using System.Windows;
using MES.Logic;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MES.Presentation
{
    /// <summary>
    /// Interaction logic for BatchSetup.xaml
    /// </summary>
    public partial class BatchSetup : Window
    {
        private IPresentation presentationFacade;
        private ObservableCollection<Batch> batchQueue = new ObservableCollection<Batch>();
        public BatchSetup(IPresentation pf)
        {
            this.presentationFacade = pf;
            genTestData();
            InitializeComponent();
            batchQueueGrid.ItemsSource = batchQueue;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(presentationFacade);
            this.Close();
            mainWindow.Show();
        }

        private void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }
        private void genTestData() {
            Batch a = new Batch("Batch 1", "Pilsner");
            Batch b = new Batch("Batch 2", "Classic");
            Batch c = new Batch("Batch 3", "meow");
            batchQueue.Add(a);
            batchQueue.Add(b);
            batchQueue.Add(c);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //    try
            //    {
            //        float batchId = float.Parse(BatchIdTB.Text);
            //        float productType = float.Parse(ProductTypeTB.Text);
            //        float amount = float.Parse(AmountTB.Text);
            //        float machineSpeed = float.Parse(MachineSpeedTB.Text);
            //        c.StartMachine(batchId, productType, amount, machineSpeed);
            //        testlabel.Content = "you don gut";
            //    }
            //    catch (System.FormatException)
            //    {
            //        testlabel.Content = "you must insert correct values into the boxes";
            //    }
            //}
            Batch b = new Batch("battch", "hnnn");
            batchQueue.Add(b);
            batchQueue[0].BatchID = "69";

        }
    }
}

