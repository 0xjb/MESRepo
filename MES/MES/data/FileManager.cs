using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MES.Acquintance;

namespace MES.Data
{
    class FileManager
    {
        private ObservableCollection<IAlarmObject> _alarms;

        private int alarmNumber;

        private StringBuilder stringBuilder;
        private string[] alarmsToFile;

        public FileManager()
        {
            _alarms = new ObservableCollection<IAlarmObject>();
            alarmsToFile = new string[4];
            if (!IsFileIsEmpty())
            {
                stringBuilder = new StringBuilder();
                stringBuilder.AppendFormat("{0,-15} {1,-20} {2,-40} {3,-40} {4,-40}", "Alarm Number:", "Batch Id:",
                    "Time and Date:", "Stop Reason:", "Stop Reason ID:");
                stringBuilder.AppendLine();
            }
            else
            {
                stringBuilder = new StringBuilder();
            }
        }

        public void WriteToFile(string s)
        {

            string path = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
            path = Directory.GetParent(path).FullName;
            path = Directory.GetParent(Directory.GetParent(path).FullName).FullName;
            path += @"\MES\Data\AlarmLogFile\alarmLogFile.txt";

            stringBuilder.Append(s);
            string s2 = stringBuilder.ToString();
           

            System.IO.File.AppendAllText(path, s2);
            stringBuilder.Clear();
        }


        public ObservableCollection<IAlarmObject> ReadFile()
        {
            string path = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
            path = Directory.GetParent(path).FullName;
            path = Directory.GetParent(Directory.GetParent(path).FullName).FullName;
            path += @"\MES\Data\AlarmLogFile\alarmLogFile.txt";

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

                        if (stringTokens.Length == 8)
                        {
                            _alarms.Add(
                                new AlarmObject()
                                {
                                    AlarmNumber = Int32.Parse(stringTokens[0]),
                                    BatchID = Int32.Parse(stringTokens[1]),
                                    Timestamp = stringTokens[2] + " " + stringTokens[3],
                                    StopReason = stringTokens[4] + " " + stringTokens[5],
                                    StopID = Int32.Parse(stringTokens[6] + " " + stringTokens[7])
                                });
                        }
                    }
                }
            }
            return _alarms;
        }


        private bool IsFileIsEmpty()
        {
            string path = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
            path = Directory.GetParent(path).FullName;
            path = Directory.GetParent(Directory.GetParent(path).FullName).FullName;
            path += @"\MES\Data\AlarmLogFile\alarmLogFile.txt";

            if (File.Exists(path))
            {
                using (var sr =
                    new StreamReader(path))
                {
                    if (sr.Peek() <= 0)
                    {
                        return false;
                    }

                    return true;
                }
            }
            return false;
        }
    }
}