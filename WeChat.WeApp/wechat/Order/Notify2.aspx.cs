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
    public partial class Notify2 : WeiPage
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
                        //Pet_XXL_Address add = nvbo.GetAddress(order.AID.Value);
                        //if (add != null)
                        //{
                        //    new SendDX().Send2("我们已收到您的付款,感谢您的支持,半湿粮将会在24小时后统一配送。", add.Phone);
                        //}
                        order.OrderState = 2;
                        order.BankType = "";
                        order.OutOrderNo = Request.Params["trade_no"];
                        if (Request.Params["buyer_logon_id"] != null)
                            order.buyer_logon_id = Request.Params["buyer_logon_id"];
                        else if (Request.Params["buyer_email"] != null)
                            order.buyer_logon_id = Request.Params["buyer_email"];
                        nvbo.SavePetXXLOrder(order);
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
                                first = new { value = "我们已收到您的支付宝付款", color = "#173177" },
                                orderMoneySum = new { value = order.TotalPrice.ToString() + "元", color = "#173177" },
                                orderProductName = new { value = order.GoodsName, color = "#173177" },
                                Remark = new { value = "\n" + "我们已收到您的爱心。", color = "#CD0000" }
                            };
                            if (order.PJ != null && order.PJ != 0)
                            {
                                temp = new
                                {
                                    first = new { value = "我们已收到您的支付宝付款", color = "#173177" },
                                    orderMoneySum = new { value = order.TotalPrice.ToString() + "元", color = "#173177" },
                                    orderProductName = new { value = order.GoodsName, color = "#173177" },
                                    Remark = new { value = "\n" + "我们已收到您的爱心,您已成功捐粮" + Math.Round(order.TotalPrice.Value / 16, 2) + "kg,并且您的爱心被放大，由宠物管家赞助为其配捐" + Math.Round(order.PJ.Value / 16, 2) + "kg", color = "#CD0000" }
                                };
                            }
                            string message = SendTemplateMessage(token, order.FromUserName, "qLJpWtjKj8zm3j8cwOe0u3u7qHwQ04cSx2UUvvVu-Ug", "#FF0000", temp, WebUrl + "/wechat/test/tz.aspx?tousername=" + mjuserid);


                            //string[] op = new[] { "o3L1MwYudOztZTDDA6yPIhWapZH4", "o3L1MwVjlBoenaSbN5FIXwzQmgNM", "o3L1MwZ00yPDw1Ymsk_V4Por1ce4" };
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