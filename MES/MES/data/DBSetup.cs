using MES.acq;
using MES.data;
using MES.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES
{
    class DBSetup
    {
        private IDBManager dbManager;

        public DBSetup()
        {
            dbManager = new DBManager();
        }

        public void Setup()
        {
            //bool cre = dbManager.CreateBatchesTable();
            //// bool del = dbManager.DeleteBatchesTable();
            //Console.WriteLine(cre);

            //IBatch batch = new Batch(7, 3, 90, 10,
            //    20, 10, 15, "02/11/2018 09:53:35");
            //bool inserted = dbManager.InsertIntoBatchesTable(batch);
            //Console.WriteLine(inserted);

            //IDictionary<float, IBatch> batchlist = dbManager.GetAllBatches();
            //foreach (KeyValuePair<float, IBatch> element in batchlist)
            //{
            //    Console.WriteLine(element.ToString());
            //}
            //Console.WriteLine(dbManager.GetBatch(7).ToString());
            //DataTable table = dbManager.GetBatch((float)1.2);
            //Console.WriteLine(table.Rows.Contains((float)1.2));
        }

    }
}
