using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SPACRM.Entity.Entities
{
    public class Graphic_Detail_EX:WXGraphicDetail
    {
        private string _Name;

        /// <summary>
        /// 图文名称
        /// </summary>
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
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

       

        private int listcount;

        public int Listcount
        {
            get { return listcount; }
            set { listcount = value; }
        }

        private DateTime _CreateDate;

        public DateTime CreateDate
        {
            get { return _CreateDate; }
            set { _CreateDate = value; }
        }

        private string _FullPicUrl;
        /// <summary>
        /// 带服务器地址的图片链接
        /// </summary>
        public string FullPicUrl
        {
            get { return _FullPicUrl; }
            set { _FullPicUrl = value; }
        }
    }
}
