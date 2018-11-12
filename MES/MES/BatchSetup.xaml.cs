using System.Windows;

namespace MES
{
    /// <summary>
    /// Interaction logic for BatchSetup.xaml
    /// </summary>
    public partial class BatchSetup : Window
    {
        OpcClient c; 
        public BatchSetup(OpcClient c)
        {
            this.c = c;
            InitializeComponent();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            this.Close();
            mainWindow.Show();
        }

        private void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            float batchId = float.Parse(BatchIdTB.Text);
            float productType = float.Parse(ProductTypeTB.Text);
            float amount = float.Parse(AmountTB.Text);
            float machineSpeed = float.Parse(MachineSpeedTB.Text);
            c.StartMachine(batchId, productType, amount, machineSpeed);

        }
    }
}
