using MES.Acquintance;
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
        }
    }
}
