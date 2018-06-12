using SPACRM.Business.ServiceImpl;
using SPACRM.Entity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SPACRM.WebApp.wechat.Order
{
    public partial class sq3 : WeiPage
    {
        CommonService _mservice = new CommonService();
        NewVerService nvbo = new NewVerService();
        MySmallShopService mss = new MySmallShopService();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["para"] == null && Request.Files.Count == 0)
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
                //if ((Request.QueryString["FromUserName"] != null || Session["FromUserName"] != null) && (Request.QueryString["ToUserName"] != null || Session["ToUserName"] != null))
                //{
                //    string user = Request.QueryString["FromUserName"] == null ? Session["FromUserName"].ToString() : Request.QueryString["FromUserName"].ToString();
                //    string user2 = Request.QueryString["ToUserName"] == null ? Session["ToUserName"].ToString() : Request.QueryString["ToUserName"].ToString();
                //    Session["FromUserName"] = user;
                //    Session["ToUserName"] = user2;
                //    Pet_JiuZhu_ShenQing sq = nvbo.GetJiuZhuShenQing(user);
                //    if (sq == null)
                //    {
                //        Response.Redirect("sq1.aspx");
                //    }
                //}
            }
            else if (Request.QueryString["para"] == null && Request.Files.Count > 0 && (Request.Files[0].InputStream.Length > 0 || (Request["pic"] != null && Request["Pic"].Trim() != "")))
            {
                HttpPostedFile file = Request.Files[0];
                string Extension = Path.GetExtension(file.FileName).ToLower();

                byte[] data = new byte[file.InputStream.Length];
                file.InputStream.Read(data, 0, data.Length);
                string url = "/home/ViewImage.do";
                string remark = "";// Request["filename"];
                FILES fileEntity = _mservice.UploadFile(Extension, file.ContentType, data, url, remark);
                fileEntity.REMARK = ConfigurationSettings.AppSettings["WebUrl"] + fileEntity.FILE_URL;

                Response.Write("{\"url\":\"" + fileEntity.REMARK + "\",\"id\":\"" + fileEntity.ID + "\",\"d\":\"" + fileEntity.FILE_URL + "\"}");
                Response.End();
            }
            else if (Request.QueryString["para"] != null && Request.QueryString["para"] == "delete")
            {
                FILES f = _mservice.GetUploadFile(int.Parse(Request.QueryString["id"]));
                if (f != null)
                {
                    File.Delete(f.FILE_NAME);
                    _mservice.DeleteFiles(f.ID);
                }
                Response.Write("ok");
                Response.End();
            }
            else if (Request.QueryString["para"] != null && Request.QueryString["para"] == "tj")
            {
                //if ((Request.QueryString["FromUserName"] != null || Session["FromUserName"] != null) && (Request.QueryString["ToUserName"] != null || Session["ToUserName"] != null))
                //{
                //    string user = Request.QueryString["FromUserName"] == null ? Session["FromUserName"].ToString() : Request.QueryString["FromUserName"].ToString();
                //    string user2 = Request.QueryString["ToUserName"] == null ? Session["ToUserName"].ToString() : Request.QueryString["ToUserName"].ToString();
                //    Session["FromUserName"] = user;
                //    Session["ToUserName"] = user2;
                string[] pics = Request.Params["pic"].Split('*');
                string[] pics2 = Request.Params["pic2"].Split('*');
                //Pet_JiuZhu_ShenQing sq = nvbo.GetJiuZhuShenQing(user);
                //OAauth_Log o = mss.GetOA(user);
                //if (sq != null && o != null)
                //{
                Pet_JiuZhu_Info j = new Pet_JiuZhu_Info
                {
                    CreateTime = DateTime.Now,
                    Detail = Request.Params["xq"],
                    EndTime =DateTime.Parse(Request.Params["endtime"]),
                    FromUserName = "",
                    Goal = int.Parse(Request.Params["mb"]),
                   
                    NickName = Request.Params["jd"],
                    State = 0,
                    PX = 0,
                    RD = 0,
                    Title = Request.Params["bt"],
                    JZType = int.Parse(Request.Params["jztype"]),
                    fqr=Request.Params["fqr"],
                    jsr=Request.Params["jsr"]
                };
                if (pics.Length > 1)
                    j.Img1 = pics[0];
                if (pics.Length > 2)
                    j.Img2 = pics[1];
                if (pics.Length > 3)
                    j.Img3 = pics[2];
                if (pics.Length > 4)
                    j.Img4 = pics[3];
                if (pics.Length > 5)
                    j.Img5 = pics[4];
                if (pics.Length > 6)
                    j.Img6 = pics[5];
                if (pics.Length > 7)
                    j.Img7 = pics[6];
                if (pics.Length > 8)
                    j.Img8 = pics[7];
                if (pics2.Length > 1)
                    j.HeadImage = WebUrl + pics2[0];
                int id = nvbo.SaveJiuZhuInfo(j);
                //j.PX = 100 + id;
                //j.ID = id;
                //nvbo.SaveJiuZhuInfo(j);
                //if (j.JZType == 1)
                //    nvbo.Save50Order(id);
                if (id > 0)
                {
                    try
                    {
                        j.ID = id;
                        Task.Factory.StartNew(new Action(() =>
                        {
                            j.CImg = CutImage2(j.Img1, 1, j.CImg, true, false); //截一张长图，八张方图，一张logo
                            j.SImg1 = CutImage2(j.Img1, 2, j.SImg1);
                            j.SImg2 = CutImage2(j.Img2, 2, j.SImg2);
                            j.SImg3 = CutImage2(j.Img3, 2, j.SImg3);
                            j.SImg4 = CutImage2(j.Img4, 2, j.SImg4);
                            j.SImg5 = CutImage2(j.Img5, 2, j.SImg5);
                            j.SImg6 = CutImage2(j.Img6, 2, j.SImg6);
                            j.SImg7 = CutImage2(j.Img7, 2, j.SImg7);
                            j.SImg8 = CutImage2(j.Img8, 2, j.SImg8);
                            string himg = CutImage2(j.HeadImage, 2, j.HeadImage, true, false);
                            if(himg!=j.HeadImage&&himg!="")
                            j.HeadImage = WebUrl + himg;
                            nvbo.SaveJiuZhuInfo(j);
                        }));
                    }
                    catch (Exception)
                    {

                    }
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
        }
    }
}