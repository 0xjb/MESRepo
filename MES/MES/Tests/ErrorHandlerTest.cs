using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MES.Acquintance;
using MES.Logic;
using NUnit.Framework;

namespace MES.Tests
{
    [TestFixture]
    class ErrorHandlerTest
    {
        private readonly LogicFacade logic = new LogicFacade();

        [Test]
        public void TestErrorHandler()
        {
            logic.ErrorHandler.AddAlarm(10, 14);
           
        }
    }
}