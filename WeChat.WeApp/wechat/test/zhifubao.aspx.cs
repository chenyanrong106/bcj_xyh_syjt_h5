using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SPACRM.WebApp.wechat.test
{
    public partial class zhifubao : WeiPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string service = "alipay.wap.create.direct.pay.by.user"; //接口名称
            string partner = "2088111185835414"; //合作者身份id
            string _input_charset = "utf-8"; //编码方式
            string sign_type = "MD5";//签名方式
            string notify_url = "http://hlcb.uz.com/notify-web/TradePayNotify";//服务器异步通知地址
            string return_url = "http://cw.meijiewd.com/wechat/newver/test/index2.aspx";//回调页面
            string out_trade_no = "1234568";//商户订单号
            string subject = "小鲜粮"; //商品名称
            string total_fee = "0.01";//交易金额 单位元
            string seller_id = "luffy.lei@puman.com";//卖家支付宝账号
            string payment_type = "1";//支付类型 
            string body = "测试";//描述，可为空
            string show_url = "";//商品展示地址，可空
            string it_b_pay = "";//超时时间,可空
            string extern_token = "";//钱包，可空
            string otherfee = "";//航旅订单，可空
            string airticket = "";//航旅订单，可空

            string key = "5qsg54awgnkgqcucvrqrmsll5xhbmecp";

            //string[] ArrTmp = { "service=" + service, 
            //                      "partner=" + partner,
            //                      "_input_charset=" + _input_charset,
            //                      "out_trade_no=" + out_trade_no,
            //                      "subject=" + subject,
            //                      "total_fee=" + total_fee,
            //                      "seller_id=" + seller_id, 
            //                      "payment_type=" + payment_type };
            //Array.Sort(ArrTmp);     //字典排序
            //string tmpStr = string.Join("&", ArrTmp);
            //tmpStr += "&Key=" + key;
            //string sign = getMD5(tmpStr);
            //string url = "https://mapi.alipay.com/gateway.do?service=" + service + "&partner=" + partner + "&_input_charset=" + _input_charset + "&out_trade_no=" + out_trade_no + "&subject=" + subject + "&total_fee=" + total_fee + "&seller_id=" + seller_id + "&payment_type=" + payment_type + "&sign=" + sign + "&sign_type=" + sign_type;

            SortedDictionary<string, string> sd = new SortedDictionary<string, string>();
            sd.Add("service", service);
            sd.Add("partner", partner);
            sd.Add("_input_charset", _input_charset);
            sd.Add("out_trade_no", out_trade_no);
            sd.Add("subject", subject);
            sd.Add("total_fee", total_fee);
            sd.Add("seller_id", seller_id);
            sd.Add("payment_type", payment_type);
            sd.Add("sign_type", sign_type);
            sd.Add("notify_url", notify_url);
            sd.Add("return_url", return_url);
            sd.Add("body", body);
            sd.Add("show_url", show_url);
            sd.Add("it_b_pay", it_b_pay);
            sd.Add("extern_token", extern_token);
            sd.Add("otherfee", otherfee);
            sd.Add("airticket", airticket);
           // url = HttpUtility.UrlDecode(url, Encoding.UTF8);
            Dictionary<string, string> dic = Com.Alipay.Core.FilterPara(sd);
            string str = Com.Alipay.Core.CreateLinkString(dic);
            string sign = GetMD5(str + key, _input_charset);
            dic.Add("sign", sign);
            string url = "https://mapi.alipay.com/gateway.do?" + Com.Alipay.Core.CreateLinkStringUrlencode(dic, Encoding.UTF8);
            Response.Redirect(url);
        }


        
    }
}