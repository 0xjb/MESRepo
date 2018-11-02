using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnifiedAutomation.UaBase;
using UnifiedAutomation.UaClient;

namespace MES {
    class OpcClient {
        public Session session;
        public void Connect() {
            session = new Session();

            //Connect to server with no security
            session.Connect("opc.tcp://127.0.0.1:4840", SecuritySelection.None);

        }
        public void ResetMachine() {
            // collection of nodes to be written
            WriteValueCollection nodesToWrite = new WriteValueCollection();
            DataValue reset = new DataValue();
            reset.Value = 1;
            DataValue changeRequest = new DataValue();
            changeRequest.Value = true;

            //nodesToWrite.Add(CreateWriteValue("::Program:Cube.Command.CntrlCmd", 6, Attributes.Value, reset));
            //nodesToWrite.Add(CreateWriteValue("::Program:Cube.Command.CmdChangeRequest", 6, Attributes.Value, changeRequest));
            //Write(nodesToWrite);
        }
        public void StartMachine(float batchId, float productType, float amountToProduce, float machineSpeed) {
            // collection of nodes to be written
            WriteValueCollection nodesToWrite = new WriteValueCollection();
            // Reset machine
            ResetMachine();

            DataValue start = new DataValue();
            start.Value = 2;
            DataValue changeRequest = new DataValue();
            changeRequest.Value = true;

            //write the nodes & clear the collection
            Thread.Sleep(2000);
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
        }
        private WriteValue CreateWriteValue(string nodeId, ushort namespaceIndex, uint attributeId, DataValue val) {
            return new WriteValue() {
                NodeId = new NodeId(nodeId, namespaceIndex),
                AttributeId = attributeId,
                Value = val
            };
        }
        private DataValue CreateDataValue(float f) {
            return new DataValue() {
                Value = f
            };
        }
        private void StatusUpdateHandler(Session s, ServerConnectionStatusUpdateEventArgs e) {
            Console.WriteLine("succ");
        }
        public void Read() {
            ReadValueIdCollection nodesToRead = new ReadValueIdCollection();
            nodesToRead.Add(new ReadValueId() {
                NodeId = new NodeId("::Program:Cube.Command.Parameter[0].Value", 6),
                AttributeId = Attributes.Value
            });

            List<DataValue> results = session.Read(nodesToRead);
            foreach (DataValue v in results) {
                Console.WriteLine(v);
            }
        }
        public void Write(WriteValueCollection nodesToWrite) {
            session.Write(nodesToWrite);
        }
        private BuiltInType readDataTypes() {
            ReadValueIdCollection nodesToRead = new ReadValueIdCollection();
            nodesToRead.Add(new ReadValueId() {
                NodeId = new NodeId("::Program:Cube.Command.Parameter[0].Value", 6),
                AttributeId = Attributes.DataType
            });

            List<DataValue> result = null;
            result = session.Read(nodesToRead, 0, TimestampsToReturn.Neither, null);
            return TypeUtils.GetBuiltInType((NodeId)result[0].Value);

        }
    }
}
