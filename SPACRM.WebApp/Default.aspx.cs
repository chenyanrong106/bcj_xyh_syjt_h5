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
using Vulcan.Framework.DBConnectionManager;

namespace SPACRM.WebApp
{
    public partial class Default : WeiPage
    {

        ISystemService sbo = new SystemService();
        string Token =ConfigurationSettings.AppSettings["Token"]; // "niting";		//与微信平台那边填写的token一致
        private static object lockobject = new object();
        protected void Page_Load(object sender, EventArgs e)
        {

            //string str1 = HttpContext.Current.Server.UrlEncode("http://bcj2.puman.cn/wechat/SPA/HFOldAndNew.aspx");
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
                    WriteTxt("----------粉丝发送过来的消息，事件类型：" + requestXML.WXEVENT);
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
            WriteTxt(requestXML.MSGTYPE);
            string resxml = "<xml><ToUserName><![CDATA[" + requestXML.FROMUSERNAME + "]]></ToUserName><FromUserName><![CDATA[" + requestXML.TOUSERNAME + "]]></FromUserName><CreateTime>" + ConvertDateTimeInt(DateTime.Now) + "</CreateTime>";
            try
            {
                if (requestXML.MSGTYPE == "text")
                {
                    if (requestXML.CONTENT == "海报")
                    {
                        
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
                        WriteTxt("CLICK");
                        if (requestXML.EVENTKEY == InviteMenuId)
                        {
                            resxml += "<MsgType><![CDATA[text]]></MsgType><Content><![CDATA[分享海报邀请新用户，福利多多：\n• 邀请关注，赢佰草集太极.昼御夜养.源生霜、清肌养颜太极泥、三丽鸥KITTY礼品杯\n• 关注后注册绑定，还可获得380元代金券 \n\n点击进入<a href='" + WebUrl + "/wechat/SPA/HFOldAndNew.aspx'>活动主会场</a>\n分享海报,赢惊喜大礼!]]></Content><FuncFlag>0</FuncFlag></xml>";

                            
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
                        WriteTxt("取消关注");
                        if (DateTime.Now < DateTime.Parse(ActiveEndDate))
                        {
                            InsertFS(requestXML.FROMUSERNAME, requestXML.TOUSERNAME, 0);  //插入粉丝信息
                            if (DateTime.Now < DateTime.Parse(ActiveEndDate))
                            {
                                MySmallShopService mss = new MySmallShopService();
                                //将表Cust_Old_New中的Jie改成 - 1 ToUserName = openid
                                //将表Cust_Old_New中的Jie改成 - 1 FromUserName = openid  ToUserName = fx
                                mss.UpdateCustOldNewJie(requestXML.FROMUSERNAME);
                                //将表OAauth_Log中的字段SeaImg = '' 参加活动数量-1
                                OAauth_Log oa = mss.GetOA(requestXML.FROMUSERNAME);
                                if (oa != null)
                                {
                                    oa.SeaImg = "";
                                    oa.SeaSource = null;
                                    mss.SaveOA(oa);
                                }
                            }
                        }

                    }
                    else if (requestXML.WXEVENT.ToLower() == "location") //获取地理位置
                    {
                        WriteTxt("获取地理位置");
                    }
                    else if (requestXML.WXEVENT.ToLower() == "scan" || (requestXML.WXEVENT.ToLower() == "subscribe" && requestXML.EVENTKEY.ToLower().Contains("qrscene"))) //扫描二维码
                    {
                        WriteTxt("扫二维码");
                        string EVENTKEY = requestXML.EVENTKEY;
                        requestXML.EVENTKEY = requestXML.EVENTKEY.ToLower().Contains("qrscene") ? requestXML.EVENTKEY.Split('_')[1] : requestXML.EVENTKEY;
                        int num = int.Parse(requestXML.EVENTKEY);

                        if (requestXML.WXEVENT.ToLower() == "subscribe")
                        {
                            if (num > 10000) //邀请海报
                            {
                                resxml = OldAndNew(requestXML, resxml, num);
                            }
                            resxml = Subscribe(requestXML, resxml);
                        }
                    }

                    //关注
                    else if (requestXML.WXEVENT.ToLower() == "subscribe")
                    {
                        WriteTxt("关注");
                        resxml = Subscribe(requestXML, resxml);
                        if (DateTime.Now < DateTime.Parse(ActiveEndDate))
                        {
                     
                        }
                    }

                }
                requestXML.RESXML = resxml;
                if (!resxml.Contains("</xml>"))
                {
                    resxml = "";
                }


                AddCUST_MSG_HISHandler handler = new AddCUST_MSG_HISHandler(sbo.AddCUST_MSG_HIS);
                int result = handler.Invoke(requestXML);
                //sbo.AddCUST_MSG_HIS(requestXML);



            }
            catch (Exception ex)
            {
                resxml = "";
            }
            finally
            {
                Response.Write(resxml);
                Response.End();
            }

        }

        public delegate int AddCUST_MSG_HISHandler(WXCUST_MSG_HIS his);

        public string OldAndNew(WXCUST_MSG_HIS requestXML, string resxml, int num)
        {
            OrderService osbo = new OrderService();
            MySmallShopService mss = new MySmallShopService();
            OAauth_Log oa = mss.GetOA(num - 10000);
            if (oa != null)
            {
                lock (lockobject)
                {
                    OAauth_Log o = SaveOA(requestXML.FROMUSERNAME, requestXML.TOUSERNAME);
                    using (TransScope scope = new TransScope(System.Configuration.ConfigurationManager.ConnectionStrings[SPACRM.Common.AppConfig.MainDbKey].ConnectionString))
                    {
                        if (o != null && requestXML.WXEVENT.ToLower() == "subscribe")
                        {
                            if (o.FromUserName != oa.FromUserName)
                            {
                                int jie = 0;
                                if (DateTime.Now < DateTime.Parse(ActiveEndDate))
                                {
                                    jie = 2;
                                }
                                Cust_Old_New old = new Cust_Old_New
                                {
                                    CreateTime = DateTime.Now,
                                    FromUserName = oa.FromUserName,
                                    OrderID = 0,
                                    Price = 20,
                                    State = 0,
                                    ToUserName = o.FromUserName,
                                    ToUserNickName = o.Nickname,
                                    Jie = jie
                                };
                                osbo.SaveCustOldNew(old);
                                //给推荐人一个抽奖码
                                if (DateTime.Now < DateTime.Parse(ActiveEndDate))
                                {

                                    try
                                    {
                                        string token = Token(mjuserid);
                                        var temp = new
                                        {
                                            first = new { value = "活动状态变更通知", color = "#173177" },
                                            keyword1 = new { value = "棒棒哒！您邀请的好友**关注【佰草集汉方SPA】啦！", color = "#173177" },
                                            keyword2 = new { value = "小集", color = "#173177" },
                                            keyword3 = new { value = "以礼邀约，迎接初夏", color = "#173177" },
                                            keyword4 = new { value = "2018年4月27日18:00:00 - 5月3日23:59:59", color = "#173177" },

                                        };
                                        string message = SendTemplateMessage(token, oa.FromUserName, ActiveStateChangeTmpId, "#FF0000", temp, WebUrl + "/wechat/SPA/HFOldAndNew.aspx");//Seapage2
                                    }
                                    catch (Exception)
                                    {
                                    }
                                }
                            }
                        }
                        resxml += "<MsgType><![CDATA[text]]></MsgType><Content><![CDATA[感谢您的关注，小集在此等候多时啦！\n\n注册绑定即可获得380元代金券\n凭券消费入店，还可领取入店礼“佰草集汉方SPA金致玉妍院线产品2件套” 一份\n\n<a href='" + WebUrl + "/wechat/SPA/HFOldAndNew.aspx'>邀好友赢大奖</a> \n活动奖品：赢佰草集太极.昼御夜养.源生霜、清肌养颜太极泥、三丽鸥KITTY礼品杯\n\n保存下图并分享参与活动吧！]]></Content><FuncFlag>0</FuncFlag></xml>";

                        //生成海报
                        if (DateTime.Now < DateTime.Parse(ActiveEndDate))
                        {
                           
                        }
                        scope.Commit();
                    }
                }
            }
            return resxml;
        }

        private string Subscribe(WXCUST_MSG_HIS requestXML, string resxml)
        {
            resxml += "<MsgType><![CDATA[text]]></MsgType><Content><![CDATA[感谢您的关注，小集在此等候多时啦！\n\n<a href='" + RegisterUrl + "'>注册绑定即可获得380元代金券</a>\n凭券消费入店，还可领取入店礼“佰草集汉方SPA金致玉妍院线产品2件套” 一份\n\n<a href='" + WebUrl + "/wechat/SPA/HFOldAndNew.aspx'>邀好友赢大奖</a> \n活动奖品：赢佰草集太极.昼御夜养.源生霜、清肌养颜太极泥、三丽鸥KITTY礼品杯\n\n保存上图并分享参与活动吧！]]></Content><FuncFlag>0</FuncFlag></xml>";
            // WriteTxt("----------关注信息，消息类型：" + requestXML.MSGTYPE + "----------：" + postStr);
            //获取被关注回复的消息
            #region 注释
            //List<Information_EX> list = sbo.GetModelList(string.Format("   replytype={0} and ToUserName='{1}'", 2, requestXML.TOUSERNAME));
            //if (list.Count == 1 || (list.Count > 1 && list[0].MsgType != "news"))
            //{
            //    Information_EX i = list[0];
            //    if (i != null)
            //    {
            //        switch (i.MsgType)
            //        {
            //            case "text":
            //                resxml += "<MsgType><![CDATA[text]]></MsgType><Content><![CDATA[" + i.Content + "]]></Content><FuncFlag>0</FuncFlag></xml>";
            //                break;
            //            case "news":
            //                resxml += "<MsgType><![CDATA[news]]></MsgType><ArticleCount>1</ArticleCount><Articles>";
            //                resxml += "<item><Title><![CDATA[" + i.Title + "]]></Title><Description><![CDATA[" + i.Description + "]]></Description><PicUrl><![CDATA[" + ConfigurationSettings.AppSettings["WebUrl"] + i.PicUrl + "]]></PicUrl><Url><![CDATA[" + (i.IsURL ? (i.FulltextUrl.Contains("?id") ? (i.FulltextUrl + "&FromUserName=" + requestXML.FROMUSERNAME + "&ToUserName=" + requestXML.TOUSERNAME + "") : (i.FulltextUrl + "?FromUserName=" + requestXML.FROMUSERNAME + "&ToUserName=" + requestXML.TOUSERNAME + "")) : (ConfigurationSettings.AppSettings["WebUrl"] + "/GraphicDisplay.aspx?id=" + i.DID)) + "]]></Url></item>";//URL是点击之后跳转去那里，这里跳转到百度
            //                resxml += "</Articles><FuncFlag>0</FuncFlag></xml>";
            //                break;
            //            case "music":
            //                resxml += string.Format(@"<MsgType><![CDATA[music]]></MsgType>

            //                     <Music>
            //                     <Title><![CDATA[{0}]]></Title>
            //                     <Description><![CDATA[{1}]]></Description>
            //                     <MusicUrl><![CDATA[{2}]]></MusicUrl>
            //                     <HQMusicUrl><![CDATA[{2}]]></HQMusicUrl>
            //                     </Music>
            //                     <FuncFlag>0</FuncFlag>
            //                     </xml>", i.Title, i.Description, ConfigurationSettings.AppSettings["WebUrl"] + i.PicUrl);
            //                break;
            //            default:
            //                //resxml += "<MsgType><![CDATA[text]]></MsgType><Content><![CDATA[感谢您的关注]]></Content><FuncFlag>0</FuncFlag></xml>";
            //                break;
            //        }
            //    }
            //    else
            //    {
            //        resxml += "<MsgType><![CDATA[text]]></MsgType><Content><![CDATA[感谢您的关注]]></Content><FuncFlag>0</FuncFlag></xml>";
            //    }
            //}
            //else if (list.Count > 1) //多图
            //{
            //    int count = list.Count;
            //    resxml += "<MsgType><![CDATA[news]]></MsgType><ArticleCount>" + count + "</ArticleCount><Articles>";
            //    for (int i = 0; i < count; i++)
            //    {
            //        resxml += "<item><Title><![CDATA[" + list[i].Title + "]]></Title><Description><![CDATA[" + list[i].Description + "]]></Description><PicUrl><![CDATA[" + ConfigurationSettings.AppSettings["WebUrl"] + list[i].PicUrl + "]]></PicUrl><Url><![CDATA[" + (list[i].IsURL ? (list[i].FulltextUrl.Contains("?id") ? (list[i].FulltextUrl + "&FromUserName=" + requestXML.FROMUSERNAME + "&ToUserName=" + requestXML.TOUSERNAME + "") : (list[i].FulltextUrl + "?FromUserName=" + requestXML.FROMUSERNAME + "&ToUserName=" + requestXML.TOUSERNAME + "")) : (ConfigurationSettings.AppSettings["WebUrl"] + "/GraphicDisplay.aspx?id=" + list[i].DID)) + "]]></Url></item>";//URL是点击之后跳转去那里，这里跳转到百度
            //    }
            //    resxml += "</Articles><FuncFlag>0</FuncFlag></xml>";

            //}

            #endregion
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
            //SaveOA(requestXML.FROMUSERNAME, requestXML.TOUSERNAME);
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


        //public delegate int WriteTxtHandler(WXLOG log);
        /// <summary>
        /// 记录bug，以便调试
        /// </summary>
        /// 
        /// <returns></returns>
        //public bool WriteTxt(string str)
        //{
        //    try
        //    {
        //        WXLOG l = new WXLOG();
        //        l.CON = str;
        //        l.TIME = DateTime.Now;
        //        //sbo.AddLog(l);

        //        WriteTxtHandler handler = new WriteTxtHandler(sbo.AddLog);
        //        int result = handler.Invoke(l);
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //    return true;

        //}

        class Info
        {
            public int subscribe { get; set; }
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
