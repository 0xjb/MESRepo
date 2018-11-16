using NUnit.Framework;
using System.Threading;
using UnifiedAutomation.UaClient;

namespace MES.Logic
{
    [TestFixture]
    public class OpcTests
    {
        private readonly OpcClient opc = new OpcClient();
        [Test]
        public void TestConnection()
        {
            opc.Connect();
            Assert.AreEqual(opc.session.ConnectionStatus, ServerConnectionStatus.Connected);
        }
        [Test]
        public void TestReset()
        {
            opc.Connect();
            opc.ResetMachine();
            Thread.Sleep(500);
            Assert.AreEqual(opc.ReadStateCurrent(), 4);
            Thread.Sleep(1000);
        }
        [Test]
        public void TestStart()
        {
            opc.Connect();
            opc.StartMachine(003, 1, 200, 1);
            Thread.Sleep(700);
            Assert.AreEqual(opc.ReadStateCurrent(), 6);
            Thread.Sleep(1000);
        }

        [Test]
        public void TestStop()
        {

            opc.Connect();
            opc.StopMachine();
            Thread.Sleep(1000);
            Assert.AreEqual(opc.ReadStateCurrent(), 2);
            Thread.Sleep(1000);
        }

        [Test]
        public void TestAbort()
        {

            opc.Connect();
            opc.AbortMachine();
            Thread.Sleep(1000);
            Assert.AreEqual(opc.ReadStateCurrent(), 9);
            Thread.Sleep(1000);
        }

        [Test]
        public void TestClear()
        {

            opc.Connect();
            opc.ClearMachine();
            Thread.Sleep(1000);
            Assert.AreEqual(opc.ReadStateCurrent(), 2);
            Thread.Sleep(1000);
        }
        [Test]
        public void TestCurrentMachineSpeed()
        {

            opc.Connect();
            opc.ReadCurrentMachineSpeed();
            Thread.Sleep(1000);
            Assert.AreEqual(opc.ReadCurrentMachineSpeed(), 1);
            Thread.Sleep(1000);
        }
        [Test]
        public void TestMachineSpeed()
        {

            opc.Connect();
            opc.ReadMachineSpeed();
            Thread.Sleep(1000);
            Assert.AreEqual(opc.ReadMachineSpeed(), 1);
            Thread.Sleep(1000);
        }
        [Test]
        public void TestReadBatchId()
        {

            opc.Connect();
            opc.ReadCurrentBatchId();
            Thread.Sleep(1000);
            Assert.AreEqual(opc.ReadCurrentBatchId(), 3);
            Thread.Sleep(1000);
        }
        [Test]
        public void TestProductAmountInBatch()
        {

            opc.Connect();
            opc.ReadProductAmountInBatch();
            Thread.Sleep(1000);
            Assert.AreEqual(opc.ReadProductAmountInBatch(), 200);
            Thread.Sleep(1000);
        }
        [Test]
        public void TestCurrentHumidity()
        {

            opc.Connect();
            opc.ReadCurrentHumidity();
            Thread.Sleep(1000);
            Assert.AreEqual(opc.ReadCurrentHumidity(), 0);
            Thread.Sleep(1000);
        }
        [Test]
        public void TestCurrentTemperature()
        {

            opc.Connect();
            opc.ReadCurrentTemperature();
            Thread.Sleep(1000);
            Assert.AreEqual(opc.ReadCurrentTemperature(), 0);
            Thread.Sleep(1000);
        }
        [Test]
        public void TestCurrentVibration()
        {

            opc.Connect();
            opc.ReadCurrentVibration();
            Thread.Sleep(1000);
            Assert.AreEqual(opc.ReadCurrentVibration(), 0);
            Thread.Sleep(1000);
        }
    }

}
