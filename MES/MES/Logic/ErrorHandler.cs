using MES.Acquintance;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;
using System.Windows.Data;


namespace MES.Logic
{
    public class ErrorHandler
    {
        private ObservableCollection<IAlarmObject> _alarms;
        private ILogic iLogic;

        private int alarmNumber;
        private int stopReasonID;

        private StringBuilder stringBuilder;
        private string[] alarmsToFile;

        private static object _lock = new object();


        public ErrorHandler(ILogic iL)
        {
            alarmsToFile = new string[5];
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
            int index = (int)stopReason - 9;
            if (index < 0)
            {
                index = 0;
            }

            //DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff");
            DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss.fff");

            string _date = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            try
            {
                if (index > 0)
                {

                    alarmNumber = _alarms.Count + 1;
                    stopReasonID = (int)stopReason;
                    _alarms.Add(new LogicAlarm(alarmNumber, _date, stopReasons[index], batchID, stopReasonID));

                    Console.WriteLine("\n\n new alarm added  " + alarmNumber + " " + batchID + " " + _date + " " +
                                      stopReasons[index] + " " + stopReason);
                    Console.WriteLine(" number of alarms: " + _alarms.Count);


                    alarmsToFile[0] = alarmNumber.ToString();
                    alarmsToFile[1] = batchID.ToString();
                    alarmsToFile[2] = _date;
                    alarmsToFile[3] = stopReasons[index];
                    alarmsToFile[4] = stopReasonID.ToString();

                    stringBuilder.AppendFormat("{0,-15} {1,-20} {2,-40} {3,-40} {4,-40}", alarmsToFile[0], alarmsToFile[1],
                        alarmsToFile[2], alarmsToFile[3], alarmsToFile[4]);
                    stringBuilder.AppendLine();

                    string result = stringBuilder.ToString();

                    iLogic.Data.WriteToFile(result);

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
            _alarms = iLogic.Data.ReadFile();
        }


        public ObservableCollection<IAlarmObject> Alarms
        {
            get { return _alarms; }

            set { _alarms = value; }
        }
    }
}
