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
using System.Drawing;
using System.Drawing.Imaging;

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

    /// <summary>
    /// 海报1
    /// </summary>
    /// <param name="openid"></param>
    /// <returns></returns>
    public string SeadSeaNews(string openid)
    {
        string img = "/assets/img/bottom.jpg";
        img = Server.MapPath(img);
        string headimg = "/wechat/spa/image/logo.jpg";
        string qrimg = "/wechat/spa/image/logo.jpg";
        OAauth_Log oa = mss.GetOA(openid);
        if (oa != null)
        {
            if (!string.IsNullOrEmpty(oa.MEDIA_ID) && oa.MEDIA_Time.HasValue && oa.MEDIA_Time > DateTime.Now)
            {
                return oa.MEDIA_ID; //如果已有海报，并且未过期，则直接返回之前的海报。
            }
            if (string.IsNullOrEmpty(oa.Ticket)) //如果没有生成过邀请二维码，则生成一个。
            {
                //string access_token = w.Token(w.mjuserid);
                string jason = "{\"action_name\": \"QR_LIMIT_SCENE\", \"action_info\": {\"scene\": {\"scene_id\": " + (oa.ID + 10000) + "}}}"; //oa的id增加一万
                string resMessage = WeiPage.HttpXmlPostRequest("https://api.weixin.qq.com/cgi-bin/qrcode/create?access_token=" + Token(mjuserid), jason, Encoding.UTF8);
                string[] a = resMessage.Split('\"');
                if (a.Length > 3)
                {
                    oa.Ticket = Server.UrlEncode(a[3]);
                    DownQRImage(oa.Ticket);
                    qrimg = "/wechat/QRImage/" + oa.Ticket + ".jpg";
                }
            }
            else
            {
                qrimg = "/wechat/QRImage/" + oa.Ticket + ".jpg";
            }
            qrimg = Server.MapPath(qrimg);
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
            headimg = Server.MapPath(headimg);

            string img2 = @"E:\ASPNETTempFiles\seanews\";
            if (!System.IO.Directory.Exists(img2))
            {
                Directory.CreateDirectory(img2);
            }
            oa.SeaImg = Guid.NewGuid().ToString();
            img2 += oa.SeaImg + ".jpg";
            if (File.Exists(img) && File.Exists(headimg)&&File.Exists(qrimg))
            {
                ImageWriter iw = new ImageWriter();
                System.Drawing.Bitmap bm = new System.Drawing.Bitmap(headimg);
                System.Drawing.Image newImage = CutEllipse(bm, new Rectangle(0, 0, bm.Width, bm.Height), new Size(200, 200));
                iw.SaveWatermark(new System.Drawing.Bitmap(img),(Bitmap) newImage, ImageWriter.WatermarkPosition.LeftTop, 90, 70, new System.Drawing.Bitmap(qrimg), ImageWriter.WatermarkPosition.LeftBottom, 220, 50, img2, oa.Nickname); ;// f.FILE_NAME, Server.MapPath("~/assets/images/shuiyin.png"), Server.MapPath("~/assets/images/shuiyin2.png"), Server.MapPath("~/assets/images/shuiyin3.png"), 0.3f, ImageWriter.WatermarkPosition.Center, 10, f.FILE_NAME);
            }

            if (File.Exists(img2))
            {
                List<string> imglist = new List<string>();
                imglist.Add(img2);
                string a = HttpUploadFile("https://api.weixin.qq.com/cgi-bin/media/upload?access_token=" + Token(mjuserid) + "&type=image", "图片", "application/x-jpg", new System.Collections.Specialized.NameValueCollection { }, imglist);
                Rootobject root = JsonConvert.DeserializeObject<Rootobject>(a);
                if (root.media_id != null)
                {
                    oa.MEDIA_ID = root.media_id;
                    oa.MEDIA_Time = DateTime.Now.AddDays(3).AddHours(-2);
                    mss.SaveOA(oa);
                    return root.media_id;
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
        return "err";
    }

    /// <summary>
    /// 海报1
    /// </summary>
    /// <param name="openid"></param>
    /// <returns></returns>
    public OAauth_Log SeadSeaNews(OAauth_Log oa)
    {
        string img = "/assets/img/bottom.jpg";
        img = Server.MapPath(img);
        string headimg = "/wechat/spa/image/logo.jpg";
        string qrimg = "/wechat/spa/image/logo.jpg";
        if (oa != null)
        {
            if (string.IsNullOrEmpty(oa.Ticket)) //如果没有生成过邀请二维码，则生成一个。
            {
                string jason = "{\"action_name\": \"QR_LIMIT_SCENE\", \"action_info\": {\"scene\": {\"scene_id\": " + (oa.ID + 10000) + "}}}"; //oa的id增加一万
                string resMessage = WeiPage.HttpXmlPostRequest("https://api.weixin.qq.com/cgi-bin/qrcode/create?access_token=" + Token(mjuserid), jason, Encoding.UTF8);
                string[] a = resMessage.Split('\"');
                if (a.Length > 3)
                {
                    oa.Ticket = Server.UrlEncode(a[3]);
                    DownQRImage(oa.Ticket);
                    qrimg = "/wechat/QRImage/" + oa.Ticket + ".jpg";
                }
            }
            else
            {
                qrimg = "/wechat/QRImage/" + oa.Ticket + ".jpg";
            }
            qrimg = Server.MapPath(qrimg);
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
            headimg = Server.MapPath(headimg);

            string img2 = @"E:\ASPNETTempFiles\seanews\";
            if (!System.IO.Directory.Exists(img2))
            {
                Directory.CreateDirectory(img2);
            }
            oa.SeaImg = Guid.NewGuid().ToString();
            img2 += oa.SeaImg + ".jpg";
            if (File.Exists(img) && File.Exists(headimg) && File.Exists(qrimg))
            {
                ImageWriter iw = new ImageWriter();
                System.Drawing.Bitmap bm = new System.Drawing.Bitmap(headimg);
                System.Drawing.Image newImage = CutEllipse(bm, new Rectangle(0, 0, bm.Width, bm.Height), new Size(200, 200));
                iw.SaveWatermark(new System.Drawing.Bitmap(img), (Bitmap)newImage, ImageWriter.WatermarkPosition.LeftTop, 90, 70, new System.Drawing.Bitmap(qrimg), ImageWriter.WatermarkPosition.LeftBottom, 220, 50, img2, oa.Nickname); ;// f.FILE_NAME, Server.MapPath("~/assets/images/shuiyin.png"), Server.MapPath("~/assets/images/shuiyin2.png"), Server.MapPath("~/assets/images/shuiyin3.png"), 0.3f, ImageWriter.WatermarkPosition.Center, 10, f.FILE_NAME);
            }

            if (File.Exists(img2))
            {
                List<string> imglist = new List<string>();
                imglist.Add(img2);
                string a = HttpUploadFile("https://api.weixin.qq.com/cgi-bin/media/upload?access_token=" + Token(mjuserid) + "&type=image", "图片", "application/x-jpg", new System.Collections.Specialized.NameValueCollection { }, imglist);
                Rootobject root = JsonConvert.DeserializeObject<Rootobject>(a);
                if (root.media_id != null)
                {
                    oa.MEDIA_ID = root.media_id;
                    oa.MEDIA_Time = DateTime.Now.AddDays(3).AddHours(-2);
                    mss.SaveOA(oa);
                    return oa;
                }
            }
        }
        return oa;
    }

    /// <summary>
    /// 海报2
    /// </summary>
    /// <param name="openid"></param>
    /// <returns></returns>
    public string SeadSeaNews2(string openid)
    {
        string img = "/assets/img/bottom2.jpg";
        img = Server.MapPath(img);
        string headimg = "/wechat/spa/image/logo.jpg";
        string qrimg = "/wechat/spa/image/logo.jpg";
        OAauth_Log oa = mss.GetOA(openid);
        if (oa != null)
        {
            //if (!string.IsNullOrEmpty(oa.MEDIA_ID) && oa.MEDIA_Time.HasValue && oa.MEDIA_Time > DateTime.Now)
            //{
            //    return oa.MEDIA_ID; //如果已有海报，并且未过期，则直接返回之前的海报。
            //}
            if (string.IsNullOrEmpty(oa.Ticket)) //如果没有生成过邀请二维码，则生成一个。
            {
                //string access_token = w.Token(w.mjuserid);
                string jason = "{\"action_name\": \"QR_LIMIT_SCENE\", \"action_info\": {\"scene\": {\"scene_id\": " + (oa.ID + 10000) + "}}}"; //oa的id增加一万
                string resMessage = WeiPage.HttpXmlPostRequest("https://api.weixin.qq.com/cgi-bin/qrcode/create?access_token=" + Token(mjuserid), jason, Encoding.UTF8);
                string[] a = resMessage.Split('\"');
                if (a.Length > 3)
                {
                    oa.Ticket = Server.UrlEncode(a[3]);
                    DownQRImage(oa.Ticket);
                    qrimg = "/wechat/QRImage/" + oa.Ticket + ".jpg";
                }
            }
            else
            {
                qrimg = "/wechat/QRImage/" + oa.Ticket + ".jpg";
            }
            qrimg = Server.MapPath(qrimg);
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
            headimg = Server.MapPath(headimg);

            string img2 = @"E:\ASPNETTempFiles\seanews\";
            if (!System.IO.Directory.Exists(img2))
            {
                Directory.CreateDirectory(img2);
            }
            img2 += Guid.NewGuid() + ".jpg";
            if (File.Exists(img) && File.Exists(headimg) && File.Exists(qrimg))
            {
                ImageWriter iw = new ImageWriter();
                System.Drawing.Bitmap bm = new System.Drawing.Bitmap(headimg);
                System.Drawing.Image newImage = CutEllipse(bm, new Rectangle(0, 0, bm.Width, bm.Height), new Size(190, 190));
                
                iw.SaveWatermark(new System.Drawing.Bitmap(img), (Bitmap)newImage, ImageWriter.WatermarkPosition.LeftTop, 380, 390, new System.Drawing.Bitmap(qrimg), ImageWriter.WatermarkPosition.LeftBottom, 425, 530, img2,null); ;// f.FILE_NAME, Server.MapPath("~/assets/images/shuiyin.png"), Server.MapPath("~/assets/images/shuiyin2.png"), Server.MapPath("~/assets/images/shuiyin3.png"), 0.3f, ImageWriter.WatermarkPosition.Center, 10, f.FILE_NAME);
            }

//            if (File.Exists(img2))
//            {
//                List<string> imglist = new List<string>();
//                imglist.Add(img2);
//                string a = HttpUploadFile("https://api.weixin.qq.com/cgi-bin/media/upload?access_token=" + Token(mjuserid) + "&type=image", "图片", "application/x-jpg", new System.Collections.Specialized.NameValueCollection { }, imglist);
//                Rootobject root = JsonConvert.DeserializeObject<Rootobject>(a);
//                if (root.media_id != null)
//                {
//                    oa.MEDIA_ID = root.media_id;
//                    oa.MEDIA_Time = DateTime.Now.AddDays(3).AddHours(-2);
//                    mss.SaveOA(oa);
//                    return root.media_id;
//                    string message = @"{
//                ""touser"":""{0}"",
//                ""msgtype"":""image"",
//                ""image"":
//                {
//                  ""media_id"":""{1}""
//                }
//            }";
//                    message = message.Replace("{0}", oa.FromUserName).Replace("{1}", root.media_id);
//                    string Access_token = Token(mjuserid);

//                    var postUrl = "https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token=" + Access_token;
//                    string d = message;
//                    //d = d.Replace("{0}", "oS7pm1iNL2P2pjdgHO3xC2NRdWE8").Replace("{1}", Message);
//                    string resMessage = HttpXmlPostRequest(postUrl, d, Encoding.UTF8);
//                    //Response.Write(resMessage);
//                }
//            }
        }
        return "err";
    }

    /// <summary>
    /// 佰草集邀请海报1
    /// </summary>
    /// <param name="openid"></param>
    /// <returns></returns>
    public OAauth_Log SeadSeaNews_BCJ(OAauth_Log oa, int SeaSource)
    {
        string img = "/assets/img/poster.jpg";
        img = Server.MapPath(img);
        string headimg = "/wechat/spa/image/logo.jpg";
        string qrimg = "/wechat/spa/image/logo.jpg";
        if (oa != null)
        {
            if (!oa.SeaSource.HasValue)
                oa.SeaSource = SeaSource;

            if (string.IsNullOrEmpty(oa.Ticket)) //如果没有生成过邀请二维码，则生成一个。
            {
                string jason = "{\"action_name\": \"QR_LIMIT_SCENE\", \"action_info\": {\"scene\": {\"scene_id\": " + (oa.ID + 10000) + "}}}"; //oa的id增加一万
                string resMessage = WeiPage.HttpXmlPostRequest("https://api.weixin.qq.com/cgi-bin/qrcode/create?access_token=" + Token(mjuserid), jason, Encoding.UTF8);
                string[] a = resMessage.Split('\"');
                if (a.Length > 3)
                {
                    oa.Ticket = Server.UrlEncode(a[3]);
                    DownQRImage(oa.Ticket);
                    qrimg = "/wechat/QRImage/" + oa.Ticket + ".jpg";
                }
            }
            else
            {
                qrimg = "/wechat/QRImage/" + oa.Ticket + ".jpg";
            }
            qrimg = Server.MapPath(qrimg);
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
            headimg = Server.MapPath(headimg);

            string img2 = @"D:\ASPNETTempFiles\seanews\";
            if (!System.IO.Directory.Exists(img2))
            {
                Directory.CreateDirectory(img2);
            }
            oa.SeaImg = Guid.NewGuid().ToString();
            img2 += oa.SeaImg + ".jpg";
            if (File.Exists(img) && File.Exists(headimg) && File.Exists(qrimg))
            {
                ImageWriter iw = new ImageWriter();
                System.Drawing.Bitmap bm = new System.Drawing.Bitmap(headimg);
                System.Drawing.Image newImage = CutEllipse(bm, new Rectangle(0, 0, bm.Width, bm.Height), new Size(200, 200));
                using (Image image = Image.FromFile(img))
                {
                    if (IsPixelFormatIndexed(image.PixelFormat))
                    {
                        Bitmap bmp = new Bitmap(image.Width, image.Height, PixelFormat.Format32bppArgb);
                        using (Graphics g = Graphics.FromImage(bmp))
                        {
                            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                            g.DrawImage(image, 0, 0);
                        }
                        
                        iw.SaveWatermark(bmp, (Bitmap)newImage, ImageWriter.WatermarkPosition.LeftTop, 120, 120, new System.Drawing.Bitmap(qrimg), ImageWriter.WatermarkPosition.LeftBottom, 130, 30, img2, oa.Nickname); ;// f.FILE_NAME, Server.MapPath("~/assets/images/shuiyin.png"), Server.MapPath("~/assets/images/shuiyin2.png"), Server.MapPath("~/assets/images/shuiyin3.png"), 0.3f, ImageWriter.WatermarkPosition.Center, 10, f.FILE_NAME);
                    }else
                        iw.SaveWatermark(new System.Drawing.Bitmap(img), (Bitmap)newImage, ImageWriter.WatermarkPosition.LeftTop, 120, 120, new System.Drawing.Bitmap(qrimg), ImageWriter.WatermarkPosition.LeftBottom, 130, 30, img2, oa.Nickname); ;// f.FILE_NAME, Server.MapPath("~/assets/images/shuiyin.png"), Server.MapPath("~/assets/images/shuiyin2.png"), Server.MapPath("~/assets/images/shuiyin3.png"), 0.3f, ImageWriter.WatermarkPosition.Center, 10, f.FILE_NAME);
                }


                
            }

            if (File.Exists(img2))
            {
                List<string> imglist = new List<string>();
                imglist.Add(img2);
                string a = HttpUploadFile("https://api.weixin.qq.com/cgi-bin/media/upload?access_token=" + Token(mjuserid) + "&type=image", "图片", "application/x-jpg", new System.Collections.Specialized.NameValueCollection { }, imglist);
                Rootobject root = JsonConvert.DeserializeObject<Rootobject>(a);
                if (root.media_id != null)
                {
                    oa.MEDIA_ID = root.media_id;
                    oa.MEDIA_Time = DateTime.Now.AddDays(3).AddHours(-2);
                    mss.SaveOA(oa);
                    return oa;
                }
            }
        }
        return oa;
    }


    /// <summary>
    /// 会产生graphics异常的PixelFormat
    /// </summary>
    private static PixelFormat[] indexedPixelFormats = { PixelFormat.Undefined, PixelFormat.DontCare,
PixelFormat.Format16bppArgb1555, PixelFormat.Format1bppIndexed, PixelFormat.Format4bppIndexed,
PixelFormat.Format8bppIndexed
    };

    /// <summary>
    /// 判断图片的PixelFormat 是否在 引发异常的 PixelFormat 之中
    /// </summary>
    /// <param name="imgPixelFormat">原图片的PixelFormat</param>
    /// <returns></returns>
    private static bool IsPixelFormatIndexed(PixelFormat imgPixelFormat)
    {
        foreach (PixelFormat pf in indexedPixelFormats)
        {
            if (pf.Equals(imgPixelFormat)) return true;
        }

        return false;
    }


    /// <summary>
    /// 佰草集邀请海报1
    /// </summary>
    /// <param name="openid"></param>
    /// <returns></returns>
    public string SeadSeaNews_BCJ(string openid,int SeaSource)
    {
        string img = "/assets/img/poster.jpg";
        img = Server.MapPath(img);
        string headimg = "/wechat/spa/image/logo.jpg";
        string qrimg = "/wechat/spa/image/logo.jpg";
        OAauth_Log oa = mss.GetOA(openid);
        if (oa != null)
        {
            if (!oa.SeaSource.HasValue)
                oa.SeaSource = SeaSource;

            if (!string.IsNullOrEmpty(oa.MEDIA_ID) && oa.MEDIA_Time.HasValue && oa.MEDIA_Time > DateTime.Now)
            {
                return oa.MEDIA_ID; //如果已有海报，并且未过期，则直接返回之前的海报。
            }
            if (string.IsNullOrEmpty(oa.Ticket)) //如果没有生成过邀请二维码，则生成一个。
            {
                //string access_token = w.Token(w.mjuserid);
                string jason = "{\"action_name\": \"QR_LIMIT_SCENE\", \"action_info\": {\"scene\": {\"scene_id\": " + (oa.ID + 10000) + "}}}"; //oa的id增加一万
                string resMessage = WeiPage.HttpXmlPostRequest("https://api.weixin.qq.com/cgi-bin/qrcode/create?access_token=" + Token(mjuserid), jason, Encoding.UTF8);
                string[] a = resMessage.Split('\"');
                if (a.Length > 3)
                {
                    oa.Ticket = Server.UrlEncode(a[3]);
                    DownQRImage(oa.Ticket);
                    qrimg = "/wechat/QRImage/" + oa.Ticket + ".jpg";
                }
            }
            else
            {
                qrimg = "/wechat/QRImage/" + oa.Ticket + ".jpg";
            }
            qrimg = Server.MapPath(qrimg);
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
            headimg = Server.MapPath(headimg);

            string img2 = @"D:\ASPNETTempFiles\seanews\";
            if (!System.IO.Directory.Exists(img2))
            {
                Directory.CreateDirectory(img2);
            }
            oa.SeaImg = Guid.NewGuid().ToString();
            img2 += oa.SeaImg + ".jpg";
            if (File.Exists(img) && File.Exists(headimg) && File.Exists(qrimg))
            {
                ImageWriter iw = new ImageWriter();
                System.Drawing.Bitmap bm = new System.Drawing.Bitmap(headimg);
                System.Drawing.Image newImage = CutEllipse(bm, new Rectangle(0, 0, bm.Width, bm.Height), new Size(200, 200));
                using (Image image = Image.FromFile(img))
                {
                    if (IsPixelFormatIndexed(image.PixelFormat))
                    {
                        Bitmap bmp = new Bitmap(image.Width, image.Height, PixelFormat.Format32bppArgb);
                        using (Graphics g = Graphics.FromImage(bmp))
                        {
                            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                            g.DrawImage(image, 0, 0);
                        }

                        iw.SaveWatermark(bmp, (Bitmap)newImage, ImageWriter.WatermarkPosition.LeftTop, 120, 120, new System.Drawing.Bitmap(qrimg), ImageWriter.WatermarkPosition.LeftBottom, 130, 30, img2, oa.Nickname); ;// f.FILE_NAME, Server.MapPath("~/assets/images/shuiyin.png"), Server.MapPath("~/assets/images/shuiyin2.png"), Server.MapPath("~/assets/images/shuiyin3.png"), 0.3f, ImageWriter.WatermarkPosition.Center, 10, f.FILE_NAME);
                    }
                    else
                        iw.SaveWatermark(new System.Drawing.Bitmap(img), (Bitmap)newImage, ImageWriter.WatermarkPosition.LeftTop, 120, 120, new System.Drawing.Bitmap(qrimg), ImageWriter.WatermarkPosition.LeftBottom, 130, 30, img2, oa.Nickname); ;// f.FILE_NAME, Server.MapPath("~/assets/images/shuiyin.png"), Server.MapPath("~/assets/images/shuiyin2.png"), Server.MapPath("~/assets/images/shuiyin3.png"), 0.3f, ImageWriter.WatermarkPosition.Center, 10, f.FILE_NAME);
                }



            }

            if (File.Exists(img2))
            {
                List<string> imglist = new List<string>();
                imglist.Add(img2);
                string a = HttpUploadFile("https://api.weixin.qq.com/cgi-bin/media/upload?access_token=" + Token(mjuserid) + "&type=image", "图片", "application/x-jpg", new System.Collections.Specialized.NameValueCollection { }, imglist);
                Rootobject root = JsonConvert.DeserializeObject<Rootobject>(a);
                if (root.media_id != null)
                {
                    oa.MEDIA_ID = root.media_id;
                    oa.MEDIA_Time = DateTime.Now.AddDays(3).AddHours(-2);
                    mss.SaveOA(oa);
                    return root.media_id;
            //        string message = @"{
            //    ""touser"":""{0}"",
            //    ""msgtype"":""image"",
            //    ""image"":
            //    {
            //      ""media_id"":""{1}""
            //    }
            //}";
            //        message = message.Replace("{0}", oa.FromUserName).Replace("{1}", root.media_id);
            //        string Access_token = Token(mjuserid);

            //        var postUrl = "https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token=" + Access_token;
            //        string d = message;
            //        //d = d.Replace("{0}", "oS7pm1iNL2P2pjdgHO3xC2NRdWE8").Replace("{1}", Message);
            //        string resMessage = HttpXmlPostRequest(postUrl, d, Encoding.UTF8);
                }
            }
        }
        return "err";
    }

    /// <summary>
    /// 下载二维码到本地
    /// </summary>
    /// <param name="oa1"></param>
    public void DownQRImage(string Ticket)
    {
        try
        {
            //下载头像到本地
            WebClient web = new WebClient();
            string loclurl = "~/wechat/QRImage/";// +new Guid("d").ToString();
            string path = Server.MapPath(loclurl);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string guid = Ticket;
            string path2 = path + guid + ".jpg";//缩略图
            path += guid + "Y.jpg";  //原图 缩略图后加个Y即为原图
            //if (!string.IsNullOrEmpty(oa1.headimgurl))
                web.DownloadFile("https://mp.weixin.qq.com/cgi-bin/showqrcode?ticket="+guid, path);
            MakeThumbnail2(path, path2, 240, 240, true);//120 120
            //File.Delete(path);
            //return path;
        }
        catch (Exception ex)
        {
            //WXLOG log = new WXLOG();
            //log.CON = ex.Message.ToString() + "," + ex.StackTrace;
            //log.TIME = DateTime.Now;
            //mss.SaveLog(log);
            //return null;
        }
    }

    /// <summary>
    /// 转圆形图片
    /// </summary>
    /// <param name="img"></param>
    /// <param name="rec"></param>
    /// <param name="size"></param>
    /// <returns></returns>
    private System.Drawing.Image CutEllipse(System.Drawing.Image img, Rectangle rec, Size size)
    {
        Bitmap bitmap = new Bitmap(size.Width, size.Height);
        using (Graphics g = Graphics.FromImage(bitmap))
        {
            using (TextureBrush br = new System.Drawing.TextureBrush(img, System.Drawing.Drawing2D.WrapMode.Clamp, rec))
            {
                br.ScaleTransform(bitmap.Width / (float)rec.Width, bitmap.Height / (float)rec.Height);
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.FillEllipse(br, new Rectangle(Point.Empty, size));
            }
        }
        return bitmap;
    }

}

