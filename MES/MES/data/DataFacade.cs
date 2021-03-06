﻿using MES.Acquintance;
using MES.data;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MES.Data
{
    public class DataFacade : IData
    {
        private IDBManager dbManager;
        private IUserManager userManager;
        private IUser currentUser;
        private FileManager fileManager;
        private BatchReportGenerator batchReportGenerator;

        public DataFacade()
        {
            dbManager = new DBManager();
            userManager = new UserManager();
            currentUser = null;
            fileManager = new FileManager();
            batchReportGenerator = new BatchReportGenerator();
        }

        public ObservableCollection<IAlarmObject> ReadFile()
        {
            return fileManager.ReadFile();
        }

        public void WriteToFile(string s)
        {
            fileManager.WriteToFile(s);
        }

        public bool SaveBatch(float batchId, float beerId, int acceptableProducts,
            int defectProducts, string timestampStart, string timestampEnd, double oee)
        {
            return dbManager.InsertIntoBatchesTable(
                new Batch(batchId, beerId, acceptableProducts,
                    defectProducts, 10, timestampStart, timestampEnd, oee, 10));
        }

        public bool SaveBatch(float batchId, float beerId, int acceptableProducts,
           int defectProducts, double speed, string timestampStart, string timestampEnd,
           double oee, ISet<IList<IBatchValue>> batchValues, double ppm)
        {
            // Creates batch report - null value is given in place of "timeUsed", which has not been implemented.
            batchReportGenerator.GenerateFile(batchId, beerId, acceptableProducts, defectProducts, null, batchValues);

            IBatch batch = new Batch(batchId, beerId, acceptableProducts,
                    defectProducts, speed, timestampStart, timestampEnd, oee, ppm);
            foreach (IList<IBatchValue> list in batchValues)
            {
                batch.AddBatchValues(list);
            }
            return dbManager.InsertIntoBatchesTable(batch);
        }

        public bool SaveBatch(IBatch batch)
        {
            return dbManager.InsertIntoBatchesTable(batch);
        }

        public bool InsertBatchValueSet(ISet<IList<IBatchValue>> batchValues, float batchId)
        {
            return dbManager.InsertBatchValueSet(batchValues, batchId);
        }

        public bool UpdateBatch(IBatch batch)
        {
            return dbManager.UpdateBatch(batch);
        }

        public IDictionary<float, IBatch> GetAllBatches()
        {
            return dbManager.GetAllBatches();
        }

        public IDictionary<float, IBatch> GetBatches(string month, string year)
        {
            return dbManager.GetBatches(month, year);
        }

        public IDictionary<float, IBatch> GetBatches(int amount)
        {
            return dbManager.GetBatches(amount);
        }

        public IBatch GetBatch(float batchId)
        {
            return dbManager.GetBatch(batchId);
        }

        public bool DeleteAllBatches()
        {
            return dbManager.DeleteAllBatches();
        }

        public bool DeleteBatch(float batchId)
        {
            return dbManager.DeleteBatch(batchId);
        }

        public bool RunQueries(string[] statements)
        {
            return dbManager.RunQueries(statements);
        }

        public bool RunQuery(string statement)
        {
            string[] s = { statement };
            return dbManager.RunQueries(s);
        }

        public IDictionary<float, IRecipe> GetAllRecipes()
        {
            return dbManager.GetAllRecipes();
        }

        public bool AddRecipes(IRecipe[] recipes)
        {
            return dbManager.AddRecipes(recipes);
        }

        public float GetHighestBatchId()
        {
            return dbManager.GetHighestBatchId();
        }

        public bool AuthenticateUserInformation(string username, string password)
        {
            IUser user = userManager.AuthenticateUserInformation(username, password);
            if (user != null)
            {
                this.currentUser = user;
                return true;
            }
            else
            {
                return false;
            }
        }

        public double GetOptimalSpeed(IRecipe recipe)
        {
            return dbManager.GetOptimalSpeed(recipe);
        }
    }
}
