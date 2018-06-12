using SPACRM.Business.ServiceImpl;
using SPACRM.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace SPACRM.WebApp.wechat.Order
{
    public partial class PayApi : WeiPage
    {
        NewVerService nvbo = new NewVerService();
        MySmallShopService mss = new MySmallShopService();
        public Pet_JiuZhu_Info j = new Pet_JiuZhu_Info { je = 0, rc = 0 };
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                Pet_JiuZhu_Info j = nvbo.GetJiuZhuInfo(int.Parse(Request.Params["iid"]));
                if (j != null && j.EndTime < DateTime.Now)
                {
                    Response.Write("{\"err_code\":\"" + "本次救助已结束，请救助其他项目" + "\",\"st\":1}");
                    Response.End();
                }
                else
                {

                    Pet_XXL_Order order = new Pet_XXL_Order();
                    order.CreateTime = DateTime.Now;

                    order.GoodsName = "爱宠筹 - 为爱筹";
                    order.OrderState = 0;
                    order.OutOrderNo = "";
                    order.Source = int.Parse(Request.Params["iid"]);//来源 自己平台
                    order.CatSource = 0;
                    order.PetSource = 0;
                    order.PayType = Request.Params["zf"] == "微信支付" ? 1 : 2;

                    order.UnitPrice = decimal.Parse(Request.Params["txtnum"]);
                    order.TotalPrice = order.UnitPrice;
                    order.CourierRemark = Request.Params["remark"];
                    order.Remark = "0";
                    order.UrlPara = -1;//小程序
                    order.iswx = 1;
                    order.FromUserName = Request.Params["openid"];
                    order.OrderNo = DateTime.Now.ToString("yyyyMMddHHmmssffff") + new Random().Next(1000, 9999) + (order.FromUserName.Length > 10 ? order.FromUserName.Substring(order.FromUserName.Length - 6, 6) : "");

                    if (order.TotalPrice > 200)
                        order.Remark = "1";
                    order.PJ = 0;
                    int oid = nvbo.SavePetXXLOrder(order);
                    order.ID = oid;
                    WX(order);
                }

                //}
            }

        }

        /// <summary>
        /// 微信支付  小程序
        /// </summary>
        /// <param name="order"></param>
        private void WX(Pet_XXL_Order order)
        {
            string appid = "wxdf3b5e388e6ae04c"; //公众号，固定
            string mch_id = "1378506402";//商户号，固定
            string nonce_str = Guid.NewGuid().ToString("d").Replace("-", "").Substring(0, 25); //随机码
            string body = order.GoodsName;//商品描述
            string notify_url = WebUrl + "/wechat/order/Notify.aspx";
            string openid = order.FromUserName;//微信编号
            string out_trade_no = order.OrderNo; //商户订单编号
            string spbill_create_ip = GetLoginIp(); //ip地址
            string total_fee = Convert.ToInt32(order.TotalPrice * 100).ToString();  //总价，分
            string trade_type = "JSAPI";//交易类型
            string key = "7M8Doa2lesq70RbvjhhpZGjeqcuJDReZ";//商户key
            string attach = order.OrderNo;//附加数据

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
            //WXLOG log = new WXLOG { CON = resxml + "," + tmpStr + "," + jg + "zhifu", TIME = DateTime.Now };
            //mss.SaveLog(log);
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
                order.buyer_logon_id = prepay_id;
                nvbo.SaveOrder(order);
                //WXLOG log2 = new WXLOG { CON = resxml + "," + tmpStr + "," + tmpStr2 + "," + jg + "INDEX", TIME = DateTime.Now };
                //mss.SaveLog(log2);
                Response.Write("{\"appId\":\"" + appid + "\",\"nonceStr\":\"" + nonce_str + "\",\"timeStamp\":\"" + timeStamp + "\",\"package\":\"" + package + "\",\"signType\":\"" + signType + "\",\"paySign\":\"" + sign2 + "\",\"st\":0,\"returnurl\":\"" + WebUrl + "/wechat/order/sueccss.aspx?oid=" + order.ID + "," + order.UrlPara + "," + order.OrderNo + "\"}");
                Response.End();
            }
            else
            {
                Response.Write("{\"err_code\":\"" + rootElement.SelectSingleNode("err_code_des").InnerXml.Replace("<![CDATA[", "").Replace("]]>", "") + "\",\"st\":1}");
                Response.End();
            }
        }
    }
}