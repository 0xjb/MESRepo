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

            connString = "Server=" + server + "; Port=" + port + "; User Id=" + userId + "; Password=" + password + "; Database=" + database;

            batchesTable = "batches";
        }

        public void SendSqlCommand(String statement)
        {
            try
            {
                NpgsqlConnection conn = new NpgsqlConnection(connString);
                conn.Open();
                NpgsqlCommand command = new NpgsqlCommand(statement, conn);
                command.ExecuteNonQuery();

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                
            }
        }

        public void GetSqlCommand(String statement)
        {
            try
            {
                NpgsqlConnection conn = new NpgsqlConnection(connString);
                conn.Open();
                NpgsqlCommand command = new NpgsqlCommand(statement, conn);
                NpgsqlDataReader dRead = command.ExecuteReader();

                while (dRead.Read())
                {
                    for (int i = 0; i < dRead.FieldCount; i++)
                        Console.Write("{0} \t \n", dRead[i].ToString());
                }
                dRead.Close();

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

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

            SendSqlCommand(sql);
                return true;
        }
            
             
    

        public bool DeleteBatchesTable()
        {
            
                string sql = "DELETE TABLE " + batchesTable;

            SendSqlCommand(sql);
            
            return true;
            
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


            SendSqlCommand(sql);
                return true;
        }

        public DataTable GetAllBatches()
        {
            try
            {
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
