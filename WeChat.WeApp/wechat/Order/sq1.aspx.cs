using SPACRM.Business.ServiceImpl;
using SPACRM.Entity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SPACRM.WebApp.wechat.Order
{
    public partial class sq1 : WeiPage
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
                if ((Request.QueryString["FromUserName"] != null || Session["FromUserName"] != null) && (Request.QueryString["ToUserName"] != null || Session["ToUserName"] != null))
                {
                    string user = Request.QueryString["FromUserName"] == null ? Session["FromUserName"].ToString() : Request.QueryString["FromUserName"].ToString();
                    string user2 = Request.QueryString["ToUserName"] == null ? Session["ToUserName"].ToString() : Request.QueryString["ToUserName"].ToString();
                    Session["FromUserName"] = user;
                    Session["ToUserName"] = user2;
                    OAauth_Log oa = mss.GetOA(user);
                    if (oa != null && string.IsNullOrEmpty(oa.Phone))
                    {
                        Response.Redirect("bangphone.aspx?beforeurl2=" +AbsoluteUri.Replace("&", "*"));
                    }
                    else
                    {
                        Pet_JiuZhu_ShenQing sq = nvbo.GetJiuZhuShenQing(user);
                        if (sq != null)
                        {
                            Response.Redirect("sq2.aspx");
                        }
                    }
                }
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
                if ((Request.QueryString["FromUserName"] != null || Session["FromUserName"] != null) && (Request.QueryString["ToUserName"] != null || Session["ToUserName"] != null))
                {
                    string user = Request.QueryString["FromUserName"] == null ? Session["FromUserName"].ToString() : Request.QueryString["FromUserName"].ToString();
                    string user2 = Request.QueryString["ToUserName"] == null ? Session["ToUserName"].ToString() : Request.QueryString["ToUserName"].ToString();
                    Session["FromUserName"] = user;
                    Session["ToUserName"] = user2;
                    string[] pics1 = Request.Params["pic1"].Split('*');
                    string[] pics2 = Request.Params["pic2"].Split('*');
                    Pet_JiuZhu_ShenQing sq = new Pet_JiuZhu_ShenQing 
                    {
                    CardNo=Request.Params["cardno"],
                    CreateTime=DateTime.Now,
                    FromUserName=user,
                    JDName=Request.Params["jdname"],
                    Name=Request.Params["name"],
                    Phone=Request.Params["phone"],
                    WXNo=Request.Params["wxno"],
                    State=0
                    };
                    if (pics1.Length > 1)
                        sq.JDImg1 = pics1[0];
                    if (pics1.Length > 2)
                        sq.JDImg2 = pics1[1];
                    if (pics1.Length > 3)
                        sq.JDImg3 = pics1[2];
                    if (pics1.Length > 4)
                        sq.JDImg4 = pics1[3];
                    if (pics1.Length > 5)
                        sq.JDImg5 = pics1[4];
                    if (pics2.Length > 1)
                        sq.CardNoImg1 = pics2[0];
                    if (pics2.Length > 2)
                        sq.CardNoImg2 = pics2[1];
                   int id= nvbo.SaveJiuZhuShenQing(sq);
                   if (id > 0)
                   {
                       Response.Write("{\"msg\":\"您的资料已经提交，我们会在1个工作日进行资料审核。\",\"st\":\"1\",\"url\":\"\"}");
                       Response.End();
                   }
                   else
                   {
                       Response.Write("{\"msg\":\"失败\",\"st\":\"-1\",\"url\":\"\"}");
                       Response.End();
                   }
                }
            }
        }
    }
}