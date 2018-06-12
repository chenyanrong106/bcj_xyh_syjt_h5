using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SPACRM.Entity.Entities
{
    public class CUST_MSG_RECORD_EX:WXCUST_MSG_RECORD
    {
        private string _name;
        /// <summary>
        /// 发送人姓名
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _IMAGE;
        /// <summary>
        /// 头像
        /// </summary>
        public string IMAGE
        {
            get { return _IMAGE; }
            set { _IMAGE = value; }
        }

        private string _fname;
        /// <summary>
        /// 微信好友昵称
        /// </summary>
        public string Fname
        {
            get { return _fname; }
            set { _fname = value; }
        }

        private string _tuwen;
        /// <summary>
        /// 图文
        /// </summary>
        public string Tuwen
        {
            get { return _tuwen; }
            set { _tuwen = value; }
        }

        private int _FID;
        /// <summary>
        /// 粉丝ID
        /// </summary>
        public int FID
        {
            get { return _FID; }
            set { _FID = value; }
        }

        private string _biaoqing;
        /// <summary>
        /// 表情
        /// </summary>
        public string Biaoqing
        {
            get { return _biaoqing; }
            set { _biaoqing = value; }
        }

        private List<WXBiaoqing> _BQlist;

        public List<WXBiaoqing> BQlist
        {
            set { _BQlist = value; }
            get { return _BQlist; }
        }
    }
}
