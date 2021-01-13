using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpenseM.Entities;

namespace ExpenseM.Utilities
{
  class FileUtilities
  {
    // thread safe singleton pattern
    private static FileUtilities instance = null;
    private static readonly object syncObj = new object();

    FileUtilities()
    {
    }

    public static FileUtilities GetInstance
    {
      get
      {
        lock (syncObj)
        {
          if (instance == null)
          {
            instance = new FileUtilities();
          }
          return instance;
        }
      }
    }
    public void DeleteFile(String path)
    {
      File.Delete(path);
    }

    public void WriteToFile(ExpenseMDataSet expenseMData, String path)
    {
      expenseMData.WriteXml(path);
    }

    public dynamic ReadFromFile(ExpenseMDataSet expenseMData, String path)
    {
      if (File.Exists(path) == true)
      {
        return expenseMData.ReadXml(path);
      }

      return null;
    }
  }
}
