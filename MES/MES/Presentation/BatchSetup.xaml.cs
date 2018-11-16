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
        public BatchSetup(IPresentation pf)
        {
            this.presentationFacade = pf;
            InitializeComponent();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(presentationFacade);
            this.Close();
            mainWindow.Show();
        }
    }
}
