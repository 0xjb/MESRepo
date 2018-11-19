using System;
using System.Collections;
using System.Collections.ObjectModel;
using MES.Acquintance;
using System.Windows;
using System.Windows.Controls;

namespace MES.Presentation
{
    /// <summary>
    /// Interaction logic for Alarms.xaml
    /// </summary>
    public partial class Alarms : Window
    {
        private ArrayList _arrayList = new ArrayList();
        public ObservableCollection<AlarmObject> _alarms { get; set; }
        private int testInt = 0;
        private IPresentation presentationFacade;
        public Alarms(IPresentation pf)
        {
            this._alarms = new ObservableCollection<AlarmObject>();
            this.presentationFacade = pf;
            InitializeComponent();

            this.DataContext = this;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {

            MainWindow mainWindow = new MainWindow(presentationFacade);

            this.Close();

            mainWindow.Show();
        }

        //_arrayList.Add(new AlarmObject(testInt, 10,
        //    DateTime.Now.ToString("MM/dd/yyyy") + " " + DateTime.Now.ToString("HH:mm:ss tt ")));
        //listViewAlarms.ItemsSource = _arrayList;


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            testInt++;
            string _date = DateTime.Now.ToString("MM/dd/yyyy") + " " + DateTime.Now.ToString("HH:mm:ss tt ");
            _alarms.Add(new AlarmObject() { AlarmNumber = testInt, BatchID = testInt, Timestamp = _date, StopReason="Do not know......" });
            listViewAlarms.ItemsSource = _alarms;
        }

        public class AlarmObject
        {

            public int AlarmNumber { get; internal set; }
            public string Timestamp { get; internal set; }
            public string StopReason { get; internal set; }
            public int BatchID { get; internal set; }
        }
    }
}
