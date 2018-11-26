using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using MES.Annotations;
using MES.Presentation;


namespace MES.Logic
{
    public class ErrorHandler
    {
        private ObservableCollection<AlarmObject> _alarms;

        private int alarmNumber;

        //TODO Skal være i data
        private string filePathAndName = @"Logic\AlarmLogFiles\alarmLogFile.txt";

        private StringBuilder stringBuilder;
        private string[] alarmsToFile;

        private static object _lock = new object();


        public ErrorHandler()
        {
            _alarms = new ObservableCollection<AlarmObject>();
            alarmsToFile = new string[4];
            if (!IsFileIsEmpty())
            {
                stringBuilder = new StringBuilder();
                stringBuilder.AppendFormat("{0,-15} {1,-20} {2,-40} {3,-40}", "Alarm Number:", "Batch Id:",
                    "Time and Date:", "Stop Reason:");
                stringBuilder.AppendLine();
            }
            else
            {
                stringBuilder = new StringBuilder();
            }

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
                    _alarms.Add(new AlarmObject()
                    {
                        AlarmNumber = alarmNumber, BatchID = batchID, Timestamp = _date, StopReason = stopReasons[index]
                    });
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


                    string path = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
                    path = Directory.GetParent(path).FullName;
                    path = Directory.GetParent(Directory.GetParent(path).FullName).FullName;
                    path += @"\MES\Logic\AlarmLogFileTest\alarmLogFile.txt";

                    System.IO.File.AppendAllText(path, result);


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
            string path = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
            path = Directory.GetParent(path).FullName;
            path = Directory.GetParent(Directory.GetParent(path).FullName).FullName;
            path += @"\MES\Logic\AlarmLogFileTest\alarmLogFile.txt";

            if (File.Exists(path))
            {

                using (var sr =
                    new StreamReader(path))
                {
                    string[] stringTokens;
                    int i = 0;
                    while (!sr.EndOfStream)
                    {
                        string fileLine = sr.ReadLine();
                        i++;

                        stringTokens = fileLine.Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries);

                        if (stringTokens.Length == 6)
                        {
                            _alarms.Add(
                                new AlarmObject()
                                {
                                    AlarmNumber = Int32.Parse(stringTokens[0]),
                                    BatchID = Int32.Parse(stringTokens[1]),
                                    Timestamp = stringTokens[2] + " " + stringTokens[3],
                                    StopReason = stringTokens[4] + " " + stringTokens[5]
                                });
                        }
                    }
                }
            }
        }

        private bool IsFileIsEmpty()
        {
            string path = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
            path = Directory.GetParent(path).FullName;
            path = Directory.GetParent(Directory.GetParent(path).FullName).FullName;
            path += @"\MES\Logic\AlarmLogFileTest\alarmLogFile.txt";
            if (File.Exists(path))
            {
                using (var sr =
                    new StreamReader(path)) {
                    string[] stringTokens;
                    if (sr.Peek() <= 0) {
                        return false;
                    }

                    return true;
                }

            }

            return false;


        }

        public ObservableCollection<AlarmObject> Alarms
        {
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