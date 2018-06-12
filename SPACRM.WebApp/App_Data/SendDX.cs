using SPACRM.Business.ServiceImpl;
using SPACRM.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;


public class SendDX
{
    MySmallShopService mss = new MySmallShopService();
    public SendDX()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }

    ///// <summary>
    ///// 发送短信
    ///// </summary>
    ///// <param name="Message">信息内容</param>
    ///// <param name="Phone">手机号码</param>
    ///// <param name="ToUserName">商户微信号或ID</param>
    ///// <returns>发送结果</returns>
    //public string Send(ref SMS_MESSAGE org)
    //{
    //    string content = org.CONTENT + "【小时光SPA】";
    //    string url = "http://www.ztsms.cn:8800/sendXSms.do?username=puman&password=puman2014&mobile=" + org.PHONE_NUMBER + "&content=" + content + "&dstime=&productid=676767&xh=";
    //    string msg = PostRequest(url);
    //    org.RESULT = msg;
    //    try
    //    {
    //        if (msg.Contains("1,"))
    //            msg = "发送成功";
    //        else
    //            msg = "发送失败";
    //    }
    //    catch (Exception)
    //    {

    //    }
    //    return msg;
    //}

    ///// <summary>
    ///// 发送短信 即时验证码
    ///// </summary>
    ///// <param name="Message">信息内容</param>
    ///// <param name="Phone">手机号码</param>
    ///// <param name="ToUserName">商户微信号或ID</param>
    ///// <returns>发送结果</returns>
    //public string Send(string Message, string Phone)
    //{
    //    string content = Message + "【小时光SPA】";
    //    string url = "http://www.ztsms.cn:8800/sendXSms.do?username=puman&password=puman2014&mobile=" + Phone + "&content=" + content + "&dstime=&productid=676767&xh=";
    //    string msg = PostRequest(url);

    //    try
    //    {
    //        WXDXLog d = new WXDXLog();
    //        d.JG = msg;
    //        d.Merchants_ID = 0;
    //        d.CONTENT = content;
    //        d.Time = DateTime.Now;
    //        d.ToUser = Phone;
    //        d.MSTYPE = "微信";
    //        mss.SaveDX(d);
    //        if (msg.Contains("1,"))
    //            msg = "发送成功";
    //        else
    //            msg = "发送失败";
    //    }
    //    catch (Exception)
    //    {

    //    }

    //    return msg;

    //}

    public string PostRequest(string url)
    {
        HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);  //定义请求对象，并设置好请求URL地址
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();    //定义响应对象，request在调用GetResponse方法事执行请求了，而不是在HttpWebRequest.Create的时候执行。
        Stream stream = response.GetResponseStream(); //定义一个流对象，来获取响应流
        StreamReader sr = new StreamReader(stream);  //定义一个流读取对象，读取响应流
        string responseHTML = sr.ReadToEnd();
        return responseHTML;
    }
}
