using MES.Acquintance;
using System;
using System.Windows;

namespace MES.Presentation
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private IPresentation presentation;
        private bool closeApp;

        public LoginWindow(IPresentation presentation)
        {
            InitializeComponent();
            this.presentation = presentation;

            loginButton.IsDefault = true;
            usernameTextBox.Focus();

            Closed += new EventHandler(Window_Closed);
            closeApp = true;
        }

        private void HandleClickLoginButtonEvent(object sender, RoutedEventArgs e)
        {
            bool authenticated = presentation.ILogic.AuthenticateUserInformation
                (usernameTextBox.Text, passwordBox.Password);

            if (authenticated)
            {
                MainWindow mainWindow = new MainWindow(presentation);
                closeApp = false;
                this.Close();
                mainWindow.Show();
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (closeApp)
                Application.Current.Shutdown();
        }
    }
}
