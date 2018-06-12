using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SPACRM.Entity.Entities
{
    public class PVData
    {
        public int ID { get; set; }
        public string DOMAIN { get; set; }
        public string URL { get; set; }
        public string TITLE { get; set; }
        public string REFERRER { get; set; }
        public string SH { get; set; }
        public string SW { get; set; }
        public string CD { get; set; }
        public string ACCOUNT { get; set; }
        public string LTIME { get; set; }
        public DateTime CREATE_DATE { get; set; }
    }
}
