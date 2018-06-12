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
    public partial class Notify7 : WeiPage
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
                        Pet_XXL_Order order = nvbo.GetPetXXLOrder(orderno);
                        if (order != null && order.OrderState != 2)
                        {
                            Pet_XXL_Address add = nvbo.GetAddress(order.AID.Value);
                            if (add != null)
                            {
                                new SendDX().Send2("我们已收到您的付款,感谢您的支持,半湿粮将会在24小时后统一配送。", add.Phone);
                            }
                            order.OrderState = 2;
                            order.BankType = bank_type;
                            order.OutOrderNo = transaction_id;
                            nvbo.SavePetXXLOrder(order);
                            if (order.yhqid != null && order.yhqid != 0)
                            {
                               
                            }
                            try
                            {
                                //InsertQR2(order.FromUserName, "o3L1MwfQMvX6DKzkc041wd-wShkU");//添加下线
                                string token = Token(mjuserid);
                                var temp = new
                                {
                                    first = new { value = "我们已收到您的微信付款", color = "#173177" },
                                    orderMoneySum = new { value = order.TotalPrice.ToString() + "元", color = "#173177" },
                                    orderProductName = new { value = order.GoodsName, color = "#173177" },
                                    Remark = new { value = "\n" + "我们已收到您的付款,感谢您的支持。", color = "#CD0000" }
                                };
                                string message = SendTemplateMessage(token, order.FromUserName, "qLJpWtjKj8zm3j8cwOe0u3u7qHwQ04cSx2UUvvVu-Ug", "#FF0000", temp, WebUrl + "/wechat/test/tz.aspx?tousername=" + mjuserid);


                                string[] op = new[] { "o3L1MwfQMvX6DKzkc041wd-wShkU", "o3L1MwYudOztZTDDA6yPIhWapZH4", "o3L1MwVjlBoenaSbN5FIXwzQmgNM", "o3L1MwZ00yPDw1Ymsk_V4Por1ce4" };
                                foreach (string o in op)
                                {
                                    var temp2 = new
                                    {
                                        first = new { value = "新订单提醒", color = "#173177" },
                                        OrderSn = new { value = order.OrderNo, color = "#173177" },
                                        OrderStatus = new { value = "已支付", color = "#173177" },
                                        remark = new { value = "\n" + order.GoodsName + "(宠物帮)," + order.TotalPrice.ToString() + "元", color = "#CD0000" }
                                    };
                                    message = SendTemplateMessage(token, o, "wY05AHjKkPAmyygWfzM0D1EWeGOrzCAZDI6c1jmGPGY", "#FF0000", temp2, WebUrl + "/wechat/test/tz.aspx?tousername=" + mjuserid);
                                }

                            }
                            catch (Exception)
                            {

                            }
                            Response.Write("success");
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