using SPACRM.Business.ServiceImpl;
using SPACRM.Business.WXService;
using SPACRM.Entity;
using SPACRM.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;

namespace SPACRM.WebApp.wcf
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“WechatCRMService”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 WechatCRMService.svc 或 WechatCRMService.svc.cs，然后开始调试。

    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class WechatCRMService : IWechatCRMService
    {
        private ICustomerService cs = new CustomerService();
        private ICompanyService cys = new CompanyService();
        private WXLogService wls = new WXLogService();

        public PostMessageResult PostMessageByID(int custInfoId, string message)
        {
            PostMessageResult pmr = new PostMessageResult() { Success = false };
            var cust = cs.GetCustomer(custInfoId);
            if (cust == null)
            {
                pmr.ErrorCode = 0;
                pmr.Message = "未找到用户";
                return pmr;
            }
            if (!cust.ORG_ID.HasValue)
            {
                pmr.ErrorCode = 1;
                return pmr;
            }
            var org = cys.GetCompany(cust.ORG_ID.Value);
            if (org == null)
            {
                pmr.ErrorCode = 2;
                pmr.Message = "未找到公司";
                return pmr;
            }
            if (string.IsNullOrEmpty(cust.FROM_USER_NAME) || string.IsNullOrEmpty(org.AppID) || string.IsNullOrEmpty(org.Appsecret))
            {
                pmr.ErrorCode = 3;
                pmr.Message = "未找到用户OPENID或公司应用ID及密钥";
                return pmr;
            }
            var access_token = GetAccess(org.AppID, org.Appsecret);
            if (string.IsNullOrEmpty(access_token))
            {
                pmr.ErrorCode = 4;
                pmr.Message = "获取Token授权失败";
                return pmr;
            }
            var postUrl = "https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token=" + access_token;
            string d = @"{
    ""touser"":""{0}"",
    ""msgtype"":""text"",
    ""text"":
    {
         ""content"":""{1}""
    }
}";
            d = d.Replace("{0}", cust.FROM_USER_NAME).Replace("{1}", message);

            try
            {
                string resMessage = HttpXmlPostRequest(postUrl, d, Encoding.UTF8);
                WXLOG l = new WXLOG();
                l.CON = d + "," + resMessage;
                l.TIME = DateTime.Now;
                wls.Save(l);

                if (resMessage.Contains("ok"))
                {
                    pmr.Success = true;
                    pmr.Message = "已推送";
                    pmr.SendTime = DateTime.Now;
                    return pmr;
                }
                else
                {
                    pmr.ErrorCode = 5;
                    pmr.Message = "调用微信发消息接口失败";
                    return pmr;
                }
            }
            catch (Exception ex)
            {
                pmr.ErrorCode = 6;
                pmr.Message = ex.ToString();
                return pmr;
            }
        }

        public PostMessageResult PostMessageByMobile(string mobile, int orgID, string message)
        {
            var cust = cs.GetCustomerByMobile(mobile, orgID.ToString());
            if (cust == null)
            {
                return new PostMessageResult() { Success = false, ErrorCode = 0, Message = "未找到用户" };
            }
            return this.PostMessageByID(cust.ID, message);
        }

        private string GetAccess(string appID, string appSecret)
        {
            var access_token = "";
            string url = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=" + appID + "&secret=" + appSecret;
            try
            {
                string token = PostRequest(url);
                if (token.Contains("7200"))
                {
                    string[] b = token.Split('\"');
                    access_token = b[3];
                }
            }
            catch (Exception)
            {
                access_token = "";
            }
            return access_token;
        }

        private string PostRequest(string url)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);  //定义请求对象，并设置好请求URL地址
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();    //定义响应对象，request在调用GetResponse方法事执行请求了，而不是在HttpWebRequest.Create的时候执行。
            Stream stream = response.GetResponseStream(); //定义一个流对象，来获取响应流
            StreamReader sr = new StreamReader(stream);  //定义一个流读取对象，读取响应流
            string responseHTML = sr.ReadToEnd();
            return responseHTML;
        }

        public static string HttpXmlPostRequest(string postUrl, string postXml, Encoding encoding)
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


        public int Test(string text)
        {
            return 1;
        }

        public PostMessageResult Test2(string text)
        {
            return new PostMessageResult() { ErrorCode = 0, Success = false, Message = "测试" };
        }
    }
}
