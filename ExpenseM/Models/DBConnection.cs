using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpenseM.Entities;

namespace ExpenseM.Models
{
    class DBConnection
    {
        private static DBConnection instance = null;
        private static readonly object syncObj = new object();

        ExpenseMDataModelContainer dbConnection;

        DBConnection()
        {
            dbConnection = new ExpenseMDataModelContainer();
        }

        public static ExpenseMDataModelContainer Connection
        {
            get
            {
                lock (syncObj)
                {
                    if (instance == null)
                    {
                        instance = new DBConnection();
                    }
                    return instance.dbConnection;
                }
            }
        }

    }
}
