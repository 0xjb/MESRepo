using System.Windows;

namespace MES
{
    /// <summary>
    /// Interaction logic for Alarms.xaml
    /// </summary>
    public partial class Alarms : Window
    {
        OpcClient opc;
        public Alarms(OpcClient _opc)
        {
            opc = _opc;
            InitializeComponent();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {

            MainWindow mainwindow = new MainWindow(opc);
            
            this.Close();
            
            mainwindow.Show();
        }
    }
}
