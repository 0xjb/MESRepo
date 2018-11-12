using System;
using System.Collections.Generic;
using System.Threading;
using UnifiedAutomation.UaBase;
using UnifiedAutomation.UaClient;

namespace MES
{
    public class OpcClient
    {
        private static ThreadStart ts = new ThreadStart(Subscript);
        Thread subscribeThread = new Thread(ts);



        bool isProcessRunning = false;
        public Session session;


        public OpcClient()
        {
            subscribeThread.IsBackground = true;
            subscribeThread.Start();
        }


        public void Connect()
        {


            session = new Session();

            //Connect to server with no security
            session.Connect("opc.tcp://127.0.0.1:4840", SecuritySelection.None);


        }

        public static void Subscript()
        {
            Session sessionSubscript = new Session();
            sessionSubscript.Connect("opc.tcp://127.0.0.1:4840", SecuritySelection.None);

            NodeId nodeId = new NodeId("::Program:Cube.Command.Parameter[0].Value", 6);
            MonitoredItem monitoredItem = new DataMonitoredItem(nodeId);
            Subscription subscription = new Subscription(sessionSubscript);
            //subscription.MonitoredItems[0] = monitoredItem;
            subscription.MonitoredItems[0].SamplingInterval = 100;


        }

        public void stopThread()
        {
            subscribeThread.Abort();
        }

        public void Disconnect()
        {


            session.Disconnect();


        }
        public void ResetMachine()
        {
            if (!isProcessRunning) {
                isProcessRunning = true;
                // collection of nodes to be written
                WriteValueCollection nodesToWrite = new WriteValueCollection();
                DataValue reset = new DataValue();
                reset.Value = 1;
                DataValue changeRequest = new DataValue();
                changeRequest.Value = true;

                nodesToWrite.Add(CreateWriteValue("::Program:Cube.Command.CntrlCmd", 6, Attributes.Value, reset));
                nodesToWrite.Add(CreateWriteValue("::Program:Cube.Command.CmdChangeRequest", 6, Attributes.Value, changeRequest));
                Write(nodesToWrite);
                isProcessRunning = false;
            }
        }

        public void StopMachine()
        {
            if (!isProcessRunning) {
                isProcessRunning = true;
                // collection of nodes to be written
                WriteValueCollection nodesToWrite = new WriteValueCollection();
                DataValue stop = new DataValue();
                stop.Value = 3;
                DataValue changeRequest = new DataValue();
                changeRequest.Value = true;

                nodesToWrite.Add(CreateWriteValue("::Program:Cube.Command.CntrlCmd", 6, Attributes.Value, stop));
                nodesToWrite.Add(CreateWriteValue("::Program:Cube.Command.CmdChangeRequest", 6, Attributes.Value, changeRequest));
                Write(nodesToWrite);
                isProcessRunning = false;
            }
        }

        public void AbortMachine()
        {
            if (!isProcessRunning) {
                isProcessRunning = true;
                // collection of nodes to be written
                WriteValueCollection nodesToWrite = new WriteValueCollection();
                DataValue abort = new DataValue();
                abort.Value = 4;
                DataValue changeRequest = new DataValue();
                changeRequest.Value = true;

                nodesToWrite.Add(CreateWriteValue("::Program:Cube.Command.CntrlCmd", 6, Attributes.Value, abort));
                nodesToWrite.Add(CreateWriteValue("::Program:Cube.Command.CmdChangeRequest", 6, Attributes.Value, changeRequest));
                Write(nodesToWrite);
                isProcessRunning = false;
            }
        }

        public void ClearMachine()
        {
            if (!isProcessRunning) {
                isProcessRunning = true;
                // collection of nodes to be written
                WriteValueCollection nodesToWrite = new WriteValueCollection();
                DataValue clear = new DataValue();
                clear.Value = 5;
                DataValue changeRequest = new DataValue();
                changeRequest.Value = true;

                nodesToWrite.Add(CreateWriteValue("::Program:Cube.Command.CntrlCmd", 6, Attributes.Value, clear));
                nodesToWrite.Add(CreateWriteValue("::Program:Cube.Command.CmdChangeRequest", 6, Attributes.Value, changeRequest));
                Write(nodesToWrite);
                isProcessRunning = false;
            }
        }

        public void StartMachine(float batchId, float productType, float amountToProduce, float machineSpeed)
        {

            if (!isProcessRunning) {
                isProcessRunning = true;
                // collection of nodes to be written
                WriteValueCollection nodesToWrite = new WriteValueCollection();

                DataValue start = new DataValue();
                start.Value = 2;
                DataValue changeRequest = new DataValue();
                changeRequest.Value = true;

                //write the nodes & clear the collection
                //Thread.Sleep(2000);
                // Create nodes necessary for starting machine
                // batch id
                nodesToWrite.Add(CreateWriteValue("::Program:Cube.Command.Parameter[0].Value", 6,
                    Attributes.Value, CreateDataValue(batchId)));
                // product type
                nodesToWrite.Add(CreateWriteValue("::Program:Cube.Command.Parameter[1].Value", 6,
                    Attributes.Value, CreateDataValue(productType)));
                // amount of product to be produced
                nodesToWrite.Add(CreateWriteValue("::Program:Cube.Command.Parameter[2].Value", 6,
                    Attributes.Value, CreateDataValue(amountToProduce)));
                // machine speed
                nodesToWrite.Add(CreateWriteValue("::Program:Cube.Command.MachSpeed", 6,
                    Attributes.Value, CreateDataValue(machineSpeed)));
                nodesToWrite.Add(CreateWriteValue("::Program:Cube.Command.CntrlCmd", 6, Attributes.Value, start));
                nodesToWrite.Add(CreateWriteValue("::Program:Cube.Command.CmdChangeRequest", 6, Attributes.Value, changeRequest));


                Write(nodesToWrite);
                isProcessRunning = false;
            }

        }
        private WriteValue CreateWriteValue(string nodeId, ushort namespaceIndex, uint attributeId, DataValue val)
        {
            return new WriteValue() {
                NodeId = new NodeId(nodeId, namespaceIndex),
                AttributeId = attributeId,
                Value = val
            };
        }
        private DataValue CreateDataValue(float f)
        {
            return new DataValue() {
                Value = f
            };
        }
        private void StatusUpdateHandler(Session s, ServerConnectionStatusUpdateEventArgs e)
        {
            Console.WriteLine("succ");
        }
        public Int32 ReadStateCurrent()
        {
            ReadValueIdCollection nodesToRead = new ReadValueIdCollection();
            nodesToRead.Add(new ReadValueId() {
                NodeId = new NodeId("::Program:Cube.Status.StateCurrent", 6),
                AttributeId = Attributes.Value
            });

            List<DataValue> results = session.Read(nodesToRead);
            DataValue dv = results[0];
            return (int)dv.Value;
        }
        public void Write(WriteValueCollection nodesToWrite)
        {
            session.Write(nodesToWrite);
        }
        public int readDataTypes()
        {
            ReadValueIdCollection nodesToRead = new ReadValueIdCollection();
            nodesToRead.Add(new ReadValueId() {
                NodeId = new NodeId("::Program:Cube.Command.Parameter[0].Value", 6),
                AttributeId = Attributes.Value
            });

            List<DataValue> result = null;
            result = session.Read(nodesToRead, 0, TimestampsToReturn.Neither, null);
            //return TypeUtils.GetBuiltInType((NodeId)result[0].Value);
            return (int)result[0].Value;

        }

        public float ReadCurrentMachineSpeed()
        {
            ReadValueIdCollection nodesToRead = new ReadValueIdCollection();
            nodesToRead.Add(new ReadValueId() {
                NodeId = new NodeId("::Program:Cube.Status.CurMachSpeed", 6),
                AttributeId = Attributes.Value
            });

            List<DataValue> results = session.Read(nodesToRead);
            DataValue dv = results[0];
            return (float)dv.Value;
        }
        public float ReadMachineSpeed()
        {
            ReadValueIdCollection nodesToRead = new ReadValueIdCollection();
            nodesToRead.Add(new ReadValueId() {
                NodeId = new NodeId("::Program:Cube.Status.MachSpeed", 6),
                AttributeId = Attributes.Value
            });

            List<DataValue> results = session.Read(nodesToRead);
            DataValue dv = results[0];
            return (float)dv.Value;
        }

        public float ReadCurrentBatchId()
        {
            ReadValueIdCollection nodesToRead = new ReadValueIdCollection();
            nodesToRead.Add(new ReadValueId() {
                NodeId = new NodeId("::Program:Cube.Status.Parameter[0].Value", 6),
                AttributeId = Attributes.Value
            });

            List<DataValue> results = session.Read(nodesToRead);
            DataValue dv = results[0];
            return (float)dv.Value;
        }
        public float ReadProductAmountInBatch()
        {
            ReadValueIdCollection nodesToRead = new ReadValueIdCollection();
            nodesToRead.Add(new ReadValueId() {
                NodeId = new NodeId("::Program:Cube.Status.Parameter[1].Value", 6),
                AttributeId = Attributes.Value
            });

            List<DataValue> results = session.Read(nodesToRead);
            DataValue dv = results[0];
            return (float)dv.Value;
        }
        public float ReadCurrentHumidity()
        {
            ReadValueIdCollection nodesToRead = new ReadValueIdCollection();
            nodesToRead.Add(new ReadValueId() {
                NodeId = new NodeId("::Program:Cube.Status.Parameter[2].Value", 6),
                AttributeId = Attributes.Value
            });

            List<DataValue> results = session.Read(nodesToRead);
            DataValue dv = results[0];
            return (float)dv.Value;
        }
        public float ReadCurrentTemperature()
        {
            ReadValueIdCollection nodesToRead = new ReadValueIdCollection();
            nodesToRead.Add(new ReadValueId() {
                NodeId = new NodeId("::Program:Cube.Status.Parameter[3].Value", 6),
                AttributeId = Attributes.Value
            });

            List<DataValue> results = session.Read(nodesToRead);
            DataValue dv = results[0];
            return (float)dv.Value;
        }
        public float ReadCurrentVibration()
        {
            ReadValueIdCollection nodesToRead = new ReadValueIdCollection();
            nodesToRead.Add(new ReadValueId() {
                NodeId = new NodeId("::Program:Cube.Status.Parameter[4].Value", 6),
                AttributeId = Attributes.Value
            });

            List<DataValue> results = session.Read(nodesToRead);
            DataValue dv = results[0];
            return (float)dv.Value;
        }
    }
}
