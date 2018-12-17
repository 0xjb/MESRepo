using NUnit.Framework;
using System.Threading;
using MES.Acquintance;
using UnifiedAutomation.UaClient;
using MES.Logic;

namespace MES.Tests
{
    [TestFixture]
    public class OpcTests
    {
        private readonly OpcClient opc = new OpcClient();

        [Test]
        public void TestOpcClient()
        {
            // Test the if the client is connected to the server.
            opc.Connect();

            // sleep for x amount of miliseconds because there is 
            // several machine statues before reaching endstatus.
            Thread.Sleep(500);
            Assert.AreEqual(opc.session.ConnectionStatus, ServerConnectionStatus.Connected);

            // Test to reboot the system by aborting any current state.
            opc.AbortMachine();

            // sleep for x amount of miliseconds because there is 
            // several machine statues before reaching endstatus.
            Thread.Sleep(500);
            Assert.AreEqual(opc.ReadStateCurrent(), 9);

            // Test to clear the abort status to be able to start the program again.
            opc.ClearMachine();

            // sleep for x amount of miliseconds because there is 
            // several machine statues before reaching endstatus.
            Thread.Sleep(500);
            Assert.AreEqual(opc.ReadStateCurrent(), 2);

            // Test if the machine has the reset status after reset
            opc.ResetMachine();

            // sleep for x amount of miliseconds because there is 
            // several machine statues before reaching endstatus.
            Thread.Sleep(4000);
            Assert.AreEqual(opc.ReadStateCurrent(), 4);

            // Test to see if the machine has been started correctly.
            opc.StartMachine(003, 1, 200, 1);

            // sleep for x amount of miliseconds because there is 
            // several machine statues before reaching endstatus.
            Thread.Sleep(300);
            Assert.AreEqual(opc.ReadStateCurrent(), 6);

            // Test to see if the machine speed of the test system is equal 
            // to current machine speed in primary products per minute.
            // Result will depend on product type.
            opc.ReadMachineSpeed();
            Assert.AreEqual(opc.ReadMachineSpeed(), 1);

            // Test to see if the current machine speed of the test system 
            // is equal to a normalized machine speed requested by the client.
            opc.ReadCurrentMachineSpeed();
            Assert.AreEqual(opc.ReadCurrentMachineSpeed(), 1);

            // Test to see if the current batch ID of the test system 
            // is equal to the batch ID requested by the client.
            opc.ReadCurrentBatchId();
            Assert.AreEqual(opc.ReadCurrentBatchId(), 3);

            // Test to see if the product amount in the batch of the 
            // test system is equal to the batch ID requested by the client.
            opc.ReadProductAmountInBatch();
            Assert.AreEqual(opc.ReadProductAmountInBatch(), 200);

            // Test to see if the machine has been stopped correctly.
            opc.StopMachine();
            Thread.Sleep(200);
            Assert.AreEqual(opc.ReadStateCurrent(), 2);
        }
    }
}