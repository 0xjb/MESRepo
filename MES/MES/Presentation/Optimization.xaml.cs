using MES.Acquintance;
using System.Windows;

namespace MES.Presentation
{
    /// <summary>
    /// Interaction logic for Optimization.xaml
    /// </summary>
    public partial class Optimization : Window
    {
        private IPresentation presentationFacade;
        public Optimization(IPresentation pf)
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
