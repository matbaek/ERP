using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    class Database
    {
        private string connectionString =
               "Data Source = EALSQL1.Eal.Local;" +
               "Database = C_DB10_2018;" +
               "User ID = C_STUDENT10;" +
               "Password = C_OPENDB10;";
        public static Database instance = null;
        public static Database Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Database();
                }
                return instance;
            }
        }
        
        public string ConnectionString
        {
            get { return connectionString; }
        }
    }
}
