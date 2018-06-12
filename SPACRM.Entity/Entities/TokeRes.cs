using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SPACRM.Entity.Entities
{
    public class TokeRes: WxResBase
    {
        /// <summary>
        /// access_token
        /// </summary>
        [JsonProperty("access_token")]
        public string Access_Token { get; set; }

        /// <summary>
        /// 有效时间
        /// </summary>
        [JsonProperty("expires_in")]
        public int Expires_In { get; set; }
    }
}
