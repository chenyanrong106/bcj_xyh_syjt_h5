using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using Newtonsoft.Json;
using System.Text;
using System.Net;
using System.IO;
using SPACRM.Business.ServiceImpl;
using SPACRM.Entity;

namespace SPACRM.WebApp.wechat.test
{
    public partial class Update : WeiPage
    {
        NewVerService nvbo = new NewVerService();
        protected void Page_Load(object sender, EventArgs e)
        {
            List<Pet_XXL_Address> list = nvbo.GetAddressNoCity();
            foreach (Pet_XXL_Address a in list)
            {


                string url = "http://life.tenpay.com/cgi-bin/mobile/MobileQueryAttribution.cgi?chgmobile="+a.Phone;
                // string jg = PostRequest(url);
                string jg = SendRequest(url, "", "get");
                XmlDocument doc = new XmlDocument();
                try
                {
                doc.LoadXml(jg);
                XmlElement rootElement = doc.DocumentElement;
                string city = rootElement.SelectSingleNode("city").InnerXml;
                string province = rootElement.SelectSingleNode("province").InnerXml;
                a.a1 = province;
                a.a2 = city;
                nvbo.SavePetXXLAddress(a);
                }
                catch (Exception)
                {

                }
            }
        }

        class add
        {
            public string province { get; set; }
            public string cityname { get; set; }
        }

        #region 通讯函数
        /// <summary>
        /// 通讯函数
        /// </summary>
        /// <param name="url">请求Url</param>
        /// <param name="para">请求参数</param>
        /// <param name="method">请求方式GET/POST</param>
        /// <returns></returns>
        public static string SendRequest(string url, string para, string method)
        {
            string strResult = "";
            if (url == null || url == "")
                return null;
            if (method == null || method == "")
                method = "GET";
            // GET方式
            if (method.ToUpper() == "GET")
            {
                try
                {
                    System.Net.WebRequest wrq = System.Net.WebRequest.Create(url + para);
                    wrq.Method = "GET";
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
                    System.Net.WebResponse wrp = wrq.GetResponse();
                    System.IO.StreamReader sr = new System.IO.StreamReader(wrp.GetResponseStream(), System.Text.Encoding.GetEncoding("gb2312"));
                    strResult = sr.ReadToEnd();
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
            // POST方式
            if (method.ToUpper() == "POST")
            {
                if (para.Length > 0 && para.IndexOf('?') == 0)
                {
                    para = para.Substring(1);
                }
                WebRequest req = WebRequest.Create(url);
                req.Method = "POST";
                req.ContentType = "application/x-www-form-urlencoded";
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
                StringBuilder UrlEncoded = new StringBuilder();
                Char[] reserved = { '?', '=', '&' };
                byte[] SomeBytes = null;
                if (para != null)
                {
                    int i = 0, j;
                    while (i < para.Length)
                    {
                        j = para.IndexOfAny(reserved, i);
                        if (j == -1)
                        {
                            UrlEncoded.Append(HttpUtility.UrlEncode(para.Substring(i, para.Length - i), System.Text.Encoding.GetEncoding("gb2312")));
                            break;
                        }
                        UrlEncoded.Append(HttpUtility.UrlEncode(para.Substring(i, j - i), System.Text.Encoding.GetEncoding("gb2312")));
                        UrlEncoded.Append(para.Substring(j, 1));
                        i = j + 1;
                    }
                    SomeBytes = Encoding.Default.GetBytes(UrlEncoded.ToString());
                    req.ContentLength = SomeBytes.Length;
                    Stream newStream = req.GetRequestStream();
                    newStream.Write(SomeBytes, 0, SomeBytes.Length);
                    newStream.Close();
                }
                else
                {
                    req.ContentLength = 0;
                }
                try
                {
                    WebResponse result = req.GetResponse();
                    Stream ReceiveStream = result.GetResponseStream();
                    Byte[] read = new Byte[512];
                    int bytes = ReceiveStream.Read(read, 0, 512);
                    while (bytes > 0)
                    {
                        // 注意：
                        // 下面假定响应使用 UTF-8 作为编码方式。
                        // 如果内容以 ANSI 代码页形式（例如，932）发送，则使用类似下面的语句：
                        // Encoding encode = System.Text.Encoding.GetEncoding("shift-jis");
                        Encoding encode = System.Text.Encoding.GetEncoding("gb2312");
                        strResult += encode.GetString(read, 0, bytes);
                        bytes = ReceiveStream.Read(read, 0, 512);
                    }
                    return strResult;
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
            return strResult;
        }
        #endregion
    }
}