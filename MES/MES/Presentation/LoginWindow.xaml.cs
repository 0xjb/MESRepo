using MES.Acquintance;
using System.Windows;

namespace MES.Presentation
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        IPresentation presentation;

        public LoginWindow(IPresentation presentation)
        {
            InitializeComponent();
            this.presentation = presentation;
        }

        private void HandleClickLoginButtonEvent(object sender, RoutedEventArgs e)
        {
            bool authenticated = presentation.ILogic.AuthenticateUserInformation
                (usernameTextBox.Text, passwordTextBox.Text);

            if (authenticated)
            {
                MainWindow mainWindow = new MainWindow(presentation);
                this.Close();
                mainWindow.Show();
            }
        }
    }
}
