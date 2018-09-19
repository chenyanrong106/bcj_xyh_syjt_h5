using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPACRM.WebApp
{
    public class CardApi
    {
        public int errcode { get; set; }
        public string errmsg { get; set; }
        public string ticket { get; set; }
        public int expires_in { get; set; }
    }
}