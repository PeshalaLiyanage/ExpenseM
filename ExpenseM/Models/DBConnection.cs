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
        private static readonly object padlock = new object();

        ExpenseMDataModelContainer dbConnection;

        DBConnection()
        {
            dbConnection = new ExpenseMDataModelContainer();
        }

        public static DBConnection GetInstance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new DBConnection();
                    }
                    return instance;
                }
            }
        }


        public ExpenseMDataModelContainer DB
        {
            get { return dbConnection; }

        }

    }
}
