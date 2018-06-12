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
    public partial class DongTai : WeiPage
    {
        NewVerService nvbo = new NewVerService();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["para"] == null)
            {
                if (Request.Params["para"] == null)
                {
                    Response.Write(" <input type='hidden' value='" + Server.UrlEncode(AbsoluteUri) + "' id='url' />");
                }
            }
            else
            {
                string html = "";
                if ((Request.QueryString["FromUserName"] != null || Session["FromUserName"] != null) && (Request.QueryString["ToUserName"] != null || Session["ToUserName"] != null))
                {
                    string user = Request.QueryString["FromUserName"] == null ? Session["FromUserName"].ToString() : Request.QueryString["FromUserName"].ToString();
                    string user2 = Request.QueryString["ToUserName"] == null ? Session["ToUserName"].ToString() : Request.QueryString["ToUserName"].ToString();
                    List<Pet_JiuZhu_Info_EX> jlist = nvbo.GetAllJiuZhuNews(int.Parse(Request.Params["page"]),user);
                    foreach (Pet_JiuZhu_Info_EX o in jlist)
                    {
                        if (o.State == 3) //已筹款或筹粮结束
                            html += string.Format(@"<li>
       <div class=""aipet-dongtai-person"">
         <p><span>{8}</span><img src=""{1}"" width=""50"" height=""50""><strong>{2}</strong>{3}</p>
      </div>
      <a href=""detail.aspx?id={0}""><div class=""aipet-dongtai-chou"">
       <p><img src=""{7}"" width=""60"" height=""60""><strong>{4}</strong>
       <span>{5}人支持</span>
        <span>已筹{6}</span>
         <span>{3}</span>
       </p>
      </div>
      </a>
      </li>", o.ID, o.HeadImage, (o.NickName.Length > 15 ? o.NickName.Substring(0, 15) + "..." : o.NickName), o.FromUserName, o.Title, o.rc, o.JZType == 1 ? Math.Round(o.je.Value / 16, 2) + "Kg" : o.je + "元", WebUrl + o.CImg, o.CreateTime.Value.ToString("yyyy-MM-dd HH:mm"));
                        else
                            html += string.Format(@"<li>
    <div class=""aipet-dongtai-person"">
         <p><span>{7}</span><img src=""{1}"" width=""50"" height=""50""><strong>{2}</strong>{3}</p>
      </div>
      <a href=""detail.aspx?id={0}""><div class=""aipet-dongtai-news"">
         <strong>{4}</strong>
         <p>{5}</p>
      </div></a>
       <div class=""aipet-dongtai-imgbox"">
           {6}
         </div>
      </li>", o.ID, o.HeadImage, (o.NickName.Length > 15 ? o.NickName.Substring(0, 15) + "..." : o.NickName), o.FromUserName, o.Title, (o.Detail.Length > 50 ? o.Detail.Substring(0, 50) + "..." : o.Detail), GetImgs(o), o.CreateTime.Value.ToString("yyyy-MM-dd HH:mm"));
                    }
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
                                    <a href=""{1}"" data-lightbox=""example""><img data-echo=""{0}""></a>
                                </span>
                            </ol>", WebUrl + (o.SImg1 == null ? o.Img1 : o.SImg1), WebUrl + o.Img1);
            if (o.Img2 != null)
                html += string.Format(@" <ol>
                                <span>
                                    <a href=""{1}"" data-lightbox=""example""><img data-echo=""{0}""></a>
                                </span>
                            </ol>", WebUrl + (o.SImg2 == null ? o.Img2 : o.SImg2), WebUrl + o.Img1);
            if (o.Img3 != null)
                html += string.Format(@" <ol>
                                <span>
                                    <a href=""{1}"" data-lightbox=""example""><img data-echo=""{0}""></a>
                                </span>
                            </ol>", WebUrl + (o.SImg3 == null ? o.Img3 : o.SImg3), WebUrl + o.Img1);
            if (o.Img4 != null)
                html += string.Format(@" <ol>
                                <span>
                                    <a href=""{1}"" data-lightbox=""example""><img data-echo=""{0}""></a>
                                </span>
                            </ol>", WebUrl + (o.SImg4 == null ? o.Img4 : o.SImg4), WebUrl + o.Img1);
            return html;


        }
    }
}