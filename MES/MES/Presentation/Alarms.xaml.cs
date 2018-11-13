using System.Windows;

namespace MES.Presentation
{
    /// <summary>
    /// Interaction logic for Alarms.xaml
    /// </summary>
    public partial class Alarms : Window
    {
        public Alarms()
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
