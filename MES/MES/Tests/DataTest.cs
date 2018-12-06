using MES.Acquintance;
using MES.Data;
using NUnit.Framework;
using System.Collections.Generic;
using System.Windows;

namespace MES.Tests
{
    [TestFixture]
    class DataTest
    {
        private readonly IDBManager dbManager = new data.DBManager();

        [Test]
        public void TestDBManager()
        {
            // Testing GetAllRecipes

            IDictionary<float, IRecipe> recipes = dbManager.GetAllRecipes();
            Assert.IsNotNull(recipes, "Succes");

            IRecipe recipe;
            recipes.TryGetValue(0, out recipe);
            Assert.IsNotNull(recipe, "Succes");

            // Testing InsertBatch & InsertBatchValueSet

            IBatch batch0 = new Batch(-1, 3, 90, 10,
                "02/11/2018 09:18:35.500", "02/11/2018 09:26:35.500", 10);
            batch0.AddBatchValue(20, "02/11/2018 09:18:35.500", -1);
            batch0.AddBatchValue(30, "02/11/2018 09:19:35.500", 0);
            batch0.AddBatchValue(40, "02/11/2018 09:20:35.500", 1);
            batch0.AddBatchValue(60, "02/11/2018 09:21:35.500", -1);
            batch0.AddBatchValue(70, "02/11/2018 09:22:35.500", 0);
            batch0.AddBatchValue(80, "02/11/2018 09:23:35.500", 1);
            batch0.AddBatchValue(70, "02/11/2018 09:24:35.500", -1);
            batch0.AddBatchValue(80, "02/11/2018 09:25:35.500", 0);
            batch0.AddBatchValue(90, "02/11/2018 09:26:35.500", 1);
            bool inserted0 = dbManager.InsertIntoBatchesTable(batch0);
            Assert.IsTrue(inserted0, "Inserted first");

            IBatch batch1 = new Batch(-2, 3, 90, 10,
                "02/11/2018 10:18:35.500", "02/11/2018 10:20:35.500", 10);
            batch1.AddBatchValue(30, "02/11/2018 10:18:35.500", -1);
            batch1.AddBatchValue(40, "02/11/2018 10:19:35.500", 0);
            batch1.AddBatchValue(50, "02/11/2018 10:20:35.500", 1);
            bool inserted1 = dbManager.InsertIntoBatchesTable(batch1);
            Assert.IsTrue(inserted1, "Inserted second");

            IBatch batch2 = new Batch(-3, 3, 90, 10,
                "02/12/2018 11:18:35.500", "02/12/2018 11:20:35.500", 10);
            batch2.AddBatchValue(40, "02/12/2018 11:18:35.500", -1);
            batch2.AddBatchValue(50, "02/12/2018 11:19:35.500", 0);
            batch2.AddBatchValue(60, "02/12/2018 11:20:35.500", 1);
            bool inserted2 = dbManager.InsertIntoBatchesTable(batch2);
            Assert.IsTrue(inserted2, "Inserted third");

            IBatch batch3 = new Batch(-4, 3, 90, 10,
                "02/12/2018 12:18:35.500", "02/12/2018 12:20:35.500", 10);
            batch3.AddBatchValue(50, "02/12/2018 12:18:35.500", -1);
            batch3.AddBatchValue(60, "02/12/2018 12:19:35.500", 0);
            batch3.AddBatchValue(70, "02/12/2018 12:20:35.500", 1);
            bool inserted3 = dbManager.InsertIntoBatchesTable(batch3);
            Assert.IsTrue(inserted3, "Inserted fourth");

            // Testing UpdateBatch

            batch3.AddProducts(15, true);
            batch3.AddProducts(5, false);
            batch3.SetTimestampEnd("02/12/2018 12:21:35.500");
            bool updateSucces = dbManager.UpdateBatch(batch3);
            Assert.IsTrue(updateSucces, "Succes");

            // Testing GetAllBatches

            IDictionary<float, IBatch> allBatches = dbManager.GetAllBatches();

            IBatch loadedBatch0 = null;
            allBatches.TryGetValue(-1, out loadedBatch0);
            Assert.IsNotNull(loadedBatch0, "Succes");

            IBatch loadedBatch1 = null;
            allBatches.TryGetValue(-2, out loadedBatch1);
            Assert.IsNotNull(loadedBatch1, "Succes");

            IBatch loadedBatch2 = null;
            allBatches.TryGetValue(-3, out loadedBatch2);
            Assert.IsNotNull(loadedBatch2, "Succes");

            IBatch loadedBatch3 = null;
            allBatches.TryGetValue(-4, out loadedBatch3);
            Assert.IsNotNull(loadedBatch3, "Succes");

            // Testing if batch values was loaded

            IList<IBatchValue> temps0 = loadedBatch0.GetBatchTemperatures();
            IList<IBatchValue> humids0 = loadedBatch0.GetBatchHumidities();
            IList<IBatchValue> vibs0 = loadedBatch0.GetBatchVibrations();

            bool loadedBatchValues0 = false;
            if (temps0.Count == 3 && humids0.Count == 3 && vibs0.Count == 3)
            {
                foreach (IBatchValue value in temps0)
                {
                    if (value.Timestamp.Equals("02/11/2018 09:24:35.500"))
                    {
                        loadedBatchValues0 = true;
                    }
                }
            }
            Assert.IsTrue(loadedBatchValues0, "Succes");

            // Testing if the batch was updated correctly

            Assert.IsTrue(loadedBatch3.GetAcceptableProducts() == 105, "Succes");

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

            // Testing GetHighestBatchId

            float id = dbManager.GetHighestBatchId();
            bool succes = false;
            if (id >= -1)
            {
                succes = true;
            }
            MessageBox.Show("Test: " + id);
            Assert.IsTrue(succes, "Succes");

            // Testing DeleteBatch

            bool deleted0 = dbManager.DeleteBatch(-1);
            Assert.IsTrue(deleted0, "Deleted");
            IBatch loadedBatch10 = dbManager.GetBatch(-1);
            Assert.IsNull(loadedBatch10, "Succes");

            bool deleted1 = dbManager.DeleteBatch(-2);
            Assert.IsTrue(deleted1, "Deleted");
            IBatch loadedBatch11 = dbManager.GetBatch(-2);
            Assert.IsNull(loadedBatch11, "Succes");

            bool deleted2 = dbManager.DeleteBatch(-3);
            Assert.IsTrue(deleted2, "Deleted");
            IBatch loadedBatch12 = dbManager.GetBatch(-3);
            Assert.IsNull(loadedBatch12, "Succes");

            bool deleted3 = dbManager.DeleteBatch(-4);
            Assert.IsTrue(deleted3, "Deleted");
            IBatch loadedBatch13 = dbManager.GetBatch(-4);
            Assert.IsNull(loadedBatch13, "Succes");
        }


        [Test]
        public void DBSetup()
        {
            string[] statements = new string[5];

            statements[0] = "CREATE TABLE recipes ("
                + "beerid FLOAT PRIMARY KEY, "
                + "maxspeed FLOAT, "
                + "name VARCHAR(20), "
                + "barley FLOAT, "
                + "hops FLOAT, "
                + "malt FLOAT, "
                + "wheat FLOAT, "
                + "yeast FLOAT);";

            statements[1] = "CREATE TABLE batches ("
                + "batchid FLOAT PRIMARY KEY, "
                + "beerid FLOAT, "
                + "acceptableproducts INT, "
                + "defectproducts INT, "
                + "timestampStart CHAR(23), "
                + "timestampEnd CHAR(23), "
                + "oee FLOAT, "
                + "FOREIGN KEY(beerid) REFERENCES recipes(beerid) ON DELETE CASCADE ON UPDATE CASCADE);";

            statements[2] = "CREATE TABLE temperaturevalues ("
                + "temperature FLOAT, "
                + "timestampx CHAR(23), "
                + "belongingto FLOAT, "
                + "FOREIGN KEY(belongingto) REFERENCES batches(batchid) ON DELETE CASCADE ON UPDATE CASCADE, "
                + "PRIMARY KEY(timestampx, belongingto));";

            statements[3] = "CREATE TABLE humidityvalues ("
                + "humidity FLOAT, "
                + "timestampx CHAR(23), "
                + "belongingto FLOAT, "
                + "FOREIGN KEY(belongingto) REFERENCES batches(batchid) ON DELETE CASCADE ON UPDATE CASCADE, "
                + "PRIMARY KEY(timestampx, belongingto));";

            statements[4] = "CREATE TABLE vibrationvalues ("
                + "vibration FLOAT, "
                + "timestampx CHAR(23), "
                + "belongingto FLOAT, "
                + "FOREIGN KEY(belongingto) REFERENCES batches(batchid) ON DELETE CASCADE ON UPDATE CASCADE, "
                + "PRIMARY KEY(timestampx, belongingto));";

            bool succes = dbManager.RunQueries(statements);
            Assert.IsTrue(succes, "Tables created");
        }

        [Test]
        public void DBDelete()
        {
            string[] statements = { "DROP TABLE temperaturevalues",
                "DROP TABLE humidityvalues", "DROP TABLE vibrationvalues",
                "DROP TABLE batches", "DROP TABLE recipes" };
            bool succes = dbManager.RunQueries(statements);
            Assert.IsTrue(succes, "Tables deleted");
        }

        [Test]
        public void CreateRecipes()
        {
            IRecipe[] recipes = new Recipe[6];

            recipes[0] = new Recipe(0, 600, "Pilsner", 4, 2, 1, 1, 4);
            recipes[1] = new Recipe(1, 300, "Wheat", 1, 4, 1, 6, 3);
            recipes[2] = new Recipe(2, 150, "IPA", 4, 1, 5, 4, 1);
            recipes[3] = new Recipe(3, 200, "Stout", 3, 4, 6, 1, 2);
            recipes[4] = new Recipe(4, 100, "Ale", 4, 6, 2, 2, 8);
            recipes[5] = new Recipe(5, 125, "Alcohol Free", 1, 1, 4, 5, 0);

            bool succes = dbManager.AddRecipes(recipes);
            Assert.IsTrue(succes, "Succes");
        }

        [Test]
        public void UpdateRecipes()
        {
            IRecipe[] recipes = new Recipe[6];
            string[] statements = new string[recipes.Length];

            recipes[0] = new Recipe(0, 600, "Pilsner", 4, 2, 1, 1, 4);
            recipes[1] = new Recipe(1, 300, "Wheat", 1, 4, 1, 6, 3);
            recipes[2] = new Recipe(2, 150, "IPA", 4, 1, 5, 4, 1);
            recipes[3] = new Recipe(3, 200, "Stout", 3, 4, 6, 1, 2);
            recipes[4] = new Recipe(4, 100, "Ale", 4, 6, 2, 2, 8);
            recipes[5] = new Recipe(5, 125, "Alcohol Free", 1, 1, 4, 5, 0);

            for (int i = 0; i < recipes.Length; i++)
            {
                statements[i] = "UPDATE recipes SET barley = " + recipes[i].Barley
                    + ", hops = " + recipes[i].Hops
                    + ", malt = " + recipes[i].Malt
                    + ", wheat = " + recipes[i].Wheat
                    + ", yeast = " + recipes[i].Yeast
                    + " WHERE beerid = " + recipes[i].BeerId;
            }

            bool succes = dbManager.RunQueries(statements);
            Assert.IsTrue(succes, "Succes");
        }

        [Test]
        public void DeleteBatches()
        {
            bool deleted0 = dbManager.DeleteBatch(-1);
            Assert.IsTrue(deleted0, "Deleted");
            IBatch loadedBatch0 = dbManager.GetBatch(-1);
            Assert.IsNull(loadedBatch0, "Succes");

            bool deleted1 = dbManager.DeleteBatch(-2);
            Assert.IsTrue(deleted1, "Deleted");
            IBatch loadedBatch1 = dbManager.GetBatch(-2);
            Assert.IsNull(loadedBatch1, "Succes");

            bool deleted2 = dbManager.DeleteBatch(-3);
            Assert.IsTrue(deleted2, "Deleted");
            IBatch loadedBatch2 = dbManager.GetBatch(-3);
            Assert.IsNull(loadedBatch2, "Succes");

            bool deleted3 = dbManager.DeleteBatch(-4);
            Assert.IsTrue(deleted3, "Deleted");
            IBatch loadedBatch3 = dbManager.GetBatch(-4);
            Assert.IsNull(loadedBatch3, "Succes");
        }

    }
}
