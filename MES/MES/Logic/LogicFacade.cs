﻿using MES.Acquintance;
using System;

namespace MES.Logic {
    public class LogicFacade : ILogic {
        private IData data;
        private ErrorHandler errorHandler;
        private OpcClient opc;
        private BatchQueue batches;
        private TestSimulation _testSimulation;
        private bool isSimulationON;

        public LogicFacade() {
            this.opc = new OpcClient(this);
            Batches = new BatchQueue(OPC);
        }

        public BatchQueue Batches {
            get { return batches; }
            set { batches = value; }
        }

        public TestSimulation TestSimulation {
            get => _testSimulation;
            set => _testSimulation = value;
        }

        public OpcClient OPC {
            get { return opc; }
            set { opc = value; }
        }

        public IData Data {
            get => data;
            set => data = value;
        }

        public ErrorHandler ErrorHandler {
            get => errorHandler;
            set => errorHandler = value;
        }

        public bool IsSimulationOn
        {
            get => isSimulationON;
            set => isSimulationON = value;
        }

        public void InjectData(IData dataLayer) {
            data = dataLayer;
            CreateErrorHandler();
        }

        public void CreateSimulation() {
            if (isSimulationON) {
                Console.WriteLine("Simulation ON");
                this._testSimulation = new TestSimulation(opc, this);
            }
        }

        public void CreateErrorHandler() {
            this.errorHandler = new ErrorHandler(this);
        }
        public void CreateBatch(float batchId, float amount, float productType) {
            SimpleBatch b = new SimpleBatch(batchId, productType, amount);
            if (Batches.CurrentBatch == null) {
                Batches.CurrentBatch = b;
            } else {
                Batches.Batches.Add(new SimpleBatch(batchId, productType, amount));
            }
        }

        public void AddBatch(string batchID, string productType, string amount) {
            Console.WriteLine("yeet");
        }

        public void StartProduction() {
            OPC.StartMachine(Batches.CurrentBatch.BatchID, Batches.CurrentBatch.BeerType, Batches.CurrentBatch.DesiredAmount, 60);
        }

        public bool AuthenticateUserInformation(string username, string password) {
            return data.AuthenticateUserInformation(username, password);
        }
    }
}


