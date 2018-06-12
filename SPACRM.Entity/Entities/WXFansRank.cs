using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SPACRM.Entity.Entities
{
    /// <summary>
    /// 微信粉丝排名
    /// </summary>
    public class WXFansRank
    {
        public int rowNumber { get; set; }
        public string FromUserName { get; set; }
        public string Nickname { get; set; }
        public int InviteCnt { get; set; }
        public string headimgurl { get; set; }
        public DateTime? CreateTime { get; set; }
    }
}
