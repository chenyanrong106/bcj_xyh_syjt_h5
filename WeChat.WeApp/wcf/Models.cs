using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SPACRM.WebApp.wcf
{
    [DataContract]
    public class PostMessageResult
    {
        [DataMember]
        public bool Success { get; set; }

        [DataMember]
        public int? ErrorCode { get; set; }

        [DataMember]
        public string Message { get; set; }

        [DataMember]
        public DateTime? SendTime { get; set; }
    }
}