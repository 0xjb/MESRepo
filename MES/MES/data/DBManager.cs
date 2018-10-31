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
            userId = "si3_2018_group_23";
            password = "ear70.doling";
            database = "si3_2018_group_23_db";

            connString = String.Format("Server={0};Port={1};"
                + "User Id={2};Password={3};Database={4};",
                server, port, userId, password, database);

            batchesTable = "batches";
        }

        public bool CreateBatchesTable()
        {
            try
            {
                Console.WriteLine("1");
                NpgsqlConnection conn = new NpgsqlConnection(connString);
                Console.WriteLine("2");
                conn.Open();
                Console.WriteLine("3");

                string sql = "CREATE TABLE batches ("
                + "batchid FLOAT PRIMARY KEY,"
                + "beerid FLOAT,"
                + "acceptableproducts INT,"
                + "defectproducts INT,"
                + "temperature FLOAT,"
                + "humidity FLOAT,"
                + "vibration FLOAT,"
                + "timestampx CHAR(19));";
                Console.WriteLine("4");

                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
                Console.WriteLine("5");

                conn.Close();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }

        public bool InsertIntoBatchesTable(IBatch batch)
        {
            try
            {
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }

        public string GetAllBatches()
        {
            try
            {
                // Connect to a PostgreSQL database
                NpgsqlConnection conn = new NpgsqlConnection(connString);
                conn.Open();

                // Define a query returning a single row result set
                NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM TEST", conn);

                // Execute the query and obtain the value of the first column of the first row
                string count = (string)command.ExecuteScalar();

                Console.Write("{0}\n", count);
                conn.Close();
                return count;
            }
            catch (Exception ex)
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }
    }
}
