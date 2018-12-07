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
        IPresentation presentation;

        public LoginWindow(IPresentation presentation)
        {
            InitializeComponent();
            this.presentation = presentation;

            loginButton.IsDefault = true;
            usernameTextBox.Focus();

            Closed += new EventHandler(Window_Closed);
        }

        private void HandleClickLoginButtonEvent(object sender, RoutedEventArgs e)
        {
            bool authenticated = presentation.ILogic.AuthenticateUserInformation
                (usernameTextBox.Text, passwordBox.Password);

            if (authenticated)
            {
                MainWindow mainWindow = new MainWindow(presentation);
                this.Close();
                mainWindow.Show();
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
