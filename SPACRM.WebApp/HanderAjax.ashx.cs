using SPACRM.Common;
using SPACRM.Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace SPACRM.WebApp
{
    /// <summary>
    /// HanderAjax 的摘要说明
    /// </summary>
    public class HanderAjax : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            //Access access = new Access();
            context.Response.ContentType = "text/plain";
            if (GetQeuryString("para", context) == "jsapi") //获取js接口凭证
            {
                try
                {
                   
                    string sj = ConvertDateTimeInt(DateTime.Now).ToString();//时间戳
                    string sjm = Guid.NewGuid().ToString("d"); //随机码
                    string jsapimsg = NetHelper.HttpRequest(AppConfig.JsApiTokenUrl, "", "GET", 2000,
                    Encoding.UTF8, "application/json");
                    JsApiTicket JsApiTicket = JsonHelper.DeserializeObject<JsApiTicket>(jsapimsg);


                    string ticket = JsApiTicket.JsApi;// GetJSAPI_Ticket(token, m); //凭证
                    string dz = context.Server.UrlDecode(context.Request.Params["apiurl"]);
                    string noncestr = "noncestr=" + sjm;
                    string jsapi_ticket = "jsapi_ticket=" + ticket;
                    string timestamp = "timestamp=" + sj;
                    string url = "url=" + dz;
                    string[] ArrTmp = { noncestr, jsapi_ticket, timestamp, url };
                    Array.Sort(ArrTmp);     //字典排序
                    string tmpStr = string.Join("&", ArrTmp);
                    tmpStr = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(tmpStr, "SHA1");
                    tmpStr = tmpStr.ToLower();
                    if (ticket == "")
                    {
                        context.Response.Write("{\"status\":\"" + -1 + "\"}");
                    }
                    else
                    {
                        var re = new
                        {
                            state = 0,
                            appId = AppConfig.WXAppId,// "wx37a6d83af764d816",
                            timestamp = sj,
                            nonceStr = sjm,
                            signature = tmpStr,
                            url = dz,
                            link = (dz.IndexOf("&") == -1 ? dz : dz.Substring(0, dz.IndexOf("&"))) + "&id=" + GetValueFromUrl(dz, "state"),
                            title = "佰草集心约会",
                            imgUrl = "https://mmbiz.qlogo.cn/mmbiz_jpg/ugkm3wzgIFILaicicIzX7hfxblXziaEUliaGk37Q0VukC4tiaWB3lfcxYopOn72bHibaKEenbsPHzqpvxppHk3NanItw/0?wx_fmt=jpeg",
                            desc = "佰草集心约会"
                        };

                        string ret = Newtonsoft.Json.JsonConvert.SerializeObject(re);
                        context.Response.Write(ret);
                    }
                }
                catch (Exception ex)
                {
                    context.Response.Write(ex.Message + "," + ex.StackTrace);
                }
            }
        }


        public string GetQeuryString(string para, HttpContext context)
        {
            if (context.Request.QueryString[para] != null) return context.Request.QueryString[para].ToString();
            else return "";
        }
        /// <summary>
        /// datetime转换为unixtime
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        private int ConvertDateTimeInt(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return (int)(time - startTime).TotalSeconds;
        }
        // <summary>
        /// 获取URL中的参数
        /// </summary>
        /// <param name="url"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public string GetValueFromUrl(string url, string name)
        {
            string[] a = url.Split('?');
            if (a.Length <= 1)
                return "-1";
            else
            {
                string[] b = a[1].Split('&');
                string d = "";
                foreach (string c in b)
                {
                    if (c.Split('=')[0] == name)
                    {
                        d = c;
                    }
                }
                if (d == "")
                    return "-1";
                else
                    return d.Split('=')[1];
            }
        }
        private string reverse(string value)
        {
            System.Text.StringBuilder s = new System.Text.StringBuilder();

            for (int i = value.Length - 1; i >= 0; i--)
            {
                s.Append(value[i]);

            }
            return s.ToString();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

    }
}