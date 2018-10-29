using System.Windows;

namespace MES
{
    /// <summary>
    /// Interaction logic for BatchReport.xaml
    /// </summary>
    public partial class BatchReport : Window
    {
        public BatchReport()
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
