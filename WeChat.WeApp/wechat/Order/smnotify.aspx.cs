using SPACRM.Business.ServiceImpl;
using SPACRM.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace SPACRM.WebApp.wechat.Order
{
    public partial class smnotify : WeiPage
    {

        NewVerService nvbo = new NewVerService();
        MySmallShopService mss = new MySmallShopService();
        const string Token = "puman";		//与微信平台那边填写的token一致
        protected void Page_Load(object sender, EventArgs e)
        {
            string postStr = "";
            //WXLOG log2 = new WXLOG { CON = postStr + "weixin", TIME = DateTime.Now };
            //mss.SaveLog(log2);
            if (Request.HttpMethod.ToLower() == "post")
            {
                Stream s = System.Web.HttpContext.Current.Request.InputStream;
                byte[] b = new byte[s.Length];
                s.Read(b, 0, (int)s.Length);
                postStr = Encoding.UTF8.GetString(b);
                if (!string.IsNullOrEmpty(postStr))
                {
                    //封装请求类
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(postStr);
                    XmlElement rootElement = doc.DocumentElement;

                    string result_code = rootElement.SelectSingleNode("result_code").InnerXml.Replace("<![CDATA[", "").Replace("]]>", "");
                    if (result_code == "SUCCESS")
                    {
                        string orderno = rootElement.SelectSingleNode("out_trade_no").InnerXml.Replace("<![CDATA[", "").Replace("]]>", "");
                        string bank_type = rootElement.SelectSingleNode("bank_type").InnerXml.Replace("<![CDATA[", "").Replace("]]>", "");
                        string transaction_id = rootElement.SelectSingleNode("transaction_id").InnerXml.Replace("<![CDATA[", "").Replace("]]>", "");
                        string openid = rootElement.SelectSingleNode("openid").InnerXml.Replace("<![CDATA[", "").Replace("]]>", "");
                        Pet_XXL_Order order = nvbo.GetPetXXLOrder(orderno);
                        if (order != null && order.OrderState != 2)
                        {
                            //Pet_XXL_Address add = nvbo.GetAddress(order.AID.Value);
                            //if (add != null)
                            //{
                            //    new SendDX().Send2("我们已收到您的付款,感谢您的支持,半湿粮将会在24小时后统一配送。", add.Phone);
                            //}
                            order.OrderState = 2;
                            order.BankType = bank_type;
                            order.OutOrderNo = transaction_id;
                            order.FromUserName = openid;
                            nvbo.SavePetXXLOrder(order);
                            //if (order.yhqid != null && order.yhqid != 0)
                            //{

                            //}

                            try
                            {
                                OAauth_Log o = mss.GetOA(order.FromUserName);
                                if (o != null)
                                {
                                    Pet_JiuZhu_Info j = nvbo.GetJiuZhuInfo(order.Source.Value);
                                    if (j != null && (j.JZType == 1 || j.JZType == 2))
                                    {
                                        Pet_JiuZhu_Love l = new Pet_JiuZhu_Love
                                        {
                                            CreateTime = DateTime.Now,
                                            Detail = "捐助基地[" + j.NickName + "][" + (j.JZType == 1 ? "捐粮" : "捐款") + "]",
                                            FromUserName = order.FromUserName,
                                            OID = order.ID,
                                            Love = j.JZType == 1 ? Convert.ToInt32(order.TotalPrice) : Convert.ToInt32(order.TotalPrice / 2)
                                        };
                                        int lid = nvbo.SavePetJiuZhuLove(l);
                                        if (lid > 0)
                                        {
                                            o.TotalLove = (o.TotalLove ?? 0) + l.Love;
                                            o.SurLove = (o.SurLove ?? 0) + l.Love;
                                            mss.SaveOA(o);
                                        }
                                    }
                                }
                            }
                            catch (Exception)
                            {

                            }
                            try
                            {

                                string token = Token(mjuserid);
                                var temp = new
                                {
                                    first = new { value = "我们已收到您的微信付款", color = "#173177" },
                                    orderMoneySum = new { value = order.TotalPrice.ToString() + "元", color = "#173177" },
                                    orderProductName = new { value = order.GoodsName, color = "#173177" },
                                    Remark = new { value = "\n" + "我们已收到您的爱心。", color = "#CD0000" }
                                };
                                if (order.PJ != null && order.PJ != 0)
                                {
                                    temp = new
                                    {
                                        first = new { value = "我们已收到您的微信付款", color = "#173177" },
                                        orderMoneySum = new { value = order.TotalPrice.ToString() + "元", color = "#173177" },
                                        orderProductName = new { value = order.GoodsName, color = "#173177" },
                                        Remark = new { value = "\n" + "我们已收到您的爱心,您已成功捐粮" + Math.Round(order.TotalPrice.Value / 16, 2) + "kg,并且您的爱心被放大，由宠物管家赞助为其配捐" + Math.Round(order.PJ.Value / 16, 2) + "kg", color = "#CD0000" }
                                    };
                                }
                                string message = SendTemplateMessage(token, order.FromUserName, "qLJpWtjKj8zm3j8cwOe0u3u7qHwQ04cSx2UUvvVu-Ug", "#FF0000", temp, WebUrl + "/wechat/test/tz.aspx?tousername=" + mjuserid);


                                //string[] op = new[] { "o3L1MwYudOztZTDDA6yPIhWapZH4", "o3L1MwVjlBoenaSbN5FIXwzQmgNM","o3L1MwZ00yPDw1Ymsk_V4Por1ce4" };
                                //foreach (string o in op)
                                //{
                                //    var temp2 = new
                                //    {
                                //        first = new { value = "新订单提醒", color = "#173177" },
                                //        OrderSn = new { value = order.OrderNo, color = "#173177" },
                                //        OrderStatus = new { value = "已支付", color = "#173177" },
                                //        remark = new { value = "\n" + order.GoodsName + "," + order.TotalPrice.ToString() + "元", color = "#CD0000" }
                                //    };
                                //    message = SendTemplateMessage(token, o, "wY05AHjKkPAmyygWfzM0D1EWeGOrzCAZDI6c1jmGPGY", "#FF0000", temp2, WebUrl + "/wechat/test/tz.aspx?tousername=" + mjuserid);
                                //}

                            }
                            catch (Exception)
                            {

                            }
                            Response.Write(@"<xml>
  <return_code><![CDATA[SUCCESS]]></return_code>
  <return_msg><![CDATA[OK]]></return_msg>
</xml>");
                            Response.End();
                        }
                    }
                    //WXLOG log = new WXLOG { CON = postStr + "weixin", TIME = DateTime.Now };
                    //mss.SaveLog(log);
                }

            }
        }
    }
}