using System;
using MES.Acquintance;
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

        public DataFacade()
        {
            dbManager = new DBManager();
            userManager = new UserManager();
            currentUser = null;
            fileManager=new FileManager();
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
            int defectProducts, string timestampStart, string timestampEnd)
        {
            return dbManager.InsertIntoBatchesTable(
                new Batch(batchId, beerId, acceptableProducts,
                    defectProducts, timestampStart, timestampEnd));
        }

        public bool SaveBatch(IBatch batch)
        {
            return dbManager.InsertIntoBatchesTable(batch);
        }

        public bool InsertBatchValueSet(float temperature, float humidity,
            float vibration, string timestamp, float batchId)
        {
            return dbManager.InsertBatchValueSet(temperature, humidity,
                vibration, timestamp, batchId);
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
            string[] s = {statement};
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

        public IAlarmObject CreateNewAlarm(int alarmNumber, int batchId, string date, string stopReason, int stopId)
        {
            IAlarmObject alarm = new AlarmObject();

            alarm.AlarmNumber = alarmNumber;
            alarm.BatchID = batchId;
            alarm.Timestamp = date;
            alarm.StopReason = stopReason;
            alarm.StopID = stopId;

            return alarm;
        }
    }
}