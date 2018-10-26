using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MES.acq;

namespace MES.data
{
    class DBManager
    {
        // database info
        private string server;
        private string port;
        private string userId;
        private string password;
        private string database;

        private string connString;

        // database table names
        private string batchesTable;

        public DBManager()
        {
            server = "tek-mmmi-db0a.tek.c.sdu.dk";
            port = "5432";
            userId = "si3_2018_group_23_db";
            password = "ear70.doling";
            database = "si3_2018_group_23_db";

            connString = String.Format("Server={0};Port={1}"
                + "User Id={2};Password={3};Database={4}",
                server, port, userId, password, database);

            batchesTable = "batches";
        }

        public bool createBatchesTable()
        {
            string queue = "CREATE TABLE batches ("
            + "batchid FLOAT PRIMARY KEY,"
            + "beerid FLOAT,"
            + "acceptableproducts INT,"
            + "defectproducts INT,"
            + "temperature FLOAT,"
            + "humidity FLOAT,"
            + "vibration FLOAT,"
            + "timestampx CHAR(19)"
            + ");";

            return false;
        }

        public bool deleteBatchesTable()
        {
            string queue = "DELETE TABLE " + batchesTable + " ;";
            return false;
        }

        public bool insertIntoBatchesTable(IBatch batch)
        {
            string queue = "INSERT INTO " + batchesTable + " VALUES("
                + batch.GetBatchId() + ", "
                + batch.GetBeerId() + ", "
                + batch.GetAcceptableProducts() + ", "
                + batch.GetDefectProducts() + ", "
                + batch.GetTemperature() + ", "
                + batch.GetHumidity() + ", "
                + batch.GetVibration() + ", '"
                + batch.GetTimestamp() + "');";

            return false;
        }

        public bool getBatch(float batchId)
        {
            string queue = "SELECT * FROM " + batchesTable
                + " WHERE batchid = " + batchId;

            return false;
        }
    }
}
