using MES.Acquintance;
using MES.Data;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES.Tests {
    [TestFixture]
    class FileManagerTest {
        private readonly FileManager fileManager = new FileManager();
        [Test]
        public void WriteToFileTest() {
            string path = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
            path = Directory.GetParent(path).FullName;
            path = Directory.GetParent(Directory.GetParent(path).FullName).FullName;
            path += @"\MES\Data\AlarmLogFile\alarmLogFile.txt";

            fileManager.WriteToFile("\nTEST STRING");
            string lastLine = File.ReadLines(path).Last();
            Assert.AreEqual("TEST STRING", lastLine);


            // read all lines from file & then shove them all back in without the test string
            var lines = System.IO.File.ReadAllLines(path);
            System.IO.File.WriteAllLines(path, lines.Take(lines.Length - 1).ToArray());
        }
        [Test]
        public void ReadFileTest() {
            string path = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
            path = Directory.GetParent(path).FullName;
            path = Directory.GetParent(Directory.GetParent(path).FullName).FullName;
            path += @"\MES\Data\AlarmLogFile\alarmLogFile.txt";

            ObservableCollection<IAlarmObject> alarms = new ObservableCollection<IAlarmObject>();
            fileManager.WriteToFile("\n39              0                    12-03-2018 09:26:00                      Manual TEST                              12");
            alarms = fileManager.ReadFile();
            IAlarmObject alarm = alarms.Last();
            Assert.AreEqual(alarm.StopReason, "Manual TEST");

            // read all lines from file & then shove them all back in without the test string
            var lines = System.IO.File.ReadAllLines(path);
            System.IO.File.WriteAllLines(path, lines.Take(lines.Length - 1).ToArray());
        }
    }
}
