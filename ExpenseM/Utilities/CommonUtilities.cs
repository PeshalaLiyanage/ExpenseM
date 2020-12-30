using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpenseM.Entities;

namespace ExpenseM.Utilities
{
    class CommonUtilities
    {
        private static CommonUtilities instance = null;
        private static readonly object padlock = new object();

        CommonUtilities()
        {
        }

        public static CommonUtilities GetInstance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new CommonUtilities();
                    }
                    return instance;
                }
            }
        }
        public void DeleteFile(String path)
        {
            File.Delete(path);
        }

        public void WriteToFile(ExpenseMDataSet expenseMData ,String path)
        {
            expenseMData.WriteXml(path);
        }

        public void ReadFromFile(ExpenseMDataSet expenseMData, String path)
        {
            expenseMData.ReadXml(path);
        }
    }
}
