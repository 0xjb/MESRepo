using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MES.acq;
using MES.Data;
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

            connString = "Server=" + server + "; Port=" + port + "; User Id=" + userId + "; Password=" + password + "; Database=" + database;

            batchesTable = "batches";
        }

        private bool SendSqlCommand(String statement)
        {
            try
            {
                NpgsqlConnection conn = new NpgsqlConnection(connString);
                conn.Open();
                NpgsqlCommand command = new NpgsqlCommand(statement, conn);
                command.ExecuteNonQuery();

                conn.Close();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }

        private IDictionary<float, IBatch> GetSqlCommand(String statement)
        {
            try
            {
                NpgsqlConnection conn = new NpgsqlConnection(connString);
                conn.Open();
                NpgsqlCommand command = new NpgsqlCommand(statement, conn);
                NpgsqlDataReader dRead = command.ExecuteReader();

                IDictionary<float, IBatch> batches = new Dictionary<float, IBatch>();
                while (dRead.Read())
                {
                    //for (int i = 0; i < dRead.FieldCount; i++)
                    //Console.Write("{0} \t \n", dRead[i].ToString());
                    //Console.WriteLine(dRead.GetDouble(0));
                    double batchId = dRead.GetDouble(0);
                    double beerId = dRead.GetDouble(1);
                    int acceptableProducts = dRead.GetInt32(2);
                    int defectProducts = dRead.GetInt32(3);
                    double temperature = dRead.GetDouble(4);
                    double humidity = dRead.GetDouble(5);
                    double vibration = dRead.GetDouble(6);
                    string timestamp = dRead.GetString(7);

                    IBatch batch = new Batch((float)batchId, (float)beerId,
                        acceptableProducts, defectProducts, (float)temperature,
                        (float)humidity, (float)vibration, timestamp);
                    batches.Add((float)batchId, batch);
                }
                dRead.Close();

                conn.Close();
                return batches;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }

        public bool CreateBatchesTable()
        {
            string sql = "CREATE TABLE " + batchesTable
            + " (batchid FLOAT PRIMARY KEY,"
            + "beerid FLOAT,"
            + "acceptableproducts INT,"
            + "defectproducts INT,"
            + "temperature FLOAT,"
            + "humidity FLOAT,"
            + "vibration FLOAT,"
            + "timestampx CHAR(19));";

            return SendSqlCommand(sql);
        }

        public bool DeleteBatchesTable()
        {
            string sql = "DELETE TABLE " + batchesTable;

            return SendSqlCommand(sql);
        }

        public bool InsertIntoBatchesTable(IBatch batch)
        {
            string sql = "INSERT INTO " + batchesTable + " VALUES("
                + batch.GetBatchId() + ", "
                + batch.GetBeerId() + ", "
                + batch.GetAcceptableProducts() + ", "
                + batch.GetDefectProducts() + ", "
                + batch.GetTemperature() + ", "
                + batch.GetHumidity() + ", "
                + batch.GetVibration() + ", '"
                + batch.GetTimestamp() + "');";

            //String sql = "INSERT INTO batches VALUES ( 5, 9, 90, 10, 7, 9, 2, '02/11/2018 09:53:35');";

            return SendSqlCommand(sql);
        }

        public IDictionary<float, IBatch> GetAllBatches()
        {
            //try
            //{
            //NpgsqlConnection conn = new NpgsqlConnection(connString);
            //conn.Open();

            string sql = "SELECT * FROM " + batchesTable + ";";

            return GetSqlCommand(sql);

            //NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);

            //DataSet ds = new DataSet();
            //DataTable dt = new DataTable();
            //da.Fill(ds);
            //dt = ds.Tables[0];

            // connect grid to DataTable
            // dataGridView.DataSource = dt;

            //conn.Close();
            //return dt;
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //    return null;
            //}
        }

        public IDictionary<float, IBatch> GetBatches(string month, string year)
        {
            if (month.Length == 2 && year.Length == 4)
            {
                string sql = "SELECT * FROM " + batchesTable
                    + " WHERE timestampx LIKE '___" + month + "/" + year + "%'";

                return GetSqlCommand(sql);
            }
            else return null;
        }

        public IDictionary<float, IBatch> GetBatches(int amount)
        {
            string sql = "SELECT * FROM " + batchesTable + "LIMIT " + amount;

            return GetSqlCommand(sql);
        }

        public bool DeleteAllBatches()
        {
            string sql = "DELETE * FROM " + batchesTable;

            return SendSqlCommand(sql);
        }

        public IBatch GetBatch(float batchId)
        {
            //try
            //{
            //    NpgsqlConnection conn = new NpgsqlConnection(connString);
            //    conn.Open();

            string sql = "SELECT * FROM " + batchesTable
                + " WHERE batchid = " + batchId;

            IDictionary<float, IBatch> batch = GetSqlCommand(sql);

            IBatch result;
            if (batch.TryGetValue(batchId, out result))
            {
                return result;
            }
            else
            {
                return null;
            }

            //NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);

            //DataSet ds = new DataSet();
            //DataTable dt = new DataTable();
            //da.Fill(ds);
            //dt = ds.Tables[0];

            //return dt;
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //    return null;
            //}
        }
    }
}
