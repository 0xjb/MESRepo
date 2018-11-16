﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using UnifiedAutomation.UaBase;
using UnifiedAutomation.UaClient;

namespace MES.Logic
{
    public class OpcClient : INotifyPropertyChanged
    {
        private bool isProcessRunning = false;
        private bool isMachineStarted = false;
        private double processedProducts;
        private double defectProducts;
        private double stateCurrent;
        private double tempCurrent;
        public Session session;

        //TODO Skal fjernes
        private Thread thread2;

        public event PropertyChangedEventHandler PropertyChanged;

        public OpcClient()
        {
            Connect();
            // testing purposes
            CreateSubscription();
        }


        public void Connect()
        {
            session = new Session();

            //Connect to server with no security
            session.Connect("opc.tcp://127.0.0.1:4840", SecuritySelection.None);
        }

        public void CreateSubscription()
        {
            Subscription s = new Subscription(session);
            // node to monitor
            NodeId amountNode = new NodeId("::Program:Cube.Admin.ProdProcessedCount", 6);
            NodeId stateNode = new NodeId("::Program:Cube.Status.StateCurrent", 6);
            NodeId defectNode = new NodeId("::Program:Cube.Admin.ProdDefectiveCount", 6);
            NodeId tempNode = new NodeId("::Program:Cube.Status.Parameter[3].Value", 6);
            // list of monitored items
            List<MonitoredItem> monitoredItems = new List<MonitoredItem>();
            // convert nodeid to datamonitoreditem
            MonitoredItem miAmountNode = new DataMonitoredItem(amountNode);
            MonitoredItem miStateNode = new DataMonitoredItem(stateNode);
            MonitoredItem miDefectNode = new DataMonitoredItem(defectNode);
            MonitoredItem miTempNode = new DataMonitoredItem(tempNode);
            monitoredItems.Add(miAmountNode);
            monitoredItems.Add(miStateNode);
            monitoredItems.Add(miDefectNode);
            monitoredItems.Add(miTempNode);

            // init subscription with parameters
            s = new Subscription(session);
            s.PublishingInterval = 100;
            s.MaxKeepAliveTime = 1000;
            s.Lifetime = 1000000;
            s.MaxNotificationsPerPublish = 1;
            s.Priority = (byte)0;
            s.DataChanged += OnDataChanged;
            s.PublishingEnabled = true;
            s.CreateMonitoredItems(monitoredItems);
            // create the actual subscription
            s.Create(new RequestSettings() { OperationTimeout = 10000 });
        }

        private void OnDataChanged(Subscription s, DataChangedEventArgs e)
        {
            foreach (DataChange dc in e.DataChanges) {
                switch (dc.MonitoredItem.NodeId.Identifier.ToString()) {
                    case "::Program:Cube.Status.StateCurrent":
                        StateCurrent = double.Parse(dc.Value.ToString());
                        break;
                    // products processed
                    case "::Program:Cube.Admin.ProdProcessedCount":
                        ProcessedProducts = double.Parse(dc.Value.ToString());
                        break;
                    //  temperature
                    case "::Program:Cube.Status.Parameter[3]":
                        TempCurrent = double.Parse(dc.Value.ToString());
                        break;
                    // defect products processed
                    case "::Program:Cube.Admin.ProdDefectiveCount":
                        ProcessedProducts = double.Parse(dc.Value.ToString());
                        break;
                    default:
                        break;
                }

            }
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
                nodesToWrite.Add(CreateWriteValue("::Program:Cube.Command.CmdChangeRequest", 6, Attributes.Value,
                    changeRequest));
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
                nodesToWrite.Add(CreateWriteValue("::Program:Cube.Command.CmdChangeRequest", 6, Attributes.Value,
                    changeRequest));
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
                nodesToWrite.Add(CreateWriteValue("::Program:Cube.Command.CmdChangeRequest", 6, Attributes.Value,
                    changeRequest));
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
                nodesToWrite.Add(CreateWriteValue("::Program:Cube.Command.CmdChangeRequest", 6, Attributes.Value,
                    changeRequest));
                Write(nodesToWrite);
                isProcessRunning = false;
            }
        }

        public void StartMachine(float batchId, float productType, float amountToProduce, float machineSpeed)
        {
            isMachineStarted = true;

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
                nodesToWrite.Add(CreateWriteValue("::Program:Cube.Command.CmdChangeRequest", 6, Attributes.Value,
                    changeRequest));


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
        public void Write(WriteValueCollection nodesToWrite)
        {
            session.Write(nodesToWrite);
        }
        //TODO skal fjernes herfra og ned (read metoder)
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

        public Int32 ReadCurrentProductsProcessed()
        {
            ReadValueIdCollection nodesToRead = new ReadValueIdCollection();
            nodesToRead.Add(new ReadValueId() {
                NodeId = new NodeId("::Program:Cube.Admin.ProdProcessedCount", 6),
                AttributeId = Attributes.Value
            });

            List<DataValue> results = session.Read(nodesToRead);
            DataValue dv = results[0];
            return (int)dv.Value;
        }

        public Int32 ReadDefectProducts()
        {
            ReadValueIdCollection nodesToRead = new ReadValueIdCollection();
            nodesToRead.Add(new ReadValueId() {
                NodeId = new NodeId("::Program:Cube.Admin.ProdDefectiveCount", 6),
                AttributeId = Attributes.Value
            });

            List<DataValue> results = session.Read(nodesToRead);
            DataValue dv = results[0];
            return (int)dv.Value;
        }


        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        public double ProcessedProducts
        {
            get { return processedProducts; }
            set
            {
                processedProducts = value;
                OnPropertyChanged("ProcessedProducts");
            }
        }

        public double DefectProducts
        {
            get { return defectProducts; }
            set
            {
                defectProducts = value;
                OnPropertyChanged("DefectProducts");
            }
        }

        public double StateCurrent
        {
            get { return stateCurrent; }
            set
            {
                stateCurrent = value;
                OnPropertyChanged("StateCurrent");
            }
        }

        public double TempCurrent
        {
            get { return tempCurrent; }
            set
            {
                tempCurrent = value;
                OnPropertyChanged("TempCurrent");
            }
        }
    }
}
