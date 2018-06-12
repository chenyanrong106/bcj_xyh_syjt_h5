using Newtonsoft.Json;
using SPACRM.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SPACRM.Business.ServiceImpl;
using System.Text;
using System.Web.Security;
using WeChatCRM.Common.Utils;

namespace SPACRM.WebApp.wechat.test
{
    public partial class test : WeiPage
    {
        NewVerService nvbo = new NewVerService();
        MySmallShopService mss = new MySmallShopService();
        protected void Page_Load(object sender, EventArgs e)
        {
            string token = GetSRToken();
            var temp = new
            {
                keyword1 = new { value = DateTime.Now.ToString("yyyy年MM月dd日 HH:mm"), color = "#173177" },
                keyword2 = new { value = 1 + "元", color = "#173177" },
                keyword3 = new { value = "爱宠筹", color = "#173177" },
                keyword4 = new { value = "为毛孩子筹粮/筹款", color = "#CD0000" },
                keyword5 = new { value = Guid.NewGuid().ToString(), color = "#CD0000" }
            };
            string message = SendTemplateMessageSR(token, "oEU0J0XFBV6HXIyb1R-vYsZtOI2g", "IuoZZEIkILzvazy94NRpYrQ9usZ0wc3RyeSSp85gfmA", "#FF0000", temp, "pages/Pet/Detail/Detail?id=487", "wx20170106175142db2e7ef2690037999241");
                                   
            //Pet_XXL_Order order = new Pet_XXL_Order {FromUserName="oloJGv4lWL-TS8yn8uo4Fu1rbVTI",Source=487,ID=500,TotalPrice=20 };
            //OAauth_Log o = mss.GetOA(order.FromUserName);
            //if (o != null)
            //{
            //    Pet_JiuZhu_Info j = nvbo.GetJiuZhuInfo(order.Source.Value);
            //    if (j != null && (j.JZType == 1 || j.JZType == 2))
            //    {
            //        Pet_JiuZhu_Love l = new Pet_JiuZhu_Love
            //        {
            //            CreateTime = DateTime.Now,
            //            Detail = "捐助基地[" + j.NickName + "][" + (j.JZType == 1 ? "捐粮" : "捐款") + "]",
            //            FromUserName = order.FromUserName,
            //            OID = order.ID,
            //            Love = j.JZType == 1 ? Convert.ToInt32(order.TotalPrice) : Convert.ToInt32(order.TotalPrice / 2)
            //        };
            //        int lid = nvbo.SavePetJiuZhuLove(l);
            //        if (lid > 0)
            //        {
            //            o.TotalLove = (o.TotalLove ?? 0) + l.Love;
            //            o.SurLove = (o.SurLove ?? 0) + l.Love;
            //            mss.SaveOA(o);
            //        }
            //    }
            //}
            if (Request.QueryString["token"] != null&&Request.QueryString["cardid"]!=null)
            {
                //string message = AddCard();
                //Response.Write(message);
                //Response.End();
            }
            else if (Request.QueryString["delete"] != null)
            {
                //nvbo.DeleteBcj();
                Response.Write("ok");
                Response.End();
            }


//            string url = "https://api.weixin.qq.com/card/batchget?access_token=grU-YSTRd9XZvkKs9SffHue4B_ZkaopRWLN4KaSIY3B9od72_tXwV6iPG6qya4_jknroCZqeDsEUtC1-xr40MlOcugRtJvRDnSySi9Z2QAnoQGhxqkSBIk7ToKfvJTkiDEFcAAARST";
//            string json = @"{
//  ""offset"": 0,
//  ""count"": 10, 
//  ""status_list"": [""CARD_STATUS_VERIFY_OK"", ""CARD_STATUS_DISPATCH""]
//}";
//            string resMessage = HttpXmlPostRequest(url, json, Encoding.UTF8);
//            Response.Write(resMessage);
//            Response.End();


            //string nonce_str = "jonyqin";
            //string card_id = "pjZ8Yt1XGILfi-FUsewpnnolGgZk";
            //string code = "jonyqin_1434008071";
            //string openid = "oX154jmkDxRytCwhdkq_STKcLp5s";
            //string timestamp = "1404896688";
            //string api_ticket = "ojZ8YtyVyr30HheH3CM73y7h4jJE";
            //string[] ArrTmp = { code,  card_id, timestamp, api_ticket, nonce_str };
            //Array.Sort(ArrTmp);
            //Response.Write(string.Join("", ArrTmp));
            //Response.Write("<br>");
            //string tmpStr = string.Join("", ArrTmp);
            //tmpStr = FormsAuthentication.HashPasswordForStoringInConfigFile(tmpStr, "SHA1");
            //string signature = tmpStr;

            //Response.Write(nonce_str);
            //Response.Write("<br>");
            //Response.Write(card_id);
            //Response.Write("<br>");
            //Response.Write(code);
            //Response.Write("<br>");
            //Response.Write(timestamp);
            //Response.Write("<br>");
            //Response.Write(api_ticket);
            //Response.Write("<br>");

            //string cardExt = "{\"timestamp\":\"" + timestamp + "\",\"nonce_str\":\"" + nonce_str + "\",\"code\":\"" + code + "\",\"openid\":\"" + openid + "\",\"signature\":\"" + signature + "\"}";

            //Response.Write(signature);
            //Response.Write("<br>");
            //Response.Write(cardExt);
            

            //ImageWriter iw = new ImageWriter();
            //iw.SaveWatermark("E:\\22.jpg", Server.MapPath("~/assets/images/shuiyin.png"), Server.MapPath("~/assets/images/shuiyin2.png"), Server.MapPath("~/assets/images/shuiyin3.png"), 0.9f, ImageWriter.WatermarkPosition.Center, 10, "E:\\22.jpg");
            //string url = "/home/ViewImage.do/883";
            //int dex= url.LastIndexOf('/');
            //if (dex > 0)
            //{
            //    dex++;
            //    Response.Write(url.Substring(dex,url.Length-dex));
            //    Response.Write(IsNum(url.Substring(dex,url.Length-dex)));
            //}

            //string url = "http://ip.taobao.com/service/getIpInfo.php?ip=116.226.106.27";
            //string a= GetRequest(url);

            // GetLocalHeadImage();
            //string ip = "123.123.456.456";
            //string[] ips = ip.Split('.');
            //if (ips.Length == 4)
            //{
            //    ip = ips[0] + ".*.*." + ips[3];
            //}

            //Response.Write(DateTime.Now.Hour);
            //Response.Write("<br>");
            //Response.Write(DateTime.Parse("2016-09-22 20:09:23").Hour);
            //Response.Write("<br>");
            //Response.Write(DateTime.Parse("2016-09-22 10:09:23").Hour);
            //Response.Write("<br>");
            //Response.Write(DateTime.Parse("2016-09-22 09:09:23").Hour);
            //Response.Write("<br>");

            //string a=PostRequestGet("http://SPACRM.meijiewd.com/api/jiuzhu.ashx?para=GetJiuZhuList&page=1");
            //data d = JsonConvert.DeserializeObject<data>(a);
            //Response.Write(d.j.Length);
            //string a=@"[{"ID":63,"FromUserName":"","HeadImage":"http://SPACRM.meijiewd.com/home/ViewImage.do/1403","NickName":"南京市建邺区平安阿福流浪动物救助中心","Title":"众人拾柴火焰高，为1万多只流浪猫汪筹集口粮！","Detail":"秋天来了，又到了给毛孩子们贴秋膘的时候，也是为猫狗粮发愁的日子，尽管如此，平安阿福平日里最基本的猫狗粮费用，也是紧张得很，东家借一下，西家佘一下，非常勉强地撑着。  我们总是感到很愧疚，平安的孩子们住的是再简单不过的房子，吃的是普通的狗粮，由于数量众多，不能照顾到每一只猫汪的内心情感需要，只能勉强维持它们的生活，不让它们流落街头，扒垃圾，饿肚子，即便如此，它们也是如此地容易满足，总能在它们脸上看到快乐的笑容。  其实，哈大姐没有哪一天不是提心吊胆，石佛寺和基地的饲养员抢猫狗粮的情况越来越多，如果没有吃的，平安一天都撑不下去，家里的小猫小狗如果饿了，都会对主人叫，何况是1万多只猫汪，真不敢去想象这个可怕的后果。  愿意将你买一杯咖啡的费用，一次吃冰淇淋的钱，留给平安的孩子们吗？众人拾柴火焰高，汇聚大家的微小力量，就能创造奇迹！","Goal":2000,"EndTime":"2016-10-01T14:50:45.583","Img1":"http://SPACRM.meijiewd.com/home/ViewImage.do/1404","Img2":"http://SPACRM.meijiewd.com/home/ViewImage.do/1405","Img3":"http://SPACRM.meijiewd.com/home/ViewImage.do/1406","Img4":"http://SPACRM.meijiewd.com/home/ViewImage.do/1407","Img5":"http://SPACRM.meijiewd.com/home/ViewImage.do/1408","Img6":"http://SPACRM.meijiewd.com/home/ViewImage.do/1409","Img7":"http://SPACRM.meijiewd.com/home/ViewImage.do/1410","Img8":"http://SPACRM.meijiewd.com/home/ViewImage.do/1411","CreateTime":"2016-09-21T14:50:45.583","rc":616,"je":21059.71,"pj":null,"State":null,"PX":null,"RD":null,"JZType":1,"FullUpdate":false},{"ID":69,"FromUserName":"","HeadImage":"http://SPACRM.meijiewd.com/home/ViewImage.do/1499","NickName":"北京高俊玲救助小院","Title":"为狗狗筹集过冬粮！","Detail":"我六年前听说南四环拆迁狗猫很可怜，农民上楼，外地人也搬走，狗孩子还在给他们看家，我就从二环骑电动车一奔南四环拆迁地往返5一6小时一路喂，后来开始做绝育，救回狗14O多只，猫3O多只，近期又救回9只狗狗共近15O只，我在房山租的院子己全部装满狗孩子，我给它们大部分己做绝育，还有小部分也就是去年救的两窝小奶狗还有几只上半年救的大狗等有钱了会一只只做绝育，现在粮食是大问题，因为我的腿因车祸受伤，去不了拆迁地喂剩下的近30只狗，只能花钱在网上买粮快递给好心人帮我继续喂孩们，所以希望广大网友帮帮我的小院，解决拆迁地孩子的生活问题，再次谢谢大家。","Goal":500,"EndTime":"2016-10-06T09:48:17.613","Img1":"http://SPACRM.meijiewd.com/home/ViewImage.do/1500","Img2":"http://SPACRM.meijiewd.com/home/ViewImage.do/1501","Img3":"http://SPACRM.meijiewd.com/home/ViewImage.do/1502","Img4":"http://SPACRM.meijiewd.com/home/ViewImage.do/1503","Img5":"http://SPACRM.meijiewd.com/home/ViewImage.do/1504","Img6":"http://SPACRM.meijiewd.com/home/ViewImage.do/1505","Img7":"http://SPACRM.meijiewd.com/home/ViewImage.do/1506","Img8":"http://SPACRM.meijiewd.com/home/ViewImage.do/1507","CreateTime":"2016-09-26T09:48:17.613","rc":129,"je":4614.60,"pj":null,"State":null,"PX":null,"RD":null,"JZType":1,"FullUpdate":false},{"ID":66,"FromUserName":"","HeadImage":"http://SPACRM.meijiewd.com/home/ViewImage.do/1459","NickName":"京东流浪狗救助站","Title":"群护狗狗很饿，它们非常需要吃饱肚子，这点救命的粮食就是它们唯一的企盼，希望大家帮帮它们。","Detail":"大家好，我们群护的猫狗位于北京东郊，这里重点喂养着大约七十多只狗狗及猫咪，它们长年饥渴不饱，冬冷夏热，受尽折磨，完全失去自由，每只狗狗的凄惨不是常人所能理解的，几乎没有人关心它们爱护它们，这些毛孩所谓的主人也是自私冷血到极点，即使偶尔给些残汤剩饭也是馊了的甚至生蛆的，根本无法下咽，它们真是太饿了，有的大狗把粮刚倒进盆里几口就能吃光，小狗也是饥不择食，令人辛酸不己……天气逐渐变冷，它们的饭量也随之增加，由于本己严重营养不良，这点救命的粮食就是它们唯一的企盼，做为救助者也只能尽力而为，尽量让这些可怜的孩子们填饱肚子，可是一己之力又能维持多久？毕竟粮食是大的奢侈品，如果没有吃的东西再同情也是一句空话，它们的生命很短暂，随时都有可能被杀被偷被残害，现实非常残酷，我们的举手之劳就等于是救狗狗的命，没有过多华丽的言辞，在此真诚的希望大家能够帮帮可怜的它们，我们做到的也只有让狗狗们吃顿饱饭别再痛苦的同时再饿肚子，别在离开这个世界之后成为饿死的冤魂，授人玫瑰，手留余香，替毛孩感恩大家的支持与厚爱，阿弥陀佛，善良永存！","Goal":500,"EndTime":"2016-10-03T10:56:04.813","Img1":"http://SPACRM.meijiewd.com/home/ViewImage.do/1460","Img2":"http://SPACRM.meijiewd.com/home/ViewImage.do/1461","Img3":"http://SPACRM.meijiewd.com/home/ViewImage.do/1462","Img4":"http://SPACRM.meijiewd.com/home/ViewImage.do/1463","Img5":"http://SPACRM.meijiewd.com/home/ViewImage.do/1464","Img6":"http://SPACRM.meijiewd.com/home/ViewImage.do/1465","Img7":"http://SPACRM.meijiewd.com/home/ViewImage.do/1466","Img8":"http://SPACRM.meijiewd.com/home/ViewImage.do/1468","CreateTime":"2016-09-23T10:56:04.813","rc":129,"je":4086.02,"pj":null,"State":null,"PX":null,"RD":null,"JZType":1,"FullUpdate":false}]";
        }

//        public string AddCard()
//        {
//            string message="";
//            List<Bcj> list = nvbo.GetBCjlist();
//            int id=0;
//            foreach (Bcj b in list)
//            {
//                string url = "http://api.weixin.qq.com/card/code/deposit?access_token=" + Request.QueryString["token"];
//                string json = @"{
//   ""card_id"": """+Request.QueryString["cardid"]+@""",
//   ""code"": [
//       """ + b.code + @"""
//   ]
//}";
//                string resMessage = HttpXmlPostRequest(url, json, Encoding.UTF8);
//                b.jg = resMessage;
//                if (b.jg.Contains("succ_code")) //成功
//                {
//                    id=b.ID;
//                    nvbo.Savebcj(b);
//                }
//                else
//                {
//                    id = b.ID;
//                    message = b.jg+"<br>ID:"+b.ID;
//                    continue;
//                }
//            }
//            if (list.Count == 0)
//            {

//                message = "{\"meg\":\"end\",\"id\":" + id + "}";
//            }
//            else if (message != "")
//            {
//                message = "{\"meg\":\""+message+"\",\"id\":" + id + "}";
//            }
//            else
//            {
//                message = "{\"meg\":\"ok\",\"id\":" + id + "}";
//                //AddCard();
//            }
//            return message;
//        }
        /// <summary>
        /// 模板消息
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="wxOpenID"></param>
        /// <param name="tempID"></param>
        /// <param name="topColor"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public string SendTemplateMessageSR(string accessToken, string wxOpenID, string tempID, string topColor, object data, string url, string form_id)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("touser", wxOpenID);
            dict.Add("template_id", tempID.Trim());
            dict.Add("page", url);
            dict.Add("form_id", form_id);
            dict.Add("topcolor", "#FF0000");
            dict.Add("data", data);
            return this.DoJSONRequestSR("cgi-bin/message/wxopen/template/send?access_token=" + accessToken, dict, "POST");
        }
        private static string WeiXinUrl = "https://api.weixin.qq.com/";
        private string DoJSONRequestSR(string path, Dictionary<string, object> data, string method = "POST")
        {
            string strdata = JsonConvert.SerializeObject(data);

            if (!path.Contains("?"))
            {
                path += "?";
            }
            string url = WeiXinUrl + path;
            return NetHelper.HttpRequest(url, strdata, method, 60000, Encoding.UTF8, ContentTypes.JSON);
        }
        public class data
        {
            public Pet_JiuZhu_Info[] j { get; set; }
        }

        /// <summary>
        /// 将头像保存到本地
        /// </summary>
        private void GetLocalHeadImage()
        {
            WebClient web = new WebClient();
            string loclurl = "/wechat/HeadImage/";// +new Guid("d").ToString();
            string path = Server.MapPath(loclurl);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string guid = Guid.NewGuid().ToString("d");
            string path2 = path + guid + ".jpg";//缩略图
            path += guid + "Y.jpg";  //原图 缩略图后加个Y即为原图
            web.DownloadFile("http://wx.qlogo.cn/mmopen/JzdmghKzubCQzbQ1ibJLbPwzkiahMhva9Xx4jGqUbic6jN7uHa6jID4VU84vFGuugl6vssplkX3pWLryLDphsMyicJksB66qr6lE/0", path);
            MakeThumbnail(path, path2, 20, 20, true);
            string DownPic = loclurl + path + ".jpg";
        }

        /// <SUMMARY>
        /// 生成缩略图//带压缩图片不压缩22k压缩2k
        /// </SUMMARY>
        /// <PARAM name="originalImagePath" />原始路径
        /// <PARAM name="thumbnailPath" />生成缩略图路径
        /// <PARAM name="width" />缩略图的宽
        /// <PARAM name="height" />缩略图的高
        //是否压缩图片质量
        public void MakeThumbnail(string originalImagePath, string thumbnailPath, int width, int height, bool Ys)
        {
            //获取原始图片  
            System.Drawing.Image originalImage = System.Drawing.Image.FromFile(originalImagePath);
            //缩略图画布宽高 
            int towidth = 0;
            int toheight = 0;
            if (originalImage.Width > 1800) //图片像素大于1800，则缩放四倍
            {
                width = originalImage.Width / 4;
                height = originalImage.Height / 4;
                towidth = width;
                toheight = height;
            }
            else if (originalImage.Width > 600) //图片像素大于800，则缩放2倍
            {
                width = originalImage.Width / 4;
                height = originalImage.Height / 4;
                towidth = width;
                toheight = height;
            }
            else  //图片不是太大，则保持原大小
            {
                towidth = originalImage.Width; //width;
                toheight = originalImage.Height; //height;
                width = towidth;
                height = toheight;
            }
            //原始图片写入画布坐标和宽高(用来设置裁减溢出部分)  
            int x = 0;
            int y = 0;
            int ow = originalImage.Width;
            int oh = originalImage.Height;
            //原始图片画布,设置写入缩略图画布坐标和宽高(用来原始图片整体宽高缩放)  
            int bg_x = 0;
            int bg_y = 0;
            int bg_w = towidth;
            int bg_h = toheight;
            //倍数变量  
            double multiple = 0;
            //获取宽长的或是高长与缩略图的倍数  
            if (originalImage.Width >= originalImage.Height)
                multiple = (double)originalImage.Width / (double)width;
            else
                multiple = (double)originalImage.Height / (double)height;
            //上传的图片的宽和高小等于缩略图  
            if (ow <= width && oh <= height)
            {
                //缩略图按原始宽高  
                bg_w = originalImage.Width;
                bg_h = originalImage.Height;
                //空白部分用背景色填充  
                bg_x = Convert.ToInt32(((double)towidth - (double)ow) / 2);
                bg_y = Convert.ToInt32(((double)toheight - (double)oh) / 2);
            }
            //上传的图片的宽和高大于缩略图  
            else
            {
                //宽高按比例缩放  
                bg_w = Convert.ToInt32((double)originalImage.Width / multiple);
                bg_h = Convert.ToInt32((double)originalImage.Height / multiple);
                //空白部分用背景色填充  
                bg_y = Convert.ToInt32(((double)height - (double)bg_h) / 2);
                bg_x = Convert.ToInt32(((double)width - (double)bg_w) / 2);
            }
            //新建一个bmp图片,并设置缩略图大小.  
            System.Drawing.Image bitmap = new System.Drawing.Bitmap(towidth, toheight);
            //新建一个画板  
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap);
            //设置高质量插值法  
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
            //设置高质量,低速度呈现平滑程度  
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            //清空画布并设置背景色  
            g.Clear(System.Drawing.ColorTranslator.FromHtml("#FFF"));
            //在指定位置并且按指定大小绘制原图片的指定部分  
            //第一个System.Drawing.Rectangle是原图片的画布坐标和宽高,第二个是原图片写在画布上的坐标和宽高,最后一个参数是指定数值单位为像素  
            g.DrawImage(originalImage, new System.Drawing.Rectangle(bg_x, bg_y, bg_w, bg_h), new System.Drawing.Rectangle(x, y, ow, oh), System.Drawing.GraphicsUnit.Pixel);

            if (Ys)
            {

                System.Drawing.Imaging.ImageCodecInfo encoder = GetEncoderInfo("image/jpeg");
                try
                {
                    if (encoder != null)
                    {
                        System.Drawing.Imaging.EncoderParameters encoderParams = new System.Drawing.Imaging.EncoderParameters(1);
                        //设置 jpeg 质量为 60
                        encoderParams.Param[0] = new System.Drawing.Imaging.EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (long)50);
                        bitmap.Save(thumbnailPath, encoder, encoderParams);
                        encoderParams.Dispose();

                    }
                }
                catch (System.Exception e)
                {
                    //throw e;
                }
                finally
                {
                    originalImage.Dispose();
                    bitmap.Dispose();
                    g.Dispose();
                }

            }
            else
            {

                try
                {
                    //获取图片类型  
                    string fileExtension = System.IO.Path.GetExtension(originalImagePath).ToLower();
                    //按原图片类型保存缩略图片,不按原格式图片会出现模糊,锯齿等问题.  
                    switch (fileExtension)
                    {
                        case ".gif": bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Gif); break;
                        case ".jpg": bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg); break;
                        case ".bmp": bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Bmp); break;
                        case ".png": bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Png); break;
                    }
                }
                catch (System.Exception e)
                {
                    throw e;
                }
                finally
                {
                    originalImage.Dispose();
                    bitmap.Dispose();
                    g.Dispose();
                }

            }

        }

        private System.Drawing.Imaging.ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            //根据 mime 类型，返回编码器
            System.Drawing.Imaging.ImageCodecInfo result = null;
            System.Drawing.Imaging.ImageCodecInfo[] encoders = System.Drawing.Imaging.ImageCodecInfo.GetImageEncoders();
            for (int i = 0; i < encoders.Length; i++)
            {
                if (encoders[i].MimeType == mimeType)
                {
                    result = encoders[i];
                    break;
                }

            }
            return result;

        }
    }


}