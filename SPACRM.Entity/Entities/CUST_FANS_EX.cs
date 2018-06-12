using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SPACRM.Entity.Entities
{
    public class CUST_FANS_EX:WXCUST_FANS
    {
        private string _qx;

        public string qx
        {
            get { return _qx; }
            set { _qx = value; }
        }

        private string _xb;

        public string xb
        {
            get { return _xb; }
            set { _xb = value; }
        }

        private string _message;

        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }

        private string _MEM_LEVEL;
        /// <summary>
        /// 会员类型
        /// </summary>
        public string MEM_LEVEL
        {
            get { return _MEM_LEVEL; }
            set { _MEM_LEVEL = value; }
        }
        private string _XM;
        /// <summary>
        /// 姓名
        /// </summary>
        public string XM
        {
            get { return _XM; }
            set { _XM = value; }
        }
        private string _phone;
        /// <summary>
        /// 手机
        /// </summary>
        public string Phone
        {
            get { return _phone; }
            set { _phone = value; }
        }
        private string _birthday;
        /// <summary>
        /// 生日
        /// </summary>
        public string Birthday
        {
            get { return _birthday; }
            set { _birthday = value; }
        }
        private string _yzm;
        /// <summary>
        /// 验证码
        /// </summary>
        public string Yzm
        {
            get { return _yzm; }
            set { _yzm = value; }
        }

        private string _HTML;

        public string HTML
        {
            get { return _HTML; }
            set { _HTML = value; }
        }
    }
}
