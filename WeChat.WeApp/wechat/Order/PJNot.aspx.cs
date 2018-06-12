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
    public partial class PJNot : WeiPage
    {
        NewVerService nvbo = new NewVerService();
        //public List<Pet_JiuZhu_PeiJuan> list = new List<Pet_JiuZhu_PeiJuan>();
        public List<Pet_JiuZhu_Info> jzlist = new List<Pet_JiuZhu_Info>();
        public List<Pet_JiuZhu_NotPeiJuan> ntlist = new List<Pet_JiuZhu_NotPeiJuan>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["para"] != null && Request.QueryString["para"] == "tj")
            {
                Pet_JiuZhu_PeiJuan j = new Pet_JiuZhu_PeiJuan
                {
                    PJ = int.Parse(Request.Params["pj"]),
                    PJDate = DateTime.Parse(Request.Params["rq"]),
                    YJ = 0
                };

                Pet_JiuZhu_PeiJuan j2 = nvbo.GetPeiJuan(j.PJDate.Value);
                int id = 0;
                if (j2 != null)
                {
                    j2.PJDate = j.PJDate;
                    id = nvbo.SavePetJiuZhuPeiJuan(j2);
                }
                else
                {
                    id = nvbo.SavePetJiuZhuPeiJuan(j);
                }
                if (id > 0)
                {
                    Response.Write("{\"msg\":\"成功\",\"st\":\"1\",\"url\":\"\"}");
                    Response.End();
                }
                else
                {
                    Response.Write("{\"msg\":\"失败\",\"st\":\"-1\",\"url\":\"\"}");
                    Response.End();
                }
                //}
                //}
            }
            else if (Request.QueryString["para"] != null && Request.QueryString["para"] == "pj")
            {
                int id = nvbo.SaveJiuZhuPeiJuan(int.Parse(Request.Params["xmid"]), int.Parse(Request.Params["pjzl"]) * 16);
                if (id > 0)
                {
                    Response.Write("{\"msg\":\"成功\",\"st\":\"1\",\"url\":\"\"}");
                    Response.End();
                }
                else
                {
                    Response.Write("{\"msg\":\"失败\",\"st\":\"-1\",\"url\":\"\"}");
                    Response.End();
                }
            }
            else if (Request.QueryString["para"] != null && Request.QueryString["para"] == "ntpj")
            {
                int id = nvbo.SaveNotPeiJuan(new Pet_JiuZhu_NotPeiJuan { JID = int.Parse(Request.Params["xmid"]), Time = DateTime.Parse(Request.Params["time"]), JNickName = Request.Params["jd"] });
                if (id > 0)
                {
                    Response.Write("{\"msg\":\"成功\",\"st\":\"1\",\"url\":\"\"}");
                    Response.End();
                }
                else
                {
                    Response.Write("{\"msg\":\"失败\",\"st\":\"-1\",\"url\":\"\"}");
                    Response.End();
                }
            }
            else if (Request.QueryString["para"] != null && Request.QueryString["para"] == "dt")
            {
                Pet_JiuZhu_Info j = nvbo.GetJiuZhuInfo(int.Parse(Request.Params["xmid"]));
                if (j != null)
                {
                    string[] pics = Request.Params["pic"].Split('*');
                    Pet_JiuZhu_News n = new Pet_JiuZhu_News
                    {
                        CreateTime = DateTime.Now,
                        Detail = Request.Params["dtxq"],
                        HeadImage = j.HeadImage,
                        IID = j.ID,
                        NickName = j.NickName
                    };
                    if (pics.Length > 1)
                        n.Img1 = pics[0];
                    if (pics.Length > 2)
                        n.Img2 = pics[1];
                    if (pics.Length > 3)
                        n.Img3 = pics[2];
                    if (pics.Length > 4)
                        n.Img4 = pics[3];
                    if (pics.Length > 5)
                        n.Img5 = pics[4];
                    if (pics.Length > 6)
                        n.Img6 = pics[5];
                    if (pics.Length > 7)
                        n.Img7 = pics[6];
                    if (pics.Length > 8)
                        n.Img8 = pics[7];
                    if (n.Img1 != null && n.SImg1 == null)
                        n.SImg1 = CutImage2(n.Img1, 2, j.SImg1);
                    if (n.Img2 != null && n.SImg2 == null)
                        n.SImg2 = CutImage2(n.Img2, 2, j.SImg2);
                    if (n.Img3 != null && n.SImg3 == null)
                        n.SImg3 = CutImage2(n.Img3, 2, j.SImg3);
                    if (n.Img4 != null && n.SImg4 == null)
                        n.SImg4 = CutImage2(n.Img4, 2, j.SImg4);
                    if (n.Img5 != null && n.SImg5 == null)
                        n.SImg5 = CutImage2(n.Img5, 2, j.SImg5);
                    if (n.Img6 != null && n.SImg6 == null)
                        n.SImg6 = CutImage2(n.Img6, 2, j.SImg6);
                    if (n.Img7 != null && n.SImg7 == null)
                        n.SImg7 = CutImage2(n.Img7, 2, j.SImg7);
                    if (n.Img8 != null && n.SImg8 == null)
                        n.SImg8 = CutImage2(n.Img8, 2, j.SImg8);
                    int id = nvbo.SaveJiuZhuNews(n);
                    if (id > 0)
                    {
                        Response.Write("{\"msg\":\"成功\",\"st\":\"1\",\"url\":\"\"}");
                        Response.End();
                    }
                    else
                    {
                        Response.Write("{\"msg\":\"失败\",\"st\":\"-1\",\"url\":\"\"}");
                        Response.End();
                    }
                }
            }
            else
            {
                //list = nvbo.GetPeiJuanList();
                jzlist = nvbo.GetJiuZhuNotEndTitleList();
                ntlist = nvbo.GetNotPeiJuanList();
            }
        }
    }
}