using Newtonsoft.Json;
using SPACRM.Business.XYH_Coupon;
using SPACRM.Common;
using SPACRM.Common.Utils;
using SPACRM.Entity;
using SPACRM.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SPACRM.WebApp.wechat.XYH_Coupon_H5.wxcard
{
    public partial class wxcard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //创建卡券
        protected void btnCreateCoupon_Click(object sender, EventArgs e)
        {
            long begin_timestamp = 1528732800;//2018-06-12 00:00:00
            long end_timestamp = 1530372239;//2018-06-30 23:59:59
            string resmsg = NetHelper.HttpRequest(AppConfig.TokenUrl, "", "GET", 2000,
                        Encoding.UTF8, "application/json");

            TokeRes toke = JsonHelper.DeserializeObject<TokeRes>(resmsg);
            string token = toke.Access_Token;
            string url = "https://api.weixin.qq.com/card/create?access_token=" + token;
            string json = @"{ 
            ""card"": {
              ""card_type"": ""GENERAL_COUPON"",
              ""general_coupon"": {
                  ""base_info"": {
                      ""logo_url"": 
            ""https://mmbiz.qlogo.cn/mmbiz_jpg/c8icWUnxBQmEib9ZUicSFGkBiaRg4cbYxq1p3JygXK3eC0Rfy5HocvYdbOJGQaRJNfCDfFJBib7AsRQeZFqFtNADwBw/0"",
                      ""brand_name"":""佰草集心约会"",
                      ""code_type"":""CODE_TYPE_TEXT"",
                      ""title"": ""夏日璀璨大礼包"",
                      ""sub_title"": ""新恒美紧肤洁面乳爆款招新"",
                      ""color"": ""Color020"",
                      ""notice"": ""请出示您的唯一优惠券码"",
                      ""description"": ""1）优惠券仅限本人使用，不可转赠，每人限领一张
                                         2）活动仅限指定产品
                                         3）该活动仅限在佰草集专柜首次购买的顾客"",
                      ""date_info"": {
                                          ""type"": ""DATE_TYPE_FIX_TIME_RANGE"",
                                          ""begin_timestamp"": " + begin_timestamp + " ,"
                              + @"""end_timestamp"":  " + end_timestamp + @" },
                       ""sku"": {
                            ""quantity"": 0
                      },
                      ""get_limit"": 1,
                      ""use_custom_code"": true,
                      ""get_custom_code_mode"":""GET_CUSTOM_CODE_MODE_DEPOSIT"",
                      ""bind_openid"": false,
                      ""can_share"": false,
                      ""can_give_friend"": false,
                      ""center_title"": ""立即使用"",
                          ""center_sub_title"": """",
                          ""center_url"": """ + AppConfig.ActivityWebApp + @"wechat/XYH_Coupon_H5/html/hxcoupon.html""
                  },
               ""advanced_info"": {

                           ""time_limit"": [
                               {
                            ""type"": ""MONDAY"",
                                   ""begin_hour"":0,
                                   ""begin_minute"":0,
                                   ""end_hour"":23,
                                   ""end_minute"":59
                               },
                                 {
                            ""type"": ""TUESDAY"",
                               ""begin_hour"":0,
                                   ""begin_minute"":0,
                                   ""end_hour"":23,
                                   ""end_minute"":59
                               },
                                 {
                            ""type"": ""WEDNESDAY"",
                                  ""begin_hour"":0,
                                   ""begin_minute"":0,
                                   ""end_hour"":23,
                                   ""end_minute"":59
                               },
                                 {
                            ""type"": ""THURSDAY"",
                                   ""begin_hour"":0,
                                   ""begin_minute"":0,
                                   ""end_hour"":23,
                                   ""end_minute"":59
                               },
                                 {
                            ""type"": ""FRIDAY"",
                                ""begin_hour"":0,
                                   ""begin_minute"":0,
                                   ""end_hour"":23,
                                   ""end_minute"":59
                               },
                                 {
                            ""type"": ""SATURDAY"",
                                   ""begin_hour"":0,
                                   ""begin_minute"":0,
                                   ""end_hour"":23,
                                   ""end_minute"":59
                               },
                                 {
                            ""type"": ""SUNDAY"",
                                    ""begin_hour"":0,
                                   ""begin_minute"":0,
                                   ""end_hour"":23,
                                   ""end_minute"":59
                               }
                           ]

                       },
                  ""default_detail"": ""原价160元，现价80元""}
            }
            }";

            var resMessage = NetHelper.HttpRequest(url, json, "POST", 2000, Encoding.UTF8, "application/json");
            CouponResponse response = JsonConvert.DeserializeObject<CouponResponse>(resMessage);
            //pDRuD1EutQ_b25Qd8c27hJ-rl7Do
            //{"status": 1,    "message": "{\"errcode\":0,\"errmsg\":\"ok\",\"card_id\":\"pDRuD1EutQ_b25Qd8c27hJ-rl7Do\"}",    "data": null}
            if (response.errmsg == "ok")
            {
                string cardId = response.card_id;

                this.txtCreateCouponLog.Text = "卡券创建成功，cardId = " + cardId;
            }
            else
            {
                this.txtCreateCouponLog.Text = "卡券创建失败";

            }
        }

        //导入卡券
        protected void btnImportCoupon_Click(object sender, EventArgs e)
        {
            XHYCouponService _xyhservice = new XHYCouponService();
            string message = "";
            string url = "";
            string json = "";
            string cardid = this.txtCardId.Text;

            string resmsg = NetHelper.HttpRequest(AppConfig.TokenUrl, "", "GET", 2000,
                        Encoding.UTF8, "application/json");

            TokeRes toke = JsonHelper.DeserializeObject<TokeRes>(resmsg);
            string token = toke.Access_Token;
            List<WXCouponNoInfo> list = _xyhservice.QueryWXCouponNoInfo(cardid);

            int succ_cnt = 0;
            string codes = "";
            foreach (WXCouponNoInfo model in list)
            {
                url = "http://api.weixin.qq.com/card/code/deposit?access_token=" + token;
                json = @"{
                                   ""card_id"": """ + cardid + @""",
                                   ""code"": [
                                       """ + model.CouponNo + @"""
                                   ]
                                }";
                var resMessage = NetHelper.HttpRequest(url, json, "POST", 2000, Encoding.UTF8, "application/json");
                if (resMessage.Contains("succ_code")) //成功
                {
                    succ_cnt++;
                    _xyhservice.UpdateWXCouponNoInfoIsImport(model.id);
                }
                else
                {
                    message = resMessage + "<br>ID:" + model.id;
                }
                codes += "\"" + model.CouponNo + "\",";
            }
            codes = codes.Substring(0, codes.Length - 1);

            if (succ_cnt > 0)
            {
                //修改库存
                url = "https://api.weixin.qq.com/card/modifystock?access_token=" + token;
                json = @"{""card_id"": """ + cardid + @""",""increase_stock_value"":" + succ_cnt + @",""reduce_stock_value"": 0}";
                var resMessage = NetHelper.HttpRequest(url, json, "POST", 2000, Encoding.UTF8, "application/json");

            }

            //核查code接口
            url = "http://api.weixin.qq.com/card/code/checkcode?access_token=" + token;
            json = @"{""card_id"": """ + cardid + @""",""code"":[" + codes + @"]}";
            message = NetHelper.HttpRequest(url, json, "POST", 2000, Encoding.UTF8, "application/json");

            this.txtImportCouponLog.Text = message;
        }

        protected void btnQueryCoupon_Click(object sender, EventArgs e)
        {
            
        }


        public bool HXCoupon(string code, string card_id)
        {
            XHYCouponService _xyhservice = new XHYCouponService();
            try
            {
                string resmsg = NetHelper.HttpRequest(AppConfig.TokenUrl, "", "GET", 2000,
                        Encoding.UTF8, "application/json");

                TokeRes toke = JsonHelper.DeserializeObject<TokeRes>(resmsg);
                string token = toke.Access_Token;
                string url = "https://api.weixin.qq.com/card/code/consume?access_token=" + token;
                string json = @"{
  ""code"": ""{0}"",
  ""card_id"": """ + card_id + @"""}";
                json = json.Replace("{0}", code);
                var resMessage = NetHelper.HttpRequest(url, json, "POST", 2000, Encoding.UTF8, "application/json");
                //{ "errcode":0,"errmsg":"ok","card":{                "card_id":"pFS7Fjg8kV1IdDz01r4SQwMkuCKc"},"openid":"oFS7Fjl0WsZ9AMZqrI80nbIq8xrA"}
                if (resMessage.Split('"')[5] == "ok")
                {
                    //_logwarn.Warn("核销成功！code=" + code + " ,card_id=" + card_id);

                    _xyhservice.AddLodInfo(new Tb_log() { title = "核销卡券", msgType = "message", msgContent = "核销成功！code=" + code + " ,card_id=" + card_id, createDate = DateTime.Now });
                    return true;
                }
                _xyhservice.AddLodInfo(new Tb_log() { title = "核销卡券", msgType = "error", msgContent = "核销失败", createDate = DateTime.Now });
                return false;
            }
            catch (Exception ex)
            {
                _xyhservice.AddLodInfo(new Tb_log() { title = "核销异常", msgType = "exception", msgContent = "核销异常：" + ex.Message, createDate = DateTime.Now });
                return false;
            }

        }

        public wxCardInfo GetCardDetail(string code, string card_id)
        {
            try
            {
                string resmsg = NetHelper.HttpRequest(AppConfig.TokenUrl, "", "GET", 2000,
                         Encoding.UTF8, "application/json");

                TokeRes toke = JsonHelper.DeserializeObject<TokeRes>(resmsg);
                string token = toke.Access_Token;
                wxCardInfo cardInfo = new wxCardInfo();
                cardInfo.card_id = card_id;
                string url = "https://api.weixin.qq.com/card/code/decrypt?access_token=" + token;
                string json = @"{""encrypt_code"":""" + code + @"""}";
                string resMessage = NetHelper.HttpRequest(url, json, "POST", 2000, Encoding.UTF8, "application/json");

                if (resMessage.Contains("\"ok\""))
                {
                    code = resMessage.Split('"')[9];
                    cardInfo.code = code;
                    //查询卡券信息
                    url = "https://api.weixin.qq.com/card/get?access_token=" + token;
                    json = @"{
                            ""card_id"": """ + card_id + @"""
                           }";
                    resMessage = NetHelper.HttpRequest(url, json, "POST", 2000, Encoding.UTF8, "application/json");
                    if (resMessage.Contains("\"ok\""))
                    {
                        try
                        {
                            var brand_name = resMessage.Substring(resMessage.IndexOf("brand_name") + 10).Split('"')[2];
                            var title = resMessage.Substring(resMessage.IndexOf("title") + 5).Split('"')[2];
                            var color = resMessage.Substring(resMessage.IndexOf("color") + 5).Split('"')[2];
                            cardInfo.brand_name = brand_name;
                            cardInfo.title = title;
                            cardInfo.color = color;
                        }
                        catch
                        {
                            cardInfo.brand_name = "";
                            cardInfo.title = "";
                            cardInfo.color = "";
                        }
                    }
                }
                string cardInfoMsg = JsonConvert.SerializeObject(cardInfo);
                return cardInfo;
            }
            catch (Exception ex)
            {
                return new wxCardInfo();
            }
        }
    }

    public class CouponResponse
    {
        public string errcode { get; set; }
        public string errmsg { get; set; }
        public string card_id { get; set; }
    }

    public class wxCardInfo
    {
        public string card_id { get; set; }
        public string code { get; set; }
        public string brand_name { get; set; }
        public string title { get; set; }
        public string color { get; set; }
    }
}