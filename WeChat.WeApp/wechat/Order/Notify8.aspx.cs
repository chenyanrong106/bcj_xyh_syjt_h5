using SPACRM.Business.ServiceImpl;
using SPACRM.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SPACRM.WebApp.wechat.Order
{
    public partial class Notify8 : WeiPage
    {

        NewVerService nvbo = new NewVerService();
        MySmallShopService mss = new MySmallShopService();
        protected void Page_Load(object sender, EventArgs e)
        {
            string postStr = "";


            //else
            //{
            //WXLOG log = new WXLOG { CON = Request.Params[""] + "INDEX2", TIME = DateTime.Now };
            //mss.SaveLog(log);
            if (Request.Params["out_trade_no"] != null && Request.Params["trade_status"] != null)
            {
                if (Request.Params["trade_status"] == "TRADE_SUCCESS")
                {
                    Pet_XXL_Order order = nvbo.GetPetXXLOrder(Request.Params["out_trade_no"]);
                    if (order != null && order.OrderState != 2)
                    {
                        Pet_XXL_Address add = nvbo.GetAddress(order.AID.Value);
                        if (add != null)
                        {
                            new SendDX().Send2("我们已收到您的付款,感谢您的支持,半湿粮将会在24小时后统一配送。", add.Phone);
                        }
                        order.OrderState = 2;
                        order.BankType = "";
                        order.OutOrderNo = Request.Params["trade_no"];
                        nvbo.SavePetXXLOrder(order);

                        try
                        {
                            //InsertQR2(order.FromUserName, "o3L1MwfQMvX6DKzkc041wd-wShkU");//添加下线
                            string token = Token(mjuserid);
                            var temp = new
                            {
                                first = new { value = "我们已收到您的支付宝付款", color = "#173177" },
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
            }
            //WXLOG log2 = new WXLOG { CON = Request.Params.AllKeys + "zhifubao", TIME = DateTime.Now };
            //mss.SaveLog(log2);
            //string cs = "";
            //for (int i = 0; i < Request.Form.Count; i++)
            //{
            //    if (Request.Form.Keys[i].ToString().Substring(0, 1) != "_")
            //        cs += Request.Form.Keys[i].ToString() + " = " + Request.Form[i].ToString()+"&";
            //}
            //WXLOG log3 = new WXLOG { CON = cs + "zhifubao", TIME = DateTime.Now };
            //mss.SaveLog(log3);

            //}
        }
    }
}