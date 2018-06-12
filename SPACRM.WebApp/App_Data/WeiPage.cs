using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using System.Text;
using System.Configuration;
using Newtonsoft.Json;
using SPACRM.Entity;
using SPACRM.Business.ServiceImpl;
using SPACRM.Interface;
using SPACRM.Common;
using SPACRM.Entity.Entities;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using SPACRM.Common.Utils;

/// <summary>
///WeiPage 的摘要说明
/// </summary>
public class WeiPage : System.Web.UI.Page
{
    ISystemService sbo = new SystemService();
    public WeiPage()
    {
        //  Response.Write(" <input type='hidden'' value='" + Server.UrlEncode(AbsoluteUri) + "' id='url' />");
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }
    public class result
    {
        public int errcode { get; set; }
        public string errmsg { get; set; }
        public int st { get; set; }
        public string msg { get; set; }
    }
    /// <summary>
    /// 模板消息
    /// </summary>
    /// <param name="accessToken"></param>
    /// <param name="wxOpenID"></param>
    /// <param name="tempID"></param>
    /// <param name="topColor"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    public string SendTemplateMessage(string accessToken, string wxOpenID, string tempID, string topColor, object data, string url)
    {
        Dictionary<string, object> dict = new Dictionary<string, object>();
        dict.Add("touser", wxOpenID);
        dict.Add("template_id", tempID.Trim());
        dict.Add("url", url);
        dict.Add("topcolor", "#FF0000");
        dict.Add("data", data);
        return this.DoJSONRequest("cgi-bin/message/template/send?access_token=" + accessToken, dict, "POST");
    }
    private static string WeiXinUrl = "https://api.weixin.qq.com/";
    private string DoJSONRequest(string path, Dictionary<string, object> data, string method = "POST")
    {
        string strdata = JsonConvert.SerializeObject(data);

        if (!path.Contains("?"))
        {
            path += "?";
        }
        string url = WeiXinUrl + path;
        string message= NetHelper.HttpRequest(url, strdata, method, 60000, Encoding.UTF8, ContentTypes.JSON);
        sbo.SaveTempMessageLog(new WD_TemplateMessageLog{CreateDate=DateTime.Now,FromUserName=data["touser"].ToString(),First="",keyword=strdata,remark=data["template_id"].ToString(),Result=message,url=url});
        return message;
    }
    //推送一个模板
    public string GetMo(string FromUserName)
    {
        Json rMsg = new Json();
        string message = "";
        string token = Token(mjuserid);
        rMsg.Data += "," + token;
        var temp = new
        {
            first = new { value = "您好!宠物医生提醒您：\n", color = "#FF9E0D" },
            keyword1 = new { value = "\n为了医生能够更清楚的诊断宠物病情，请提前填写完整。\n", color = "#FF9E0D" },
            keyword2 = new { value = "宠医Joker\n", color = "#FF9E0D" },
            remark = new { value = "点击这里立即填写病历卡", color = "#CD0000" }
        };
        message = SendTemplateMessage(token, FromUserName, "P5UKlSojWzehU6jNsVfjaocdUIDjkvHOGJbumYchdkw", "#4876FF", temp, "https://open.weixin.qq.com/connect/oauth2/authorize?appid=wx011e5d2c9355eac4&redirect_uri=http://cw.meijiewd.com/MR/Index.aspx&response_type=code&scope=snsapi_base&state=STATE#wechat_redirect");
        result data = JsonConvert.DeserializeObject<result>(message);
        if (data != null)
        {
            rMsg.Status = data.errcode;
            rMsg.Message = data.errmsg;
        }
        else
        {
            rMsg.Status = -1;
            rMsg.Message = "出现异常";
        }
        return message;
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

    public string GetRequest(string url)
    {
        HttpWebRequest Request = (HttpWebRequest)HttpWebRequest.Create(url);  //定义请求对象，并设置好请求URL地址
        HttpWebResponse response = (HttpWebResponse)Request.GetResponse();    //定义响应对象，System.Web.HttpContext.Current.Request在调用GetResponse方法事执行请求了，而不是在HttpWebRequest.Create的时候执行。       
        Stream stream = response.GetResponseStream(); //定义一个流对象，来获取响应流
        StreamReader sr = new StreamReader(stream);  //定义一个流读取对象，读取响应流
        string responseHTML = sr.ReadToEnd();
        return responseHTML;
    }

    public void AlertMessage(string message)
    {
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alertCloseReload", "<script>alert(\"" + message + "\")</script>");
    }



    /// <summary>
    /// 再客户端显示一条信息，然后转到一个页面
    /// </summary>
    /// <param name="message">显示的信息</param>
    /// <param name="url">转到页面路径</param>
    protected void AlertRedirect(string message, string url)
    {
        Page.RegisterStartupScript("AlertRedirect", "<script language='javascript'>alert(\"" + message.Replace("'", "′") + "\");window.navigate('" + url + "');</script>");
    }
    /// <summary>
    /// 再客户端显示一条信息，然后转到一个页面
    /// </summary>
    /// <param name="message">显示的信息</param>
    /// <param name="url">转到页面路径</param>
    protected void AlertRedirectNew(string message, string url)
    {
        Page.RegisterStartupScript("AlertRedirect", "<script language='javascript'>alert(\"" + message.Replace("'", "′") + "\");location = '" + url + "';</script>");
    }
    /// <summary>
    /// 再客户端显示一条信息，然后关闭页面
    /// </summary>
    /// <param name="message"></param>
    protected void AlertClose(string message)
    {
        Page.RegisterStartupScript("AlertClose", "<script language='javascript'>alert(\"" + message.Replace("'", "′") + "\");window.close();</script>");
    }

    /// <summary>
    /// 关闭窗口
    /// </summary>
    protected void CloseWindow()
    {
        Page.RegisterStartupScript("CloseWindow", "<script language='javascript'>window.close();</script>");
    }


    /// <summary>
    /// 在客户端显示一条信息，然后返回上一个页面
    /// </summary>
    /// <param name="message"></param>
    protected void AlertReflashFrame(string message, string frame)
    {
        Page.RegisterStartupScript("AlertReflashFrame_" + frame, "<script language='javascript'>alert('"
            + message.Replace("'", "′") + "');window.parent.frames['" + frame + "'].location.reload();</script>");
    }

    /// <summary>
    ///  在客户端显示一条信息，然后把页面刷新一遍
    /// </summary>
    /// <param name="message"></param>
    public void AlertReLoad(string message)
    {
        Page.RegisterStartupScript("AlertReLoad", "<script language='javascript'>alert('"
            + message.Replace("'", "′") + "');window.location.href=window.location.href;</script>");
    }


    ///// <summary>
    ///// 获取站点根路径
    ///// </summary>
    ///// <returns></returns>
    //protected string GetSitePath()
    //{
    //    string root = Page.System.Web.HttpContext.Current.Request.ApplicationPath;
    //    if (root == "/") return root;
    //    else return root + "/";
    //}


    public string GetURL()
    {
        string url = ConfigurationManager.AppSettings["caidan"].ToString();
        return url;
        //return "m.shanghaivive";
        //return "122.226.44.60";
    }

    protected void ReturnMessage(string msg)
    {

        //Exception ex = Server.GetLastError();

        //string root = this.GetSitePath();
        //string css = "<link rel='stylesheet' href='" + root + "Skin/Main.css'/>";
        System.Web.HttpContext.Current.Response.Clear();
        //Response.Write(css);
        //Response.Write( "<br><br><h3><font color='red'>系统运行发生错误!请联系管理员!</font></h3><br>错误信息:" );
        //Response.Write( "<hr size='1' color='red'><b>" + msg + "</b><hr size='1' color='red'>" );
        //Response.Write(  ex.StackTrace );

        //出错信息提示样式
        string strHtml = "<body  bgcolor='#CCCCCC'><br><br><br><br><br><br>";
        strHtml += "<div align='center'>";
        strHtml += "  <table width='80%' border='1' cellspacing='0' bordercolorlight='000000' bordercolordark='FFFFFF' bgcolor='E0E0E0'>";
        strHtml += "    <tr>";
        strHtml += "      <td><table border='0' bgcolor='#0066CC' cellspacing='0' cellpadding='2'  height='35'  width='100%'>";
        strHtml += "          <tr>";
        strHtml += "            <td align='center'><font color='FFFFFF' size='3'><b>System messages</b></font></td>";
        strHtml += "          </tr>";
        strHtml += "          </table>";
        strHtml += "          <table border='0' width='100%' cellpadding='4'>";
        strHtml += "            <tr>";
        //strHtml += "              <td width='59' align='center' valign='top'><img src='" + root + "images/28.png'/></td>";
        strHtml += "              <td colspan='2' align='center'><font size='2'><b>" + msg + "</b></font></td>";
        strHtml += "            </tr>";
        strHtml += "            <tr>";
        strHtml += "              <td colspan='2' align='center' valign='top'><input type='button' name='ok' class='formButton' value='　Return　' onclick='javascript:history.back()' />";
        strHtml += "              </td>";
        strHtml += "            </tr>";
        strHtml += "        </table></td>";
        strHtml += "    </tr>";
        strHtml += "  </table>";
        strHtml += "</div></body>";

        System.Web.HttpContext.Current.Response.Write(strHtml);
        System.Web.HttpContext.Current.Response.End();
    }

    protected override void OnError(EventArgs e)
    {

        Exception ex = Server.GetLastError();

        string root = this.GetSitePath();
        //string css = "<link rel='stylesheet' href='" + root + "Skin/Main.css'/>";
        string msg = getExMsg(ex);
        mss.SaveLog(new WXLOG { CON = msg + ",root:" + root, TIME = DateTime.Now });
    }


    private string getExMsg(Exception e)
    {
        string cc = "";
        cc += e.Message;
        if (e.InnerException != null)
            cc = getExMsg(e.InnerException);

        return cc;
    }



    public string GetAppid(string ToUserName)
    {
        m = sbo.GetMerchantsByToUserName(ToUserName);
        return m == null ? "" : m.AppID;
    }

    public string GetSecret(string ToUserName)
    {
        m = sbo.GetMerchantsByToUserName(ToUserName);
        return m == null ? "" : m.Appsecret;
    }

    ORG_INFO m;
    public string Token(string ToUserName)
    {
        m = sbo.GetMerchantsByToUserName(ToUserName);
        if (m == null)
            return "";
        string Access_token = "";
        if (m.Access_token != "")
        {
            Access_token = m.Access_token;
            string url = "https://api.weixin.qq.com/cgi-bin/user/info?access_token=" + Access_token + "&openid=" + (m.OneOpenID == null ? "oS7pm1iNL2P2pjdgHO3xC2NRdWE8" : m.OneOpenID);
            string b = PostRequest(url);
            if (b.Contains("errcode"))  //返回错误信息
            {
                Access_token = GetAccess(m);
                m.Access_token = Access_token;
                sbo.SaveMerchants(m);
            }
            if (m.OneOpenID == "" || m.OneOpenID == null)
            {
                WXCUST_FANS fans = sbo.GetOneFans(m.ToUserName);
                if (fans != null)
                {
                    m.OneOpenID = fans.FROMUSERNAME;
                    sbo.SaveMerchants(m);
                }
            }
        }
        else
        {
            if (m.OneOpenID == "" || m.OneOpenID == null)
            {
                WXCUST_FANS fans = sbo.GetOneFans(m.ToUserName);
                if (fans != null)
                {
                    m.OneOpenID = fans.FROMUSERNAME;
                    // sbo.SaveMerchants(m);
                }
            }
            Access_token = GetAccess(m);
            m.Access_token = Access_token;
            sbo.SaveMerchants(m);

        }
        return Access_token;
    }

    public string Token()
    {
        string url = TokenUrl;
        string token = GetRequest(url);
        WriteTxt("获取token：" + token);
        TokenResponse response = JsonConvert.DeserializeObject<TokenResponse>(token);
        //if (response.Status == 1)
            return response.Access_token;
        //else
        //{
        //    return "";
        //}

    }

    class TokenResponse
    {
        //public int Status { get; set; }
        //public string Message { get; set; }
        public string Access_token { get; set; }

    }

    private string GetAccess(ORG_INFO m)
    {
        string Access_token = "";
        string url = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=" + m.AppID + "&secret=" + m.Appsecret;
        try
        {
            string token = PostRequest(url);
            if (token.Contains("7200"))
            {
                string[] b = token.Split('\"');
                Access_token = b[3];
            }
        }
        catch (Exception)
        {
            Access_token = "";
        }
        return Access_token;
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

        var Request = (HttpWebRequest)WebRequest.Create(postUrl);
        byte[] byteArray = encoding.GetBytes(postXml);
        Request.ContentLength = byteArray.Length;
        Request.Method = "post";
        Request.ContentType = contype;

        using (var requestStream = Request.GetRequestStream())
        {
            requestStream.Write(byteArray, 0, byteArray.Length);
        }

        using (var responseStream = Request.GetResponse().GetResponseStream())
        {
            return new StreamReader(responseStream, encoding).ReadToEnd();
        }
    }



    class baidumap
    {
        public int status { get; set; }
        public zuobiao[] result { get; set; }
    }

    class zuobiao
    {
        public decimal x { get; set; }
        public decimal y { get; set; }
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
    /// 插入粉丝信息
    /// </summary>
    /// <param name="FromUserName">微信号</param>
    /// <param name="state">状态</param>
    /// <returns></returns>
    public int InsertFS(string FromUserName, string ToUserName, int state)
    {
        //获取Token字符串
        string access_token = Token(ToUserName);
        //获取用户信息列表
        string info = "https://api.weixin.qq.com/cgi-bin/user/info?access_token=" + access_token + "&openid=" + FromUserName + "&lang=zh_CN";
        string infomes = PostRequestGet(info);
        Info winfo = JsonConvert.DeserializeObject<Info>(infomes);
        //把用户的信息存到粉丝表,保存之前先判断粉丝表里面有没有这个用户
        WXCUST_FANS cfan = sbo.GetFansByFromUserName(FromUserName);

        SaveOA(FromUserName, ToUserName);
        //未关注时更新状态
        if (cfan != null)
        {
            if (state == 1) //新增
            {
                cfan.NAME = winfo.nickname;
                cfan.GENDER = winfo.sex == "1" ? true : false;
                cfan.COUNTRY = winfo.country;
                cfan.PROVINCE = winfo.province;
                cfan.CITY = winfo.city;
                cfan.FROMUSERNAME = winfo.openid;
                cfan.ToUserName = ToUserName;
                cfan.IMAGE = winfo.headimgurl;
                cfan.STATUS = state;
                cfan.NOTICE_DATE = UnixTimeToTime(winfo.subscribe_time);
                cfan.CANCEL_DATE = DateTime.Now;
                //cfan.CREATE_DATE = DateTime.Now;
                cfan.CREATE_USER = "system";
                cfan.LAST_CONN_DATE = DateTime.Parse("1900-01-01");
                cfan.LAST_MODI_DATE = DateTime.Parse("1900-01-01");
                cfan.REMARK = "";
                cfan.LAST_MODI_USER = "system";
            }
            else  //取消关注
            {
                cfan.STATUS = state;
                cfan.CANCEL_DATE = DateTime.Now;
            }
            return sbo.UpdateFans(cfan);

        }
        else
        {
            cfan = new WXCUST_FANS();
            cfan.NAME = winfo.nickname;
            cfan.GENDER = winfo.sex == "1" ? true : false;
            cfan.COUNTRY = winfo.country;
            cfan.PROVINCE = winfo.province;
            cfan.CITY = winfo.city;
            cfan.FROMUSERNAME = winfo.openid;
            cfan.ToUserName = ToUserName;
            cfan.IMAGE = winfo.headimgurl;
            cfan.STATUS = 1;
            cfan.NOTICE_DATE = UnixTimeToTime(winfo.subscribe_time);
            cfan.CANCEL_DATE = DateTime.Parse("1900-01-01");
            cfan.CREATE_DATE = DateTime.Now;
            cfan.CREATE_USER = "system";
            cfan.LAST_CONN_DATE = DateTime.Parse("1900-01-01");
            cfan.LAST_MODI_DATE = DateTime.Parse("1900-01-01");
            cfan.REMARK = "";
            cfan.LAST_MODI_USER = "system";
            cfan.AVAL_OPPR = 1;
            cfan.TOTAL_OPPR = 1;
            cfan.UNIN_CODE = "";
            cfan.REFE_CODE = "";
            return sbo.InsertFans(cfan);
        }

    }

    public OAauth_Log SaveOA(string FromUserName, string ToUserName)
    {
        MySmallShopService mss = new MySmallShopService();
        OAauth_Log oa = mss.GetOA(FromUserName);
        if (oa == null)
        {
            //获取Token字符串
            string access_token = Token(ToUserName);
            //获取用户信息列表
            string info = "https://api.weixin.qq.com/cgi-bin/user/info?access_token=" + access_token + "&openid=" + FromUserName + "&lang=zh_CN";
            string infomes = PostRequestGet(info);
            Info winfo = JsonConvert.DeserializeObject<Info>(infomes);

            //把用户的信息存到粉丝表,保存之前先判断粉丝表里面有没有这个用户
            oa = new OAauth_Log();
            oa.ID = 0;
            oa.city = winfo.city;
            oa.country = winfo.country;
            oa.CreateDate = DateTime.Now;
            oa.FromUserName = FromUserName;
            oa.headimgurl = winfo.headimgurl;
            oa.Nickname = winfo.nickname;
            oa.province = winfo.province;
            oa.sex = winfo.sex;
            oa.ToUserName = ToUserName;
            mss.SaveOA(oa);

        }
        return oa;
    }

    //发送GET请求
    public string PostRequestGet(string url)
    {
        HttpWebRequest Request = (HttpWebRequest)HttpWebRequest.Create(url);
        Request.Method = "GET";  //定义请求对象，并设置好请求URL地址      
        //System.Web.HttpContext.Current.Request.ProtocolVersion = HttpVersion.Version10;
        //System.Web.HttpContext.Current.Request.ContentType = "image/jpg";
        HttpWebResponse response = (HttpWebResponse)Request.GetResponse();    //定义响应对象，System.Web.HttpContext.Current.Request在调用GetResponse方法事执行请求了，而不是在HttpWebRequest.Create的时候执行。

        //response.ContentType = "image/jpg";
        Stream stream = response.GetResponseStream(); //定义一个流对象，来获取响应流
        StreamReader sr = new StreamReader(stream, Encoding.UTF8);  //定义一个流读取对象，读取响应流
        string responseHTML = sr.ReadToEnd();
        return responseHTML;
    }

    class Info
    {
        public int subscribe { get; set; }
        public string openid { get; set; }
        public string nickname { get; set; }
        public string sex { get; set; }
        public string language { get; set; }
        public string city { get; set; }
        public string province { get; set; }
        public string country { get; set; }
        public string headimgurl { get; set; }
        public string subscribe_time { get; set; }

    }

    /// <summary>
    /// 向粉丝发送信息
    /// </summary>
    /// <param name="FromUserName"></param>
    /// <param name="Message"></param>
    public void PostMessage(string FromUserName, string ToUserName, string Message)
    {
        string Access_token = Token(mjuserid);

        var postUrl = "https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token=" + Access_token;
        string d = @"{
    ""touser"":""{0}"",
    ""msgtype"":""text"",
    ""text"":
    {
         ""content"":""{1}""
    }
}";
        d = d.Replace("{0}", FromUserName).Replace("{1}", Message);
        string resMessage = HttpXmlPostRequest(postUrl, d, Encoding.UTF8);

    }

    /// <summary>
    /// unix时间转换为datetime
    /// </summary>
    /// <param name="timeStamp"></param>
    /// <returns></returns>
    private DateTime UnixTimeToTime(string timeStamp)
    {
        DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
        long lTime = long.Parse(timeStamp + "0000000");
        TimeSpan toNow = new TimeSpan(lTime);
        return dtStart.Add(toNow);
    }
    /// <summary>
    /// datetime转换为unixtime
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    public int ConvertDateTimeInt(DateTime time)
    {
        System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
        return (int)(time - startTime).TotalSeconds;
    }


    

    /// <summary>
    /// 获取商户标示 非前台页面调用
    /// </summary>
    public string CurrentToUserName
    {
        get
        {
            return mjuserid;
        }
    }
    

    /// <summary>
    /// 授权 snsapi_userinfo方式  需要用户点击授权  可获取用户详细信息
    /// </summary>
    /// <param name="appid">公众号APPID</param>
    /// <param name="redirect_uri">回调地址</param>
    public void OAuth(string appid, string redirect_uri)
    {
        redirect_uri = HttpUtility.UrlDecode(redirect_uri);
        if (appid != "")
        {
            string url = "https://open.weixin.qq.com/connect/oauth2/authorize?appid={1}&redirect_uri={2}&st=1&response_type=code&scope=snsapi_userinfo&state={3}&component_appid={0}#wechat_redirect";
            url = "https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri={1}&response_type=code&scope=snsapi_userinfo&state={2}#wechat_redirect";
            url = string.Format(url, appid, redirect_uri, System.Web.HttpContext.Current.Request.Params["id"] != null ? System.Web.HttpContext.Current.Request.Params["id"] : "0");
            System.Web.HttpContext.Current.Response.Redirect(url, false);
        }
        else
        {
            System.Web.HttpContext.Current.Response.Redirect("/wechat/500.jpg");
        }
    }


    public void OAuthDou(string redirect_uri)
    {
        //snsapi_base
        string url = "http://wechat.jahwa.com.cn/Herborist/GetUrl.aspx?url={0}&scope=snsapi_userinfo";
        url = string.Format(url, redirect_uri);
        WriteTxt(url);
        Response.Redirect(url, false);

    }

    /// <summary>
    /// 授权 snsapi_base方式  不需要用户点击授权  只可获取用户openid
    /// </summary>
    /// <param name="appid">公众号APPID</param>
    /// <param name="redirect_uri">回调地址</param>
    public void OAuth2(string appid, string redirect_uri)
    {
        redirect_uri = HttpUtility.UrlDecode(redirect_uri);
        if (appid != "")
        {
            string url = "https://open.weixin.qq.com/connect/oauth2/authorize?appid={1}&redirect_uri={2}&st=2&response_type=code&scope=snsapi_base&state={3}&component_appid={0}#wechat_redirect";
            url = "https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri={1}&response_type=code&scope=snsapi_base&state={2}#wechat_redirect";
            url = string.Format(url, appid, redirect_uri, System.Web.HttpContext.Current.Request.Params["id"] != null ? System.Web.HttpContext.Current.Request.Params["id"] : "0");
            System.Web.HttpContext.Current.Response.Redirect(url, false);
        }
        else
        {
            System.Web.HttpContext.Current.Response.Redirect("/wechat/500.jpg");
        }
    }




    public ORG_INFO GetNewWD
    {
        get
        {


            ORG_INFO WD = sbo.GetMerchants(mjuserid);
            System.Web.HttpContext.Current.Session["WD"] = WD;
            return WD;

            return new ORG_INFO { AppID = "" };

        }
    }




    MySmallShopService mss = new MySmallShopService();
    public string tousername = "";
    public string versions = "3";//css版本号

    /// <summary>
    /// 每次需要重新授权，获取最新的昵称和头像
    /// </summary>
    public void BaseLoad2()
    {
        if ((System.Web.HttpContext.Current.Session["ToUserName"] == null && System.Web.HttpContext.Current.Session["FromUserName"] == null) || (System.Web.HttpContext.Current.Session["FromUserName"] != null && mss.GetOAByDay(System.Web.HttpContext.Current.Session["FromUserName"].ToString()) == 0)) //如果当天没有授权过，则重新授权，获取最新头像
        {
            tousername = mjuserid;//WXLOG wlog = new WXLOG { TIME = DateTime.Now };
            if (System.Web.HttpContext.Current.Request.QueryString["code"] != null)
            {
                //mss.SaveLog(new WXLOG { CON = System.Web.HttpContext.Current.Request.QueryString["code"], TIME = DateTime.Now });
                try
                {
                    string url = "https://api.weixin.qq.com/sns/oauth2/access_token?appid=" + GetAppid(tousername) + "&secret=" + GetSecret(tousername) + "&code=" + (System.Web.HttpContext.Current.Request.QueryString["code"].ToString().Split(',').Length > 1 ? System.Web.HttpContext.Current.Request.QueryString["code"].ToString().Split(',')[1] : System.Web.HttpContext.Current.Request.QueryString["code"].ToString().Split(',')[0]) + "&grant_type=authorization_code";
                    string token = PostRequest(url);
                    // mss.SaveLog(new WXLOG { CON = token, TIME = DateTime.Now });
                    if (token.Contains("7200") || token.Contains("expires_in"))
                    {
                        OpenInfo autho = JsonConvert.DeserializeObject<OpenInfo>(token);
                        System.Web.HttpContext.Current.Session["FromUserName"] = autho.openid;
                        System.Web.HttpContext.Current.Session["ToUserName"] = tousername;
                        // mss.SaveLog(new WXLOG { CON = System.Web.HttpContext.Current.Session["ToUserName"].ToString(), TIME = DateTime.Now });
                        try
                        {
                            if (autho.scope == "snsapi_userinfo")
                            {
                                OAauth_Log oa = new OAauth_Log();
                                oa.CreateDate = DateTime.Now;
                                oa.FromUserName = autho.openid;
                                oa.ToUserName = tousername;
                                url = "https://api.weixin.qq.com/sns/userinfo?access_token=" + autho.access_token + "&openid=" + autho.openid + "&lang=zh_CN";
                                token = PostRequest(url);
                                autho = JsonConvert.DeserializeObject<OpenInfo>(token);
                                oa.headimgurl = autho.headimgurl;
                                oa.Nickname = autho.nickname;
                                oa.sex = autho.sex;
                                oa.country = autho.country;
                                oa.province = autho.province;
                                oa.city = autho.city;
                                OAauth_Log oa1 = mss.GetOA(autho.openid);
                                if (oa1 == null)
                                {
                                    DownHeadImage(oa);
                                    mss.SaveOAtuh(oa);
                                }
                                else
                                {
                                    oa1.headimgurl = oa.headimgurl;
                                    oa1.Nickname = oa.Nickname;
                                    oa1.sex = oa.sex;
                                    oa1.country = oa.country;
                                    oa1.province = oa.province;
                                    oa1.city = oa.city;
                                    oa1.CreateDate = DateTime.Now;
                                    if (!string.IsNullOrEmpty(oa1.DownPic))
                                        DeleteHeadImage(oa1.DownPic);
                                    DownHeadImage(oa1);
                                    mss.SaveOAtuh(oa1);
                                }
                            }
                            else
                            {
                                OAuth(GetNewWD.AppID, AbsoluteUri);
                            }

                        }
                        catch (Exception ex)
                        {
                            WXLOG log = new WXLOG();
                            log.CON = ex.Message.ToString();
                            log.TIME = DateTime.Now;
                            mss.SaveLog(log);
                        }
                    }
                    else
                    {
                        OAuth(GetNewWD.AppID, AbsoluteUri);
                    }
                }
                catch (Exception ex)
                {
                    mss.SaveLog(new WXLOG { CON = ex.Message + ex.StackTrace, TIME = DateTime.Now });
                }
                //System.Web.HttpContext.Current.Response.Write(" <input type='hidden'' value='" + AbsoluteUri + "' id='url' />");
            }
            else
            {
                OAuth(GetNewWD.AppID, AbsoluteUri);
            }
        }
        else
        {
            //System.Web.HttpContext.Current.Response.Write(" <input type='hidden'' value='" + AbsoluteUri + "' id='url' />");
        }
    }

    
    /// <summary>
    /// 只需授权一次
    /// </summary>
    public void BaseLoad()
    {
        if ((System.Web.HttpContext.Current.Session["ToUserName"] == null && System.Web.HttpContext.Current.Session["FromUserName"] == null) || (System.Web.HttpContext.Current.Session["FromUserName"] != null && mss.GetISOA(System.Web.HttpContext.Current.Session["FromUserName"].ToString()) == 0))
        {
            tousername = mjuserid;
            if (System.Web.HttpContext.Current.Request.QueryString["code"] != null)
            {
                try
                {
                    //string url = string.Format(@"https://api.weixin.qq.com/sns/oauth2/component/access_token?appid=" + WD.AppID + "&code=" + System.Web.HttpContext.Current.Request.QueryString["code"].ToString() + "&grant_type=authorization_code&component_appid=&component_access_token=");
                    string url = "https://api.weixin.qq.com/sns/oauth2/access_token?appid=" + GetAppid(tousername) + "&secret=" + GetSecret(tousername) + "&code=" + (System.Web.HttpContext.Current.Request.QueryString["code"].ToString().Split(',').Length > 1 ? System.Web.HttpContext.Current.Request.QueryString["code"].ToString().Split(',')[1] : System.Web.HttpContext.Current.Request.QueryString["code"].ToString().Split(',')[0]) + "&grant_type=authorization_code";
                    string token = PostRequest(url);
                    //  mss.SaveLog(new WXLOG { CON = token, TIME = DateTime.Now });
                    if (token.Contains("7200") || token.Contains("expires_in"))
                    {
                        OpenInfo autho = JsonConvert.DeserializeObject<OpenInfo>(token);
                        System.Web.HttpContext.Current.Session["FromUserName"] = autho.openid;
                        System.Web.HttpContext.Current.Session["ToUserName"] = tousername;
                        try
                        {
                            if (autho.scope == "snsapi_userinfo")
                            {
                                OAauth_Log oa = new OAauth_Log();
                                oa.CreateDate = DateTime.Now;
                                oa.FromUserName = autho.openid;
                                oa.ToUserName = tousername;
                                url = "https://api.weixin.qq.com/sns/userinfo?access_token=" + autho.access_token + "&openid=" + autho.openid + "&lang=zh_CN";
                                token = PostRequest(url);
                                autho = JsonConvert.DeserializeObject<OpenInfo>(token);
                                oa.headimgurl = autho.headimgurl;
                                oa.Nickname = autho.nickname;
                                oa.sex = autho.sex;
                                oa.country = autho.country;
                                oa.province = autho.province;
                                oa.city = autho.city;
                                OAauth_Log oa1 = mss.GetOA(autho.openid);
                                if (oa1 == null)
                                {
                                    DownHeadImage(oa);
                                    mss.SaveOAtuh(oa);
                                }
                                else
                                {
                                    oa1.headimgurl = oa.headimgurl;
                                    oa1.Nickname = oa.Nickname;
                                    oa1.sex = oa.sex;
                                    oa1.country = oa.country;
                                    oa1.province = oa.province;
                                    oa1.city = oa.city;
                                    oa1.CreateDate = DateTime.Now;
                                    if (!string.IsNullOrEmpty(oa1.DownPic))
                                        DeleteHeadImage(oa1.DownPic);
                                    DownHeadImage(oa1);
                                    mss.SaveOAtuh(oa1);
                                }
                            }
                            else
                            {
                                OAauth_Log oa = mss.GetOA(autho.openid);
                                if (oa == null)
                                {
                                    OAuth(GetNewWD.AppID, AbsoluteUri);
                                }
                                else if (oa.DownPic == null)
                                {
                                    OAuth(GetNewWD.AppID, AbsoluteUri);
                                }
                            }

                        }
                        catch (Exception ex)
                        {
                            WXLOG log = new WXLOG();
                            log.CON = ex.Message.ToString();
                            log.TIME = DateTime.Now;
                            mss.SaveLog(log);
                        }
                    }
                    else
                    {
                        OAuth(GetNewWD.AppID, AbsoluteUri);
                    }
                }
                catch (Exception)
                {

                }
                //System.Web.HttpContext.Current.Response.Write(" <input type='hidden'' value='" + AbsoluteUri + "' id='url' />");
            }
            else
            {
                OAuth2(GetNewWD.AppID, AbsoluteUri);
            }
        }
        else
        {
            //System.Web.HttpContext.Current.Response.Write(" <input type='hidden'' value='" + AbsoluteUri + "' id='url' />");
        }
    }

    /// <summary>
    /// 获取id,state参数
    /// </summary>
    /// <returns></returns>
    public string ParaState
    {
        get
        {
            if (System.Web.HttpContext.Current.Request.QueryString["id"] != null || (System.Web.HttpContext.Current.Request.QueryString["state"] != null && System.Web.HttpContext.Current.Request.QueryString["state"] != "0"))
            {
                return Server.UrlDecode(System.Web.HttpContext.Current.Request.QueryString["id"] == null ? System.Web.HttpContext.Current.Request.QueryString["state"] : System.Web.HttpContext.Current.Request.QueryString["id"]);
            }
            else
                return "";
        }
    }

    /// <summary>
    /// 删除过期头像
    /// </summary>
    /// <param name="url"></param>
    public void DeleteHeadImage(string url)
    {
        try
        {
            string path = Server.MapPath(url);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            path = Server.MapPath(url.Replace(".jpg", "Y.jpg"));
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
        catch (Exception)
        {

        }
    }

    /// <summary>
    /// 下载头像到本地
    /// </summary>
    /// <param name="oa1"></param>
    public string DownHeadImage(OAauth_Log oa1)
    {
        try
        {
            //下载头像到本地
            WebClient web = new WebClient();
            string loclurl = "~/wechat/HeadImage/";// +new Guid("d").ToString();
            string path = Server.MapPath(loclurl);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string guid = Guid.NewGuid().ToString("d");
            string path2 = path + guid + ".jpg";//缩略图
            path += guid + "Y.jpg";  //原图 缩略图后加个Y即为原图
            //WXLOG log = new WXLOG();
            //log.CON = path;
            //log.TIME = DateTime.Now;
            //mss.SaveLog(log);
            if (!string.IsNullOrEmpty(oa1.headimgurl))
                web.DownloadFile(oa1.headimgurl, path);
            MakeThumbnail(path, path2, 20, 20, true);//20 20
            oa1.DownPic = loclurl + guid + ".jpg";
            return oa1.DownPic;
        }
        catch (Exception ex)
        {
            WXLOG log = new WXLOG();
            log.CON = ex.Message.ToString() + "," + ex.StackTrace;
            log.TIME = DateTime.Now;
            mss.SaveLog(log);
            return null;
        }
    }

    /// <SUMMARY>
    /// 生成缩略图//带压缩图片不压缩22k压缩2k
    /// </SUMMARY>
    /// <PARAM name="originalImagePath" />原始路径
    /// <PARAM name="thumbnailPath" />生成缩略图路径
    /// <PARAM name="width" />缩略图的宽
    /// <PARAM name="height" />缩略图的高
    //是否压缩图片质量
    public void MakeThumbnail(string originalImagePath, string thumbnailPath, int width, int height, bool Ys, int imgagevalue = 50) //图片质量，默认50
    {
        //获取原始图片  
        System.Drawing.Image originalImage = System.Drawing.Image.FromFile(originalImagePath);
        //缩略图画布宽高 
        int towidth = 0;
        int toheight = 0;
        if (originalImage.Width > 1800) //图片像素大于1800，则缩放四倍
        {
            width = originalImage.Width / 4;
            height = originalImage.Height / 4;
            towidth = width;
            toheight = height;
        }
        else if (originalImage.Width > 400) //图片像素大于800，则缩放2倍
        {
            width = originalImage.Width / 4;
            height = originalImage.Height / 4;
            towidth = width;
            toheight = height;
        }
        else  //图片不是太大，则保持原大小
        {
            towidth = originalImage.Width; //width;
            toheight = originalImage.Height; //height;
            width = towidth;
            height = toheight;
        }
        //原始图片写入画布坐标和宽高(用来设置裁减溢出部分)  
        int x = 0;
        int y = 0;
        int ow = originalImage.Width;
        int oh = originalImage.Height;
        //原始图片画布,设置写入缩略图画布坐标和宽高(用来原始图片整体宽高缩放)  
        int bg_x = 0;
        int bg_y = 0;
        int bg_w = towidth;
        int bg_h = toheight;
        //倍数变量  
        double multiple = 0;
        //获取宽长的或是高长与缩略图的倍数  
        if (originalImage.Width >= originalImage.Height)
            multiple = (double)originalImage.Width / (double)width;
        else
            multiple = (double)originalImage.Height / (double)height;
        //上传的图片的宽和高小等于缩略图  
        if (ow <= width && oh <= height)
        {
            //缩略图按原始宽高  
            bg_w = originalImage.Width;
            bg_h = originalImage.Height;
            //空白部分用背景色填充  
            bg_x = Convert.ToInt32(((double)towidth - (double)ow) / 2);
            bg_y = Convert.ToInt32(((double)toheight - (double)oh) / 2);
        }
        //上传的图片的宽和高大于缩略图  
        else
        {
            //宽高按比例缩放  
            bg_w = Convert.ToInt32((double)originalImage.Width / multiple);
            bg_h = Convert.ToInt32((double)originalImage.Height / multiple);
            //空白部分用背景色填充  
            bg_y = Convert.ToInt32(((double)height - (double)bg_h) / 2);
            bg_x = Convert.ToInt32(((double)width - (double)bg_w) / 2);
        }
        //新建一个bmp图片,并设置缩略图大小.  
        System.Drawing.Image bitmap = new System.Drawing.Bitmap(towidth, toheight);
        //新建一个画板  
        System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap);
        //设置高质量插值法  
        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
        //设置高质量,低速度呈现平滑程度  
        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
        //清空画布并设置背景色  
        g.Clear(System.Drawing.ColorTranslator.FromHtml("#FFF"));
        //在指定位置并且按指定大小绘制原图片的指定部分  
        //第一个System.Drawing.Rectangle是原图片的画布坐标和宽高,第二个是原图片写在画布上的坐标和宽高,最后一个参数是指定数值单位为像素  
        g.DrawImage(originalImage, new System.Drawing.Rectangle(bg_x, bg_y, bg_w, bg_h), new System.Drawing.Rectangle(x, y, ow, oh), System.Drawing.GraphicsUnit.Pixel);

        if (Ys)
        {

            System.Drawing.Imaging.ImageCodecInfo encoder = GetEncoderInfo("image/jpeg");
            try
            {
                if (encoder != null)
                {
                    //if (File.Exists(thumbnailPath))
                    //{
                    //    originalImage.Dispose();
                    //    File.Delete(thumbnailPath);
                    //}
                    System.Drawing.Imaging.EncoderParameters encoderParams = new System.Drawing.Imaging.EncoderParameters(1);
                    //设置 jpeg 质量为 60
                    encoderParams.Param[0] = new System.Drawing.Imaging.EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (long)imgagevalue);
                    bitmap.Save(thumbnailPath, encoder, encoderParams);
                    encoderParams.Dispose();

                }
            }
            catch (System.Exception e)
            {
                mss.SaveLog(new WXLOG { CON = e.Message + e.StackTrace, TIME = DateTime.Now });
            }
            finally
            {
                originalImage.Dispose();
                bitmap.Dispose();
                g.Dispose();
            }

        }
        else
        {

            try
            {
                //获取图片类型  
                string fileExtension = System.IO.Path.GetExtension(originalImagePath).ToLower();
                //按原图片类型保存缩略图片,不按原格式图片会出现模糊,锯齿等问题.  
                switch (fileExtension)
                {
                    case ".gif": bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Gif); break;
                    case ".jpg": bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg); break;
                    case ".jpeg": bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg); break;
                    case ".bmp": bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Bmp); break;
                    case ".png": bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Png); break;
                    default: bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg); break;
                }
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                originalImage.Dispose();
                bitmap.Dispose();
                g.Dispose();
            }

        }

    }


    /// <SUMMARY>
    /// 生成缩略图//带压缩图片不压缩22k压缩2k
    /// </SUMMARY>
    /// <PARAM name="originalImagePath" />原始路径
    /// <PARAM name="thumbnailPath" />生成缩略图路径
    /// <PARAM name="width" />缩略图的宽
    /// <PARAM name="height" />缩略图的高
    //是否压缩图片质量
    public void MakeThumbnail2(string originalImagePath, string thumbnailPath, int width, int height, bool Ys, int imgagevalue = 50) //图片质量，默认50
    {
        //获取原始图片  
        System.Drawing.Image originalImage = System.Drawing.Image.FromFile(originalImagePath);
        //缩略图画布宽高 
        int towidth = width;
        int toheight = height;
        //原始图片写入画布坐标和宽高(用来设置裁减溢出部分)  
        int x = 0;
        int y = 0;
        int ow = originalImage.Width;
        int oh = originalImage.Height;
        //原始图片画布,设置写入缩略图画布坐标和宽高(用来原始图片整体宽高缩放)  
        int bg_x = 0;
        int bg_y = 0;
        int bg_w = towidth;
        int bg_h = toheight;
        //倍数变量  
        double multiple = 0;
        //获取宽长的或是高长与缩略图的倍数  
        if (originalImage.Width >= originalImage.Height)
            multiple = (double)originalImage.Width / (double)width;
        else
            multiple = (double)originalImage.Height / (double)height;
        //上传的图片的宽和高小等于缩略图  
        if (ow <= width && oh <= height)
        {
            //缩略图按原始宽高  
            bg_w = originalImage.Width;
            bg_h = originalImage.Height;
            //空白部分用背景色填充  
            bg_x = Convert.ToInt32(((double)towidth - (double)ow) / 2);
            bg_y = Convert.ToInt32(((double)toheight - (double)oh) / 2);
        }
        //上传的图片的宽和高大于缩略图  
        else
        {
            //宽高按比例缩放  
            bg_w = Convert.ToInt32((double)originalImage.Width / multiple);
            bg_h = Convert.ToInt32((double)originalImage.Height / multiple);
            //空白部分用背景色填充  
            bg_y = Convert.ToInt32(((double)height - (double)bg_h) / 2);
            bg_x = Convert.ToInt32(((double)width - (double)bg_w) / 2);
        }
        //新建一个bmp图片,并设置缩略图大小.  
        System.Drawing.Image bitmap = new System.Drawing.Bitmap(towidth, toheight);
        //新建一个画板  
        System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap);
        //设置高质量插值法  
        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
        //设置高质量,低速度呈现平滑程度  
        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
        //清空画布并设置背景色  
        g.Clear(System.Drawing.ColorTranslator.FromHtml("#FFF"));
        //在指定位置并且按指定大小绘制原图片的指定部分  
        //第一个System.Drawing.Rectangle是原图片的画布坐标和宽高,第二个是原图片写在画布上的坐标和宽高,最后一个参数是指定数值单位为像素  
        g.DrawImage(originalImage, new System.Drawing.Rectangle(bg_x, bg_y, bg_w, bg_h), new System.Drawing.Rectangle(x, y, ow, oh), System.Drawing.GraphicsUnit.Pixel);

        if (Ys)
        {

            System.Drawing.Imaging.ImageCodecInfo encoder = GetEncoderInfo("image/jpeg");
            try
            {
                if (encoder != null)
                {
                    //if (File.Exists(thumbnailPath))
                    //{
                    //    originalImage.Dispose();
                    //    File.Delete(thumbnailPath);
                    //}
                    System.Drawing.Imaging.EncoderParameters encoderParams = new System.Drawing.Imaging.EncoderParameters(1);
                    //设置 jpeg 质量为 60
                    encoderParams.Param[0] = new System.Drawing.Imaging.EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (long)imgagevalue);
                    bitmap.Save(thumbnailPath, encoder, encoderParams);
                    encoderParams.Dispose();

                }
            }
            catch (System.Exception e)
            {
                mss.SaveLog(new WXLOG { CON = e.Message + e.StackTrace, TIME = DateTime.Now });
            }
            finally
            {
                originalImage.Dispose();
                bitmap.Dispose();
                g.Dispose();
            }

        }
        else
        {

            try
            {
                //获取图片类型  
                string fileExtension = System.IO.Path.GetExtension(originalImagePath).ToLower();
                //按原图片类型保存缩略图片,不按原格式图片会出现模糊,锯齿等问题.  
                switch (fileExtension)
                {
                    case ".gif": bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Gif); break;
                    case ".jpg": bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg); break;
                    case ".jpeg": bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg); break;
                    case ".bmp": bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Bmp); break;
                    case ".png": bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Png); break;
                    default: bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg); break;
                }
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                originalImage.Dispose();
                bitmap.Dispose();
                g.Dispose();
            }

        }

    }

    private System.Drawing.Imaging.ImageCodecInfo GetEncoderInfo(string mimeType)
    {
        //根据 mime 类型，返回编码器
        System.Drawing.Imaging.ImageCodecInfo result = null;
        System.Drawing.Imaging.ImageCodecInfo[] encoders = System.Drawing.Imaging.ImageCodecInfo.GetImageEncoders();
        for (int i = 0; i < encoders.Length; i++)
        {
            if (encoders[i].MimeType == mimeType)
            {
                result = encoders[i];
                break;
            }

        }
        return result;

    }



    /// <summary>
    /// 裁剪图片  
    /// </summary>
    /// <param name="oldPath">原路径</param>
    /// <param name="imagetype">图片类型 1宽高比2:1的扁长图片  2正方形 默认为第一种</param>
    /// <returns>新路径</returns>
    public string CutImage(string oldPath, int imagevalue, int imagetype = 1, bool ys = true) //默认压缩
    {

        try
        {


            //加载图片
            System.Drawing.Image img = System.Drawing.Image.FromStream(new System.IO.MemoryStream(System.IO.File.ReadAllBytes(oldPath)));
            //设置截取的坐标和大小
            int x = 0, y = 0, width = 360, height = 300;  //坐标以左上角为原点
            if (imagetype == 1)  //高度的两倍大于宽度就需要截
            {
                if (img.Width >= img.Height * 1.63 && img.Width <= img.Height * 1.69) //尺寸差不多，就不裁剪
                {
                    return oldPath;
                }
                if (img.Width >= img.Height * 1.66) //宽度大于高度的1.66倍
                {
                    height = img.Height;
                    width = Convert.ToInt32(img.Height * 1.66);  //宽度等于高度的1.66倍
                    x = (img.Width - width) / 2;
                }
                else
                {
                    width = img.Width; //宽度不变
                    height = Convert.ToInt32(img.Width / 1.66); //高度为宽度一半   宽比高1.66
                    y = (img.Height - height) / 2; //纵坐标设置为原高度减去新高度剩余部分的一半
                }
            }
            else if (imagetype == 2 && img.Height != img.Width)//不是正方形
            {
                if (img.Height > img.Width) //高度大于宽度，以宽度为边长
                {
                    width = img.Width;
                    height = img.Width;
                    y = (img.Height - img.Width) / 2;
                }
                else //以高度为边长
                {
                    width = img.Height;
                    height = img.Height;
                    x = (img.Width - img.Height) / 2;
                }
            }
            else
            {
                return oldPath;
            }
            string guid1 = Guid.NewGuid().ToString("d");
            string guid2 = Guid.NewGuid().ToString("d");

            //新图片路径
            String newPath = System.IO.Path.GetExtension(oldPath);

            string path3 = oldPath.Substring(0, oldPath.Length - newPath.Length) + guid1 + newPath; //第三个地址，方便压缩

            //计算新的文件名，在旧文件名后加_new
            newPath = oldPath.Substring(0, oldPath.Length - newPath.Length) + guid2 + newPath;
            //Response.Write(oldPath);
            //Response.Write("<br>");
            //Response.Write(newPath);
            //定义截取矩形
            System.Drawing.Rectangle cropArea = new System.Drawing.Rectangle(x, y, width, height); //要截取的区域大小

            //判断超出的位置否
            if ((img.Width < x + width) || img.Height < y + height)
            {
                //Response.Write("截取的区域超过了图片本身的高度、宽度.");
                img.Dispose();
                return "";
            }
            //定义Bitmap对象
            System.Drawing.Bitmap bmpImage = new System.Drawing.Bitmap(img);

            //进行裁剪
            System.Drawing.Bitmap bmpCrop = bmpImage.Clone(cropArea, bmpImage.PixelFormat);

            //保存成新文件
            bmpCrop.Save(newPath);

            //释放对象
            img.Dispose();
            bmpCrop.Dispose();
            try
            {
                if (ys)
                {
                    MakeThumbnail(newPath, path3, 20, 20, true, imagevalue);//压缩图片
                    File.Delete(newPath);
                    return path3;
                }
            }
            catch (Exception ex)
            {
                mss.SaveLog(new WXLOG { CON = ex.Message + ex.StackTrace, TIME = DateTime.Now });
            }
            return newPath;
        }
        catch (Exception ex)
        {
            mss.SaveLog(new WXLOG { CON = ex.Message + ex.StackTrace, TIME = DateTime.Now });
        }
        return oldPath;
    }


    /// <summary>
    /// 剪切正方形图片，并返回新地址  1宽高比2:1的扁长图片  2正方形
    /// </summary>
    /// <param name="img">图片相对路径</param>
    /// <param name="imagetype">图片剪切类型 1宽高比2:1的扁长图片  2正方形</param>
    /// <param name="simg">原有已剪切图片地址 用于删除旧图片</param>
    /// <param name="ys">是否压缩 默认压缩</param>
    /// <param name="sy">是否为原图加水印</param>
    /// <returns></returns>
    public string CutImage2(string img, int imagetype, string simg, bool ys = true, bool sy = true)
    {
        //if (!string.IsNullOrEmpty(img))
        //{
        //    int dex = img.LastIndexOf('/');
        //    if (dex > 0)
        //    {
        //        dex++;
        //        if (IsNum(img.Substring(dex, img.Length - dex))) //如果是数字
        //        {
        //            int fid = int.Parse(img.Substring(dex, img.Length - dex));
        //            if (fid > 0)
        //            {
        //                CommonService _mservice = new CommonService();
        //                FILES f = _mservice.GetUploadFile(fid);
        //                if (f != null)
        //                {
        //                    string newpath = CutImage(f.FILE_NAME, 100, imagetype, ys);
        //                    try
        //                    {
        //                        if (sy)  //是否压缩
        //                        {
        //                            ImageWriter iw = new ImageWriter();
        //                            iw.SaveWatermark(f.FILE_NAME, System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "assets\\images\\shuiyin.png", System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "assets\\images\\shuiyin2.png", System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "assets\\images\\shuiyin3.png", 0.3f, ImageWriter.WatermarkPosition.Center, 10, f.FILE_NAME);
        //                        }
        //                    }
        //                    catch (Exception)
        //                    {

        //                    }
        //                    if (newpath == f.FILE_NAME || newpath == "")
        //                    {
        //                        //if (string.IsNullOrEmpty(img2))
        //                        return img;
        //                        //else
        //                        //    return img2;
        //                    }
        //                    else
        //                    {
        //                        //try
        //                        //{
        //                        //    if (!string.IsNullOrEmpty(simg))  //删除原来的图片
        //                        //    {
        //                        //        dex = simg.LastIndexOf('/');
        //                        //        if (dex > 0)
        //                        //        {
        //                        //            dex++;
        //                        //            if (IsNum(simg.Substring(dex, simg.Length - dex))) //如果是数字
        //                        //            { 
        //                        //                fid = int.Parse(simg.Substring(dex, simg.Length - dex));
        //                        //                if (fid > 0)
        //                        //                {
        //                        //                    FILES sf = _mservice.GetUploadFile(fid);
        //                        //                    if (sf != null)
        //                        //                    {
        //                        //                        if (File.Exists(sf.FILE_NAME))
        //                        //                        {
        //                        //                            File.Delete(sf.FILE_NAME);
        //                        //                        }
        //                        //                    }

        //                        //                }

        //                        //            }

        //                        //        }
        //                        //    }
        //                        //}
        //                        //catch (Exception)
        //                        //{

        //                        //}
        //                        f.FILE_NAME = newpath;
        //                        // f.FILE_URL=
        //                        f.ID = 0;
        //                        f.ID = _mservice.SaveFlies(f);
        //                        f.FILE_URL = "/home/ViewImage.do/" + f.ID;
        //                        _mservice.SaveFlies(f);
        //                        return f.FILE_URL;
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}
        //return img;
        return "";
    }

    /// <summary>
    /// 添加水印
    /// </summary>
    /// <param name="img"></param>
    public void ImageAddShuiYin(string img)
    {
        //if (!string.IsNullOrEmpty(img))
        //{
        //    int dex = img.LastIndexOf('/');
        //    if (dex > 0)
        //    {
        //        dex++;
        //        if (IsNum(img.Substring(dex, img.Length - dex))) //如果是数字
        //        {
        //            int fid = int.Parse(img.Substring(dex, img.Length - dex));
        //            if (fid > 0)
        //            {
        //                CommonService _mservice = new CommonService();
        //                FILES f = _mservice.GetUploadFile(fid);
        //                if (f != null)
        //                {
        //                    try
        //                    {
        //                        ImageWriter iw = new ImageWriter();
        //                        iw.SaveWatermark(f.FILE_NAME, Server.MapPath("~/assets/images/shuiyin.png"), Server.MapPath("~/assets/images/shuiyin2.png"), Server.MapPath("~/assets/images/shuiyin3.png"), 0.3f, ImageWriter.WatermarkPosition.Center, 10, f.FILE_NAME);
        //                    }
        //                    catch (Exception)
        //                    {

        //                    }

        //                }
        //            }
        //        }
        //    }
        //}

    }


    public class access_token
    {
        public string authorizer_access_token { get; set; }
        public string authorizer_refresh_token { get; set; }
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
    /// 获取图片的跟路径
    /// </summary>
    /// <returns></returns>
    public string TokenUrl
    {
        get { return ConfigurationSettings.AppSettings["TokenUrl"]; }
    }

    /// <summary>
    /// 获取图片的跟路径
    /// </summary>
    /// <returns></returns>
    public string WebUrl
    {
        get { return ConfigurationSettings.AppSettings["WebUrl"]; }
    }

    /// <summary>
    /// 获取POS系统图片的跟路径
    /// </summary>
    /// <returns></returns>
    public string PosUrl
    {
        get { return ConfigurationSettings.AppSettings["POS_WebApi"]; }
    }

    public string RegisterUrl
    {
        get { return ConfigurationSettings.AppSettings["RegisterUrl"]; }
    }

    //活动状态变更消息模版
    public string ActiveStateChangeTmpId
    {
        get { return ConfigurationSettings.AppSettings["ActiveStateChangeTmpId"]; }
    }

    //中奖通知
    public string WinningNotificationTmpId
    {
        get { return ConfigurationSettings.AppSettings["WinningNotificationTmpId"]; }
    }

    //活动截止日期
    public string ActiveEndDate
    {
        get { return ConfigurationSettings.AppSettings["ActiveEndDate"]; }
    }

    //邀请有礼活动菜单id  生成海报
    public string InviteMenuId
    {
        get { return ConfigurationSettings.AppSettings["InviteMenuId"]; }
    }

    ///// <summary>
    ///// 微信号
    ///// </summary>
    //public string Openid
    //{
    //    get
    //    {
    //        if ((System.Web.HttpContext.Current.Request.QueryString["FromUserName"] != null || System.Web.HttpContext.Current.Session["FromUserName"] != null) && (System.Web.HttpContext.Current.Request.QueryString["ToUserName"] != null || System.Web.HttpContext.Current.Session["ToUserName"] != null))
    //        {
    //            string user = System.Web.HttpContext.Current.Request.QueryString["FromUserName"] == null ? System.Web.HttpContext.Current.Session["FromUserName"].ToString() : System.Web.HttpContext.Current.Request.QueryString["FromUserName"].ToString();
    //            string user2 = System.Web.HttpContext.Current.Request.QueryString["ToUserName"] == null ? System.Web.HttpContext.Current.Session["ToUserName"].ToString() : System.Web.HttpContext.Current.Request.QueryString["ToUserName"].ToString();
    //            return user;
    //        }
    //        else
    //            return "";
    //    }
    //}

  

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

    /// <summary>
    /// 官方美街微店微信账号
    /// </summary>
    /// <returns></returns>
    public string mjuserid {
        get { return ConfigurationSettings.AppSettings["OriginalID"]; }

    }

    /// <summary>
    /// 一个粉丝openid
    /// </summary>
    public string oneopenid {
        get { return ConfigurationSettings.AppSettings["oneopenid"]; }
    }

    /// <summary>
    /// 是否是测试
    /// </summary>
    public bool IsTest { get; set; }

    /// <summary>
    /// 获取微店信息
    /// </summary>
    /// <param name="ToUserName"></param>
    /// <returns></returns>
    public ORG_INFO GetWD(string ToUserName)
    {
        m = sbo.GetMerchantsByToUserName(ToUserName);
        if (m == null)
            return null;
        return m;
    }

    /// <summary>
    /// 格式化发布时间
    /// </summary>
    /// <param name="dt"></param>
    /// <returns></returns>
    public static string DateFormatToString(DateTime dt)
    {
        TimeSpan span = (DateTime.Now - dt).Duration();
        if (span.TotalDays > 60)
        {
            return dt.ToString("yyyy-MM-dd");
        }
        else if (span.TotalDays > 30)
        {
            return "1个月前";
        }
        else if (span.TotalDays > 14)
        {
            return "2周前";
        }
        else if (span.TotalDays > 7)
        {
            return "1周前";
        }
        else if (span.TotalDays > 1)
        {
            return string.Format("{0}天前", (int)Math.Floor(span.TotalDays));
        }
        else if (span.TotalHours > 1)
        {
            return string.Format("{0}小时前", (int)Math.Floor(span.TotalHours));
        }
        else if (span.TotalMinutes > 1)
        {
            return string.Format("{0}分钟前", (int)Math.Floor(span.TotalMinutes));
        }
        else if (span.TotalSeconds >= 1)
        {
            return string.Format("{0}秒前", (int)Math.Floor(span.TotalSeconds));
        }
        else
        {
            return "1秒前";
        }
    }

    /// <summary>
    /// 格式化狗狗年龄
    /// </summary>
    /// <param name="mon"></param>
    /// <returns></returns>
    public string GetSJ(int mon)
    {
        if (mon < 12)
            return mon + "个月";
        if (mon % 12 == 0)
            return mon / 12 + "岁";
        else
            return mon / 12 + "岁" + mon % 12 + "个月";
        return "";
    }

    /// <summary>
    /// 上传文件到指定url
    /// </summary>
    /// <param name="url"></param>
    /// <param name="paramName"></param>
    /// <param name="contentType"></param>
    /// <param name="nvc"></param>
    /// <param name="fileList"></param>
    /// <returns></returns>
    public static string HttpUploadFile(string url, string paramName, string contentType, NameValueCollection nvc, List<string> fileList)
    {
        string result = string.Empty;
        string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
        byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

        HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(url);
        wr.ContentType = "multipart/form-data; boundary=" + boundary;
        wr.Method = "POST";
        wr.KeepAlive = true;
        wr.Credentials = System.Net.CredentialCache.DefaultCredentials;
        

        Stream rs = wr.GetRequestStream();

        string formdataTemplate = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";
        foreach (string key in nvc.Keys)
        {
            rs.Write(boundarybytes, 0, boundarybytes.Length);
            string formitem = string.Format(formdataTemplate, key, nvc[key]);
            byte[] formitembytes = System.Text.Encoding.UTF8.GetBytes(formitem);
            rs.Write(formitembytes, 0, formitembytes.Length);
        }


        foreach (var file in fileList)
        {
 
            FileStream fileStream = new FileStream(file, FileMode.Open, FileAccess.Read);
            rs.Write(boundarybytes, 0, boundarybytes.Length);
            string headerTemplate = "Content-Disposition: form-data;id=\"media\";filelength=\"{3}\";name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n";
            string header = string.Format(headerTemplate, "media", file, contentType,fileStream.Length);
            byte[] headerbytes = System.Text.Encoding.UTF8.GetBytes(header);
            rs.Write(headerbytes, 0, headerbytes.Length);

           
            
            byte[] buffer = new byte[4096];
            int bytesRead = 0;
            while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
            {
                rs.Write(buffer, 0, bytesRead);
            }
            //fileStream.Close();
        }

        byte[] trailer = System.Text.Encoding.UTF8.GetBytes("\r\n--" + boundary + "--\r\n");
        rs.Write(trailer, 0, trailer.Length);







        rs.Close();

        WebResponse wresp = null;
        try
        {
            wresp = wr.GetResponse();
            Stream stream2 = wresp.GetResponseStream();
            StreamReader reader2 = new StreamReader(stream2);

            result = reader2.ReadToEnd();
        }
        catch (Exception ex)
        {
            if (wresp != null)
            {
                wresp.Close();
                wresp = null;
            }
        }
        finally
        {
            wr = null;
        }

        return result;
    }

    /// <summary>  
    /// 获取远程访问用户的Ip地址  
    /// </summary>  
    /// <returns>返回Ip地址</returns>  
    public string GetLoginIp()
    {
        string result = String.Empty;

        result = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

        // 如果使用代理，获取真实IP 
        if (result != null && result.IndexOf(".") == -1)    //没有“.”肯定是非IPv4格式 
            result = null;
        else if (result != null)
        {
            if (result.IndexOf(",") != -1)
            {
                //有“,”，估计多个代理。取第一个不是内网的IP。 
                result = result.Replace(" ", "").Replace("'", "");
                string[] temparyip = result.Split(",;".ToCharArray());
                for (int i = 0; i < temparyip.Length; i++)
                {
                    if (IsIPAddress(temparyip[i])
                        && temparyip[i].Substring(0, 3) != "10."
                        && temparyip[i].Substring(0, 7) != "192.168"
                        && temparyip[i].Substring(0, 7) != "172.16.")
                    {
                        return temparyip[i];    //找到不是内网的地址 
                    }
                }
            }
            else if (IsIPAddress(result)) //代理即是IP格式 
                return result;
            else
                result = null;    //代理中的内容 非IP，取IP 
        }
        if (null == result || result == String.Empty)
            result = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

        if (result == null || result == String.Empty)
            result = System.Web.HttpContext.Current.Request.UserHostAddress;

        return result;
    }


    /// <summary>
    /// 判断是否是IP地址格式 0.0.0.0
    /// </summary>
    /// <param name="str1">待判断的IP地址</param>
    /// <returns>true or false</returns>
    private static bool IsIPAddress(string str1)
    {
        if (str1 == null || str1 == string.Empty || str1.Length < 7 || str1.Length > 15) return false;

        string regformat = @"^\d{1,3}[\.]\d{1,3}[\.]\d{1,3}[\.]\d{1,3}$";

        Regex regex = new Regex(regformat, RegexOptions.IgnoreCase);
        return regex.IsMatch(str1);
    }

    /// <summary>
    /// MD5加密
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public string getMD5(string source)
    {
        string result = "";
        try
        {
            MD5 getmd5 = new MD5CryptoServiceProvider();
            byte[] targetStr = getmd5.ComputeHash(UnicodeEncoding.UTF8.GetBytes(source));
            result = BitConverter.ToString(targetStr).Replace("-", "");
            return result;
        }
        catch (Exception)
        {
            return "0";
        }

    }

    public string GetMD5(string s, string _input_charset)
    {
        MD5 md5 = new MD5CryptoServiceProvider();
        byte[] t = md5.ComputeHash(Encoding.GetEncoding(_input_charset).GetBytes(s));
        StringBuilder sb = new StringBuilder(32);
        for (int i = 0; i < t.Length; i++)
        {
            sb.Append(t[i].ToString("x").PadLeft(2, '0'));
        }
        return sb.ToString();
    }

    /// <summary>
    /// OrgID
    /// </summary>
    public int OrgID
    {
        get { return 4; }
    }


    public void BL()
    {
        ////如果是测试

        Session["FromUserName"] = "odvypuPcfjrS4O-fnmrMBd9HnNU4";
        Session["ToUserName"] = mjuserid;


        //if (IsTest) //如果是测试
        //{
        //Session["FromUserName"] = oneopenid;
        //Session["ToUserName"] = mjuserid;
        if ((Request.QueryString["FromUserName"] != null || Session["FromUserName"] != null) && (Request.QueryString["ToUserName"] != null || Session["ToUserName"] != null))
        {
            string user = Request.QueryString["FromUserName"] == null ? Session["FromUserName"].ToString() : Request.QueryString["FromUserName"].ToString();
            string user2 = Request.QueryString["ToUserName"] == null ? Session["ToUserName"].ToString() : Request.QueryString["ToUserName"].ToString();
            Session["FromUserName"] = user;
            Session["ToUserName"] = user2;
        }
        //}
        if (Session["FromUserName"] == null && Session["ToUserName"] == null)
        {
            HttpCookie cookie = Request.Cookies["cookiedtmoon"];
            if (cookie == null)
            {
                cookie = new HttpCookie("cookiedtmoon");
                cookie.Expires = DateTime.Now.AddDays(3);
                cookie.Name = "cookiedtmoon";
                cookie.Value = DateTime.Now.AddDays(3).ToString("yyyy.MM.dd");
                HttpContext.Current.Response.Cookies.Add(cookie);
                BaseLoad2();
            }
            else
            {
                DateTime dt = DateTime.Parse(cookie.Value);
                if (dt < DateTime.Now)
                {
                    cookie.Value = DateTime.Now.AddDays(3).ToString("yyyy.MM.dd");
                    cookie.Expires = DateTime.Now.AddDays(3);
                    HttpContext.Current.Response.Cookies.Add(cookie);
                    BaseLoad2();
                }
                else
                {
                    BaseLoad();
                }
            }
        }
        Page.RegisterStartupScript("hiddenurl", "<input type=\"hidden\" value=\"" + Server.UrlEncode(AbsoluteUri) + "\" id=\"url\" />");
        //Response.Write("<input type=\"hidden\" value=\"" + Server.UrlEncode(AbsoluteUri) + "\" id=\"url\" />");
    }

    public string OpenID
    {
        get
        {
            //WriteTxt("Request.QueryString['FromUserName']" + Request.QueryString["FromUserName"]);
            //WriteTxt("Session['FromUserName']" + Session["FromUserName"]);

            //WriteTxt("Request.QueryString['ToUserName']" + Request.QueryString["ToUserName"]);
            //WriteTxt("Session['ToUserName']" + Session["ToUserName"]);
            if ((Request.QueryString["FromUserName"] != null || Session["FromUserName"] != null) && (Request.QueryString["ToUserName"] != null || Session["ToUserName"] != null))
            {
                string user = Request.QueryString["FromUserName"] == null ? Session["FromUserName"].ToString() : Request.QueryString["FromUserName"].ToString();
                string user2 = Request.QueryString["ToUserName"] == null ? Session["ToUserName"].ToString() : Request.QueryString["ToUserName"].ToString();
                Session["FromUserName"] = user;
                Session["ToUserName"] = user2;
                return user;
            }
            else return null;
        }
    }

    /// <summary>
    /// 生成随机数
    /// </summary>
    /// <param name="randtype">1，数字及大小写字母；2，数字</param>
    /// <param name="pwdlen">随机数长度</param>
    /// <returns></returns>
    public  string RandNum(int randtype, int pwdlen)
    {
        string pwdchars1 = "abcdefghijklmnopqrstuvwxyz0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string pwdchars2 = "0123456789";
        string pwdchars="";
        if (randtype == 1)
            pwdchars = pwdchars1;
        else
            pwdchars = pwdchars2;
        string tmpstr = "";
        int iRandNum;
        Random rnd = new Random();
        for (int i = 0; i < pwdlen; i++)
        {
            iRandNum = rnd.Next(pwdchars.Length);
            tmpstr += pwdchars[iRandNum];
        }
        return tmpstr;
    }



    #region 系统方法

    /// <summary>
    /// 判断是否为数字
    /// </summary>
    /// <param name="Str"></param>
    /// <returns></returns>
    public bool IsNum(string Str)
    {
        bool bl = false;
        string Rx = @"^[1-9]\d*$";
        if (Regex.IsMatch(Str, Rx))
        {
            bl = true;
        }
        else
        {
            bl = false;
        }
        return bl;
    }

    /// <summary>
    /// 替换关键字
    /// </summary>
    /// <param name="remark"></param>
    /// <returns></returns>
    public string ReplaceKeyWord(string remark)
    {
        remark = remark ?? "";
        remark = remark.Trim().Replace(" ", "");
        string[] key = new[] { "这种人", "他妈的", "TMD", "tmd", "狗日", "傻逼", "sb", "SB", "搅屎棍", "搅屎", "人渣", "梅毒", "艾滋病", "逼", "娘们", "骗", "草", "操", "艹", "cao", "滚", "我日", "不要脸", "傻B" };
        foreach (string k in key)
        {
            remark = remark.Replace(k, "*");
        }
        return remark;
    }

    /// <summary>
    /// 获取站点根路径
    /// </summary>
    /// <returns></returns>
    protected string GetSitePath()
    {
        string root = Page.Request.ApplicationPath;
        if (root == "/") return root;
        else return root + "/";
    }


    protected override void CreateChildControls()
    {
        base.CreateChildControls();

        string root = this.GetSitePath();

        //Page.RegisterClientScriptBlock("main_style_file", "<link rel='stylesheet' href='" + root + "Skin/Main.css'/>");

        //Page.RegisterClientScriptBlock("validate-js", "<script language='javascript' src='" + root + "assets/app/baidu/BaiDuTongJi.js'></script>");

        //Page.RegisterClientScriptBlock("validate-js", "<script type=\"text/javascript\">var cnzz_protocol = ((\"https:\" == document.location.protocol) ? \" https://\" : \" http://\");document.write(unescape(\"%3Cspan id='cnzz_stat_icon_1256988976'%3E%3C/span%3E%3Cscript src='\" + cnzz_protocol + \"s4.cnzz.com/z_stat.php%3Fid%3D1256988976' type='text/javascript'%3E%3C/script%3E\"));</script>");

        //Page.RegisterClientScriptBlock("common-js", "<script language='javascript' src='" + root + "Common/Js/Common.js'></script>");

        //Page.RegisterClientScriptBlock("common-Progress", "<script language='javascript' src='" + root + "Common/Js/Progress.js'></script>");

        //Page.RegisterClientScriptBlock("datepicker-js", "<script language='javascript' src='" + root + "Common/Js/DatePicker.js'></script>");
    }

    //protected void ReturnMessage(string msg)
    //{
    //    //Exception ex = Server.GetLastError();

    //    //string root = this.GetSitePath();
    //    //string css = "<link rel='stylesheet' href='" + root + "Skin/Main.css'/>";
    //    Response.Clear();
    //    //Response.Write(css);
    //    //Response.Write( "<br><br><h3><font color='red'>系统运行发生错误!请联系管理员!</font></h3><br>错误信息:" );
    //    //Response.Write( "<hr size='1' color='red'><b>" + msg + "</b><hr size='1' color='red'>" );
    //    //Response.Write(  ex.StackTrace );

    //    //出错信息提示样式
    //    string strHtml = "<body  bgcolor='#CCCCCC'><br><br><br><br><br><br>";
    //    strHtml += "<div align='center'>";
    //    strHtml += "  <table width='80%' border='1' cellspacing='0' bordercolorlight='000000' bordercolordark='FFFFFF' bgcolor='E0E0E0'>";
    //    strHtml += "    <tr>";
    //    strHtml += "      <td><table border='0' bgcolor='#0066CC' cellspacing='0' cellpadding='2'  height='35'  width='100%'>";
    //    strHtml += "          <tr>";
    //    strHtml += "            <td align='center'><font color='FFFFFF' size='3'><b>System messages</b></font></td>";
    //    strHtml += "          </tr>";
    //    strHtml += "          </table>";
    //    strHtml += "          <table border='0' width='100%' cellpadding='4'>";
    //    strHtml += "            <tr>";
    //    //strHtml += "              <td width='59' align='center' valign='top'><img src='" + root + "images/28.png'/></td>";
    //    strHtml += "              <td colspan='2' align='center'><font size='2'><b>" + msg + "</b></font></td>";
    //    strHtml += "            </tr>";
    //    strHtml += "            <tr>";
    //    strHtml += "              <td colspan='2' align='center' valign='top'><input type='button' name='ok' class='formButton' value='　Return　' onclick='javascript:history.back()' />";
    //    strHtml += "              </td>";
    //    strHtml += "            </tr>";
    //    strHtml += "        </table></td>";
    //    strHtml += "    </tr>";
    //    strHtml += "  </table>";
    //    strHtml += "</div></body>";

    //    Response.Write(strHtml);
    //    Response.End();
    //}
    #endregion


}

/// <summary>
/// 扩展类
/// </summary>
public static class kz
{
    public static int IsNullOrZero(this int? a)
    {
        if (a == null)
            return 0;
        else return a.Value;
    }

    public static string GetUploadVideoResult(string accessToken, string filePath, string title, string introduction)
    {
        var url = string.Format("https://api.weixin.qq.com/cgi-bin/material/add_material?access_token={0}", accessToken);
        var fileDictionary = new Dictionary<string, string>();
        fileDictionary["media"] = filePath;
        fileDictionary["description"] = string.Format("{{\"title\":\"{0}\", \"introduction\":\"{1}\"}}", title, introduction);

        string returnText = string.Empty;
        Dictionary<string, string> postDataDictionary = null;
        using (MemoryStream ms = new MemoryStream())
        {
            //postDataDictionary.Add(ms); //填充formData  
            returnText = HttpPost(url, null, ms, fileDictionary, null, null, 1200000);
        }
        return returnText;
    }

    /// <summary>  
    /// 填充表单信息的Stream  
    /// </summary>  
    /// <param name="formData"></param>  
    /// <param name="stream"></param>  
    public static void FillFormDataStream(this Dictionary<string, string> formData, Stream stream)
    {
        string dataString = GetQueryString(formData);
        var formDataBytes = formData == null ? new byte[0] : Encoding.UTF8.GetBytes(dataString);
        stream.Write(formDataBytes, 0, formDataBytes.Length);
        stream.Seek(0, SeekOrigin.Begin);//设置指针读取位置  
    }

    /// <summary>  
    /// 组装QueryString的方法  
    /// 参数之间用&连接，首位没有符号，如：a=1&b=2&c=3  
    /// </summary>  
    /// <param name="formData"></param>  
    /// <returns></returns>  
    public static string GetQueryString(this Dictionary<string, string> formData)
    {
        if (formData == null || formData.Count == 0)
        {
            return "";
        }

        StringBuilder sb = new StringBuilder();

        var i = 0;
        foreach (var kv in formData)
        {
            i++;
            sb.AppendFormat("{0}={1}", kv.Key, kv.Value);
            if (i < formData.Count)
            {
                sb.Append("&");
            }
        }

        return sb.ToString();
    }

    /// <summary>  
    /// 根据完整文件路径获取FileStream  
    /// </summary>  
    /// <param name="fileName"></param>  
    /// <returns></returns>  
    public static FileStream GetFileStream(string fileName)
    {
        FileStream fileStream = null;
        if (!string.IsNullOrEmpty(fileName) && File.Exists(fileName))
        {
            fileStream = new FileStream(fileName, FileMode.Open);
        }
        return fileStream;
    }

    /// <summary>  
    /// 使用Post方法获取字符串结果  
    /// </summary>  
    /// <param name="url"></param>  
    /// <param name="cookieContainer"></param>  
    /// <param name="postStream"></param>  
    /// <param name="fileDictionary">需要上传的文件，Key：对应要上传的Name，Value：本地文件名</param>  
    /// <param name="timeOut">超时</param>  
    /// <returns></returns>  
    public static string HttpPost(string url, CookieContainer cookieContainer = null, Stream postStream = null, Dictionary<string, string> fileDictionary = null, string refererUrl = null, Encoding encoding = null, int timeOut = 1200000)
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        request.Method = "POST";
        request.Timeout = timeOut;

        #region 处理Form表单文件上传
        var formUploadFile = fileDictionary != null && fileDictionary.Count > 0;//是否用Form上传文件  
        if (formUploadFile)
        {
            //通过表单上传文件  
            postStream = postStream ?? new MemoryStream();

            string boundary = "----" + DateTime.Now.Ticks.ToString("x");
            //byte[] boundarybytes = Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");  
            string fileFormdataTemplate = "\r\n--" + boundary + "\r\nContent-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: application/octet-stream\r\n\r\n";
            string dataFormdataTemplate = "\r\n--" + boundary +
                                          "\r\nContent-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";
            foreach (var file in fileDictionary)
            {
                try
                {
                    var fileName = file.Value;

                    //准备文件流  
                    using (var fileStream = GetFileStream(fileName))
                    {
                        string formdata = null;
                        if (fileStream != null)
                        {
                            //存在文件  
                            formdata = string.Format(fileFormdataTemplate, file.Key, /*fileName*/ Path.GetFileName(fileName));
                        }
                        else
                        {
                            //不存在文件或只是注释  
                            formdata = string.Format(dataFormdataTemplate, file.Key, file.Value);
                        }

                        //统一处理  
                        var formdataBytes = Encoding.UTF8.GetBytes(postStream.Length == 0 ? formdata.Substring(2, formdata.Length - 2) : formdata);//第一行不需要换行  
                        postStream.Write(formdataBytes, 0, formdataBytes.Length);

                        //写入文件  
                        if (fileStream != null)
                        {
                            byte[] buffer = new byte[1024];
                            int bytesRead = 0;
                            while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                            {
                                postStream.Write(buffer, 0, bytesRead);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            //结尾  
            var footer = Encoding.UTF8.GetBytes("\r\n--" + boundary + "--\r\n");
            postStream.Write(footer, 0, footer.Length);

            request.ContentType = string.Format("multipart/form-data; boundary={0}", boundary);
        }
        else
        {
            request.ContentType = "application/x-www-form-urlencoded";
        }
        #endregion

        request.ContentLength = postStream != null ? postStream.Length : 0;
        request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
        request.KeepAlive = true;

        if (!string.IsNullOrEmpty(refererUrl))
        {
            request.Referer = refererUrl;
        }
        request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.57 Safari/537.36";

        if (cookieContainer != null)
        {
            request.CookieContainer = cookieContainer;
        }

        #region 输入二进制流
        if (postStream != null)
        {
            postStream.Position = 0;

            //直接写入流  
            Stream requestStream = request.GetRequestStream();

            byte[] buffer = new byte[1024];
            int bytesRead = 0;
            while ((bytesRead = postStream.Read(buffer, 0, buffer.Length)) != 0)
            {
                requestStream.Write(buffer, 0, bytesRead);
            }

            //debug  
            postStream.Seek(0, SeekOrigin.Begin);
            StreamReader sr = new StreamReader(postStream);
            var postStr = sr.ReadToEnd();
            postStream.Close();//关闭文件访问  
        }
        #endregion

        HttpWebResponse response = (HttpWebResponse)request.GetResponse();

        if (cookieContainer != null)
        {
            response.Cookies = cookieContainer.GetCookies(response.ResponseUri);
        }

        using (Stream responseStream = response.GetResponseStream())
        {
            using (StreamReader myStreamReader = new StreamReader(responseStream, encoding ?? Encoding.GetEncoding("utf-8")))
            {
                string retString = myStreamReader.ReadToEnd();
                return retString;
            }
        }
    }  

}