using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SPACRM.Common.Utils
{
    public class IdUtils
    {
        public static String NewGuid()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
