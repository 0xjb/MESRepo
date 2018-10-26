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
            } catch   (Exception ex)
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

        public DataTable GetAllBatches()
        {
            try {
            NpgsqlConnection conn = new NpgsqlConnection(connString);
            conn.Open();

            string sql = "SELECT * FROM " + batchesTable;

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            da.Fill(ds);
            dt = ds.Tables[0];

            // connect grid to DataTable
            // dataGridView.DataSource = dt;

            conn.Close();
            return dt;
        } catch (Exception ex)
            {
            MessageBox.Show(ex.ToString());
            return null;
            }
        }

        public DataTable GetBatch(float batchId)
        {
            try
            {
                NpgsqlConnection conn = new NpgsqlConnection(connString);
                conn.Open();

                string sql = "SELECT * FROM " + batchesTable
                    + " WHERE batchid = " + batchId;

                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);

                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                da.Fill(ds);
                dt = ds.Tables[0];

                return dt;
            } catch (Exception ex)
            {
                return null;
            }
        }
    }
}
