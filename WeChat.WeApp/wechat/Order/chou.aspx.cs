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
    public partial class chou : WeiPage
    {
        NewVerService nvbo = new NewVerService();
        public int ordercount = 0;
        public decimal orderprice = 0;
        //public List<Pet_JiuZhu_Info> jlist = new List<Pet_JiuZhu_Info>();
        public Pet_JiuZhu_PeiJuan pj = new Pet_JiuZhu_PeiJuan { PJ = 0, YJ = 0 };
        public List<Pet_JiuZhu_Info_EX> njlist = new List<Pet_JiuZhu_Info_EX>();
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
                ordercount = nvbo.GetOrderCount();
                orderprice = nvbo.GetOrderTotalPrice();
                pj = nvbo.GetPeiJuan(DateTime.Now);
                if (pj == null)
                    pj = new Pet_JiuZhu_PeiJuan { PJ = 0, YJ = 0 };
                njlist = nvbo.GetNewJiuZhuInfoList(1);
                //jlist = nvbo.GetJiuZhuInfoList();
            }
            else
            {
                string html = "";
                List<Pet_JiuZhu_Info_EX> jlist = nvbo.GetJiuZhuInfoList(int.Parse(Request.Params["page"]));
                foreach (Pet_JiuZhu_Info_EX o in jlist)
                {
                    if (o.JZType == 1)
                        html += string.Format(@"<li onclick=""window.location='detail.aspx?id={0}&p={12}'"">
                        <div class=""adminInfo"">
                            <p>
                                <a href=""#""><span class=""jinxingzhong"">{3}</span></a><bdo class=""touxiang"">
                                    <img src=""{1}""></bdo><bdo>{2}</bdo><!--<bdo class=""timecss"">{13}</bdo>-->
                            </p>
                        </div>
                        <div class=""shopTT"">
                            <h2>{4}</h2>
                            <p class=""upjiantou"">{5}</p>
                        </div>
                        <div class=""shopimgbox"">
                           {10}
                        </div>
                        <div class=""adminInfo"">
                            <p><span>已有{6}人支持</span>
<em class=""kuai"">{14}</em>
</p>
                        </div>
                        <div class=""choutag"">
                            <ol><span class=""mubiao"">目标:<b>{7}</b>千克</span></ol>
                            <ol><span class=""jine"">已筹:<b>{8}</b>千克</span></ol>
                            <ol><span class=""jindu"">进度:<b {11}>{9}</b>%</span></ol>
                        </div>
                    </li>", o.ID, o.HeadImage, (o.NickName.Length > 15 ? o.NickName.Substring(0, 15) + "..." : o.NickName), (o.EndTime > DateTime.Now ? "进行中" : "已结束"), o.Title, (o.Detail.Length > 50 ? o.Detail.Substring(0, 50) + "..." : o.Detail), o.rc, o.Goal, Math.Round(o.je.Value / 16, 2), Math.Round(o.je.Value / 16 / o.Goal.Value * 100m, 2), GetImgs(o), o.je / 16 > o.Goal ? "style='color:red;'" : "", Request.QueryString["p"], o.CreateTime.Value.ToString("yyyy.MM.dd"),o.JZType==1?"#筹粮":"#筹款");

//                        html += string.Format(@"<li onclick=""window.location='detail.aspx?id={0}&p={12}'"">
//                        <div class=""adminInfo"">
//                            <p>
//                                <a href=""#""><span class=""daizi"">{3}</span></a><bdo class=""touxiang"">
//                                    <img src=""{1}""></bdo>{2}
//                            </p>
//                        </div>
//                        <div class=""shopTT"">
//                            <h2>{4}</h2>
//                            <p>{5}</p>
//                        </div>
//                        <div class=""shopimgbox"">
//                           {10}
//                        </div>
//                        <div class=""adminInfo"">
//                            <p><span>已有{6}人支持</span><bdo class=""zhiding"">置顶</bdo></p>
//                        </div>
//                        <div class=""choutag"">
//                            <ol><span class=""mubiao"">目标:<b>{7}</b>千克</span></ol>
//                            <ol><span class=""jine"">已筹:<b>{8}</b>千克</span></ol>
//                            <ol><span class=""jindu"">进度:<b {11}>{9}</b>%</span></ol>
//                        </div>
//                    </li>", o.ID, o.HeadImage, o.NickName, (o.EndTime > DateTime.Now ? "进行中" : "已结束"), o.Title, (o.Detail.Length > 50 ? o.Detail.Substring(0, 50) + "..." : o.Detail), o.rc, o.Goal, Math.Round(o.je.Value / 16, 2), Math.Round(o.je.Value / 16 / o.Goal.Value * 100m, 2), GetImgs(o), o.je / 16 > o.Goal ? "style='color:red;'" : "", Request.QueryString["p"]);


                    else if (o.JZType == 2)
                        html += string.Format(@"<li onclick=""window.location='detail.aspx?id={0}&p={12}'"">
                                            <div class=""adminInfo"">
                                                <p>
                                                    <a href=""#""><span class=""daizi"">{3}</span></a><bdo class=""touxiang"">
                                                        <img src=""{1}""></bdo><bdo>{2}</bdo><!--<bdo class=""timecss"">{13}</bdo>-->
                                                </p>
                                            </div>
                                            <div class=""shopTT"">
                                                <h2>{4}</h2>
                                                <p>{5}</p>
                                            </div>
                                            <div class=""shopimgbox"">
                                               {10}
                                            </div>
                                            <div class=""adminInfo"">
                                                <p><span>已有{6}人支持</span>
<em class=""kuai"">{14}</em>
</p>
                                            </div>
                                            <div class=""choutag"">
                                                <ol><span class=""mubiao"">目标:<b>{7}</b>元</span></ol>
                                                <ol><span class=""jine"">已筹:<b>{8}</b>元</span></ol>
                                                <ol><span class=""jindu"">进度:<b {11}>{9}</b>%</span></ol>
                                            </div>
                                        </li>", o.ID, o.HeadImage, (o.NickName.Length > 15 ? o.NickName.Substring(0, 15) + "..." : o.NickName), (o.EndTime > DateTime.Now ? "进行中" : "已结束"), o.Title, (o.Detail.Length > 50 ? o.Detail.Substring(0, 50) + "..." : o.Detail), o.rc, o.Goal, o.je.Value, Math.Round(o.je.Value / o.Goal.Value * 100m, 2), GetImgs(o), o.je > o.Goal ? "style='color:red;'" : "", Request.QueryString["p"], o.CreateTime.Value.ToString("yyyy.MM.dd"), o.JZType == 1 ? "#筹粮" : "#筹款");
                }
                Response.Write(html);
                Response.End();
            }
        }

        private string GetImgs(Pet_JiuZhu_Info o)
        {
            string html = "";
            if (o.Img1 != null)
                html += string.Format(@" <ol>
                                <span>
                                    <img data-echo=""{0}"">
                                </span>
                            </ol>", WebUrl + (o.SImg1==null?o.Img1:o.SImg1));
            if (o.Img2 != null)
                html += string.Format(@" <ol>
                                <span>
                                    <img data-echo=""{0}"">
                                </span>
                            </ol>", WebUrl + (o.SImg2 == null ? o.Img2 : o.SImg2));
            if (o.Img3 != null)
                html += string.Format(@" <ol>
                                <span>
                                    <img data-echo=""{0}"">
                                </span>
                            </ol>", WebUrl + (o.SImg3 == null ? o.Img3 : o.SImg3));
            if (o.Img4 != null)
                html += string.Format(@" <ol>
                                <span>
                                    <img data-echo=""{0}"">
                                </span>
                            </ol>", WebUrl + (o.SImg4 == null ? o.Img4 : o.SImg4));
            return html;

//            string html = "";
//            if (o.Img1 != null)
//                html += string.Format(@" <ol>
//                                <span>
//                                    <img src=""{0}"">
//                                </span>
//                            </ol>", WebUrl + o.Img1);
//            if (o.Img2 != null)
//                html += string.Format(@" <ol>
//                                <span>
//                                    <img src=""{0}"">
//                                </span>
//                            </ol>", WebUrl + o.Img2);
//            if (o.Img3 != null)
//                html += string.Format(@" <ol>
//                                <span>
//                                    <img src=""{0}"">
//                                </span>
//                            </ol>", WebUrl + o.Img3);
//            if (o.Img4 != null)
//                html += string.Format(@" <ol>
//                                <span>
//                                    <img src=""{0}"">
//                                </span>
//                            </ol>", WebUrl + o.Img4);
//            return html;

        }
    }
}