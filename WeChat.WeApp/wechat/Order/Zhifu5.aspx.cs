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
    public partial class Zhifu5 : WeiPage
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

                //if (Request.QueryString["id"] != null || Request.QueryString["state"] != null)
                //{
                //    if (Request.QueryString["id"] == "1" || Request.QueryString["state"] == "1")
                //    {
                //        dj.InnerHtml = "0.01";
                //        yf.InnerHtml = "0.01";
                //        //goodname.InnerHtml = "排除毒素套餐";
                //    }
                //    else
                //    {
                //        dj.InnerHtml = "0.03";
                //        yf.InnerHtml = "0.01";
                //       // goodname.InnerHtml = "改善体质套餐";
                //    }
                //}
                //else 
                //{
                //    Response.Redirect("index.aspx");
                //}
                //if (Request.Cookies["CookID"] == null)
                //{
                //    string cookid = Guid.NewGuid().ToString("d");
                //    HttpCookie cook = new HttpCookie("CookID");
                //    cook.Value = cookid;
                //    cook.Expires = DateTime.Now.AddMonths(1);
                //    Response.Cookies.Add(cook);
                //    Session["FromUserName"] = cookid;
                //    Session["ToUserName"] = "abc";
                //}
                //else
                //{
                //    Session["FromUserName"] = Request.Cookies["CookID"].Value;
                //    Session["ToUserName"] = "abc";
                //}
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
                            addinfo.InnerHtml = "<bdo class=\"zuobiao\"></bdo><a href=\"address5.aspx?id=" + Request.QueryString["id"] + "\">" + add.Name + "<br/>" + ((add.Sheng + add.City + add.JieDao.Replace("市、县级市", "") + add.Address).Length > 15 ? (add.Sheng + add.City + add.JieDao.Replace("市、县级市", "") + add.Address).Substring(0, 15) + "..." : (add.Sheng + add.City + add.JieDao.Replace("市、县级市", "") + add.Address)) + "</a>";
                            isadd.Value = "1";
                        }
                        else
                        {
                            addinfo.InnerHtml = "<bdo class=\"zuobiao\"></bdo><a href=\"address5.aspx?id=" + Request.QueryString["id"] + "\">您还没有默认的收货信息<br/>立即新增</a>";
                        }
                        //Pet_BM bm = nvbo.GetPet_BM(user);
                        //int ordered = nvbo.GetPayedOrder(user);
                        //if (bm != null && bm.ID <= 120 && ordered == 0)//前100名并且没有支付过
                        //{
                        //    sjjg.Value = "0.01";
                        //    yh.InnerHtml = "7.99";
                        //}
                        //int num = nvbo.GetPayedCount();//获取支付数量
                        //if (num >= 2500)
                        //{
                        //    tjdd.Visible = false;
                        //    Div1.Visible = true;
                        //}
                    }
                }


            }

            if (Request.QueryString["para"] != null && Request.QueryString["para"] == "tj")
            {
                if ((Request.QueryString["FromUserName"] != null || Session["FromUserName"] != null) && (Request.QueryString["ToUserName"] != null || Session["ToUserName"] != null))
                {
                    string user = Request.QueryString["FromUserName"] == null ? Session["FromUserName"].ToString() : Request.QueryString["FromUserName"].ToString();
                    string user2 = Request.QueryString["ToUserName"] == null ? Session["ToUserName"].ToString() : Request.QueryString["ToUserName"].ToString();
                    Pet_XXL_Address add = nvbo.GetPetXXLAddress(user);
                    if (add == null)
                    {
                        Response.Write("{\"st\":\"-1\",\"msg\":\"你未填写地址\"}");
                        Response.End();
                    }
                    else if (add.Sheng != "上海市" && !add.Address.Contains("上海"))
                    {
                        Response.Write("{\"st\":\"-1\",\"msg\":\"试吃装仅限上海地区\"}");
                        Response.End();
                    }
                    else if (nvbo.GetSYOrderByOpenid(user) > 0)
                    {
                        Response.Write("{\"st\":\"-1\",\"msg\":\"试吃装每人限购一份\"}");
                        Response.End();
                    }
                    //else if (nvbo.GetPayedCount() >= 2500)//获取支付数量
                    //{
                    //    Response.Write("{\"st\":\"-1\",\"msg\":\"4000份半湿粮已抢光<br>敬请期待下一次活动。\"}");
                    //}
                    else
                    {
                        //int carnum1 = int.Parse(Request.Params["carnum1"]);  //牛肉配方 1.5kg（100g*15袋）    价格：90
                        //int carnum2 = int.Parse(Request.Params["carnum2"]);  //三文鱼配方 1.5kg（100g*15袋） 价格：90
                        //int carnum3 = int.Parse(Request.Params["carnum3"]);  //牛肉配方 5.0kg（250g*20袋）    价格：220
                        //int carnum4 = int.Parse(Request.Params["carnum4"]);  //三文鱼配方 5.0kg（250g*20袋） 价格：220
                        //Pet_BM bm = nvbo.GetPet_BM(user);
                        //int ordered = nvbo.GetPayedOrder(user);
                        Pet_XXL_Order order = new Pet_XXL_Order();
                        //if (bm != null && bm.ID <= 120 && ordered == 0)//前100名
                        //{
                        //    order.Coupon = 7.99m;
                        //    order.TotalPrice = 0.01m;
                        //}
                        //else
                        //{
                        //    order.Coupon = 0;
                        //    order.TotalPrice = 8;
                        //}
                        //order.carnum1 = carnum1;
                        //order.carnum2 = carnum2;
                        //order.carnum3 = carnum3;
                        //order.carnum4 = carnum4;
                        order.Coupon = 0;// decimal.Parse(Request.Params["yh"]);
                        order.TotalPrice = 19.9m;// decimal.Parse(Request.Params["sj"]);
                        order.AID = add.ID;
                        order.CreateTime = DateTime.Now;
                        order.FromUserName = user;
                        order.GoodsName = "PETKIN手工半湿粮试用装";
                        order.Num = 1;// int.Parse(Request.Params["sl"]);
                        order.OrderNo = DateTime.Now.ToString("yyyyMMddHHmmssffff") + new Random().Next(1000, 9999) + (order.FromUserName.Length > 10 ? order.FromUserName.Substring(order.FromUserName.Length - 6, 6) : "");
                        order.OrderState = 0;
                        order.OutOrderNo = "";
                        order.Source = 0;
                        order.PayType = Request.Params["zf"] == "微信支付" ? 1 : 2;
                        order.PostPrice = 19.9m;// decimal.Parse(Request.Params["yf"]);
                        order.UnitPrice = 0;// decimal.Parse(Request.Params["dj"]);
                        order.YNum = 0;
                        order.WNum = order.Num;
                        //order.CourierRemark = "牛肉配方 1.5kg（100g*15袋）*" + carnum1 + "<br>三文鱼配方 1.5kg（100g*15袋）*" + carnum2 + "<br>牛肉配方 5.0kg（250g*20袋）*" + carnum3 + "<br>三文鱼配方 5.0kg（250g*20袋）*" + carnum4;
                        int yhqid = 0;// int.Parse(Request.Params["yhqid"]);
                        order.yhqid = yhqid;
                        //decimal zk = 1m;  //折扣
                        //Pet_XXL_YHQ yhq = nvbo.GetYHQ(yhqid); //优惠券
                        //if (yhq != null && yhq.ZT == 0)
                        //{
                        //    zk = 0.8m;
                        //}
                        if ((order.UnitPrice + order.PostPrice - order.Coupon != order.TotalPrice))
                        {
                            Response.Write("{\"err_code\":\"" + "订单金额异常" + "\",\"st\":1}");
                            Response.End();
                        }
                        //else if (yhqid != 0 && nvbo.GetHaveYHQ(yhqid) == 1)
                        //{
                        //    Response.Write("{\"err_code\":\"" + "优惠券已被使用" + "\",\"st\":1}");
                        //    Response.End();
                        //}
                        else if (order.TotalPrice == 0)
                        {
                            Response.Write("{\"err_code\":\"" + "请选择购买半湿粮" + "\",\"st\":1}");
                            Response.End();
                        }
                        else
                        {
                            int oid = nvbo.SavePetXXLOrder(order);
                            //yhq.ZT = oid;
                            //nvbo.SaveYHQ(yhq);
                            if (order.PayType == 1)
                                WX(order);
                            else
                                ZFB(order);
                        }
                    }
                }
            }

            else if (Request.QueryString["para"] != null && Request.QueryString["para"] == "yq")
            {
                if (Request.Params["qh"] != null)
                {
                   
                }
                else
                {
                    Response.Write("{\"err_code\":\"" + "没有券号" + "\",\"st\":1}");
                }
                Response.End();
            }
            //else if (Request.QueryString["para"] != null && Request.QueryString["para"] == "qxyq")
            //{
            //    if (Request.Params["yhqid"] != null)
            //    {
            //        Pet_XXL_YHQ yhq = nvbo.GetYHQ(int.Parse(Request.Params["yhqid"]));
            //        if (yhq != null)
            //        {
            //            yhq.ZT = 0;
            //            nvbo.SaveYHQ(yhq);
            //        }
            //    }
            //    Response.Write("{\"err_code\":\"" + "已取消" + "\",\"st\":1}");
            //    Response.End();
            //}
        }

        /// <summary>
        /// 微信支付
        /// </summary>
        /// <param name="order"></param>
        private void WX(Pet_XXL_Order order)
        {
            string appid = GetAppid(mjuserid); //公众号，固定
            string mch_id = "1261198501";//商户号，固定
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
                //WXLOG log2 = new WXLOG { CON = resxml + "," + tmpStr + "," + tmpStr2 + "," + jg + "INDEX", TIME = DateTime.Now };
                //mss.SaveLog(log2);
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
            string partner = "2088021438736408"; //合作者身份id
            string _input_charset = "utf-8"; //编码方式
            string sign_type = "MD5";//签名方式
            string notify_url = WebUrl + "/wechat/order/Notify2.aspx";//服务器异步通知地址
            string return_url = WebUrl + "/wechat/order/ok.aspx";//回调页面
            string out_trade_no = order.OrderNo;//商户订单号
            string subject = order.GoodsName; //商品名称
            string total_fee = order.TotalPrice.ToString();//交易金额 单位元
            string seller_id = "luffy.lei@petkin.cn";//卖家支付宝账号
            string payment_type = "1";//支付类型 
            string body = order.OrderNo;//描述，可为空
            string show_url = "";//商品展示地址，可空
            string it_b_pay = "";//超时时间,可空
            string extern_token = "";//钱包，可空
            string otherfee = "";//航旅订单，可空
            string airticket = "";//航旅订单，可空

            string key = "8pphj74sauzr25v1wvrc30ol2tuv0euc";

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