using Newtonsoft.Json;
using SPACRM.Business.ServiceImpl;
using SPACRM.Common;
using SPACRM.Entity;
using SPACRM.Interface;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SPACRM.WebApp.wechat.XYH_Coupon_H5.html
{
    public partial class Home : System.Web.UI.Page
    {
        ISystemService sbo = new SystemService();
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Request.QueryString["para"] == null)
            //{
            //    if ((Request.QueryString["openid"] != null) || Session["FromUserName"] != null)
            //    {
            //        WriteTxt("结束授权");
            //        string user = Request.QueryString["openid"] == null ? Session["FromUserName"].ToString() : Request.QueryString["openid"].ToString();
            //        string access_token = Request.QueryString["Access_token"] == null ? Session["Access_token"].ToString() : Request.QueryString["Access_token"].ToString();
            //        Session["FromUserName"] = user;
            //        Session["Access_token"] = access_token;

            //        HttpCookie cookie = Request.Cookies["XYH_COUPON"];
            //        if (cookie == null)
            //            cookie = new HttpCookie("XYH_COUPON");
            //        cookie.Values.Set("FromUserName", user);
                    
            //        string url = "https://api.weixin.qq.com/sns/userinfo?access_token=" + access_token + "&openid=" + user + "&lang=zh_CN";
            //        string token = PostRequest(url);
            //        WriteTxt(url);
            //        WriteTxt(token);
            //        OpenInfo autho = JsonConvert.DeserializeObject<OpenInfo>(token);
            //        if (!string.IsNullOrWhiteSpace(autho.headimgurl))
            //        {
            //            MySmallShopService mss = new MySmallShopService();
            //            OAauth_Log oa = mss.GetOA(user);
            //            if (oa == null)
            //            {
            //                oa = new OAauth_Log();
            //                oa.FromUserName = user;
            //                oa.ToUserName = "商业集团送券活动";
            //                oa.headimgurl = autho.headimgurl;
            //                oa.Nickname = autho.nickname;
            //                oa.CreateDate = DateTime.Now;
            //                mss.SaveOA(oa);
            //            }
            //        }
            //    }
            //    else
            //    {
            //        WriteTxt("开始授权");
            //        OAuth(AbsoluteUri);
            //    }
            //}
        }


        public void OAuth(string redirect_uri)
        {
            string url = AppConfig.OAuthUrl + "?url={0}&scope=snsapi_userinfo";
            url = string.Format(url, redirect_uri);

            Response.Redirect(url, false);

        }


        public delegate int WriteLogHandler(WXLOG log);
        public bool WriteTxt(string str)
        {
            try
            {
                WXLOG l = new WXLOG();
                l.CON = str;
                l.TIME = DateTime.Now;
                // sbo.AddLog(l);
                WriteLogHandler handler = new WriteLogHandler(sbo.AddLog);
                int result = handler.Invoke(l);
            }
            catch (Exception)
            {
                return false;
            }
            return true;

        }

        /// <summary>
        /// 当前页面访问地址
        /// </summary>
        /// <returns></returns>
        public string AbsoluteUri
        {
            get
            {
                return Server.UrlEncode(WebUrl + System.Web.HttpContext.Current.Request.Url.AbsolutePath + System.Web.HttpContext.Current.Request.Url.Query);

                return Server.UrlEncode(System.Web.HttpContext.Current.Request.Url.Scheme + "://" + System.Web.HttpContext.Current.Request.Url.DnsSafeHost + System.Web.HttpContext.Current.Request.Url.AbsolutePath + System.Web.HttpContext.Current.Request.Url.Query);
                return Server.UrlEncode(System.Web.HttpContext.Current.Request.Url.AbsoluteUri);
            }
        }

        public string PostRequest(string url)
        {
            HttpWebRequest Request = (HttpWebRequest)HttpWebRequest.Create(url);  //定义请求对象，并设置好请求URL地址
            HttpWebResponse response = (HttpWebResponse)Request.GetResponse();    //定义响应对象，System.Web.HttpContext.Current.Request在调用GetResponse方法事执行请求了，而不是在HttpWebRequest.Create的时候执行。       
            Stream stream = response.GetResponseStream(); //定义一个流对象，来获取响应流
            StreamReader sr = new StreamReader(stream);  //定义一个流读取对象，读取响应流
            string responseHTML = sr.ReadToEnd();
            return responseHTML;
        }

        public class OpenInfo
        {
            public string access_token { get; set; }
            public string refresh_token { get; set; }
            public string openid { get; set; }
            public string scope { get; set; }
            public string nickname { get; set; }
            public string headimgurl { get; set; }
            public string sex { get; set; }
            public string province { get; set; }
            public string city { get; set; }
            public string country { get; set; }
            public int subscribe { get; set; }
        }

        /// <summary>
        /// 发布后的地址
        /// </summary>
        /// <returns></returns>
        public string WebUrl
        {
            get { return ConfigurationSettings.AppSettings["WebUrl"]; }
        }

        ///// <summary>
        ///// 授权地址
        ///// </summary>
        ///// <returns></returns>
        //public string OAuthUrl
        //{
        //    get { return ConfigurationSettings.AppSettings["OAuthUrl"]; }
        //}

        ///// <summary>
        ///// 获取Token地址
        ///// </summary>
        ///// <returns></returns>
        //public string TokenUrl
        //{
        //    get { return ConfigurationSettings.AppSettings["TokenUrl"]; }
        //}
    }
}