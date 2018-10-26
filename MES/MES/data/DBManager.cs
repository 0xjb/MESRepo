using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MES.acq;
using Npgsql;

namespace MES.data
{
    class DBManager : IDBManager
    {
        private DataSet ds;
        private DataTable dt;

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
            ds = new DataSet();
            dt = new DataTable();

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

        public bool CreateBatchesTable()
        {
            try
            {
                NpgsqlConnection conn = new NpgsqlConnection(connString);
                conn.Open();

                string sql = "CREATE TABLE batches ("
                + "batchid FLOAT PRIMARY KEY,"
                + "beerid FLOAT,"
                + "acceptableproducts INT,"
                + "defectproducts INT,"
                + "temperature FLOAT,"
                + "humidity FLOAT,"
                + "vibration FLOAT,"
                + "timestampx CHAR(19));";

                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);

                conn.Close();
                return true;
            } catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteBatchesTable()
        {
            try
            {
                NpgsqlConnection conn = new NpgsqlConnection(connString);
                conn.Open();

                string sql = "DELETE TABLE " + batchesTable;

                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);

                conn.Close();
                return true;
            } catch (Exception ex)
            {
                return false;
            }
        }

        public bool InsertIntoBatchesTable(IBatch batch)
        {
            try {
                NpgsqlConnection conn = new NpgsqlConnection(connString);
                conn.Open();

                string sql = "INSERT INTO " + batchesTable + " VALUES("
                    + batch.GetBatchId() + ", "
                    + batch.GetBeerId() + ", "
                    + batch.GetAcceptableProducts() + ", "
                    + batch.GetDefectProducts() + ", "
                    + batch.GetTemperature() + ", "
                    + batch.GetHumidity() + ", "
                    + batch.GetVibration() + ", '"
                    + batch.GetTimestamp() + "');";

                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);

                return true;
            } catch (Exception ex)
            {
                return false;
            }
        }

        public ISet<IBatch> GetAllBatches()
        {
            try {
            NpgsqlConnection conn = new NpgsqlConnection(connString);
            conn.Open();

            string sql = "SELECT * FROM " + batchesTable;

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
            ds.Reset();
            da.Fill(ds);
            dt = ds.Tables[0];

            // connect grid to DataTable
            // dataGridView.Datasource = dt;

            conn.Close();
            return null;
        } catch (Exception ex)
            {
            MessageBox.Show(ex.ToString());
            return null;
            }
        }

        public IBatch GetBatch(float batchId)
        {
            string sql = "SELECT * FROM " + batchesTable
                + " WHERE batchid = " + batchId;

            return null;
        }
    }
}
