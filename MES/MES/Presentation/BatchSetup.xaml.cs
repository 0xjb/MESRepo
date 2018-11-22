using System.IO;
using MES.Acquintance;
using System.Windows;

namespace MES.Presentation
{
    /// <summary>
    /// Interaction logic for BatchSetup.xaml
    /// </summary>
    public partial class BatchSetup : Window
    {
        private IPresentation presentationFacade;
        private MainWindow mw;
        public BatchSetup(IPresentation pf, MainWindow mainWindow)
        {
            this.presentationFacade = pf;
            this.mw = mainWindow;
            InitializeComponent();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            //MainWindow mainWindow = new MainWindow(presentationFacade);
            this.Close();
            mw.Show();
        }

        private void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

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
        }
    }
}

