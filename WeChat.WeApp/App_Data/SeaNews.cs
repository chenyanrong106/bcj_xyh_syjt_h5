using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using System.Text;
using System.Configuration;
using Newtonsoft.Json;
using SPACRM.Entity;
using SPACRM.Business.ServiceImpl;
using SPACRM.Interface;
using SPACRM.Common;
using SPACRM.Entity.Entities;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using SPACRM.Common.Utils;

/// <summary>
///WeiPage 的摘要说明
/// </summary>
public class SeaNews : WeiPage
{
    MySmallShopService mss = new MySmallShopService();
    public SeaNews()
    {
        //  Response.Write(" <input type='hidden'' value='" + Server.UrlEncode(AbsoluteUri) + "' id='url' />");
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }

    public void SeadSeaNews(string openid)
    {
        string img = "/assets/img/bottom.jpg";
        img = Server.MapPath(img);
        string headimg = "/wechat/spa/image/logo.jpg";
        OAauth_Log oa = mss.GetOA(openid);
        if (oa != null)
        {

            if (!string.IsNullOrEmpty(oa.DownPic))
            {
                headimg = oa.DownPic;
            }
            else if (!string.IsNullOrEmpty(oa.headimgurl))
            {
                string down = DownHeadImage(oa);
                if (!string.IsNullOrEmpty(down))
                {
                    headimg = down;
                }
            }
            headimg =@"D:\小时光POS系统"+headimg.Replace("~","");

            string img2 = @"D:\ASPNETTempFiles\seanews\";
            if (!System.IO.Directory.Exists(img2))
            {
                Directory.CreateDirectory(img2);
            }
            img2 += Guid.NewGuid() + ".jpg";
            if (File.Exists(img) && File.Exists(headimg))
            {
                ImageWriter iw = new ImageWriter();
                iw.SaveWatermark(new System.Drawing.Bitmap(img), new System.Drawing.Bitmap(headimg), ImageWriter.WatermarkPosition.Top, 50, img2);// f.FILE_NAME, Server.MapPath("~/assets/images/shuiyin.png"), Server.MapPath("~/assets/images/shuiyin2.png"), Server.MapPath("~/assets/images/shuiyin3.png"), 0.3f, ImageWriter.WatermarkPosition.Center, 10, f.FILE_NAME);
            }

            if (File.Exists(img2))
            {
                List<string> imglist = new List<string>();
                imglist.Add(img2);
                string a = HttpUploadFile("https://api.weixin.qq.com/cgi-bin/media/upload?access_token=" + Token(mjuserid) + "&type=image", "图片", "application/x-jpg", new System.Collections.Specialized.NameValueCollection { }, imglist);
                Rootobject root = JsonConvert.DeserializeObject<Rootobject>(a);
                if (root.media_id != null)
                {
                    string message = @"{
                ""touser"":""{0}"",
                ""msgtype"":""image"",
                ""image"":
                {
                  ""media_id"":""{1}""
                }
            }";
                    message = message.Replace("{0}", oa.FromUserName).Replace("{1}", root.media_id);
                    string Access_token = Token(mjuserid);

                    var postUrl = "https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token=" + Access_token;
                    string d = message;
                    //d = d.Replace("{0}", "oS7pm1iNL2P2pjdgHO3xC2NRdWE8").Replace("{1}", Message);
                    string resMessage = HttpXmlPostRequest(postUrl, d, Encoding.UTF8);
                    //Response.Write(resMessage);
                }
            }
        }
    }

}

public class Rootobject
{
    public string type { get; set; }
    public string media_id { get; set; }
    public int created_at { get; set; }
}
