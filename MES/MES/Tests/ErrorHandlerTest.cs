using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MES.Acquintance;
using MES.Logic;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace MES.Tests
{
    [TestFixture]
    class ErrorHandlerTest
    {
        private readonly LogicFacade logic = new LogicFacade();

        private int alarmNumber;

        //[Test]
        //public void TestAddAlarm(int batchID, double stopReason)
        //{
        //    alarmNumber = logic.ErrorHandler.Alarms.Count;
        //    logic.ErrorHandler.AddAlarm(batchID, stopReason);
        //    Assert.AreEqual(logic.ErrorHandler.Alarms.Last(), logic.ErrorHandler.Alarms[alarmNumber]);
        //}
    }
}