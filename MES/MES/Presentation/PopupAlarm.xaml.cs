using MES.Acquintance;
using System;
using System.Windows;

namespace MES.Presentation
{
    /// <summary>
    /// Interaction logic for PopupAlarm.xaml
    /// </summary>
    public partial class PopupAlarm : Window
    {
        private IAlarmObject _alarm;

        public PopupAlarm(IAlarmObject alarm)
        {
            _alarm = alarm;
            InitializeComponent();
            AlarmBox.Text = alarm.StopReason;
            Closed += new EventHandler(Window_Closed);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
