using SPACRM.Business.ServiceImpl;
using SPACRM.Entity;
using SPACRM.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SPACRM.WebApp.wechat.Order
{
    public partial class Detail : WeiPage
    {
        NewVerService nvbo = new NewVerService();
        MySmallShopService mss = new MySmallShopService();
        //public int ordercount = 0;
        //public decimal orderprice = 0;
        public Pet_JiuZhu_Info j = new Pet_JiuZhu_Info { rc = 0, je = 0 };
        public List<Pet_JiuZhu_News> nlist = new List<Pet_JiuZhu_News>();
        public int isstar = 0;
        //public Pet_JiuZhu_PeiJuan pj = new Pet_JiuZhu_PeiJuan { PJ = 0, YJ = 0 };
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["para"] == null)
            {
                //HttpCookie cookie = Request.Cookies["cookiedtgohome"];
                //if (cookie == null)
                //{
                //    cookie = new HttpCookie("cookiedtgohome");
                //    cookie.Expires = DateTime.Now.AddDays(3);
                //    cookie.Name = "cookiedtgohome";
                //    cookie.Value = DateTime.Now.AddDays(3).ToString("yyyy.MM.dd");
                //    HttpContext.Current.Response.Cookies.Add(cookie);
                //    BaseLoad2();
                //}
                //else
                //{
                //    DateTime dt = DateTime.Parse(cookie.Value);
                //    if (dt < DateTime.Now)
                //    {
                //        cookie.Value = DateTime.Now.AddDays(3).ToString("yyyy.MM.dd");
                //        cookie.Expires = DateTime.Now.AddDays(3);
                //        HttpContext.Current.Response.Cookies.Add(cookie);
                //        BaseLoad2();
                //    }
                //    else
                //    {
                //        BaseLoad();
                //    }
                //}

                if (Request.Params["para"] == null)
                {
                    Response.Write(" <input type='hidden' value='" + Server.UrlEncode(AbsoluteUri) + "' id='url' />");

                }
                //ordercount = nvbo.GetOrderCount();
                //orderprice = nvbo.GetOrderTotalPrice();
                try
                {
                    if (Request.QueryString["id"] != null || Request.QueryString["state"] != null)
                    {
                        j = nvbo.GetJiuZhuInfo(int.Parse(Request.QueryString["id"] == null ? Request.QueryString["state"] : Request.QueryString["id"]));
                        if (j != null)
                        {
                            nlist = nvbo.GetJiuZhuNewsList(j.ID);
                            if ((Request.QueryString["FromUserName"] != null || Session["FromUserName"] != null) && (Request.QueryString["ToUserName"] != null || Session["ToUserName"] != null))
                            {
                                string user = Request.QueryString["FromUserName"] == null ? Session["FromUserName"].ToString() : Request.QueryString["FromUserName"].ToString();
                                string user2 = Request.QueryString["ToUserName"] == null ? Session["ToUserName"].ToString() : Request.QueryString["ToUserName"].ToString();
                                isstar = nvbo.GetIsStarJiuZhu(user, j.ID);
                            }
                        }
                        //pj = nvbo.GetPeiJuan(DateTime.Now);
                        //if (pj == null)
                        //    pj = new Pet_JiuZhu_PeiJuan { PJ = 0, YJ = 0 };
                    }
                    else
                    {
                        Response.Redirect("detail.aspx?id=1");
                    }
                }
                catch (Exception)
                {
                    Response.Redirect("detail.aspx?id=1");
                }


            }
            else if (Request.QueryString["para"] == "tj")
            {
                int jztype = int.Parse(Request.Params["jztype"]);
                List<OrderEX> list = nvbo.GetOrderList(int.Parse(Request.Params["page"]), int.Parse(Request.QueryString["iid"]));
                string html = "";
                //                if (int.Parse(Request.Params["page"]) == 1)
                //                    html = @"<li>
                //                            <p>
                //                                <img src=""http://SPACRM.meijiewd.com/assets/images/logo.jpg"">
                //                                小宠回家  支持了<strong>300千克</strong></p>
                //                            <p><bdo style=""width:100%;"">2016-08-25 17:03:25</bdo></p>
                //                            <p>
                //                                <bdo class=""txtinfo""  style=""width:100%;"">支持！</bdo>
                //                            </p>
                //                        </li>";
                foreach (OrderEX o in list)
                {
                    try
                    {
                        if (jztype == 1)
                            html += string.Format(@"<li>
                            <div class=""comconli""> <p>
                                <img src=""{0}"">
                                {1}  支持了<strong>{2}千克{5}</strong></p>
                            <p><bdo style=""width:95%;"">{3}</bdo></p>
                            <p>
                                <bdo class=""txtinfo""  style=""width:90%;"">{4}</bdo>
                            </p> </div>
<div class=""aipet-pinglun-box"" id=""aipet-pinglun-box{6}"">
           <div class=""pinglun-bar""><span onClick=""javascript:layerpl({6});"">回复</span></div>
           {7}
        </div>
                        </li>", o.headimgurl, o.Nickname + GetZFBName(o.buyer_logon_id, o.Nickname), Math.Round(o.TotalPrice / 16, 3), o.CreateTime.ToString("yyyy-MM-dd HH:mm:ss"), o.CourierRemark == "" ? "支持" : ReplaceKeyWord(o.CourierRemark), o.pj == 0 ? "" : "(获得配捐" + Math.Round(o.pj / 16, 2) + "kg)", o.ID, GetPL(o.ID));
                        else if (jztype == 2)
                            html += string.Format(@"<li>
                            <div class=""comconli""><p>
                                <img src=""{0}"">
                                {1}  支持了<strong>{2}元</strong></p>
                            <p><bdo style=""width:95%;"">{3}</bdo></p>
                            <p>
                                <bdo class=""txtinfo""  style=""width:90%;"">{4}</bdo>
                            </p></div>
<div class=""aipet-pinglun-box"" id=""aipet-pinglun-box{6}"">
           <div class=""pinglun-bar""><span onClick=""javascript:layerpl({5});"">回复</span></div>
            {6}
        </div>
                        </li>", o.headimgurl, o.Nickname + GetZFBName(o.buyer_logon_id, o.Nickname), o.TotalPrice, o.CreateTime.ToString("yyyy-MM-dd HH:mm:ss"), o.CourierRemark == "" ? "支持" : ReplaceKeyWord(o.CourierRemark), o.ID, GetPL(o.ID));
                    }
                    catch (Exception)
                    {
                       
                    }
                }

                if (list.Count == 0)
                    html = "";
                Response.Write(html);
                Response.End();
            }
            else if (Request.QueryString["para"] == "star")
            {
                if ((Request.QueryString["FromUserName"] != null || Session["FromUserName"] != null) && (Request.QueryString["ToUserName"] != null || Session["ToUserName"] != null))
                {
                    string user = Request.QueryString["FromUserName"] == null ? Session["FromUserName"].ToString() : Request.QueryString["FromUserName"].ToString();
                    string user2 = Request.QueryString["ToUserName"] == null ? Session["ToUserName"].ToString() : Request.QueryString["ToUserName"].ToString();
                    int num = nvbo.GetIsStarJiuZhu(user, int.Parse(Request.QueryString["jid"]));
                    if (num > 0)
                    {
                        nvbo.DeleteJiuZhuStar(user, int.Parse(Request.QueryString["jid"]));
                        Response.Write("{\"st\":2}");
                    }
                    else
                    {
                        nvbo.SaveJiuZhuStar(new Pet_JiuZhu_Star { CreateTime = DateTime.Now, FromUserName = user, JID = int.Parse(Request.QueryString["jid"]) });
                        Response.Write("{\"st\":1}");
                    }
                }
                else
                {
                    Response.Write("{\"st\":3}");
                }
                Response.End();
            }
            else if (Request.QueryString["para"] == "pl")
            {
                if ((Request.QueryString["FromUserName"] != null || Session["FromUserName"] != null) && (Request.QueryString["ToUserName"] != null || Session["ToUserName"] != null))
                {
                    string user = Request.QueryString["FromUserName"] == null ? Session["FromUserName"].ToString() : Request.QueryString["FromUserName"].ToString();
                    string user2 = Request.QueryString["ToUserName"] == null ? Session["ToUserName"].ToString() : Request.QueryString["ToUserName"].ToString();
                    int num = nvbo.IsHaveOrderByOpenid(user, int.Parse(Request.QueryString["jid"]));
                    Pet_JiuZhu_Info pj = nvbo.GetJiuZhuInfo(int.Parse(Request.QueryString["jid"]));
                    if (num == 0 && pj.FromUserName != user && user != "oloJGv4lWL-TS8yn8uo4Fu1rbVTI" && user != "oloJGvzwelDQjIKMMWC8Z6ngM7gk" && user != "oloJGvx3hLkCNadLGIpAMimz2Xwc")
                    {
                        Response.Write("{\"st\":2,\"msg\":\"你没有支持该项目，不能评论。\"}");
                    }
                    else
                    {
                        OAauth_Log o = mss.GetOA(user);
                        if (o != null)
                        {
                            nvbo.SavePetComment(new Pet_JiuZhu_comment { CreateTime = DateTime.Now, Detail = Request.Params["detail"], FromUserName = user, OID = int.Parse(Request.Params["oid"]), HeadImage = o.headimgurl, NiceName = o.Nickname });
                            Response.Write("{\"st\":1,\"msg\":\"<p><strong style='color:#689cc3;'>" + o.Nickname + "：</strong>" + Request.Params["detail"] + "</p>\"}");
                        }
                        else
                        {
                            Response.Write("{\"st\":2,\"msg\":\"异常\"}");
                        }
                    }
                }
                else
                {
                    Response.Write("{\"st\":3}");
                }
                Response.End();
            }
        }

        private string GetZFBName(string zfb, string Nickname)
        {
            string name = "";
            if (string.IsNullOrEmpty(zfb) || zfb.Length < 8)
            {
                return name;
            }
            else if (Nickname != "支付宝用户")
            {
                return name;
            }
            else
            {
                int num = zfb.IndexOf("@");
                if (num == -1)
                    return zfb.Substring(0, zfb.Length / 2) + "****";
                else
                    return zfb.Substring(0, num) + "****";
            }
        }

        /// <summary>
        /// 获取评论列表
        /// </summary>
        /// <param name="oid"></param>
        /// <returns></returns>
        private string GetPL(int oid)
        {
            string html = "";
            List<Pet_JiuZhu_comment> list = new List<Pet_JiuZhu_comment>();
            list = nvbo.GetPetCommentList(oid);
            foreach (var c in list)
            {
                html += "<p><strong style='color:#689cc3;'>" + c.NiceName + "：</strong>" + "[" + c.CreateTime.Value.ToString("yyyy.MM.dd HH:mm") + "]" + ReplaceKeyWord(c.Detail) + "</p>";
            }
            return html;
        }
    }
}