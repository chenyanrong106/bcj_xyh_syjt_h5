using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SPACRM.DataAccess
{
    public class TransScope : Vulcan.Framework.DBConnectionManager.TransScope
    {
        public TransScope()
            : base(ConnectionStringHelper.GetValueByKey(SPACRM.Common.AppConfig.MainDbKey))
        {

        }
    }
}
