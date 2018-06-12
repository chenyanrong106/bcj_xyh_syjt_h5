using Newtonsoft.Json;
using SPACRM.Business.ServiceImpl;
using SPACRM.Entity;
using SPACRM.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using WeChatCRM.Common.Utils;

namespace SPACRM.WebApp.wechat.Order
{
    public partial class Notify : WeiPage
    {

        NewVerService nvbo = new NewVerService();
        MySmallShopService mss = new MySmallShopService();
        const string Token = "puman";		//与微信平台那边填写的token一致
        protected void Page_Load(object sender, EventArgs e)
        {
            string postStr = "";
            //WXLOG log2 = new WXLOG { CON = postStr + "weixin", TIME = DateTime.Now };
            //mss.SaveLog(log2);
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

                    string result_code = rootElement.SelectSingleNode("result_code").InnerXml.Replace("<![CDATA[", "").Replace("]]>", "");
                    if (result_code == "SUCCESS")
                    {
                        string orderno = rootElement.SelectSingleNode("out_trade_no").InnerXml.Replace("<![CDATA[", "").Replace("]]>", "");
                        string bank_type = rootElement.SelectSingleNode("bank_type").InnerXml.Replace("<![CDATA[", "").Replace("]]>", "");
                        string transaction_id = rootElement.SelectSingleNode("transaction_id").InnerXml.Replace("<![CDATA[", "").Replace("]]>", "");
                        Pet_XXL_Order order = nvbo.GetPetXXLOrder(orderno);
                        if (order != null && order.OrderState != 2)
                        {
                            //Pet_XXL_Address add = nvbo.GetAddress(order.AID.Value);
                            //if (add != null)
                            //{
                            //    new SendDX().Send2("我们已收到您的付款,感谢您的支持,半湿粮将会在24小时后统一配送。", add.Phone);
                            //}
                            order.OrderState = 2;
                            order.BankType = bank_type;
                            order.OutOrderNo = transaction_id;
                            nvbo.SavePetXXLOrder(order);
                            //if (order.yhqid != null && order.yhqid != 0)
                            //{

                            //}
                            try
                            {
                                OAauth_Log o = mss.GetOA(order.FromUserName);
                                if (o != null)
                                {
                                    Pet_JiuZhu_Info j = nvbo.GetJiuZhuInfo(order.Source.Value);
                                    if (j != null && (j.JZType == 1 || j.JZType == 2))
                                    {
                                        Pet_JiuZhu_Love l = new Pet_JiuZhu_Love
                                        {
                                            CreateTime = DateTime.Now,
                                            Detail = "捐助基地[" + j.NickName + "][" + (j.JZType == 1 ? "捐粮" : "捐款") + "]",
                                            FromUserName = order.FromUserName,
                                            OID = order.ID,
                                            Love = j.JZType == 1 ? Convert.ToInt32(order.TotalPrice) : Convert.ToInt32(order.TotalPrice / 2)
                                        };
                                        int lid = nvbo.SavePetJiuZhuLove(l);
                                        if (lid > 0)
                                        {
                                            o.TotalLove = (o.TotalLove ?? 0) + l.Love;
                                            o.SurLove = (o.SurLove ?? 0) + l.Love;
                                            mss.SaveOA(o);
                                        }
                                    }
                                }
                            }
                            catch (Exception)
                            {

                            }
                            try
                            {

                                if (order.UrlPara > 0)//非小程序
                                {
                                    string token = Token(mjuserid);
                                    var temp = new
                                    {
                                        first = new { value = "我们已收到您的微信付款", color = "#173177" },
                                        orderMoneySum = new { value = order.TotalPrice.ToString() + "元", color = "#173177" },
                                        orderProductName = new { value = order.GoodsName, color = "#173177" },
                                        Remark = new { value = "\n" + "我们已收到您的爱心。", color = "#CD0000" }
                                    };
                                    if (order.PJ != null && order.PJ != 0)
                                    {
                                        temp = new
                                        {
                                            first = new { value = "我们已收到您的微信付款", color = "#173177" },
                                            orderMoneySum = new { value = order.TotalPrice.ToString() + "元", color = "#173177" },
                                            orderProductName = new { value = order.GoodsName, color = "#173177" },
                                            Remark = new { value = "\n" + "我们已收到您的爱心,您已成功捐粮" + Math.Round(order.TotalPrice.Value / 16, 2) + "kg,并且您的爱心被放大，由宠物管家赞助为其配捐" + Math.Round(order.PJ.Value / 16, 2) + "kg", color = "#CD0000" }
                                        };
                                    }
                                    string message = SendTemplateMessage(token, order.FromUserName, "qLJpWtjKj8zm3j8cwOe0u3u7qHwQ04cSx2UUvvVu-Ug", "#FF0000", temp, WebUrl + "/wechat/test/tz.aspx?tousername=" + mjuserid);
                                }
                                else
                                {
                                    string token = GetSRToken();
                                    var temp = new
                                    {
                                        keyword1 = new { value = order.CreateTime.Value.ToString("yyyy年MM月dd日 HH:mm"), color = "#173177" },
                                        keyword2 = new { value = order.TotalPrice.ToString() + "元", color = "#173177" },
                                        keyword3 = new { value = order.GoodsName, color = "#173177" },
                                        keyword4 = new { value = "为毛孩子筹粮/筹款", color = "#173177" },
                                        keyword5 = new { value = order.OrderNo, color = "#173177" }
                                    };
                                    string message = SendTemplateMessageSR(token, order.FromUserName, "IuoZZEIkILzvazy94NRpYrQ9usZ0wc3RyeSSp85gfmA", "#FF0000", temp, "pages/Pet/Detail/Detail?id="+order.Source,order.buyer_logon_id);
                                    mss.SaveLog(new WXLOG { CON = message, TIME = DateTime.Now });
                                }

                            }
                            catch (Exception)
                            {

                            }
                            Response.Write("success");
                            Response.End();
                        }
                    }
                    //WXLOG log = new WXLOG { CON = postStr + "weixin", TIME = DateTime.Now };
                    //mss.SaveLog(log);
                }

            }
        }

        /// <summary>
        /// 模板消息
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="wxOpenID"></param>
        /// <param name="tempID"></param>
        /// <param name="topColor"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public string SendTemplateMessageSR(string accessToken, string wxOpenID, string tempID, string topColor, object data, string url,string form_id)
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
            mss.SaveLog(new WXLOG { CON = strdata, TIME = DateTime.Now });
            if (!path.Contains("?"))
            {
                path += "?";
            }
            string url = WeiXinUrl + path;
            return NetHelper.HttpRequest(url, strdata, method, 60000, Encoding.UTF8, ContentTypes.JSON);
        }
    }
}