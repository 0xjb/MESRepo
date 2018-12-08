









using MES.Acquintance;
using MES.Data;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Windows;

namespace MES.data {
    public class DBManager : IDBManager {
        // database info
        private string server;
        private string port;
        private string userId;
        private string password;
        private string database;

        private string connString;

        // database table names
        private string recipesTable;
        private string batchesTable;
        private string temperatureTable;
        private string humidityTable;
        private string vibrationTable;

        public DBManager() {
            server = "tek-mmmi-db0a.tek.c.sdu.dk";
            port = "5432";
            userId = "si3_2018_group_23";
            password = "ear70.doling";
            database = "si3_2018_group_23_db";

            connString = "Server=" + server + "; Port=" + port + "; User Id=" + userId + "; Password=" + password + "; Database=" + database;

            recipesTable = "recipes";
            batchesTable = "batches";
            temperatureTable = "temperaturevalues";
            humidityTable = "humidityvalues";
            vibrationTable = "vibrationvalues";
        }

        /// <summary>
        /// Connects to the database and executes SQL queries (that does not return anything EX. INSERT statement).
        /// </summary>
        /// <param name="statement"></param>
        /// <returns></returns>
        private bool SendSqlCommand(String[] statements) {
            try {
                NpgsqlConnection conn = new NpgsqlConnection(connString);
                conn.Open();

                foreach (String statement in statements) {
                    try {
                        if (statement != null) {
                            NpgsqlCommand command = new NpgsqlCommand(statement, conn);
                            command.ExecuteNonQuery();
                        }
                    } catch (Exception ex) {
                        //MessageBox.Show("ERROR\n" + ex.ToString());
                    }
                }

                conn.Close();
                return true;
            } catch (Exception ex) {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// Connects to the database and executes an SQL query (that does not return anything EX. INSERT statement).
        /// </summary>
        /// <param name="statement"></param>
        /// <returns></returns>
        private bool SendSqlCommand(String statement) {
            String[] statements = { statement };
            return SendSqlCommand(statements);
        }

        /// <summary>
        /// Connects to the database and sends an SQL query (that does return something EX. SELECT statement).
        /// Returns the result as a IDictionary containing IBatch objects.
        /// </summary>
        /// <param name="statement"></param>
        /// <returns></returns>
        private IDictionary<float, IBatch> GetSqlCommand(String statement) {
            try {
                NpgsqlConnection conn = new NpgsqlConnection(connString);
                conn.Open();
                NpgsqlCommand command = new NpgsqlCommand(statement, conn);
                NpgsqlDataReader dRead = command.ExecuteReader();

                IDictionary<float, IBatch> batches = new Dictionary<float, IBatch>();
                while (dRead.Read()) {
                    double batchId = dRead.GetDouble(0);
                    double beerId = dRead.GetDouble(1);
                    int acceptableProducts = dRead.GetInt32(2);
                    int defectProducts = dRead.GetInt32(3);
                    string timestampStart = dRead.GetString(4);
                    string timestampEnd = dRead.GetString(5);
                    double oee = dRead.GetDouble(6);
                    double ppm = dRead.GetDouble(7);
                    double speed = dRead.GetDouble(8);

                    batches.Add((float)batchId, new Batch((float)batchId, (float)beerId,
                        acceptableProducts, defectProducts, speed,
                        timestampStart, timestampEnd, oee, ppm));
                }
                dRead.Close();

                foreach (IBatch batch in batches.Values) {
                    batch.AddBatchValues(GetBatchValues(conn, batch.GetBatchId()));
                }

                conn.Close();
                return batches;
            } catch (Exception ex) {
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
        private IList<IBatchValue> GetBatchValues(NpgsqlConnection conn, float batchId) {
            string[] sql = new string[3];
            sql[0] = "SELECT * FROM " + temperatureTable
                + " WHERE belongingto = " + batchId;
            sql[1] = "SELECT * FROM " + humidityTable
                + " WHERE belongingto = " + batchId;
            sql[2] = "SELECT * FROM " + vibrationTable
                + " WHERE belongingto = " + batchId;

            IList<IBatchValue> values = new List<IBatchValue>();

            for (int i = 0; i < sql.Length; i++) {
                try {
                    NpgsqlCommand command = new NpgsqlCommand(sql[i], conn);
                    NpgsqlDataReader dRead = command.ExecuteReader();

                    while (dRead.Read()) {
                        double value = dRead.GetDouble(0);
                        string timestamp = dRead.GetString(1);
                        int type = 0;

                        if (i == 0) {
                            type = -1;
                        } else if (i == 1) {
                            type = 0;
                        } else if (i == 2) {
                            type = 1;
                        }
                        values.Add(new BatchValue((float)value, timestamp, type));
                    }
                    dRead.Close();
                } catch (Exception ex) {
                    //MessageBox.Show("ERROR\n" + ex.ToString());
                }
            }
            return values;
        }

        /// <summary>
        /// Connects to the database and sends an SQL query (that does return something EX. SELECT statement).
        /// Returns the result as a IDictionary containing IRecipe objects.
        /// </summary>
        /// <param name="statement"></param>
        /// <returns></returns>
        private IDictionary<float, IRecipe> GetRecipeSqlCommand(String statement) {
            try {
                NpgsqlConnection conn = new NpgsqlConnection(connString);
                conn.Open();
                NpgsqlCommand command = new NpgsqlCommand(statement, conn);
                NpgsqlDataReader dRead = command.ExecuteReader();

                IDictionary<float, IRecipe> recipes = new Dictionary<float, IRecipe>();
                while (dRead.Read()) {
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
            } catch (Exception ex) {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// Creates statements for inserting batch values into the db
        /// </summary>
        /// <param name="value"></param>
        /// <param name="batchId"></param>
        /// <returns></returns>
        private String[] CreateBatchValuesStatements(ISet<IList<IBatchValue>> set,
            float batchId, int freeSpace) {
            int valueCount = 0;
            foreach (IList<IBatchValue> list in set) {
                valueCount += list.Count;
            }
            Console.WriteLine(valueCount);
            String[] sql = new String[valueCount + freeSpace];

            int index = freeSpace;
            foreach (IList<IBatchValue> list in set) {
                foreach (IBatchValue value in list) {
                    string table;
                    if (value.Type < 0) { table = temperatureTable; } else if (value.Type == 0) { table = humidityTable; } else if (value.Type > 0) { table = vibrationTable; } else {
                        table = null;
                        sql[index] = null;
                    }

                    if (table != null) {
                        // INSERT INTO vibrationvalues VALUES (40, 'lmao', 1);
                        sql[index] = "INSERT INTO " + table + " VALUES ("
                            + value.Value + ", '"
                            + value.Timestamp + "', "
                            + batchId + ");";
                    }
                    index++;
                }
            }
            return sql;
        }

        public bool InsertIntoBatchesTable(IBatch batch) {
            IList<IBatchValue> bTemps = batch.GetBatchTemperatures();
            IList<IBatchValue> bHumids = batch.GetBatchHumidities();
            IList<IBatchValue> bVibs = batch.GetBatchVibrations();

            ISet<IList<IBatchValue>> setOfValues = new HashSet<IList<IBatchValue>>();
            setOfValues.Add(bTemps);
            setOfValues.Add(bHumids);
            setOfValues.Add(bVibs);

            string sql0 = "INSERT INTO " + batchesTable + " VALUES ("
                + batch.GetBatchId() + ", "
                + batch.GetBeerId() + ", "
                + batch.GetAcceptableProducts() + ", "
                + batch.GetDefectProducts() + ", '"
                + batch.GetTimestampStart() + "', '"
                + batch.GetTimestampEnd() + "', "
                + batch.GetOEE() + ", "
                + batch.ProfitPerMin + ", "
                + batch.Speed + ");";

            String[] sql = CreateBatchValuesStatements(setOfValues, batch.GetBatchId(), 1);
            sql[0] = sql0;

            return SendSqlCommand(sql);
        }

        public bool InsertBatchValueSet(ISet<IList<IBatchValue>> batchValues, float batchId) {
            String[] sql = CreateBatchValuesStatements(batchValues, batchId, 0);

            return SendSqlCommand(sql);
        }

        public bool UpdateBatch(IBatch batch) {
            string sql = "UPDATE " + batchesTable
                + " SET acceptableproducts = " + batch.GetAcceptableProducts()
                + ", defectproducts = " + batch.GetDefectProducts()
                + ", timestampend = '" + batch.GetTimestampEnd()
                + "' WHERE batchid = " + batch.GetBatchId();

            return SendSqlCommand(sql);
        }

        public IDictionary<float, IBatch> GetAllBatches() {
            string sql = "SELECT * FROM " + batchesTable;

            return GetSqlCommand(sql);
        }

        public IDictionary<float, IBatch> GetBatches(string month, string year) {
            if (month.Length == 1) {
                month = "0" + month;
            }
            if (month.Length == 2 && year.Length == 4) {
                string sql = "SELECT * FROM " + batchesTable
                    + " WHERE timestampEnd LIKE '___" + month + "/" + year + "%'";

                return GetSqlCommand(sql);
            } else return null;
        }

        public IDictionary<float, IBatch> GetBatches(int amount) {
            string sql = "SELECT * FROM " + batchesTable + " LIMIT " + amount;

            return GetSqlCommand(sql);
        }

        public IBatch GetBatch(float batchId) {
            string sql = "SELECT * FROM " + batchesTable
                + " WHERE batchid = " + batchId;

            IDictionary<float, IBatch> batch = GetSqlCommand(sql);

            IBatch result;
            if (batch.TryGetValue(batchId, out result)) {
                return result;
            } else {
                return null;
            }
        }

        public bool DeleteAllBatches() {
            string sql = "DELETE FROM " + batchesTable + " WHERE true";

            return SendSqlCommand(sql);
        }

        public bool DeleteBatch(float batchId) {
            string sql = "DELETE FROM " + batchesTable + " WHERE batchid = " + batchId;

            return SendSqlCommand(sql);
        }

        public bool RunQueries(string[] statements) {
            return SendSqlCommand(statements);
        }

        public IDictionary<float, IRecipe> GetAllRecipes() {
            string sql = "SELECT * FROM " + recipesTable;

            return GetRecipeSqlCommand(sql);
        }

        public bool AddRecipes(IRecipe[] recipes) {
            string[] statements = new string[recipes.Length];

            for (int i = 0; i < recipes.Length; i++) {
                statements[i] = "INSERT INTO " + recipesTable + " VALUES ("
                    + recipes[i].BeerId + ", "
                    + recipes[i].MaxSpeed + ", '"
                    + recipes[i].Name + "', "
                    + recipes[i].Barley + ", "
                    + recipes[i].Hops + ", "
                    + recipes[i].Malt + ", "
                    + recipes[i].Wheat + ", "
                    + recipes[i].Yeast + ");";
            }

            return SendSqlCommand(statements);
        }

        public float GetHighestBatchId() {
            string sql = "SELECT * FROM " + batchesTable
                + " WHERE batchid = (SELECT MAX(batchid) FROM "
                + batchesTable + ");";

            IDictionary<float, IBatch> collection = GetSqlCommand(sql);

            if (collection.Count > 0) {
                foreach (KeyValuePair<float, IBatch> batch in collection) {

                    return batch.Key;
                }
            }
            return 0;
        }

        public double GetOptimalSpeed(IRecipe recipe) {
            // query for db
            string sql = String.Format("SELECT speed FROM {0} WHERE ppm = (SELECT MAX(ppm) FROM batches WHERE beerid = {1});", batchesTable, recipe.BeerId);
            // executes query and returns first column of the first row as a double
            NpgsqlConnection conn = new NpgsqlConnection(connString);
            conn.Open();
            try {              
                return (double)new NpgsqlCommand(sql,conn).ExecuteScalar();
            } catch(Exception ex) {
                MessageBox.Show(ex.StackTrace);
                return 0;
            } finally {
                conn.Close();
            }

        }
    }
}
