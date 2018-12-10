using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MES.Acquintance;
using MES.Data;
using MES.Logic;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace MES.Tests
{
    [TestFixture]
    class ErrorHandlerTest
    {
        private readonly LogicFacade logic;
        private readonly IData data;
        private readonly ObservableCollection<IAlarmObject> _alarms;
        private int alarmNumber;

        public ErrorHandlerTest()
        {
            logic = new LogicFacade();
            data = new DataFacade();
            logic.InjectData(data);
        }

        [Test]
        public void TestAddAlarm()
        {
            int batchID = 1;
            int stopReason = 14;
            alarmNumber = logic.ErrorHandler.Alarms.Count;
            logic.ErrorHandler.AddAlarm(batchID, stopReason);

            Assert.IsTrue(logic.ErrorHandler.Alarms.Last().BatchID == 1 && logic.ErrorHandler.Alarms.Last().StopID == 14);
        }
    }
}