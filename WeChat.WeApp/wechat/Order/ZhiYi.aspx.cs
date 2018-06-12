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
    public partial class ZhiYi : WeiPage
    {
        CommonService _mservice = new CommonService();
        NewVerService nvbo = new NewVerService();
        MySmallShopService mss = new MySmallShopService();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["para"] == null && Request.Files.Count == 0)
            {

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
                    string[] pics = Request.Params["pic"].Split('*');
                    Pet_JiuZhu_ZhiYi zy = new Pet_JiuZhu_ZhiYi
                    {
                        CardNo = Request.Params["cardno"],
                        CreateTime = DateTime.Now,
                        Detail = Request.Params["detail"],
                        FromUserName = user,
                        IP = GetLoginIp(),
                        Name = Request.Params["name"],
                        Phone = Request.Params["phone"],
                        JID = int.Parse(Request.Params["jid"]),
                        JNickName = Request.Params["jname"],
                        EMail=Request.Params["email"]
                    };
                    if (pics.Length > 1)
                        zy.Img1 = pics[0];
                    if (pics.Length > 2)
                        zy.Img2 = pics[1];
                    if (pics.Length > 3)
                        zy.Img3 = pics[2];
                    if (pics.Length > 4)
                        zy.Img4 = pics[3];
                    if (pics.Length > 5)
                        zy.Img5 = pics[4];
                    if (pics.Length > 6)
                        zy.Img6 = pics[5];
                    if (pics.Length > 7)
                        zy.Img7 = pics[6];
                    if (pics.Length > 8)
                        zy.Img8 = pics[7];
                    int id = nvbo.SavePetJiuZhuZhiYi(zy);

                    if (id > 0)
                    {
                        string token = Token(mjuserid);
                        string[] op = new[] { "oloJGv4lWL-TS8yn8uo4Fu1rbVTI", "oloJGvzwelDQjIKMMWC8Z6ngM7gk", "oloJGvx3hLkCNadLGIpAMimz2Xwc" };
                        foreach (string o in op)
                        {
                            var temp2 = new
                            {
                                first = new { value = "新质疑提醒", color = "#173177" },
                                keyword1 = new { value = zy.Name, color = "#173177" },
                                keyword2 = new { value = zy.CreateTime.Value.ToString("yyyy.MM.dd HH:mm"), color = "#173177" },
                                remark = new { value = "\n天啦噜！居然有人发起了质疑！快去看看吧。", color = "#CD0000" }
                            };
                            string message = SendTemplateMessage(token, o, "4CzZvg-tcOd5X72hX_H2NNWkhCVh5VFPRblk7c7s-DY", "#FF0000", temp2, WebUrl + "/wechat/order/seezhiyi.aspx?id=" + id);
                        }
                        Response.Write("{\"msg\":\"举报成功，工作人员将对举报内容认真处理。\",\"st\":\"1\",\"url\":\"\"}");
                        Response.End();
                    }
                    else
                    {
                        Response.Write("{\"msg\":\"失败\",\"st\":\"-1\",\"url\":\"\"}");
                        Response.End();
                    }
                }
                //}
            }
        }
    }
}