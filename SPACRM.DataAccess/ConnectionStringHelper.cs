using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SPACRM.DataAccess
{
    public class ConnectionStringHelper
    {

        public static string GetValueByKey(string dbkey)
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings[dbkey].ConnectionString;
        }
    }
}
