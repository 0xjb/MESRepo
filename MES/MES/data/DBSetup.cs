using MES.acq;
using MES.data;
using System;
using System.Collections.Generic;
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
            dbManager.GetAllBatches();
        }

    }
}
