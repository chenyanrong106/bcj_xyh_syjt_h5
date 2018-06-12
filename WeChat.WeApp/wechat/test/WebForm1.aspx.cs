using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SPACRM.Business.ServiceImpl;
using SPACRM.Entity;

namespace SPACRM.WebApp.wechat.test
{
    public partial class WebForm1 : WeiPage
    {
        MySmallShopService mss = new MySmallShopService();
        protected void Page_Load(object sender, EventArgs e)
        {
            //string notify_url = "http://cw.meijiewd.com/wechat/order/Notify2.aspx";
            //string jason = "{\"a\":\"b\"}";
            //string a = PostRequest(notify_url);
            //string jg = HttpXmlPostRequest(notify_url, jason, Encoding.UTF8, "application/json");

            //string access_token = Token(mjuserid);
            //string jason = "{\"action_name\": \"QR_LIMIT_SCENE\", \"action_info\": {\"scene\": {\"scene_id\":49}}}";
            //string resMessage = HttpXmlPostRequest("https://api.weixin.qq.com/cgi-bin/qrcode/create?access_token=" + access_token, jason, Encoding.UTF8);

           // string a = Server.UrlEncode(@"gQEM8DoAAAAAAAAAASxodHRwOi8vd2VpeGluLnFxLmNvbS9xL0NUbzhtRWJsWE1zbDRLNTFxaFlZAAIEa\/EdVgMEAAAAAA==");

            //Qrcode r = mss.GetQrcode(52);
            //if (r != null)
            //{
            //    string access_token = Token(mjuserid);
            //    string jason = "{\"action_name\": \"QR_LIMIT_SCENE\", \"action_info\": {\"scene\": {\"scene_id\":"+r.ID+"}}}";
            //    string resMessage = HttpXmlPostRequest("https://api.weixin.qq.com/cgi-bin/qrcode/create?access_token=" + access_token, jason, Encoding.UTF8);
            //    string[] a = resMessage.Split('\"');
            //    if (a.Length > 3)
            //    {
            //        r.Ticket = Server.UrlEncode(a[3].Replace(@"\/","/"));
            //        mss.SaveQrcode(r);
            //    }
            //}
        }
    }
}