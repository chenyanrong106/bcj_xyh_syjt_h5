using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SPACRM.Common.Utils
{
  public  class DateUtils
    {
        public static DateTime ToDateTime(object obj)
        {
            DateTime result;
            if (obj != null && DateTime.TryParse(obj.ToString(), out result))
            {
                return result;
            }
            else
            {
                return DateTime.MinValue;
            }

        }
        public static DateTime? ToDateTimeNullable(object obj)
        {

            DateTime result;
            if (obj != null && DateTime.TryParse(obj.ToString(), out result))
            {
                return result;
            }
            else
            {
                return null;
            }

        }
    }
}
