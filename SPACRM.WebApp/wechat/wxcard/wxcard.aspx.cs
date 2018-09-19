using Newtonsoft.Json;
using SPACRM.Business.ServiceImpl;
using SPACRM.Business.XYH_Coupon;
using SPACRM.Common;
using SPACRM.Common.Utils;
using SPACRM.Entity;
using SPACRM.Entity.Entities;
using SPACRM.Interface;
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
            long begin_timestamp = 1530374400;//2018-07-01 00:00:00
            //1529337600  2018-06-19 00:00:00
            //1528992000 2018-06-15 00:00:00
            long end_timestamp = 1538323199;//2018-09-30 23:59:59
            //1538323199   2018-09-30 23:59:59
            //1529423999 2018-06-19 23:59:59
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
            ""https://mmbiz.qlogo.cn/mmbiz_jpg/ugkm3wzgIFILaicicIzX7hfxblXziaEUliaGk37Q0VukC4tiaWB3lfcxYopOn72bHibaKEenbsPHzqpvxppHk3NanItw/0?wx_fmt=jpeg"",
                      ""brand_name"":""佰草集心约会"",
                      ""code_type"":""CODE_TYPE_TEXT"",
                      ""title"": ""新客专享璀璨大礼包"",
                      ""sub_title"": """",
                      ""color"": ""Color020"",
                      ""notice"": ""请出示您的唯一优惠券码"",
                      ""description"": ""1）此券仅限本人使用，不可转增
                                         2）此券包含 
                                            1.佰草集新·瓷意系列产品中小样，以门店实际领取为准  
                                            2. 30元无门槛新客专享抵用券  
                                            3.面部护理体验券
                                         3）该活动仅限在佰草集专柜使用"",
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
                  ""default_detail"": ""佰草集新客专享璀璨大礼包""}
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
                this.txtCreateCouponLog.Text = "卡券创建失败"+ response;

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

        public delegate int WriteLogHandler(WXLOG log);
        public bool WriteTxt(string str)
        {
            ISystemService sbo = new SystemService();
            try
            {
                WXLOG l = new WXLOG();
                l.CON = str;
                l.TIME = DateTime.Now;
                // sbo.AddLog(l);
                WriteLogHandler handler = new WriteLogHandler(sbo.AddLog);
                int result = handler.Invoke(l);
            }
            catch (Exception)
            {
                return false;
            }
            return true;

        }

        protected void btnCreateCoupon1_Click(object sender, EventArgs e)
        {
            long begin_timestamp = 1529856000;//2018-06-25 00:00:00
            //1529856000  2018-06-25 00:00:00
            long end_timestamp = 1533052799;//2018-07-31 23:59:59
            //1533052799   2018-07-31 23:59:59
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
            ""https://mmbiz.qlogo.cn/mmbiz_jpg/ugkm3wzgIFILaicicIzX7hfxblXziaEUliaGk37Q0VukC4tiaWB3lfcxYopOn72bHibaKEenbsPHzqpvxppHk3NanItw/0?wx_fmt=jpeg"",
                      ""brand_name"":""佰草集心约会"",
                      ""code_type"":""CODE_TYPE_TEXT"",
                      ""title"": ""指定产品60元抵用券"",
                      ""sub_title"": """",
                      ""color"": ""Color020"",
                      ""notice"": ""请出示您的唯一优惠券码"",
                      ""description"": ""1）此券仅限本人使用，不可转增
                                         2）凭券前往佰草集专柜注册成为会员后方可立即使用
                                         3）活动仅限入店购买指定产品「润·泽兰蕴美精华水」时抵扣"",
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
                          ""center_url"": """ + AppConfig.ActivityWebApp + @"wechat/XYH_2TG_Coupon_H5/html/hxcoupon.html""
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
                  ""default_detail"": ""指定产品60元抵用券""}
            }
            }";

            var resMessage = NetHelper.HttpRequest(url, json, "POST", 2000, Encoding.UTF8, "application/json");
            CouponResponse response = JsonConvert.DeserializeObject<CouponResponse>(resMessage);

            //pDRuD1EutQ_b25Qd8c27hJ-rl7Do
            //{"status": 1,    "message": "{\"errcode\":0,\"errmsg\":\"ok\",\"card_id\":\"pDRuD1EutQ_b25Qd8c27hJ-rl7Do\"}",    "data": null}
            if (response.errmsg == "ok")
            {
                string cardId = response.card_id;

                this.txtCreateCouponLog1.Text = "卡券创建成功，cardId = " + cardId;
            }
            else
            {
                this.txtCreateCouponLog1.Text = "卡券创建失败" + response;

            }
        }

        protected void btnCreateCoupon2_Click(object sender, EventArgs e)
        {
            long begin_timestamp = 1533052800;//2018-08-01 00:00:00
            long end_timestamp = 1568889428;//2018-09-30 23:59:59
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
            ""https://mmbiz.qlogo.cn/mmbiz_jpg/ugkm3wzgIFILaicicIzX7hfxblXziaEUliaGk37Q0VukC4tiaWB3lfcxYopOn72bHibaKEenbsPHzqpvxppHk3NanItw/0?wx_fmt=jpeg"",
                      ""brand_name"":""佰草集心约会"",
                      ""code_type"":""CODE_TYPE_TEXT"",
                      ""title"": ""满200减60抵用券"",
                      ""sub_title"": """",
                      ""color"": ""Color020"",
                      ""notice"": ""请出示您的唯一优惠券码"",
                      ""description"": ""1）此券仅限本人使用，不可转增，每人限领1张
                                         2）凭券返店消费满200元时抵扣
                                         3）该活动仅限在佰草集专柜购买的顾客"",
                      ""date_info"": {
                                          ""type"": ""DATE_TYPE_FIX_TIME_RANGE"",
                                          ""begin_timestamp"": " + begin_timestamp + " ,"
                              + @"""end_timestamp"":  " + end_timestamp + @" },
                       ""sku"": {
                            ""quantity"": 100
                      },
                      ""get_limit"": 10,
                      ""use_custom_code"": true,
                      ""bind_openid"": true,
                      ""can_share"": false,
                      ""can_give_friend"": false,
                      ""center_title"": ""立即使用"",
                          ""center_sub_title"": """",
                          ""center_url"": """ + AppConfig.ActivityWebApp + @"wechat/XYH_2TG_Coupon_H5/html/hxcoupon.html""
                  },
                  ""default_detail"": ""满200减60抵用券""}
            }
            }";

            var resMessage = NetHelper.HttpRequest(url, json, "POST", 2000, Encoding.UTF8, "application/json");
            CouponResponse response = JsonConvert.DeserializeObject<CouponResponse>(resMessage);

            //pDRuD1EutQ_b25Qd8c27hJ-rl7Do
            //{"status": 1,    "message": "{\"errcode\":0,\"errmsg\":\"ok\",\"card_id\":\"pDRuD1EutQ_b25Qd8c27hJ-rl7Do\"}",    "data": null}
            if (response.errmsg == "ok")
            {
                string cardId = response.card_id;

                this.txtCreateCouponLog2.Text = "卡券创建成功，cardId = " + cardId;
            }
            else
            {
                this.txtCreateCouponLog2.Text = "卡券创建失败" + response;

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