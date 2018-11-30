using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using MES.Acquintance;
using MES.Annotations;
using MES.Presentation;


namespace MES.Logic
{
    public class ErrorHandler
    {
        private ObservableCollection<IAlarmObject> _alarms;
        private ILogic iLogic;

        private int alarmNumber;

        private StringBuilder stringBuilder;
        private string[] alarmsToFile;

        private static object _lock = new object();


        public ErrorHandler(ILogic iL)
        {
            alarmsToFile = new string[4];
            Console.WriteLine("\n\nCONTRUCTOR ERRORHANDLER\n\n");
            this.iLogic = iL;
            _alarms = new ObservableCollection<IAlarmObject>();
            stringBuilder = new StringBuilder();

            ReadFile();

            BindingOperations.EnableCollectionSynchronization(_alarms, _lock);
        }

        private string[] stopReasons = new[]
            {"None", "Empty inventory", "Maintenance", "Manual stop", "Motor power loss", "Manual abort"};

        public void AddAlarm(int batchID, double stopReason)
        {
            int index = (int) stopReason - 9;
            if (index < 0)
            {
                index = 0;
            }


            string _date = DateTime.Now.ToString("MM/dd/yyyy") + " " + DateTime.Now.ToString("HH:mm:ss tt ");
            try
            {
                if (index > 0)
                {
                    
                    alarmNumber = _alarms.Count + 1;

                    _alarms.Add(iLogic.Data.CreateNewAlarm(alarmNumber, batchID, _date, stopReasons[index]));
                    //_alarms.Add(new AlarmObject()
                    //{
                    //    AlarmNumber = alarmNumber, BatchID = batchID, Timestamp = _date, StopReason = stopReasons[index]
                    //});


                    Console.WriteLine("\n\n new alarm added  " + alarmNumber + " " + batchID + " " + _date + " " +
                                      stopReasons[index]);
                    Console.WriteLine(" number of alarms: " + _alarms.Count);


                    alarmsToFile[0] = alarmNumber.ToString();
                    alarmsToFile[1] = batchID.ToString();
                    alarmsToFile[2] = _date;
                    alarmsToFile[3] = stopReasons[index];

                    stringBuilder.AppendFormat("{0,-15} {1,-20} {2,-40} {3,-40}", alarmsToFile[0], alarmsToFile[1],
                        alarmsToFile[2], alarmsToFile[3]);
                    stringBuilder.AppendLine();

                    string result = stringBuilder.ToString();

                    iLogic.Data.WriteToFile(result);

                    Console.WriteLine("\n\n Alarm " + result + " added\n\n");

                    stringBuilder.Clear();
                }
            }
            catch (NotSupportedException e)
            {
                Console.WriteLine(e);
            }
        }

        private void ReadFile()
        {

            Console.WriteLine("\n\n READ FILE ERRORHANDLER\n\n");
            //iLogic.Data.ReadFile();
            //ArrayList list = new ArrayList();
            //list = iLogic.Data.ReadFile();
            _alarms = iLogic.Data.ReadFile();
            //Console.WriteLine("\n\nArrayList count     "+list.Count+"\n\n");

        }


        public ObservableCollection<IAlarmObject> Alarms
        {
            get { return _alarms; }

            set { _alarms = value; }
        }



    }
}