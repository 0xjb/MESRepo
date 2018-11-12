using MES.Acquintance;
using MES.Data;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES.Tests
{
    [TestFixture]
    class DataTest
    {
        private readonly IDBManager dbManager = new data.DBManager();

        [Test]
        public void TestDBManager()
        {
            // Testing InsertBatch
            IBatch batch0 = new Batch(-1, 3, 90, 10,
                "02/11/2018 09:18:35", "02/11/2018 09:20:35");
            batch0.AddBatchValues(20, 30, 40, "02/11/2018 09:18:35");
            batch0.AddBatchValues(20, 30, 40, "02/11/2018 09:19:35");
            batch0.AddBatchValues(20, 30, 40, "02/11/2018 09:20:35");
            bool inserted0 = dbManager.InsertIntoBatchesTable(batch0);
            Assert.IsTrue(inserted0, "Inserted first");

            IBatch batch1 = new Batch(-2, 3, 90, 10,
               "02/11/2018 10:18:35", "02/11/2018 10:20:35");
            batch1.AddBatchValues(20, 30, 40, "02/11/2018 10:18:35");
            batch1.AddBatchValues(20, 30, 40, "02/11/2018 10:19:35");
            batch1.AddBatchValues(20, 30, 40, "02/11/2018 10:20:35");
            bool inserted1 = dbManager.InsertIntoBatchesTable(batch1);
            Assert.IsTrue(inserted1, "Inserted second");

            IBatch batch2 = new Batch(-3, 3, 90, 10,
                "02/12/2018 11:18:35", "02/12/2018 11:20:35");
            batch2.AddBatchValues(20, 30, 40, "02/12/2018 11:18:35");
            batch2.AddBatchValues(20, 30, 40, "02/12/2018 11:19:35");
            batch2.AddBatchValues(20, 30, 40, "02/12/2018 11:20:35");
            bool inserted2 = dbManager.InsertIntoBatchesTable(batch2);
            Assert.IsTrue(inserted2, "Inserted third");

            IBatch batch3 = new Batch(-4, 3, 90, 10,
                "02/12/2018 12:18:35", "02/12/2018 12:20:35");
            batch3.AddBatchValues(20, 30, 40, "02/12/2018 12:18:35");
            batch3.AddBatchValues(20, 30, 40, "02/12/2018 12:19:35");
            batch3.AddBatchValues(20, 30, 40, "02/12/2018 12:20:35");
            bool inserted3 = dbManager.InsertIntoBatchesTable(batch3);
            Assert.IsTrue(inserted3, "Inserted fourth");

            // Testing GetAllBatches
            IDictionary<float, IBatch> allBatches = dbManager.GetAllBatches();

            IBatch loadedBatch0 = null;
            allBatches.TryGetValue(-1, out loadedBatch0);
            Assert.IsNotNull(loadedBatch0, "Succes");

            IBatch loadedBatch1 = null;
            allBatches.TryGetValue(-2, out loadedBatch1);
            Assert.IsNotNull(loadedBatch0, "Succes");

            IBatch loadedBatch2 = null;
            allBatches.TryGetValue(-3, out loadedBatch2);
            Assert.IsNotNull(loadedBatch0, "Succes");

            IBatch loadedBatch3 = null;
            allBatches.TryGetValue(-4, out loadedBatch3);
            Assert.IsNotNull(loadedBatch0, "Succes");

            bool loadedBatchValuesO = false;
            if (loadedBatch0.GetBatchValues().Count == 3)
            {
                loadedBatchValuesO = true;
            }
            Assert.IsTrue(loadedBatchValuesO, "Succes");

            // Testing GetBatches
            IDictionary<float, IBatch> batchesByMonth = dbManager.GetBatches("11", "2018");

            IBatch loadedBatch4 = null;
            batchesByMonth.TryGetValue(-1, out loadedBatch4);
            Assert.IsNotNull(loadedBatch0, "Succes");

            IBatch loadedBatch5 = null;
            batchesByMonth.TryGetValue(-2, out loadedBatch5);
            Assert.IsNotNull(loadedBatch0, "Succes");

            IDictionary<float, IBatch> batchesByLimit = dbManager.GetBatches(3);

            IBatch loadedBatch6 = null;
            batchesByLimit.TryGetValue(-1, out loadedBatch6);
            Assert.IsNotNull(loadedBatch0, "Succes");

            IBatch loadedBatch7 = null;
            batchesByLimit.TryGetValue(-2, out loadedBatch7);
            Assert.IsNotNull(loadedBatch0, "Succes");

            IBatch loadedBatch8 = null;
            batchesByLimit.TryGetValue(-3, out loadedBatch8);
            Assert.IsNotNull(loadedBatch0, "Succes");

            // Testing GetBatch
            IBatch loadedBatch9 = dbManager.GetBatch(-1);
            Assert.IsNotNull(loadedBatch9, "Succes");

            // Testing DeleteBatch
            bool deleted0 = dbManager.DeleteBatch(-1);
            Assert.IsTrue(deleted0, "Deleted");
            IBatch loadedBatch10 = dbManager.GetBatch(-1);
            Assert.IsNull(loadedBatch10, "Succes");

            bool deleted1 = dbManager.DeleteBatch(-2);
            Assert.IsTrue(deleted1, "Deleted");
            IBatch loadedBatch11 = dbManager.GetBatch(-1);
            Assert.IsNull(loadedBatch11, "Succes");

            bool deleted2 = dbManager.DeleteBatch(-3);
            Assert.IsTrue(deleted2, "Deleted");
            IBatch loadedBatch12 = dbManager.GetBatch(-1);
            Assert.IsNull(loadedBatch12, "Succes");

            bool deleted3 = dbManager.DeleteBatch(-4);
            Assert.IsTrue(deleted3, "Deleted");
            IBatch loadedBatch13 = dbManager.GetBatch(-1);
            Assert.IsNull(loadedBatch13, "Succes");
        }

        [Test]
        public void DBSetup()
        {
            string[] statements = new string[2];

            statements[0] = "CREATE TABLE batches("
                + "batchid FLOAT PRIMARY KEY, "
                + "beerid FLOAT, "
                + "acceptableproducts INT, "
                + "defectproducts INT, "
                + "timestampStart CHAR(19), "
                + "timestampEnd CHAR(19));";

            statements[1] = "CREATE TABLE batchvalues("
                + "temperature FLOAT, "
                + "humidity FLOAT, "
                + "vibration FLOAT, "
                + "timestampx CHAR(19), "
                + "belongingto FLOAT, "
                + "FOREIGN KEY(belongingto) REFERENCES batches(batchid) ON DELETE CASCADE ON UPDATE CASCADE, "
                + "PRIMARY KEY(timestampx, belongingto));";

            bool succes = dbManager.RunQueries(statements);
            Assert.IsTrue(succes, "Tables created");
        }

        [Test]
        public void DBDeleteOld()
        {
            string[] statements = { "DROP TABLE batches" };
            bool succes = dbManager.RunQueries(statements);
            Assert.IsTrue(succes, "Tables deleted");
        }
    }
}
