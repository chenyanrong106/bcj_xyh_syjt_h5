using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SPACRM.Business.ServiceImpl;
using SPACRM.Entity;
using System.Xml;

namespace SPACRM.WebApp.wechat.test
{
    public partial class Index : WeiPage
    {
        MySmallShopService mss = new MySmallShopService();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.Params["para"] == null)
                {
                    Response.Write(" <input type='hidden'' value='" + Server.UrlEncode(AbsoluteUri) + "' id='url' />");
                }
            }
            if (Request.QueryString["para"] != null)
            {
                try
                {

                    string appid = GetAppid(mjuserid); //公众号，固定
                    string mch_id = "1238277202";//商户号，固定
                    string nonce_str = Guid.NewGuid().ToString("d").Replace("-", "").Substring(0, 25); //随机码
                    string body = "手工半湿粮";//商品描述
                    string notify_url = "http://cw.meijiewd.com/wechat/test/index2.aspx";
                    string openid = "o0UMCj6Y9wcOeRVu0hJ0yx2MxCZY";//微信编号
                    string out_trade_no = "1415659991"; //商户订单编号
                    string spbill_create_ip = GetLoginIp(); //ip地址
                    string total_fee = "1";  //总价，分
                    string trade_type = "JSAPI";//交易类型
                    string key = "7M8Doa2lesq70RbvjhhpZGjeqcuJDReZ";//商户key
                    string attach = "支付测试";//附加数据

                    string[] ArrTmp = { "appid=" + appid, "mch_id=" + mch_id, "nonce_str=" + nonce_str, "body=" + body, "notify_url=" + notify_url, "openid=" + openid, "out_trade_no=" + out_trade_no, "spbill_create_ip=" + spbill_create_ip, "total_fee=" + total_fee, "trade_type=" + trade_type, "attach=" + attach };
                    Array.Sort(ArrTmp);     //字典排序
                    string tmpStr = string.Join("&", ArrTmp);
                    tmpStr += "&key=" + key;
                    string sign = getMD5(tmpStr).ToUpper();

                    string resxml = string.Format(@"<xml><appid>{0}</appid><mch_id>{1}</mch_id><nonce_str>{2}</nonce_str><attach>{3}</attach>
   <body>{4}</body>
   <notify_url>{5}</notify_url>
   <openid>{6}</openid>
   <out_trade_no>{7}</out_trade_no>
   <spbill_create_ip>{8}</spbill_create_ip>
   <total_fee>{9}</total_fee>
   <trade_type>{10}</trade_type>
   <sign>{11}</sign>
</xml>", appid, mch_id, nonce_str, attach, body, notify_url, openid, out_trade_no, spbill_create_ip, total_fee, trade_type, sign);

                    string url = "https://api.mch.weixin.qq.com/pay/unifiedorder";
                    string jg = HttpXmlPostRequest(url, resxml, Encoding.UTF8);
                    WXLOG log = new WXLOG { CON = resxml + "," + tmpStr  + "," + jg + "INDEX", TIME = DateTime.Now };
                    mss.SaveLog(log);
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(jg);
                    XmlElement rootElement = doc.DocumentElement;
                    string prepay_id = rootElement.SelectSingleNode("prepay_id") == null ? "" : rootElement.SelectSingleNode("prepay_id").InnerXml.Replace("<![CDATA[", "").Replace("]]>", "");
                    if (prepay_id != "")
                    {
                        string timeStamp = ConvertDateTimeInt(DateTime.Now).ToString();
                        string package = "prepay_id=" + prepay_id;
                        string signType = "MD5";
                        string[] ArrTmp2 = { "appId=" + appid, "nonceStr=" + nonce_str, "timeStamp=" + timeStamp, "package=" + package, "signType=" + signType };
                        Array.Sort(ArrTmp2);     //字典排序
                        string tmpStr2 = string.Join("&", ArrTmp2);
                        tmpStr2 += "&key=" + key;
                        string sign2 = getMD5(tmpStr2).ToUpper();
                        WXLOG log2 = new WXLOG { CON = resxml + "," + tmpStr + "," + tmpStr2 + "," + jg + "INDEX", TIME = DateTime.Now };
                        mss.SaveLog(log2);
                        Response.Write("{\"appId\":\"" + appid + "\",\"nonceStr\":\"" + nonce_str + "\",\"timeStamp\":\"" + timeStamp + "\",\"package\":\"" + package + "\",\"signType\":\"" + signType + "\",\"paySign\":\"" + sign2 + "\",\"st\":0}");
                        Response.End();
                    }
                    else
                    {
                        Response.Write("{\"err_code\":\"" + rootElement.SelectSingleNode("err_code_des").InnerXml.Replace("<![CDATA[", "").Replace("]]>", "") + "\",\"st\":1}");
                        Response.End();
                    }


                }
                catch (Exception ex)
                {
                    WXLOG log = new WXLOG { CON = ex.Message+","+ex.StackTrace, TIME = DateTime.Now };
                    mss.SaveLog(log);
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }
    }
}