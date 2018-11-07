using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES.Data
{

    [TestFixture]
    class DataTest
    {
        private acq.IDBManager dbManager = new data.DBManager();
        [Test]
        public void TestInsertIntoBatchesTable()
        {
            acq.IBatch batch0 = new Batch(-1, 3, 90, 10,
                20, 10, 15, "02/11/2018 09:20:35");
            bool inserted0 = dbManager.InsertIntoBatchesTable(batch0);
            Assert.IsTrue(inserted0, "inserted");

            acq.IBatch batch1 = new Batch(-2, 3, 90, 10,
               20, 10, 15, "02/11/2018 10:20:35");
            bool inserted1 = dbManager.InsertIntoBatchesTable(batch1);
            Assert.IsTrue(inserted1, "inserted");

            acq.IBatch batch2 = new Batch(-3, 3, 90, 10,
                20, 10, 15, "02/12/2018 11:20:35");
            bool inserted2 = dbManager.InsertIntoBatchesTable(batch2);
            Assert.IsTrue(inserted2, "inserted");

            acq.IBatch batch3 = new Batch(-4, 3, 90, 10,
                20, 10, 15, "02/12/2018 12:20:35");
            bool inserted3 = dbManager.InsertIntoBatchesTable(batch3);
            Assert.IsTrue(inserted3, "inserted");
        }
       
        [Test]
        public void TestGetAllBatches()
        {

            IDictionary<float, acq.IBatch> batches = dbManager.GetAllBatches();
            Assert.IsNotNull(batches, "Succes");

        }
        [Test]
        public void TestGetBatches1()
        {
            IDictionary<float, acq.IBatch> batch = dbManager.GetBatches("11", "2018");
            Assert.IsNotNull(batch, "Succes");
        }
        [Test]
        public void TestGetBatches2()
        {
            IDictionary<float, acq.IBatch> batch = dbManager.GetBatches(3);
            Assert.IsNotNull(batch, "Succes");
        }
        [Test]
        public void TestGetBatch()
        {
            acq.IBatch batch = dbManager.GetBatch(-2);
            Assert.IsNotNull(batch, "Succes");
        }
        [Test]
        public void TestDeleteBatch()
        {
            bool deleted0 = dbManager.DeleteBatch(-1);
            Assert.IsTrue(deleted0, "Deleted");

            bool deleted1 = dbManager.DeleteBatch(-2);
            Assert.IsTrue(deleted1, "Deleted");

            bool deleted2 = dbManager.DeleteBatch(-3);
            Assert.IsTrue(deleted2, "Deleted");

            bool deleted3 = dbManager.DeleteBatch(-4);
            Assert.IsTrue(deleted3, "Deleted");
        }
    }
}
