using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SPACRM.Entity.Jsons;

namespace SPACRM.WebApp.Helper
{
    public class DataSourceHelper
    {
        public static List<CodeValue> GetHours()
        {
            var list = new List<CodeValue>();
            for (int i = 0; i <= 23; i++)
            {
                list.Add(new CodeValue { Code = i.ToString(), Value = i.ToString() });
            }
            return list;
        }
    }
}