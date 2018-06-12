using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SPACRM.Entity.Entities
{
    public class Information_EX:WXInformation
    {
        private string _Title;

        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }
        private string _Description;

        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
        private string _PicUrl;

        public string PicUrl
        {
            get { return _PicUrl; }
            set { _PicUrl = value; }
        }
        private string _fulltextUrl;

        public string FulltextUrl
        {
            get { return _fulltextUrl; }
            set { _fulltextUrl = value; }
        }
        private int _DID;
        /// <summary>
        /// 图文消息id
        /// </summary>
        public int DID
        {
            get { return _DID; }
            set { _DID = value; }
        }


        private string _XXType;
        /// <summary>
        /// 消息类型
        /// </summary>
        public string XXType
        {
            get { return _XXType; }
            set { _XXType = value; }
        }

        private string _PPType;
        /// <summary>
        /// 关键字匹配类型
        /// </summary>
        public string PPType
        {
            get { return _PPType; }
            set { _PPType = value; }
        }

        private bool _IsURL;
        /// <summary>
        /// 是否链接 如果是链接则直接跳转链接，不是链接则调整到指定页面显示内容
        /// </summary>
        public bool IsURL
        {
            get { return _IsURL; }
            set { _IsURL = value; }
        }

        private string _tuwen;
        /// <summary>
        /// 图文
        /// </summary>
        public string tuwen
        {
            get { return _tuwen; }
            set { _tuwen = value; }
        }

        private string _Biaoqing;
        /// <summary>
        /// 表情
        /// </summary>
        public string Biaoqing
        {
            get { return _Biaoqing; }
            set { _Biaoqing = value; }
        }
    }
}
