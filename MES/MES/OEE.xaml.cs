using System.Windows;

namespace MES
{
    /// <summary>
    /// Interaction logic for OEE.xaml
    /// </summary>
    public partial class OEE : Window
    {
        public OEE()
        {
            InitializeComponent();

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            this.Close();
            mainWindow.Show();
        }
    }
}
