using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MES.Acquintance;
using MES.Data;
using Npgsql;

namespace MES.data
{
    public class DBManager : IDBManager
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
        private string batchValuesTable;
        private string recipesTable;

        public DBManager()
        {
            server = "tek-mmmi-db0a.tek.c.sdu.dk";
            port = "5432";
            userId = "si3_2018_group_23";
            password = "ear70.doling";
            database = "si3_2018_group_23_db";

            connString = "Server=" + server + "; Port=" + port + "; User Id=" + userId + "; Password=" + password + "; Database=" + database;

            batchesTable = "batches";
            batchValuesTable = "batchvalues";
            recipesTable = "recipes";
        }

        /// <summary>
        /// Connects to the database and executes SQL queries (that does not return anything EX. INSERT statement).
        /// </summary>
        /// <param name="statement"></param>
        /// <returns></returns>
        private bool SendSqlCommand(String[] statements)
        {
            try
            {
                NpgsqlConnection conn = new NpgsqlConnection(connString);
                conn.Open();

                foreach (String statement in statements)
                {
                    NpgsqlCommand command = new NpgsqlCommand(statement, conn);
                    command.ExecuteNonQuery();
                }

                conn.Close();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// Connects to the database and executes an SQL query (that does not return anything EX. INSERT statement).
        /// </summary>
        /// <param name="statement"></param>
        /// <returns></returns>
        private bool SendSqlCommand(String statement)
        {
            String[] statements = { statement };
            return SendSqlCommand(statements);
        }

        /// <summary>
        /// Connects to the database and sends an SQL query (that does return something EX. SELECT statement).
        /// Returns the result as a IDictionary containing IBatch objects.
        /// </summary>
        /// <param name="statement"></param>
        /// <returns></returns>
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
                    double batchId = dRead.GetDouble(0);
                    double beerId = dRead.GetDouble(1);
                    int acceptableProducts = dRead.GetInt32(2);
                    int defectProducts = dRead.GetInt32(3);
                    string timestampStart = dRead.GetString(4);
                    string timestampEnd = dRead.GetString(5);

                    batches.Add((float)batchId, new Batch((float)batchId, (float)beerId,
                        acceptableProducts, defectProducts,
                        timestampStart, timestampEnd));
                }

                dRead.Close();

                foreach (IBatch batch in batches.Values)
                {
                    batch.SetBatchValueSet(GetBatchValues(conn, batch.GetBatchId()));
                }

                conn.Close();
                return batches;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// Returns the batch values for a specific batch
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="batchId"></param>
        /// <returns></returns>
        private IList<IBatchValueSet> GetBatchValues(NpgsqlConnection conn, float batchId)
        {
            string statement = "SELECT * FROM " + batchValuesTable
                + " WHERE belongingto = " + batchId;

            NpgsqlCommand command = new NpgsqlCommand(statement, conn);
            NpgsqlDataReader dRead = command.ExecuteReader();

            IList<IBatchValueSet> values = new List<IBatchValueSet>();
            while (dRead.Read())
            {
                double temperature = dRead.GetDouble(0);
                double humidity = dRead.GetDouble(1);
                double vibration = dRead.GetDouble(2);
                string timestamp = dRead.GetString(3);

                values.Add(new BatchValueSet((float)temperature,
                    (float)humidity, (float)vibration, timestamp));
            }

            dRead.Close();
            return values;
        }

        /// <summary>
        /// Connects to the database and sends an SQL query (that does return something EX. SELECT statement).
        /// Returns the result as a IDictionary containing IRecipe objects.
        /// </summary>
        /// <param name="statement"></param>
        /// <returns></returns>
        private IDictionary<float, IRecipe> GetRecipeSqlCommand(String statement)
        {
            try
            {
                NpgsqlConnection conn = new NpgsqlConnection(connString);
                conn.Open();
                NpgsqlCommand command = new NpgsqlCommand(statement, conn);
                NpgsqlDataReader dRead = command.ExecuteReader();

                IDictionary<float, IRecipe> recipes = new Dictionary<float, IRecipe>();
                while (dRead.Read())
                {
                    double beerId = dRead.GetDouble(0);
                    double maxSpeed = dRead.GetDouble(1);
                    string name = dRead.GetString(2);
                    double barley = dRead.GetDouble(3);
                    double hops = dRead.GetDouble(4);
                    double malt = dRead.GetDouble(5);
                    double wheat = dRead.GetDouble(6);
                    double yeast = dRead.GetDouble(7);

                    recipes.Add((float)beerId, new Recipe((float)beerId,
                        (float)maxSpeed, name, (float)barley, (float)hops,
                        (float)malt, (float)wheat, (float)yeast));
                }

                dRead.Close();
                conn.Close();
                return recipes;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }

        public bool InsertIntoBatchesTable(IBatch batch)
        {
            string[] sql = new string[batch.GetBatchValues().Count() + 1];
            uint stringsAdded = 0;

            string sql0 = "INSERT INTO " + batchesTable + " VALUES("
                + batch.GetBatchId() + ", "
                + batch.GetBeerId() + ", "
                + batch.GetAcceptableProducts() + ", "
                + batch.GetDefectProducts() + ", '"
                + batch.GetTimestampStart() + "', '"
                + batch.GetTimestampEnd() + "');";

            sql[0] = sql0;
            stringsAdded++;

            foreach (IBatchValueSet values in batch.GetBatchValues())
            {
            string sqlString = "INSERT INTO " + batchValuesTable + " VALUES("
                + values.GetTemperature() + ", "
                + values.GetHumidity() + ", "
                + values.GetVibration() + ", '"
                + values.GetTimeStamp() + "', "
                + batch.GetBatchId() + ");";

                sql[stringsAdded] = sqlString;
                stringsAdded++;
            }

            return SendSqlCommand(sql);
        }

        public bool InsertBatchValueSet(float temperature, float humidity,
            float vibration, string timestamp, float batchId)
        {
            string sql = "INSERT INTO " + batchValuesTable + " VALUES("
            + temperature + ", " + humidity + ", " + vibration
            + ", '" + timestamp + "', " + batchId  + ");";

            return SendSqlCommand(sql);
        }

        public bool UpdateBatch(IBatch batch)
        {
            string sql = "UPDATE " + batchesTable
                + " SET acceptableproducts = " + batch.GetAcceptableProducts()
                + ", defectproducts = " + batch.GetDefectProducts()
                + ", timestampend = '" + batch.GetTimestampEnd()
                + "' WHERE batchid = " + batch.GetBatchId();

            return SendSqlCommand(sql);
        }

        public IDictionary<float, IBatch> GetAllBatches()
        {
            string sql = "SELECT * FROM " + batchesTable + ";";

            return GetSqlCommand(sql);
        }

        public IDictionary<float, IBatch> GetBatches(string month, string year)
        {
            if (month.Length == 1)
            {
                month = " " + month;
            }
            if (month.Length == 2 && year.Length == 4)
            {
                string sql = "SELECT * FROM " + batchesTable
                    + " WHERE timestampEnd LIKE '___" + month + "/" + year + "%'";

                return GetSqlCommand(sql);
            }
            else return null;
        }

        public IDictionary<float, IBatch> GetBatches(int amount)
        {
            string sql = "SELECT * FROM " + batchesTable + " LIMIT " + amount;

            return GetSqlCommand(sql);
        }

        public IBatch GetBatch(float batchId)
        {
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
        }

        public bool DeleteAllBatches()
        {
            string sql = "DELETE * FROM " + batchesTable;

            return SendSqlCommand(sql);
        }

        public bool DeleteBatch(float batchId)
        {
            string sql = "DELETE FROM " + batchesTable + " WHERE batchid = " + batchId;

            return SendSqlCommand(sql);
        }

        public bool RunQueries(string[] statements)
        {
            return SendSqlCommand(statements);
        }

        public IDictionary<float, IRecipe> GetAllRecipes()
        {
            string sql = "SELECT * FROM " + recipesTable;

            return GetRecipeSqlCommand(sql);
        }

        public bool AddRecipes(IRecipe[] recipes)
        {
            string[] statements = new string[recipes.Length];

            for (int i = 0; i < recipes.Length; i++)
            {
                statements[i] = "INSERT INTO " + recipesTable + " VALUES("
                    + recipes[i].BeerId + ", "
                    + recipes[i].MaxSpeed + ", "
                    + recipes[i].Name + ", "
                    + recipes[i].Barley + ", "
                    + recipes[i].Hops + ", "
                    + recipes[i].Malt + ", "
                    + recipes[i].Wheat + ", "
                    + recipes[i].Yeast + ");";
            }

            return SendSqlCommand(statements);
        }
    }
}

