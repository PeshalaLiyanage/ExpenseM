using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseM.Utilities
{
  // Utility class for date functionalities
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
      monthEndDate = monthStartDate.AddMonths(1).AddDays(-1);

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

    public DateTime GetMonthEndDate(DateTime endDate)
    {
      DateTime _monthEndDate = new DateTime(endDate.Year, endDate.Month, 1);
      return _monthEndDate.AddMonths(1).AddDays(-1);
    }

    public int GetMonthDifference(DateTime startDate, DateTime endDate)
    {
      if (startDate.Month == endDate.Month && startDate.Year == endDate.Year)
      {
        return 1;
      }

      return Math.Abs((startDate.Month - endDate.Month) + 12 * (startDate.Year - endDate.Year));
    }

  }
}
