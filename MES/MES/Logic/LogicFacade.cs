using MES.Acquintance;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using MES.Annotations;
using MES.Data;

namespace MES.Logic
{
    public class LogicFacade : ILogic
    {
        // should depend on actual data but we dont have any
        private double productionCost = 5;
        private double salePrice = 20;


        private IData data;
        private ErrorHandler errorHandler;
        private OpcClient opc;
        private BatchQueue batches;
        private TestSimulation _testSimulation;
        private bool isSimulationON;
        private ObservableCollection<IBatch> oEEList;

        public LogicFacade()
        {
            CultureInfo.CurrentCulture = new CultureInfo("en-US", false);

            this.opc = new OpcClient(this);
            
            //Batches = new BatchQueue(OPC);
            oEEList = new ObservableCollection<IBatch>();
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

        public bool IsSimulationOn {
            get => isSimulationON;
            set => isSimulationON = value;
        }

        public void InjectData(IData dataLayer)
        {
            data = dataLayer;
            CreateErrorHandler();
            Batches = new BatchQueue(this);
        }

        public void CreateSimulation()
        {
            if (isSimulationON)
            {
                Console.WriteLine("Simulation ON");
                this._testSimulation = new TestSimulation(opc, this);
            }
        }

        public void CreateErrorHandler()
        {
            this.errorHandler = new ErrorHandler(this);
        }

        public void CreateBatch(float amount, float speed, IRecipe recipe)
        {
            Batches.CreateBatch(amount, speed, recipe);

        }

        public IDictionary<float, IRecipe> GetAllRecipes()
        {
            return data.GetAllRecipes();
        }

        public void AddBatch(string batchID, string productType, string amount)
        {
        }

        public float GetHighestBatchId()
        {
            return data.GetHighestBatchId();
        }

        public void StartProduction()
        {
            OPC.PrepareBatchValues();

            Batches.PrepareBatchForProduction();

            OPC.StartMachine(Batches.CurrentBatch.BatchID, Batches.CurrentBatch.BeerType,
                Batches.CurrentBatch.Amount, Batches.CurrentBatch.Speed);
        } 

        public bool AuthenticateUserInformation(string username, string password)
        {
            return data.AuthenticateUserInformation(username, password);
        }

        public bool addOEEFromBatch(int batchId)
        {
            if (data.GetBatch(batchId) != null)
            {
                oEEList.Add(data.GetBatch(batchId));
                return true;
            }
            else
            {
                return false;
            }
        }

        public void SearchNewestBatches(int number)
        {
            IDictionary<float, IBatch> dictionary = data.GetBatches(number);


            foreach (KeyValuePair<float, IBatch> entry in dictionary)
            {
                oEEList.Add(entry.Value);
            }
        }

        public void SearchDateYearBatches(string month, string year)
        {
            IDictionary<float, IBatch> dictionary = data.GetBatches(month,year);



            foreach (KeyValuePair<float, IBatch> entry in dictionary) {
                oEEList.Add(entry.Value);
            }
        }

        public ObservableCollection<IBatch> OEeList
        {
            get => oEEList;
            set => oEEList = value;
        }


        private double CalculatePPM(ISimpleBatch b)
        {
            //Profit per minute
            DateTime startTime = DateTime.Parse(b.TimeStart);
            DateTime endTime = DateTime.Parse(b.TimeEnd);
            TimeSpan elapsedTime = endTime - startTime;
            //Sale price
            double timeInMinutes = elapsedTime.TotalMinutes;
            double salePricePerMin = (OPC.AcceptableProducts / timeInMinutes) * salePrice;

            //Costs
            double costPerMin = (OPC.DefectProducts / timeInMinutes) * productionCost;

            //Profit
            return salePricePerMin - costPerMin;

        }
        public void SaveBatch(ISimpleBatch s)
        {
            ISet<IList<IBatchValue>> set = new HashSet<IList<IBatchValue>>();
            set.Add(OPC.TempList);
            set.Add(OPC.HumidityList);
            set.Add(OPC.VibrationList);
            Data.SaveBatch(s.BatchID, s.BeerType, (int)OPC.AcceptableProducts,
                (int)OPC.DefectProducts,s.Speed, s.TimeStart, s.TimeEnd, s.OEE, set, CalculatePPM(s));
        }

        public ISimpleBatch GetCurrentBatch()
        {
            return Batches.CurrentBatch;
        }
        public IDictionary<float, IBatch> GetAllBatches()
        {
            return data.GetAllBatches();
        }

        public double GetOptimalSpeed(IRecipe recipe)
        {
            return data.GetOptimalSpeed(recipe);
        }
    }
}