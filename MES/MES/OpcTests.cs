using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using UnifiedAutomation.UaClient;

namespace MES {
    [TestFixture]
    public class OpcTests {
        private readonly OpcClient opc = new OpcClient();
        [Test]
        public void TestConnection() {
            opc.Connect();
            Assert.AreEqual(opc.session.ConnectionStatus, ServerConnectionStatus.Connected);
        }

    }
}
