using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using SPACRM.Business.ServiceImpl;
using SPACRM.Entity;
using SPACRM.Entity.Entities;
using SPACRM.Interface;

namespace SPACRM.WebApp
{
    public partial class Default : WeiPage
    {

        ISystemService sbo = new SystemService();
        const string Token = "puman";		//与微信平台那边填写的token一致
        protected void Page_Load(object sender, EventArgs e)
        {
            string postStr = "";
            if (Request.HttpMethod.ToLower() == "post")
            {
                Stream s = System.Web.HttpContext.Current.Request.InputStream;
                byte[] b = new byte[s.Length];
                s.Read(b, 0, (int)s.Length);
                postStr = Encoding.UTF8.GetString(b);

                if (!string.IsNullOrEmpty(postStr))
                {
                    //封装请求类
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(postStr);
                    XmlElement rootElement = doc.DocumentElement;

                    XmlNode MsgType = rootElement.SelectSingleNode("MsgType");

                    WXCUST_MSG_HIS requestXML = new WXCUST_MSG_HIS();
                    requestXML.TOUSERNAME = rootElement.SelectSingleNode("ToUserName").InnerText;
                    requestXML.FROMUSERNAME = rootElement.SelectSingleNode("FromUserName").InnerText;
                    requestXML.CREATE_DATE = UnixTimeToTime(rootElement.SelectSingleNode("CreateTime").InnerText);
                    requestXML.CONTENT = "";
                    requestXML.EVENTKEY = "";
                    requestXML.LABEL = "";
                    requestXML.LOCATION_X = "";
                    requestXML.LOCATION_Y = "";
                    requestXML.MSGID = "";
                    requestXML.PICURL = "";
                    requestXML.RESXML = "";
                    requestXML.SCALE = "";
                    requestXML.WXEVENT = "";
                    requestXML.VEDIOURL = "";
                    requestXML.STATUS = "0";


                    requestXML.MSGTYPE = MsgType.InnerText;

                    if (requestXML.MSGTYPE == "text")
                    {
                        requestXML.CONTENT = rootElement.SelectSingleNode("Content").InnerText;
                        requestXML.MSGID = rootElement.SelectSingleNode("MsgId").InnerText;
                    }
                    else if (requestXML.MSGTYPE == "location")
                    {
                        requestXML.LOCATION_X = rootElement.SelectSingleNode("Location_X").InnerText;
                        requestXML.LOCATION_Y = rootElement.SelectSingleNode("Location_Y").InnerText;
                        requestXML.SCALE = rootElement.SelectSingleNode("Scale").InnerText;
                        requestXML.LABEL = rootElement.SelectSingleNode("Label").InnerText;
                    }
                    else if (requestXML.MSGTYPE == "image")
                    {
                        requestXML.PICURL = rootElement.SelectSingleNode("PicUrl").InnerText;
                    }
                    else if (requestXML.MSGTYPE == "event")
                    {
                        requestXML.WXEVENT = rootElement.SelectSingleNode("Event").InnerText;

                        if (requestXML.WXEVENT.ToLower() == "location")  //高级接口获取到的地理位置
                        {
                            requestXML.LOCATION_Y = rootElement.SelectSingleNode("Latitude").InnerText;  //纬度
                            requestXML.LOCATION_X = rootElement.SelectSingleNode("Longitude").InnerText;  //经度
                            requestXML.SCALE = rootElement.SelectSingleNode("Precision").InnerText;  //精度
                        }
                        else
                        {
                            requestXML.EVENTKEY = rootElement.SelectSingleNode("EventKey").InnerText;
                        }
                    }
                    WriteTxt("----------粉丝发送过来的消息，消息类型：" + requestXML.MSGTYPE + "----------：" + postStr);
                    //回复消息
                    ResponseMsg(requestXML);
                }
            }
            else
            {
                // WriteTxt("异常：");
                Valid();
            }
        }

        /// <summary>
        /// 验证微信签名
        /// </summary>
        /// * 将token、timestamp、nonce三个参数进行字典序排序
        /// * 将三个参数字符串拼接成一个字符串进行sha1加密
        /// * 开发者获得加密后的字符串可与signature对比，标识该请求来源于微信。
        /// <returns></returns>
        private bool CheckSignature()
        {
            string signature = Request.QueryString["signature"];
            string timestamp = Request.QueryString["timestamp"];
            string nonce = Request.QueryString["nonce"];
            string[] ArrTmp = { Token, timestamp, nonce };
            Array.Sort(ArrTmp);     //字典排序
            string tmpStr = string.Join("", ArrTmp);
            tmpStr = FormsAuthentication.HashPasswordForStoringInConfigFile(tmpStr, "SHA1");
            tmpStr = tmpStr.ToLower();
            if (tmpStr == signature)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void Valid()
        {
            string echoStr = Request.QueryString["echoStr"];
            if (CheckSignature())
            {
                if (!string.IsNullOrEmpty(echoStr))
                {
                    Response.Write(echoStr);
                    Response.End();
                }
            }
        }

        /// <summary>
        /// 回复消息(微信信息返回)
        /// </summary>
        /// <param name="weixinXML"></param>
        private void ResponseMsg(WXCUST_MSG_HIS requestXML)
        {
            string resxml = "<xml><ToUserName><![CDATA[" + requestXML.FROMUSERNAME + "]]></ToUserName><FromUserName><![CDATA[" + requestXML.TOUSERNAME + "]]></FromUserName><CreateTime>" + ConvertDateTimeInt(DateTime.Now) + "</CreateTime>";
            try
            {
                if (requestXML.MSGTYPE == "text")
                {
                    if (requestXML.CONTENT == "海报")
                    {
                        new SeaNews().SeadSeaNews(requestXML.FROMUSERNAME);
                        resxml += "<MsgType><![CDATA[text]]></MsgType><Content><![CDATA[生成海报]]></Content><FuncFlag>0</FuncFlag></xml>";
                    }
                    else
                    {
                        int count = 0;
                        List<Information_EX> list = sbo.GetModelList(string.Format("  ((KeyWords like '%{0}%' and MatchingType=0) or (KeyWords ='{0}' and MatchingType=1)) AND replytype={1} and ToUserName='{2}'", requestXML.CONTENT, 1, requestXML.TOUSERNAME));
                        if (list.Count == 1 || (list.Count > 1 && list[0].MsgType != "news"))
                        {
                            Information_EX f = list[0];// ibo.GetModel(requestXML.EventKey, 4); //自定义菜单回复

                            if (f != null)
                            {

                                switch (f.MsgType)
                                {
                                    case "text":
                                        resxml += "<MsgType><![CDATA[text]]></MsgType><Content><![CDATA[" + f.Content + "]]></Content><FuncFlag>0</FuncFlag></xml>";
                                        break;
                                    case "news":
                                        resxml += "<MsgType><![CDATA[news]]></MsgType><ArticleCount>1</ArticleCount><Articles>";
                                        resxml += "<item><Title><![CDATA[" + f.Title + "]]></Title><Description><![CDATA[" + f.Description + "]]></Description><PicUrl><![CDATA[" + ConfigurationSettings.AppSettings["WebUrl"] + f.PicUrl + "]]></PicUrl><Url><![CDATA[" + (f.IsURL ? (f.FulltextUrl.Contains("?id") ? (f.FulltextUrl + "&FromUserName=" + requestXML.FROMUSERNAME + "&ToUserName=" + requestXML.TOUSERNAME + "") : (f.FulltextUrl + "?FromUserName=" + requestXML.FROMUSERNAME + "&ToUserName=" + requestXML.TOUSERNAME + "")) : (ConfigurationSettings.AppSettings["WebUrl"] + "/GraphicDisplay.aspx?id=" + f.DID)) + "]]></Url></item>";//URL是点击之后跳转去那里，这里跳转到百度
                                        resxml += "</Articles><FuncFlag>0</FuncFlag></xml>";
                                        break;
                                    case "music":
                                        resxml += string.Format(@"<MsgType><![CDATA[music]]></MsgType>
                                 <Music>
                                 <Title><![CDATA[{0}]]></Title>
                                 <Description><![CDATA[{1}]]></Description>
                                 <MusicUrl><![CDATA[{2}]]></MusicUrl>
                                 <HQMusicUrl><![CDATA[{2}]]></HQMusicUrl>
                                 </Music>
                                 <FuncFlag>0</FuncFlag>
                                 </xml>", f.Title, f.Description, ConfigurationSettings.AppSettings["WebUrl"] + f.PicUrl);
                                        break;
                                    default:
                                        //resxml += "<MsgType><![CDATA[text]]></MsgType><Content><![CDATA[感谢您的支持]]></Content><FuncFlag>0</FuncFlag></xml>";
                                        break;
                                }

                            }
                        }
                        else if (list.Count > 1) //多图
                        {
                            count = list.Count;
                            resxml += "<MsgType><![CDATA[news]]></MsgType><ArticleCount>" + count + "</ArticleCount><Articles>";
                            for (int i = 0; i < count; i++)
                            {
                                resxml += "<item><Title><![CDATA[" + list[i].Title + "]]></Title><Description><![CDATA[" + list[i].Description + "]]></Description><PicUrl><![CDATA[" + ConfigurationSettings.AppSettings["WebUrl"] + list[i].PicUrl + "]]></PicUrl><Url><![CDATA[" + (list[i].IsURL ? (list[i].FulltextUrl.Contains("?id") ? (list[i].FulltextUrl + "&FromUserName=" + requestXML.FROMUSERNAME + "&ToUserName=" + requestXML.TOUSERNAME + "") : (list[i].FulltextUrl + "?FromUserName=" + requestXML.FROMUSERNAME + "&ToUserName=" + requestXML.TOUSERNAME + "")) : (ConfigurationSettings.AppSettings["WebUrl"] + "/GraphicDisplay.aspx?id=" + list[i].DID)) + "]]></Url></item>";//URL是点击之后跳转去那里，这里跳转到百度
                            }
                            resxml += "</Articles><FuncFlag>0</FuncFlag></xml>";
                        }
                        else
                        {
                            list = sbo.GetModelList(string.Format("   replytype={0} and ToUserName='{1}'", 3, requestXML.TOUSERNAME));
                            if (list.Count == 1 || (list.Count > 1 && list[0].MsgType != "news"))
                            {
                                Information_EX f = list[0];

                                if (f != null)
                                {

                                    switch (f.MsgType)
                                    {
                                        case "text":
                                            resxml += "<MsgType><![CDATA[text]]></MsgType><Content><![CDATA[" + f.Content + "]]></Content><FuncFlag>0</FuncFlag></xml>";
                                            break;
                                        case "news":
                                            resxml += "<MsgType><![CDATA[news]]></MsgType><ArticleCount>1</ArticleCount><Articles>";
                                            resxml += "<item><Title><![CDATA[" + f.Title + "]]></Title><Description><![CDATA[" + f.Description + "]]></Description><PicUrl><![CDATA[" + ConfigurationSettings.AppSettings["WebUrl"] + f.PicUrl + "]]></PicUrl><Url><![CDATA[" + (f.IsURL ? (f.FulltextUrl.Contains("?id") ? (f.FulltextUrl + "&FromUserName=" + requestXML.FROMUSERNAME + "&ToUserName=" + requestXML.TOUSERNAME + "") : (f.FulltextUrl + "?FromUserName=" + requestXML.FROMUSERNAME + "&ToUserName=" + requestXML.TOUSERNAME + "")) : (ConfigurationSettings.AppSettings["WebUrl"] + "/GraphicDisplay.aspx?id=" + f.DID)) + "]]></Url></item>";//URL是点击之后跳转去那里，这里跳转到百度
                                            resxml += "</Articles><FuncFlag>0</FuncFlag></xml>";
                                            break;
                                        case "music":
                                            resxml += string.Format(@"<MsgType><![CDATA[music]]></MsgType>
                                 <Music>
                                 <Title><![CDATA[{0}]]></Title>
                                 <Description><![CDATA[{1}]]></Description>
                                 <MusicUrl><![CDATA[{2}]]></MusicUrl>
                                 <HQMusicUrl><![CDATA[{2}]]></HQMusicUrl>
                                 </Music>
                                 <FuncFlag>0</FuncFlag>
                                 </xml>", f.Title, f.Description, ConfigurationSettings.AppSettings["WebUrl"] + f.PicUrl);
                                            break;
                                        default:
                                            //resxml += "<MsgType><![CDATA[text]]></MsgType><Content><![CDATA[感谢您的支持]]></Content><FuncFlag>0</FuncFlag></xml>";
                                            break;
                                    }

                                }
                            }
                            else if (list.Count > 1) //多图
                            {
                                int count2 = list.Count;
                                resxml += "<MsgType><![CDATA[news]]></MsgType><ArticleCount>" + count + "</ArticleCount><Articles>";
                                for (int z = 0; z < count2; z++)
                                {
                                    resxml += "<item><Title><![CDATA[" + list[z].Title + "]]></Title><Description><![CDATA[" + list[z].Description + "]]></Description><PicUrl><![CDATA[" + ConfigurationSettings.AppSettings["WebUrl"] + list[z].PicUrl + "]]></PicUrl><Url><![CDATA[" + (list[z].IsURL ? (list[z].FulltextUrl.Contains("?id") ? (list[z].FulltextUrl + "&FromUserName=" + requestXML.FROMUSERNAME + "&ToUserName=" + requestXML.TOUSERNAME + "") : (list[z].FulltextUrl + "?FromUserName=" + requestXML.FROMUSERNAME + "&ToUserName=" + requestXML.TOUSERNAME + "")) : (ConfigurationSettings.AppSettings["WebUrl"] + "/GraphicDisplay.aspx?id=" + list[z].DID)) + "]]></Url></item>";//URL是点击之后跳转去那里，这里跳转到百度
                                }
                                resxml += "</Articles><FuncFlag>0</FuncFlag></xml>";
                            }
                        }
                    }
                }

                else if (requestXML.MSGTYPE == "event")
                {
                    if (requestXML.WXEVENT == "CLICK")
                    {
                        if (requestXML.EVENTKEY.ToLower() == "dkf") //多客服接口
                        {
                            resxml += "<MsgType><![CDATA[transfer_customer_service]]></MsgType></xml>";
                        }
                        else
                        {
                            List<CustomMenu_EX> list = sbo.GetCustomMenuModelList(string.Format("  c.ID='{0}'", requestXML.EVENTKEY));
                            if (list.Count == 1 || (list.Count > 1 && list[0].MsgType != "news"))
                            {
                                CustomMenu_EX f = list[0];// ibo.GetModel(requestXML.EventKey, 4); //自定义菜单回复

                                if (f != null)
                                {

                                    switch (f.MsgType)
                                    {
                                        case "text":
                                            resxml += "<MsgType><![CDATA[text]]></MsgType><Content><![CDATA[" + f.Content + "]]></Content><FuncFlag>0</FuncFlag></xml>";
                                            break;
                                        case "news":
                                            resxml += "<MsgType><![CDATA[news]]></MsgType><ArticleCount>1</ArticleCount><Articles>";
                                            resxml += "<item><Title><![CDATA[" + f.Title + "]]></Title><Description><![CDATA[" + f.Description + "]]></Description><PicUrl><![CDATA[" + ConfigurationSettings.AppSettings["WebUrl"] + f.PicUrl + "]]></PicUrl><Url><![CDATA[" + (f.IsURL ? (f.FulltextUrl.Contains("?id") ? (f.FulltextUrl + "&FromUserName=" + requestXML.FROMUSERNAME + "&ToUserName=" + requestXML.TOUSERNAME + "") : (f.FulltextUrl + "?FromUserName=" + requestXML.FROMUSERNAME + "&ToUserName=" + requestXML.TOUSERNAME + "")) : (ConfigurationSettings.AppSettings["WebUrl"] + "/GraphicDisplay.aspx?id=" + f.DID)) + "]]></Url></item>";//URL是点击之后跳转去那里，这里跳转到百度
                                            resxml += "</Articles><FuncFlag>0</FuncFlag></xml>";
                                            break;
                                        case "music":
                                            resxml += string.Format(@"<MsgType><![CDATA[music]]></MsgType>
                                 <Music>
                                 <Title><![CDATA[{0}]]></Title>
                                 <Description><![CDATA[{1}]]></Description>
                                 <MusicUrl><![CDATA[{2}]]></MusicUrl>
                                 <HQMusicUrl><![CDATA[{2}]]></HQMusicUrl>
                                 </Music>
                                 <FuncFlag>0</FuncFlag>
                                 </xml>", f.Title, f.Description, ConfigurationSettings.AppSettings["WebUrl"] + f.PicUrl);
                                            break;
                                        default:
                                            //resxml += "<MsgType><![CDATA[text]]></MsgType><Content><![CDATA[感谢您的支持]]></Content><FuncFlag>0</FuncFlag></xml>";
                                            break;
                                    }

                                }
                            }
                            else if (list.Count > 1) //多图
                            {
                                int count = list.Count;
                                resxml += "<MsgType><![CDATA[news]]></MsgType><ArticleCount>" + count + "</ArticleCount><Articles>";
                                for (int i = 0; i < count; i++)
                                {
                                    resxml += "<item><Title><![CDATA[" + list[i].Title + "]]></Title><Description><![CDATA[" + list[i].Description + "]]></Description><PicUrl><![CDATA[" + ConfigurationSettings.AppSettings["WebUrl"] + list[i].PicUrl + "]]></PicUrl><Url><![CDATA[" + (list[i].IsURL ? (list[i].FulltextUrl.Contains("?id") ? (list[i].FulltextUrl + "&FromUserName=" + requestXML.FROMUSERNAME + "&ToUserName=" + requestXML.TOUSERNAME + "") : (list[i].FulltextUrl + "?FromUserName=" + requestXML.FROMUSERNAME + "&ToUserName=" + requestXML.TOUSERNAME + "")) : (ConfigurationSettings.AppSettings["WebUrl"] + "/GraphicDisplay.aspx?id=" + list[i].DID)) + "]]></Url></item>";//URL是点击之后跳转去那里，这里跳转到百度
                                }
                                resxml += "</Articles><FuncFlag>0</FuncFlag></xml>";
                            }
                        }
                    }
                    else if (requestXML.WXEVENT == "unsubscribe")
                    {
                        //取消关注 把粉丝表更新状态为0

                        InsertFS(requestXML.FROMUSERNAME, requestXML.TOUSERNAME, 0);  //插入粉丝信息

                    }
                    else if (requestXML.WXEVENT.ToLower() == "location") //获取地理位置
                    {

                    }
                    else if (requestXML.WXEVENT.ToLower() == "scan" || (requestXML.WXEVENT.ToLower() == "subscribe" && requestXML.EVENTKEY.ToLower().Contains("qrscene"))) //扫描二维码
                    {
                        string EVENTKEY = requestXML.EVENTKEY;
                        requestXML.EVENTKEY = requestXML.EVENTKEY.ToLower().Contains("qrscene") ? requestXML.EVENTKEY.Split('_')[1] : requestXML.EVENTKEY;
                        new MySmallShopService().SaveQRLog(new ORG_WX_QRLog { CreateTime = DateTime.Now, FromUserName = requestXML.FROMUSERNAME, QRCodeID = int.Parse(requestXML.EVENTKEY), EVENTKEY = EVENTKEY });
                        if (requestXML.WXEVENT.ToLower() == "subscribe")
                        {
                            resxml = Subscribe(requestXML, resxml);
                        }
                        //string jg = InsertQR(requestXML.FROMUSERNAME, requestXML.EVENTKEY);  //插入扫描记录
                        //if (jg == "扫描成功")
                        //{
                        //    WXGraphicDetail i = sbo.GetGraphicDetail(53);
                        //    if (i != null)
                        //    {

                        //        resxml += "<MsgType><![CDATA[news]]></MsgType><ArticleCount>1</ArticleCount><Articles>";
                        //        resxml += "<item><Title><![CDATA[" + i.Title + "]]></Title><Description><![CDATA[" + i.Describe + "]]></Description><PicUrl><![CDATA[" + WebUrl() + i.URL + "]]></PicUrl><Url><![CDATA[" + (i.IsURL.Value ? (i.URL.Contains("?id") ? (i.URL + "&FromUserName=" + requestXML.FROMUSERNAME + "&ToUserName=" + requestXML.TOUSERNAME + "") : (i.URL + "?FromUserName=" + requestXML.FROMUSERNAME + "&ToUserName=" + requestXML.TOUSERNAME + "")) : (WebUrl() + "/GraphicDisplay.aspx?id=" + i.ID)) + "]]></Url></item>";//URL是点击之后跳转去那里，这里跳转到百度
                        //        resxml += "</Articles><FuncFlag>0</FuncFlag></xml>";
                        //    }
                        //}
                        //else
                        //resxml += "<MsgType><![CDATA[text]]></MsgType><Content><![CDATA[" + jg + "]]></Content><FuncFlag>0</FuncFlag></xml>";
                    }

                    //关注
                    else if (requestXML.WXEVENT.ToLower() == "subscribe")
                    {

                        resxml = Subscribe(requestXML, resxml);
                    }

                }
                requestXML.RESXML = resxml;
                if (!resxml.Contains("</xml>"))
                {
                    resxml = "";
                    //resxml += "<MsgType><![CDATA[text]]></MsgType><Content></Content><FuncFlag>0</FuncFlag></xml>";
                }
                if (requestXML.MSGTYPE == "text")
                {
                    try
                    {
                        WXCUST_MSG_RECORD msg = new WXCUST_MSG_RECORD();
                        msg.CONTENT = requestXML.CONTENT;
                        msg.CREATE_DATE = DateTime.Now;
                        msg.FROMUSERNAME = requestXML.FROMUSERNAME;
                        msg.GraphicID = 0;
                        msg.IS_RETURN = false;
                        msg.IS_STAR = false;
                        msg.MSGTYPE = "text";
                        msg.ReturnID = 0;
                        msg.State = 0;
                        msg.TOUSERNAME = requestXML.TOUSERNAME;
                        sbo.SaveCUST_MSG_RECORD(msg);
                    }
                    catch (Exception)
                    {

                    }
                }
                sbo.AddCUST_MSG_HIS(requestXML);
            }
            catch (Exception ex)
            {
                //WriteTxt("异常：" + ex.Message);
                //WriteTxt(sql);
                resxml += "<MsgType><![CDATA[text]]></MsgType><Content><![CDATA[" + ex.Message.ToString() + "]]></Content><FuncFlag>0</FuncFlag></xml>";
                Response.Write(resxml);
            }
            //if (!resxml.Contains("</xml>"))
            //{
            //    resxml += "<MsgType><![CDATA[text]]></MsgType><Content><![CDATA[]]></Content><FuncFlag>0</FuncFlag></xml>";
            //}
            WriteTxt("返回给粉丝的消息：" + resxml);

            Response.Write(resxml);
            Response.End();
        }

        private string Subscribe(WXCUST_MSG_HIS requestXML, string resxml)
        {
            // WriteTxt("----------关注信息，消息类型：" + requestXML.MSGTYPE + "----------：" + postStr);
            //获取被关注回复的消息
            List<Information_EX> list = sbo.GetModelList(string.Format("   replytype={0} and ToUserName='{1}'", 2, requestXML.TOUSERNAME));
            if (list.Count == 1 || (list.Count > 1 && list[0].MsgType != "news"))
            {
                Information_EX i = list[0];
                if (i != null)
                {
                    switch (i.MsgType)
                    {
                        case "text":
                            resxml += "<MsgType><![CDATA[text]]></MsgType><Content><![CDATA[" + i.Content + "]]></Content><FuncFlag>0</FuncFlag></xml>";
                            break;
                        case "news":
                            resxml += "<MsgType><![CDATA[news]]></MsgType><ArticleCount>1</ArticleCount><Articles>";
                            resxml += "<item><Title><![CDATA[" + i.Title + "]]></Title><Description><![CDATA[" + i.Description + "]]></Description><PicUrl><![CDATA[" + ConfigurationSettings.AppSettings["WebUrl"] + i.PicUrl + "]]></PicUrl><Url><![CDATA[" + (i.IsURL ? (i.FulltextUrl.Contains("?id") ? (i.FulltextUrl + "&FromUserName=" + requestXML.FROMUSERNAME + "&ToUserName=" + requestXML.TOUSERNAME + "") : (i.FulltextUrl + "?FromUserName=" + requestXML.FROMUSERNAME + "&ToUserName=" + requestXML.TOUSERNAME + "")) : (ConfigurationSettings.AppSettings["WebUrl"] + "/GraphicDisplay.aspx?id=" + i.DID)) + "]]></Url></item>";//URL是点击之后跳转去那里，这里跳转到百度
                            resxml += "</Articles><FuncFlag>0</FuncFlag></xml>";
                            break;
                        case "music":
                            resxml += string.Format(@"<MsgType><![CDATA[music]]></MsgType>
                                 <Music>
                                 <Title><![CDATA[{0}]]></Title>
                                 <Description><![CDATA[{1}]]></Description>
                                 <MusicUrl><![CDATA[{2}]]></MusicUrl>
                                 <HQMusicUrl><![CDATA[{2}]]></HQMusicUrl>
                                 </Music>
                                 <FuncFlag>0</FuncFlag>
                                 </xml>", i.Title, i.Description, ConfigurationSettings.AppSettings["WebUrl"] + i.PicUrl);
                            break;
                        default:
                            //resxml += "<MsgType><![CDATA[text]]></MsgType><Content><![CDATA[感谢您的关注]]></Content><FuncFlag>0</FuncFlag></xml>";
                            break;
                    }
                }
                else
                {
                    // resxml += "<MsgType><![CDATA[text]]></MsgType><Content><![CDATA[感谢您的关注]]></Content><FuncFlag>0</FuncFlag></xml>";
                }
            }
            else if (list.Count > 1) //多图
            {
                int count = list.Count;
                resxml += "<MsgType><![CDATA[news]]></MsgType><ArticleCount>" + count + "</ArticleCount><Articles>";
                for (int i = 0; i < count; i++)
                {
                    resxml += "<item><Title><![CDATA[" + list[i].Title + "]]></Title><Description><![CDATA[" + list[i].Description + "]]></Description><PicUrl><![CDATA[" + ConfigurationSettings.AppSettings["WebUrl"] + list[i].PicUrl + "]]></PicUrl><Url><![CDATA[" + (list[i].IsURL ? (list[i].FulltextUrl.Contains("?id") ? (list[i].FulltextUrl + "&FromUserName=" + requestXML.FROMUSERNAME + "&ToUserName=" + requestXML.TOUSERNAME + "") : (list[i].FulltextUrl + "?FromUserName=" + requestXML.FROMUSERNAME + "&ToUserName=" + requestXML.TOUSERNAME + "")) : (ConfigurationSettings.AppSettings["WebUrl"] + "/GraphicDisplay.aspx?id=" + list[i].DID)) + "]]></Url></item>";//URL是点击之后跳转去那里，这里跳转到百度
                }
                resxml += "</Articles><FuncFlag>0</FuncFlag></xml>";

            }


            ////获取Token字符串
            //string access_token = acc_Token();
            ////获取用户信息列表
            //string info = "https://api.weixin.qq.com/cgi-bin/user/info?access_token=" + access_token + "&openid=" + requestXML.FROMUSERNAME + "&lang=zh_CN";
            //string infomes = PostRequestGet(info);
            //Info winfo = JsonConvert.DeserializeObject<Info>(infomes);
            ////把用户的信息存到粉丝表,保存之前先判断粉丝表里面有没有这个用户
            ////确定用户关注成功了
            //if (winfo.subscribe == 1)
            //{

            //}

            InsertFS(requestXML.FROMUSERNAME, requestXML.TOUSERNAME, 1);  //插入粉丝信息
            SaveOA(requestXML.FROMUSERNAME, requestXML.TOUSERNAME);
            return resxml;
        }



        /// <summary>
        /// unix时间转换为datetime
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        private DateTime UnixTimeToTime(string timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);
            return dtStart.Add(toNow);
        }

        /// <summary>
        /// datetime转换为unixtime
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        private int ConvertDateTimeInt(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return (int)(time - startTime).TotalSeconds;
        }

        /// <summary>
        /// 记录bug，以便调试
        /// </summary>
        /// 
        /// <returns></returns>
        public bool WriteTxt(string str)
        {
            try
            {
                //FileStream fs = new FileStream(@"D:\WeiXin\web\buglog.txt", FileMode.Append);
                //StreamWriter sw = new StreamWriter(fs);
                ////开始写入
                //sw.WriteLine(str);
                ////清空缓冲区
                //sw.Flush();
                ////关闭流
                //sw.Close();
                //fs.Close();
                WXLOG l = new WXLOG();
                l.CON = str;
                l.TIME = DateTime.Now;
                sbo.AddLog(l);
            }
            catch (Exception)
            {
                return false;
            }
            return true;

        }

        class Info
        {
            public int subscribe{get;set;}
            public string openid { get; set; }
            public string nickname { get; set; }
            public string sex { get; set; }
            public string language { get; set; }
            public string city { get; set; }
            public string province { get; set; }
            public string country { get; set; }
            public string headimgurl { get; set; }
            public string subscribe_time { get; set; }
        }


        //发送GET请求
        public string PostRequestGet(string url)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.Method = "GET";  //定义请求对象，并设置好请求URL地址      
            //request.ProtocolVersion = HttpVersion.Version10;
            //request.ContentType = "image/jpg";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();    //定义响应对象，request在调用GetResponse方法事执行请求了，而不是在HttpWebRequest.Create的时候执行。
            //response.ContentType = "image/jpg";
            Stream stream = response.GetResponseStream(); //定义一个流对象，来获取响应流
            StreamReader sr = new StreamReader(stream, Encoding.UTF8);  //定义一个流读取对象，读取响应流
            string responseHTML = sr.ReadToEnd();
            return responseHTML;
        }




        public string PostRequest(string url)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);  //定义请求对象，并设置好请求URL地址
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();    //定义响应对象，request在调用GetResponse方法事执行请求了，而不是在HttpWebRequest.Create的时候执行。
            Stream stream = response.GetResponseStream(); //定义一个流对象，来获取响应流
            StreamReader sr = new StreamReader(stream);  //定义一个流读取对象，读取响应流
            string responseHTML = sr.ReadToEnd();
            return responseHTML;
        }

    }
}
