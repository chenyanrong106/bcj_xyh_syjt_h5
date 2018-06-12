using SPACRM.Business.ServiceImpl;
using SPACRM.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WxPayAPI;

namespace SPACRM.WebApp.wechat.Order
{
    public partial class Test : System.Web.UI.Page
    {
        public string url = "";
        public decimal je;
        public string remark;
        public string orderno = "";
        NewVerService nvbo = new NewVerService();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["iid"] != null && Request.QueryString["txtnum"] != null && Request.QueryString["remark"] != null)
            {
                Pet_XXL_Order order = new Pet_XXL_Order();
                order.CreateTime = DateTime.Now;
                order.FromUserName = "";
                order.GoodsName = "爱宠筹 - 为爱筹";
                order.OrderNo = DateTime.Now.ToString("yyyyMMddHHmmssffff") + new Random().Next(1000, 9999) + Guid.NewGuid().ToString().Substring(0, 6);
                order.OrderState = 0;
                order.OutOrderNo = "";
                order.Source = int.Parse(Request.Params["iid"]);//来源 自己平台
                order.CatSource = 0;
                order.PetSource = 0;
                order.PayType = 3;
                order.UnitPrice = decimal.Parse(Request.Params["txtnum"]);
                order.TotalPrice = order.UnitPrice;
                order.CourierRemark = Request.Params["remark"] ?? "支持";
                order.Remark = "0";
                order.UrlPara = int.Parse(Request.QueryString["p"] ?? "1");
                order.iswx = int.Parse(Request.QueryString["iswx"]??"1");
                order.PJ = 0;
                Pet_JiuZhu_PeiJuan pj = nvbo.GetPeiJuan(DateTime.Now);
                Pet_JiuZhu_Info j = nvbo.GetJiuZhuInfo(int.Parse(Request.Params["iid"]));
                if (j != null && j.JZType == 1)
                {
                    if (pj != null && pj.YJ < pj.PJ * 16 && order.TotalPrice >= 10 && DateTime.Now.Hour >= 10) //配捐配置不为空，并且配捐额度未满，并且捐款金额大于10元,10点开始
                    {
                         Pet_JiuZhu_NotPeiJuan nt = nvbo.GetNotPeiJuan(j.ID, DateTime.Now.ToString("yyyy-MM-dd"));
                         if (nt == null) //没有配置不参与配捐
                         {
                             decimal cw = Math.Round(order.TotalPrice.Value / 6, 2); //宠物管家配捐金额
                             if (pj.YJ > pj.PJ * 16 / 2)//大于50%,采用随机的方式判断是否配捐
                             {
                                 Random r = new Random(DateTime.Now.Millisecond);
                                 int num = r.Next(0, 100);
                                 if (num % 2 == 0) //一半的概率配捐
                                 {
                                     order.PJ = cw;
                                 }
                             }
                             else
                             {
                                 order.PJ = cw;
                             }
                             if (order.PJ + pj.YJ > pj.PJ * 16) //如果配捐加上已捐大于额度，则配捐等于剩余额度
                             {
                                 order.PJ = pj.PJ * 16 - pj.YJ;
                             }
                         }

                    }
                }
                int oid = nvbo.SavePetXXLOrder(order);
                orderno = order.OrderNo;
                je = order.TotalPrice.Value;
                remark = order.CourierRemark;
              
                WxPayData data = new WxPayData();
                data.SetValue("body", order.GoodsName);//商品描述
                data.SetValue("attach", "扫描支付");//附加数据
                data.SetValue("out_trade_no", order.OrderNo);//随机字符串
                data.SetValue("total_fee", Convert.ToInt32(order.TotalPrice * 100));//总金额
                data.SetValue("time_start", DateTime.Now.ToString("yyyyMMddHHmmss"));//交易起始时间
                data.SetValue("time_expire", DateTime.Now.AddMinutes(10).ToString("yyyyMMddHHmmss"));//交易结束时间
                data.SetValue("goods_tag", "无");//商品标记
                data.SetValue("trade_type", "NATIVE");//交易类型
                data.SetValue("product_id", order.Source);//商品ID
                WxPayData result = new WxPayApi().UnifiedOrder(data);//调用统一下单接口
                url = result.GetValue("code_url") == null ? "" : result.GetValue("code_url").ToString();//获得统一下单接口返回的二维码链接
            }
            else if (Request.QueryString["cid"] != null && Request.QueryString["txtnum"] != null && Request.QueryString["remark"] != null)
            {
                Pet_XXL_Order order = new Pet_XXL_Order();
                order.CreateTime = DateTime.Now;
                order.FromUserName = "";
                order.GoodsName = "爱宠筹 - 为爱筹";
                order.OrderNo = DateTime.Now.ToString("yyyyMMddHHmmssffff") + new Random().Next(1000, 9999) + Guid.NewGuid().ToString().Substring(0, 6);
                order.OrderState = 0;
                order.OutOrderNo = "";
                order.CatSource = int.Parse(Request.Params["cid"]);//来源 自己平台
                order.Source = 0;
                order.PayType = 3;
                order.UnitPrice = decimal.Parse(Request.Params["txtnum"]);
                order.TotalPrice = order.UnitPrice;
                order.CourierRemark = Request.Params["remark"] ?? "支持";
                order.Remark = "0";
                order.UrlPara = int.Parse(Request.QueryString["p"] ?? "1");
                order.iswx = int.Parse(Request.QueryString["iswx"] ?? "1");
                order.PJ = 0;
                Pet_JiuZhu_PeiJuan pj = nvbo.GetPeiJuan(DateTime.Now);
                
                int oid = nvbo.SavePetXXLOrder(order);
                orderno = order.OrderNo;
                je = order.TotalPrice.Value;
                remark = order.CourierRemark;

                WxPayData data = new WxPayData();
                data.SetValue("body", order.GoodsName);//商品描述
                data.SetValue("attach", "扫描支付");//附加数据
                data.SetValue("out_trade_no", order.OrderNo);//随机字符串
                data.SetValue("total_fee", Convert.ToInt32(order.TotalPrice * 100));//总金额
                data.SetValue("time_start", DateTime.Now.ToString("yyyyMMddHHmmss"));//交易起始时间
                data.SetValue("time_expire", DateTime.Now.AddMinutes(10).ToString("yyyyMMddHHmmss"));//交易结束时间
                data.SetValue("goods_tag", "无");//商品标记
                data.SetValue("trade_type", "NATIVE");//交易类型
                data.SetValue("product_id", order.Source);//商品ID
                WxPayData result = new WxPayApi().UnifiedOrder(data);//调用统一下单接口
                url = result.GetValue("code_url") == null ? "" : result.GetValue("code_url").ToString();//获得统一下单接口返回的二维码链接
            }
            else if (Request.Params["orderno"] != null)
            {
                int num = 0;
                if (string.IsNullOrEmpty(Request.Params["orderno"]))
                {
                    Response.Write("{\"st\":" + num + "}");
                    Response.End();
                }
                else
                {
                    num = nvbo.GetOrderByoderNo(Request.Params["orderno"]);
                    Response.Write("{\"st\":" + num + "}");
                    Response.End();
                }
            }
        }
    }
}