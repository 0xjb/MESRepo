using System;
using System.Windows;

namespace MES
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Console.WriteLine("Meow");
            DBSetup dbSetup = new DBSetup();
            dbSetup.Setup();
        }
    }
}
