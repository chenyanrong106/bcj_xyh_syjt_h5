using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SPACRM.Entity.Entities
{
    public class WXTG_INFO_EX: WXTG_INFO
    {
        public DateTime EndTime { get; set; }
        public string OpenHeadimgurl { get; set; }
        public string JoinHeadimgurl { get; set; }
        public int Card1Status { get; set; }
        public int Card2Status { get; set; }

    }
}
