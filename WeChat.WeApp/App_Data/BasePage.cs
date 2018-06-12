using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Net;
using System.IO;
using System.Configuration;
using SPACRM.Extension;
using SPACRM.Entity;
using System.Text;
using SPACRM.Business.ServiceImpl;
using SPACRM.Interface;

/// <summary>
///BasePage 的摘要说明
/// </summary>
public class BasePage : WXMyContext
{
    //protected override void OnPreInit(EventArgs e)
    //{
    //    //if (Request.Url.AbsoluteUri.IndexOf("Login") == -1)
    //    //{
    //    //    if (CurrentUserInfo == null)
    //    //    {
    //    //        Response.Redirect(ResolveUrl("~/Login.aspx"));
    //    //    }
    //    //}
    //    //base.OnPreInit(e); 
    //}
   
   
  
    /// <summary>
    /// 导出excel
    /// </summary>
    /// <param name="ds"></param>
    /// <param name="FileName"></param>
    public void CreateExcel(DataSet ds, string FileName)
    {
        System.Web.UI.WebControls.DataGrid dgExport = null;
        System.Web.HttpContext curContext = System.Web.HttpContext.Current;
        System.IO.StringWriter strWriter = null;
        System.Web.UI.HtmlTextWriter htmlWriter = null;

        if (ds != null)
        {
            curContext.Response.ContentType = "application/vnd.ms-excel";
            curContext.Response.ContentEncoding = System.Text.Encoding.UTF8;
            curContext.Response.Charset = "UTF-8";
            curContext.Response.AppendHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode(System.Text.Encoding.UTF8.GetBytes(FileName)) + ".xls");

            strWriter = new System.IO.StringWriter();
            htmlWriter = new System.Web.UI.HtmlTextWriter(strWriter);

            dgExport = new System.Web.UI.WebControls.DataGrid();
            dgExport.DataSource = ds.Tables[0].DefaultView;
            dgExport.AllowPaging = false;
            dgExport.DataBind();

            dgExport.RenderControl(htmlWriter);
            curContext.Response.Write(strWriter.ToString());
            curContext.Response.End();
        }
    }


    public string GetAppid()
    {
        return CurrentMerchants.AppID;
    }

    public string GetSecret()
    {
        return CurrentMerchants.Appsecret;
    }
    ORG_INFO m;
    public string Token()
    {
         m = CurrentMerchants;
        string Access_token = "";
        if (m.Access_token != "")
        {
            Access_token = m.Access_token;
            string url = "https://api.weixin.qq.com/cgi-bin/user/info?access_token=" + Access_token + "&openid=" + (m.OneOpenID == null ? "oloJGv4lWL-TS8yn8uo4Fu1rbVTI" : m.OneOpenID);
            string b = PostRequest(url);
            if (b.Contains("errcode"))  //返回错误信息
            {
                Access_token = GetAccess(Access_token);
                m.Access_token = Access_token;
                SaveMerchants(m);
            }
            if (m.OneOpenID == ""||m.OneOpenID==null)
            {
                ISystemService sbo = new SystemService();
                WXCUST_FANS fans = sbo.GetOneFans(m.ToUserName);
                if (fans != null)
                {
                    m.OneOpenID = fans.FROMUSERNAME;
                    SaveMerchants(m);
                }
            }
        }
        else
        {
            if (m.OneOpenID == ""||m.OneOpenID==null)
            {
                ISystemService sbo = new SystemService();
                WXCUST_FANS fans = sbo.GetOneFans(m.ToUserName);
                if (fans != null)
                {
                    m.OneOpenID = fans.FROMUSERNAME;
                    //SaveMerchants(m);
                }
            }
            Access_token = GetAccess(Access_token);
            m.Access_token = Access_token;
            SaveMerchants(m);
        }
        return Access_token;
    }

    private string GetAccess(string Access_token)
    {
        string url = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=" + GetAppid() + "&secret=" + GetSecret();
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
    public string GetURL()
    {
       string url= ConfigurationManager.AppSettings["caidan"].ToString();
       return url;
        //return "m.shanghaivive";
        //return "122.226.44.60";
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

    /// <summary>
    /// 向粉丝发送信息
    /// </summary>
    /// <param name="FromUserName"></param>
    /// <param name="Message"></param>
    public void PostMessage(string FromUserName, string Message)
    {
        string Access_token = Token();

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


    public string HttpXmlPostRequest(string postUrl, string postXml, Encoding encoding)
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
        request.ContentType = "text/xml";

        using (var requestStream = request.GetRequestStream())
        {
            requestStream.Write(byteArray, 0, byteArray.Length);
        }

        using (var responseStream = request.GetResponse().GetResponseStream())
        {
            return new StreamReader(responseStream, encoding).ReadToEnd();
        }
    }

}