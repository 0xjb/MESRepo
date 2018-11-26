using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MES.Acquintance;

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

        }

        private void asdf()
        {
            //AlarmBox.Text = alarm.StopReason;
        }
    }
}
