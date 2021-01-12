using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseM.Utilities
{
  class DateUtilities
  {
    DateTime today = DateTime.Now;
    private static DateUtilities instance = null;
    private static readonly object syncObj = new object();
    DateTime monthStartDate;
    DateTime monthEndDate;

    DateUtilities()
    {
      monthStartDate = new DateTime(today.Year, today.Month, 1);
      monthEndDate= monthStartDate.AddMonths(1).AddDays(-1);

    }
    public static DateUtilities GetInstance
    {
      get
      {
        lock (syncObj)
        {
          if (instance == null)
          {
            instance = new DateUtilities();
          }
          return instance;
        }
      }
    }
    public DateTime CurrentMonthStartDate() => monthStartDate;


    public DateTime CurrentMonthEndDate() => monthEndDate;

    public DateTime GetMonthStartDate(DateTime date) => new DateTime(date.Year, date.Month, 1);
   
  }
}
