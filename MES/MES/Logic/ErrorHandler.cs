using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using MES.Annotations;
using MES.Presentation;
using UnifiedAutomation.UaClient;

namespace MES.Logic
{
    public class ErrorHandler
    {
        //public ObservableCollection<AlarmObject> _alarms { get; set; }
        private ObservableCollection<AlarmObject> _alarms;

        private int alarmNumber = 0;

        private static object _lock = new object();

        public ErrorHandler()
        {
            _alarms = new ObservableCollection<AlarmObject>();

            BindingOperations.EnableCollectionSynchronization(_alarms, _lock);
        }

        private string[] stopReasons = new[]
            {"None", "Empty inventory", "Maintenance", "Manual stop", "Motor power loss", "Manual abort"};

        public void AddAlarm(int batchID, double stopReason)
        {
            int index = (int)stopReason - 9;
            if (index < 0)
            {
                index = 0;
            }


            string _date = DateTime.Now.ToString("MM/dd/yyyy") + " " + DateTime.Now.ToString("HH:mm:ss tt ");
            try
            {
                if (index > 0)
                {
                    alarmNumber++;
                    _alarms.Add(new AlarmObject() { AlarmNumber = alarmNumber, BatchID = batchID, Timestamp = _date, StopReason = stopReasons[index] });
                    Console.WriteLine("\n\n new alarm added  " + alarmNumber + " " + batchID + " " + _date + " " +
                                      stopReasons[index]);
                    Console.WriteLine(" number of alarms: " + _alarms.Count);
                }

            }
            catch (NotSupportedException e)
            {
                Console.WriteLine(e);
            }


        }

        public ObservableCollection<AlarmObject> Alarms {
            get => _alarms;
            set => _alarms = value;
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