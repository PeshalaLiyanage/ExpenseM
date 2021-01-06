using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseM.Utilities
{
    public struct LabelWithKey{
      
        public LabelWithKey(String key, String labelName)
        {
           Key = key;
            LabelName = labelName;
        }

        public String Key { get; set; }
        public String LabelName { get; set; }
    }
}
