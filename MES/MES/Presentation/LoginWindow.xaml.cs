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
            IUser user = presentation.ILogic.AuthenticateUserInformation
                (usernameTextBox.Text, passwordTextBox.Text);

            if (user != null)
            {
                MainWindow mainWindow = new MainWindow(presentation);
                this.Hide();
                mainWindow.Show();
            }
        }
    }
}
