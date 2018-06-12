using Newtonsoft.Json;
using SPACRM.Business.ServiceImpl;
using SPACRM.Entity;
using SPACRM.Entity.Entities;
using SPACRM.Interface;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Security;

namespace WeChatCRM.WebApp.WeChat
{
    /// <summary>
    /// Business 的摘要说明
    /// </summary>
    public class Business : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        MySmallShopService mss = new MySmallShopService();
        CommonService _mservice = new CommonService();
        WeiPage w = new WeiPage();
        public void ProcessRequest(HttpContext context)
        {
            if (GetQeuryString("para", context) == "jsapi") //获取js接口凭证
            {
                try
                {
                    ORG_INFO m = mss.GetWD(w.mjuserid);
                    if (m != null)
                    {
                        string token =  w.Token();//w.mjuserid
                        string sj = ConvertDateTimeInt(DateTime.Now).ToString();//时间戳
                        string sjm = Guid.NewGuid().ToString("d"); //随机码
                        string ticket = GetJSAPI_Ticket(token, m); //凭证
                        WriteTxt("js注册：apiurl=" + context.Request.Params["apiurl"]);
                        string dz = context.Request.Params["apiurl"];// context.Server.UrlDecode(context.Request.Params["apiurl"]);
                        string noncestr = "noncestr=" + sjm;
                        string jsapi_ticket = "jsapi_ticket=" + ticket;
                        string timestamp = "timestamp=" + sj;
                        string url = "url=" + dz;
                        WriteTxt("js注册：url="+ dz);
                        string[] ArrTmp = { noncestr, jsapi_ticket, timestamp, url };
                        Array.Sort(ArrTmp);     //字典排序
                        string tmpStr = string.Join("&", ArrTmp);
                        WriteTxt("js注册：tmpStr=" + tmpStr);
                        tmpStr = FormsAuthentication.HashPasswordForStoringInConfigFile(tmpStr, "SHA1");
                        tmpStr = tmpStr.ToLower();
                        if (ticket == "")
                        {
                            WriteTxt("js注册：失败" );
                            context.Response.Write("{\"status\":\"" + -1 + "\"}");
                        }
                        else
                        {
                            var re = new
                            {
                                state = 0,
                                appId = m.AppID,
                                timestamp = sj,
                                nonceStr = sjm,
                                signature = tmpStr,
                                url = dz,
                                link = (dz.IndexOf("&") == -1 ? dz : dz.Substring(0, dz.IndexOf("&"))),
                                title = "",
                                imgUrl = ConfigurationSettings.AppSettings["WebUrl"]+"/wechat/spa/image/logo.jpg",//"http://www.meijiewd.com/assets/images/meijie.png",
                                desc = ""
                            };
                            string ret = JsonConvert.SerializeObject(re);
                            WriteTxt("js注册：" + ret);
                            context.Response.Write(ret);
                        }
                    }
                }
                catch (Exception ex)
                {
                    context.Response.Write(ex.Message + "," + ex.StackTrace);
                }

            }
            else if (GetQeuryString("para", context) == "upimg") //上传图片
            {
              

            }
            else if (GetQeuryString("para", context) == "delete") //上传图片
            {
       

            }
            else if (GetQeuryString("para", context) == "pv")
            {
                try
                {
                    PVData pv = new PVData();
                    pv.DOMAIN = context.Request.Params[1];
                    pv.URL = context.Request.Params[2];
                    pv.TITLE = context.Request.Params[3];
                    pv.REFERRER = context.Request.Params[4];
                    pv.SH = context.Request.Params[5];
                    pv.SW = context.Request.Params[6];
                    pv.CD = context.Request.Params[7];
                    pv.ACCOUNT = context.Request.Params[9];
                    pv.LTIME = context.Request.Params[10];
                    mss.InsertPVData(pv);
                }
                catch { }

            }

          

        }

        public bool WriteTxt(string str)
        {
            try
            {
                ISystemService sbo = new SystemService();
                WXLOG l = new WXLOG();
                l.CON = str;
                l.TIME = DateTime.Now;
                sbo.AddLog(l);
            }
            catch (Exception)
            {
                return false;
            }
            return true;

        }

        /// <summary>
        /// 验证会员信息
        /// </summary>
        class getmeminfo
        {
            public int status { get; set; }
            public string message { get; set; }
            public meminfo data { get; set; }
        }
        /// <summary>
        /// 会员信息
        /// </summary>
        class meminfo
        {
            public int id { get; set; }
            public string name { get; set; }
            public string mobile { get; set; }
            public string card_no { get; set; }
        }

        /// <summary>
        /// 获取订单
        /// </summary>
        class orderdata
        {
            public int status { get; set; }
            public string msg { get; set; }
            public orderlist2[] data { get; set; }
        }

        /// <summary>
        /// 订单列表
        /// </summary>
        class orderlist2
        {
            public int id { get; set; }
            public string order_no { get; set; }
            public string trans_date { get; set; }
            public string store_no { get; set; }
            public string store_name { get; set; }
        }



        const double pi = 3.14159265358979324;
        const double a = 6378245.0;
        const double ee = 0.00669342162296594323;
        const double x_pi = 3.14159265358979324 * 3000.0 / 180.0;
        /// <summary>
        /// 百度坐标转谷歌坐标（火星坐标）
        /// </summary>
        /// <param name="bd_lat"></param>
        /// <param name="bd_lon"></param>
        /// <param name="gg_lat"></param>
        /// <param name="gg_lon"></param>
        public void bd_decrypt(double bd_lat, double bd_lon, ref double gg_lat, ref double gg_lon)
        {

            double x = bd_lon - 0.0065, y = bd_lat - 0.006;

            double z = Math.Sqrt(x * x + y * y) - 0.00002 * Math.Sin(y * x_pi);

            double theta = Math.Atan2(y, x) - 0.000003 * Math.Cos(x * x_pi);

            gg_lon = z * Math.Cos(theta);

            gg_lat = z * Math.Sin(theta);

        }

        #region js接口调用
        /// <summary>
        /// 获取最新JSAPI_TICKET凭证
        /// </summary>
        /// <param name="ToUserName"></param>
        /// <returns></returns>
        public string GetJSAPI_Ticket(string Token, ORG_INFO m)
        {
            string JSapi_ticket = "";
            if (m.JSapi_Ticket != "" && m.JSapi_Ticket != null && (m.GetTicketTime == null ? DateTime.Now.AddHours(-3) : m.GetTicketTime.Value).AddHours(2) > DateTime.Now) //不为空，并且获取时间没有超过2小时
            {
                return m.JSapi_Ticket;
            }
            else
            {
                JSapi_ticket = m.JSapi_Ticket;
                string url = "https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token=" + Token + "&type=jsapi";
                string b = PostRequest(url);
                tickresult ticket = JsonConvert.DeserializeObject<tickresult>(b);
                if (ticket.errcode == 0)  //正确
                {
                    m.JSapi_Ticket = ticket.ticket;
                    m.GetTicketTime = DateTime.Now;
                    mss.SaveMD(m);
                    return m.JSapi_Ticket;
                }
                return "";
            }
        }

        /// <summary>
        /// jsapi_ticket
        /// </summary>
        class tickresult
        {
            public int errcode { get; set; }
            public string errmsg { get; set; }
            public string ticket { get; set; }
            public int expires_in { get; set; }
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

        #endregion


        public string GetQeuryString(string para, HttpContext context)
        {
            if (context.Request.QueryString[para] != null) return context.Request.QueryString[para].ToString();
            else return "";
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public string GetWeek(DateTime time)
        {
            string[] Day = new string[] { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };
            string week = Day[Convert.ToInt32(time.DayOfWeek.ToString("d"))].ToString();
            return week;
        }

        public static string HttpXmlPostRequest(string postUrl, string postXml, Encoding encoding, string contype = "text/xml")
        {
            if (string.IsNullOrEmpty(postUrl))
            {
                throw new ArgumentNullException("HttpXmlPost ArgumentNullException :  postUrl IsNullOrEmpty");
            }

            if (string.IsNullOrEmpty(postXml))
            {
                throw new ArgumentNullException("HttpXmlPost ArgumentNullException : postXml IsNullOrEmpty");
            }

            var request = (HttpWebRequest)WebRequest.Create(postUrl);
            byte[] byteArray = encoding.GetBytes(postXml);
            request.ContentLength = byteArray.Length;
            request.Method = "post";
            request.ContentType = contype;

            using (var requestStream = request.GetRequestStream())
            {
                requestStream.Write(byteArray, 0, byteArray.Length);
            }

            using (var responseStream = request.GetResponse().GetResponseStream())
            {
                return new StreamReader(responseStream, encoding).ReadToEnd();
            }
        }

        public string PostRequest(string url)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);  //定义请求对象，并设置好请求URL地址
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();    //定义响应对象，request在调用GetResponse方法事执行请求了，而不是在HttpWebRequest.Create的时候执行。       
            Stream stream = response.GetResponseStream(); //定义一个流对象，来获取响应流
            StreamReader sr = new StreamReader(stream);  //定义一个流读取对象，读取响应流
            string responseHTML = sr.ReadToEnd();
            return responseHTML;
        }

        class result
        {
            public int status { get; set; }
            public string message { get; set; }
            public int? data { get; set; }
        }

        /// <summary>
        /// 预约记录
        /// </summary>
        //class book
        //{
        //    public string cust_name { get; set; }
        //    public string cust_mobile { get; set; }
        //    public string begin_time { get; set; }
        //    public string end_time { get; set; }
        //    public string service_item { get; set; }
        //    public string booking_num { get; set; }
        //    public string store_id { get; set; }
        //    public string store_name { get; set; }
        //    public string store_tel { get; set; }
        //    public string store_addr { get; set; }
        //    /// <summary>
        //    /// 0：未处理；1：确认；2：已到店；3：已取消
        //    /// </summary>
        //    public int state { get; set; }
        //}
        /// <summary>
        /// 卡级列表
        /// </summary>
        class cardlist
        {
            public card[] data { set; get; }
        }
        /// <summary>
        /// 卡级
        /// </summary>
        class card
        {
            public string card_name { get; set; }
            public decimal service_discount { get; set; }
            public decimal product_discount { get; set; }
            public decimal lccard_discount { set; get; }
            public string begin_date { get; set; }
            public string end_date { get; set; }
            public decimal total_amt { get; set; }
            public decimal balance_amt { set; get; }
        }
        /// <summary>
        /// 订单记录列表
        /// </summary>
        class orderlilst
        {
            public order[] data { get; set; }
        }
        /// <summary>
        /// 订单记录
        /// </summary>
        class order
        {
            public string order_no { get; set; }
            public string trans_date { get; set; }
            public string item_id { get; set; }
            public decimal ori_price { get; set; }
            public decimal curr_price { get; set; }
            public int number { get; set; }
            public string emp_name { get; set; }
            public string item_name { get; set; }
            public string store_name { get; set; }
        }

        class server
        {
            public int Code { get; set; }
            public string Msg { get; set; }
        }



    }
}