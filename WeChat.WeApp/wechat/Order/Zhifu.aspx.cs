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

namespace SPACRM.WebApp.wechat.NewVer.Order
{
    public partial class Zhifu : WeiPage
    {
        NewVerService nvbo = new NewVerService();
        MySmallShopService mss = new MySmallShopService();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if ((Request.QueryString["FromUserName"] != null || Session["FromUserName"] != null) && (Request.QueryString["ToUserName"] != null || Session["ToUserName"] != null))
                //{
                //    string user = Request.QueryString["FromUserName"] == null ? Session["FromUserName"].ToString() : Request.QueryString["FromUserName"].ToString();
                //    string user2 = Request.QueryString["ToUserName"] == null ? Session["ToUserName"].ToString() : Request.QueryString["ToUserName"].ToString();
                //    Session["FromUserName"] = user;
                //    Session["ToUserName"] = user2;
                //}

                BaseLoad();

                if (Request.Params["para"] == null)
                {
                    Response.Write(" <input type='hidden' value='" + Server.UrlEncode(AbsoluteUri) + "' id='url' />");
                    if ((Request.QueryString["FromUserName"] != null || Session["FromUserName"] != null) && (Request.QueryString["ToUserName"] != null || Session["ToUserName"] != null))
                    {
                        string user = Request.QueryString["FromUserName"] == null ? Session["FromUserName"].ToString() : Request.QueryString["FromUserName"].ToString();
                        string user2 = Request.QueryString["ToUserName"] == null ? Session["ToUserName"].ToString() : Request.QueryString["ToUserName"].ToString();
                        Pet_XXL_Address add = nvbo.GetPetXXLAddress(user);
                        if (add != null)
                        {
                            addinfo.InnerHtml = "<bdo class=\"zuobiao\"></bdo><a href=\"address.aspx\">" + add.Name + "<br/>" + ((add.Sheng + add.City + add.JieDao.Replace("市、县级市", "") + add.Address).Length > 15 ? (add.Sheng + add.City + add.JieDao.Replace("市、县级市", "") + add.Address).Substring(0, 15) + "..." : (add.Sheng + add.City + add.JieDao.Replace("市、县级市", "") + add.Address)) + "</a>";
                            isadd.Value = "1";
                        }
                        else
                        {
                            addinfo.InnerHtml = "<bdo class=\"zuobiao\"></bdo><a href=\"address.aspx\">您还没有默认的收货信息<br/>立即新增</a>";
                        }
                       
                        int num = nvbo.GetPayedCount();//获取支付数量
                        if (num >= 1500)
                        {
                            tjdd.Visible = false;
                            Div1.Visible = true;
                        }
                    }
                }


            }

            if (Request.QueryString["para"] != null)
            {
                if ((Request.QueryString["FromUserName"] != null || Session["FromUserName"] != null) && (Request.QueryString["ToUserName"] != null || Session["ToUserName"] != null))
                {
                    string user = Request.QueryString["FromUserName"] == null ? Session["FromUserName"].ToString() : Request.QueryString["FromUserName"].ToString();
                    string user2 = Request.QueryString["ToUserName"] == null ? Session["ToUserName"].ToString() : Request.QueryString["ToUserName"].ToString();
                    Pet_XXL_Address add = nvbo.GetPetXXLAddress(user);
                    if (add == null)
                    {
                        Response.Write("{\"st\":\"-1\",\"msg\":\"你未填写地址\"}");
                    }
                    else if (nvbo.GetPayedCount() >= 1500)//获取支付数量
                    {
                        Response.Write("{\"st\":\"-1\",\"msg\":\"4000份半湿粮已抢光<br>敬请期待下一次活动。\"}");
                    }
                    else
                    {
                      
                    }
                }
            }
        }

        /// <summary>
        /// 微信支付
        /// </summary>
        /// <param name="order"></param>
        private void WX(Pet_XXL_Order order)
        {
            string appid = GetAppid(mjuserid); //公众号，固定
            string mch_id = "1238277202";//商户号，固定
            string nonce_str = Guid.NewGuid().ToString("d").Replace("-", "").Substring(0, 25); //随机码
            string body = order.GoodsName;//商品描述
            string notify_url = "http://cw.meijiewd.com/wechat/order/Notify.aspx";
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
            WXLOG log = new WXLOG { CON = resxml + "," + tmpStr + "," + jg + "zhifu", TIME = DateTime.Now };
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

        private void ZFB(Pet_XXL_Order order)
        {
            string service = "alipay.wap.create.direct.pay.by.user"; //接口名称
            string partner = "2088111185835414"; //合作者身份id
            string _input_charset = "utf-8"; //编码方式
            string sign_type = "MD5";//签名方式
            string notify_url = "http://cw.meijiewd.com/wechat/order/Notify2.aspx";//服务器异步通知地址
            string return_url = "http://cw.meijiewd.com/wechat/order/ok.aspx";//回调页面
            string out_trade_no = order.OrderNo;//商户订单号
            string subject = order.GoodsName; //商品名称
            string total_fee = order.TotalPrice.ToString();//交易金额 单位元
            string seller_id = "luffy.lei@puman.com";//卖家支付宝账号
            string payment_type = "1";//支付类型 
            string body = order.OrderNo;//描述，可为空
            string show_url = "";//商品展示地址，可空
            string it_b_pay = "";//超时时间,可空
            string extern_token = "";//钱包，可空
            string otherfee = "";//航旅订单，可空
            string airticket = "";//航旅订单，可空

            string key = "5qsg54awgnkgqcucvrqrmsll5xhbmecp";

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
            Dictionary<string, string> dic = Com.Alipay.Core.FilterPara(sd);
            string str = Com.Alipay.Core.CreateLinkString(dic);
            string sign = GetMD5(str + key, _input_charset);
            dic.Add("sign_type", sign_type);
            dic.Add("sign", sign);
            string url = "https://mapi.alipay.com/gateway.do?" + Com.Alipay.Core.CreateLinkStringUrlencode(dic, Encoding.UTF8).Replace("&", "!");
            Response.Write("{\"err_code\":\"" + url + "\",\"st\":2}");
            Response.End();
        }
    }
}