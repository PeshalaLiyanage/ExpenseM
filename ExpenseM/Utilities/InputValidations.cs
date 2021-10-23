using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ExpenseM.Utilities
{
  class InputValidations
  {

    private static InputValidations instance = null;
    private static readonly object syncObj = new object();

    InputValidations()
    {
    }

    public static InputValidations GetInstance
    {
      get
      {
        lock (syncObj)
        {
          if (instance == null)
          {
            instance = new InputValidations();
          }
          return instance;
        }
      }
    }

    public bool ValidateEmail(String email)
    {
      return Regex.IsMatch(email, Properties.Resources.EMAIL_REGEX, RegexOptions.IgnoreCase);
    }
  }
}
