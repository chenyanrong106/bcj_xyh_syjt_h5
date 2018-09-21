using Hmj.Business;
using HmjNew.Service;
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
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Security;

namespace SPACRM.WebApp.wechat.XYH_Coupon_H5
{
    /// <summary>
    /// XYHCouponHandler 的摘要说明
    /// </summary>
    public class XYHCouponHandler : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {

        MySmallShopService mss = new MySmallShopService();
        OrderService osbo = new OrderService();
        activity_response response = new activity_response();
        XHYCouponService _xyhService = new XHYCouponService();
        string ActivityName = "SYJT";

        public void ProcessRequest(HttpContext context)
        {

            context.Response.ContentType = "text/plain";
            var requestPath = context.Request.PathInfo.Replace("/", "");
            var RequestParams = GetRequestStringList(context.Request);
            var result = "";
            string openid = System.Web.HttpContext.Current.Session["FromUserName"] == null ? "" : System.Web.HttpContext.Current.Session["FromUserName"].ToString();
            HttpCookie cookie = context.Request.Cookies["XYH_COUPON"];

            if (openid == "" && cookie != null && !string.IsNullOrWhiteSpace(cookie["FromUserName"]))
            {
                openid = cookie["FromUserName"].ToString();
            }
           // openid = "oDRuD1DSi1yyDx9x4_Ttpf_0haB0";
            if (string.IsNullOrWhiteSpace(openid)
                && requestPath != "HXCoupon"
                && requestPath != "GetCardDetail")//核销不需要openid
            {
                response.Status = -5;
                response.Message = "重新授权";
                result = JsonHelper.ToJSON(response);
                context.Response.Write(result);
                return;
            }


            DateTime StartDate = string.IsNullOrWhiteSpace(AppConfig.ActivityStartDate) ? DateTime.MinValue : DateTime.Parse(AppConfig.ActivityStartDate);
            DateTime EndDate = string.IsNullOrWhiteSpace(AppConfig.ActivityEndDate) ? DateTime.MaxValue : DateTime.Parse(AppConfig.ActivityEndDate);
            //活动结束只能领券，核销，分享

            if (EndDate < DateTime.Now)
            {
                response.Status = -6;
                response.Message = "活动已结束";
                result = JsonHelper.ToJSON(response);
                context.Response.Write(result);
                return;
            }


            switch (requestPath)
            {
                case "AddPVData":
                    //WriteTxt("新增pv数据-开始" + openid);
                    try
                    {
                        PVData pv = new PVData();
                        pv.DOMAIN = context.Request.Params[1];
                        pv.URL = "商业集团领券活动";
                        pv.TITLE = context.Request.Params[3];
                        pv.REFERRER = context.Request.Params[4];
                        pv.SH = context.Request.Params[5];
                        pv.SW = context.Request.Params[6];
                        pv.CD = context.Request.Params[7];
                        pv.ACCOUNT = context.Request.Params[9];
                        pv.LTIME = context.Request.Params[10];
                        pv.openid = openid;
                        mss.InsertPVData(pv);
                        //WriteTxt("新增pv数据-结束" + openid);
                    }
                    catch (Exception ex) { WriteTxt("新增pv数据-异常" + ex.Message); }
                    result = "";
                    break;
                case "GetCardDetail": result = GetCardDetail(RequestParams["code"], RequestParams["card_id"]); break;
                case "HXCoupon": result = HXCoupon(RequestParams["code"], RequestParams["card_id"]); break;
                case "SendMsg": result = SendMsg(RequestParams["mobile"]); break;
                case "IsMember": result = IsMember(RequestParams["mobile"], openid); break;
                case "WXShare": result = WXShare(openid, int.Parse(RequestParams["type"])); break;
                case "VerificaMobile": result = VerificaMobile(RequestParams["mobile"]); break;
                case "VerificaOpenId": result = VerificaOpenId(openid); break;
                case "LQCoupon": result = LQCoupon(openid); break;

            }
            context.Response.Write(result);
        }
        #region 获取卡券信息
        public string GetCardDetail(string code, string card_id)
        {
            try
            {
                string resmsg = NetHelper.HttpRequest(AppConfig.TokenUrl, "", "GET", 2000,
                         Encoding.UTF8, "application/json");

                TokeRes toke = JsonHelper.DeserializeObject<TokeRes>(resmsg);
                string token = toke.Access_Token;
                cardInfo cardInfo = new cardInfo();
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
                response.Status = 1;
                response.Data = cardInfoMsg;
            }
            catch (Exception ex)
            {
                response.Status = 0;
                response.Message = "核销异常" + ex.Message;
                response.Data = null;
            }
            return JsonHelper.ToJSON(response);
        }
        #endregion

        #region 核销卡券
        public string HXCoupon(string code, string card_id)
        {
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
                if (resMessage.Split('"')[5] == "ok")
                {
                    response.Status = 1;
                    response.Message = "核销成功";
                    return JsonHelper.ToJSON(response);
                }

                response.Status = 0;
                response.Message = "核销失败";
                return JsonHelper.ToJSON(response);
            }
            catch (Exception ex)
            {
                WriteTxt("核销卡券异常：card_id=" + card_id + " 异常信息：" + ex.Message);
                response.Status = 0;
                response.Message = "核销异常" + ex.Message;
                //WriteTxt();
                return JsonHelper.ToJSON(response);
            }
        }
        #endregion

        #region 发送验证码
        public string SendMsg(string mobile)
        {
            WriteTxt("发送验证码：mobile=" + mobile);
            try
            {
                //得到请求参数
                if (string.IsNullOrEmpty(mobile))
                {
                    response.Status = 0;
                    response.Message = "手机号不能是空";
                    return JsonHelper.ToJSON(response);
                }

                Random r = new Random();
                int num = r.Next(100000, 999999);
                //string message = string.Format("本次微信平台获取的验证码是：" + num);
                //【上海家化】您的验证码为：620384。
                //此验证码10分钟内有效，如非本人操作，请联系Jahwa华美家微信后台。
                string message = string.Format("您的验证码为："
                    + num + "此验证码10分钟内有效，如非本人操作，请联系华美家微信后台。");

                dt_SMSInsert_req req = new dt_SMSInsert_req
                {
                    //SMS_ITEM = new SMS_ITEM[] { new SMS_ITEM() {
                    //    CONTENT = message,
                    //    MESSAGEID = "0000001",
                    //    MESSAGETYPE = "BC_WX_SMS",
                    //    MOBILE = mobile,
                    //    MSGFORMAT = "8",
                    //    SRCNUMBER = "1069048560003"
                    //    }
                    //}
                    SMS_ITEM = new SMS_ITEM[] { new SMS_ITEM() {
                        CONTENT = message,
                        MESSAGEID = "0000001",
                        MESSAGETYPE = "BC_WX_SMS",
                        MOBILE = mobile,
                        MSGFORMAT = "8",
                        SRCNUMBER = "106900291033" // 1069048560003 -上海家化
                        }
                    }
                };

                dt_SMSInsert_res res = WebApiHelp.SMSInsert(req, true);

                if (res != null && res.zstatus == "1")
                {
                    response.Status = 1;
                    response.Message = "发送成功";
                    response.Data = num;
                    return JsonHelper.ToJSON(response);
                }

                response.Status = 0;
                response.Message = "发送失败";
                response.Data = num;
                return JsonHelper.ToJSON(response);
            }
            catch (Exception ex)
            {
                response.Status = 0;
                response.Message = "发送异常";
                response.Data = ex.Message;
                return JsonHelper.ToJSON(response);
            }
        }
        #endregion

        #region 验证手机是否参加过活动
        public string VerificaMobile(string Mobile)
        {
            try
            {
                WXCouponGiveInfo model = _xyhService.GetWXCouponGiveInfoByMobile(Mobile, ActivityName);
                if (model == null)
                {
                    response.Status = 1;
                }
                else
                {
                    if (model.GetCoupon == 1)
                    {
                        response.Status = 2;
                        response.Message = "该手机已经参加过活动";
                        response.Data = model;
                    }
                    else
                    {
                        response.Status = 0;
                        response.Message = "该手机已验证过，不满足领券条件";
                        response.Data = model;
                    }
                }

                WriteTxt("验证手机是否参加过活动，结果" + response.Status);
            }
            catch (Exception ex)
            {
                response.Status = -1;
                response.Message = "操作异常：" + ex.Message;
                WriteTxt("验证手机是否参加过活动，异常" + ex.Message);
            }

            return JsonHelper.ToJSON(response);
        }
        #endregion

        #region 验证微信号是否参加过活动，并返回是否有资格
        public string VerificaOpenId(string OpenId)
        {
            try
            {
                WXCouponGiveInfo model = _xyhService.GetWXCouponGiveInfoByOpenid(OpenId, ActivityName);
                if (model == null)
                {
                    response.Status = 1;//没有参加过活动

                }
                else
                {
                    response.Status = 0;
                    response.Data = model;
                    response.Message = "参加过活动";
                }

                WriteTxt("验证微信号是否参加过活动，结果" + response.Status);
            }
            catch (Exception ex)
            {
                response.Status = 0;
                response.Message = "操作异常：" + ex.Message;
                WriteTxt("验证微信号是否参加过活动，异常" + ex.Message);
            }

            return JsonHelper.ToJSON(response);
        }
        #endregion

        #region LQCoupon
        public string LQCoupon(string OpendID)
        {
            var cardId = AppConfig.CardId;
            string signature = "";
            string timestamp = "";
            string api_ticket = "";
            string sjm = Guid.NewGuid().ToString("d"); //随机码
            string nonce_str = "noncestr=" + sjm;  //签名必须要用到随机数

            //string code = new Random().Next(111111111, 999999999).ToString();
            //string openid = OpendID;// "oDRuD1DSi1yyDx9x4_Ttpf_0haB0";

            timestamp = Utility.ConvertDateTimeInt(DateTime.Now).ToString();
            //string token = Token(AppConfig.FWHOriginalID);
            string resmsg = NetHelper.HttpRequest(AppConfig.TokenUrl, "", "GET", 2000,
           Encoding.UTF8, "application/json");

            TokeRes toke = JsonHelper.DeserializeObject<TokeRes>(resmsg);
            string token = toke.Access_Token;


            string resticket = NetHelper.HttpRequest(AppConfig.CardApiTokenUrl, "", "GET", 2000,
Encoding.UTF8, "application/json");

            JsApiTicket JsApiTicket = JsonHelper.DeserializeObject<JsApiTicket>(resticket);

            api_ticket = JsApiTicket.JsApi;// WXCardCommon.GetCardApi(token);
                                           //api_ticket = WXCardCommon.GetCardApi(token);
            //string[] ArrTmp = { cardId, timestamp, api_ticket, code, openid };
            string[] ArrTmp = { cardId, timestamp, api_ticket, nonce_str };
            Array.Sort(ArrTmp);
            string tmpStr = string.Join("", ArrTmp);
            tmpStr = FormsAuthentication.HashPasswordForStoringInConfigFile(tmpStr, "SHA1");
            signature = tmpStr;

            CardExt cardExt = new CardExt();
            cardExt.timestamp = timestamp;
            cardExt.signature = signature;
            cardExt.nonce_str = nonce_str;
            //cardExt.code = code;
            //cardExt.openid = openid;
            var retcardExt = JsonConvert.SerializeObject(cardExt);


            CardInfoExt CardInfo = new CardInfoExt();
            CardInfo.cardId = cardId;
            CardInfo.cardExt = retcardExt;

            response.Status = 1;
            response.Data = CardInfo;
            


            return JsonHelper.ToJSON(response);
        }
        #endregion

        #region 判断是否是华美家或佰草集会员
        public string IsMember(string mobile, string openid)
        {
            //会员快速查询
            //先查找华美家会员
            try
            {
                //判断是否已经参加过
                WXCouponGiveInfo wxcardmodel = _xyhService.GetWXCouponGiveInfoByMobile(mobile, AppConfig.CardId, ActivityName);
                if (wxcardmodel != null)
                {
                    if (wxcardmodel.GetCoupon == 1)
                    {
                        response.Status = 2;
                        response.Message = "已经参加过活动";
                        return JsonHelper.ToJSON(response);
                    }
                    else
                    {
                        response.Status = 0;
                        response.Message = "不满足条件";
                        return JsonHelper.ToJSON(response);
                    }
                }

                dt_Dyn_DispMemQuick_req w = new dt_Dyn_DispMemQuick_req
                {
                    DATA_SOURCE = "0000",
                    LOYALTY_BRAND = "HM",//忠诚度品牌
                    SOURCE_SYSTEM = "0016",//来源系统
                    VGROUP = "C001", //销售组织
                    ACCOUNT_ID = mobile//2002652891
                };
                dt_Dyn_DispMemQuick_res dt = WebApiHelp.DispMemQuick(w);

                if (dt.I_ZCRMT316 == null || dt.I_ZCRMT316.Length <= 0)
                {
                    //查找佰草集会员
                    dt_Dyn_DispMemQuick_req wbcj = new dt_Dyn_DispMemQuick_req
                    {
                        DATA_SOURCE = "0002",
                        LOYALTY_BRAND = "28",//忠诚度品牌
                        SOURCE_SYSTEM = "0003",//来源系统
                        VGROUP = "C004", //销售组织
                        ACCOUNT_ID = mobile
                    };
                    dt_Dyn_DispMemQuick_res dtbcj = WebApiHelp.DispMemQuick(w);
                    if (dtbcj.I_ZCRMT316 == null || dtbcj.I_ZCRMT316.Length <= 0)
                    {

                        long ret = _xyhService.AddWXCouponGiveInfo(new WXCouponGiveInfo()
                        {
                            Openid = openid,
                            Mobile = mobile,
                            GetCoupon = 1,
                            Status = 0,
                            ActivityName = ActivityName,
                            CardId = AppConfig.CardId,
                            CreateDate = DateTime.Now
                        });

                        CreateCrmCust(mobile,openid);

                        response.Status = 1;
                        response.Message = "不存在会员";
                        response.Data = ret;
                        return JsonHelper.ToJSON(response);

                    }
                    else
                    {
                        long ret = _xyhService.AddWXCouponGiveInfo(new WXCouponGiveInfo()
                        {
                            Openid = openid,
                            Mobile = mobile,
                            GetCoupon = 0,
                            Status = 0,
                            ActivityName = ActivityName,
                            CardId = "",
                            CreateDate = DateTime.Now
                        });
                        response.Status = 0;
                        response.Message = "存在会员";
                        return JsonHelper.ToJSON(response);
                    }
                }
                else
                {
                    //存在会员
                    long ret = _xyhService.AddWXCouponGiveInfo(new WXCouponGiveInfo()
                    {
                        Openid = openid,
                        Mobile = mobile,
                        GetCoupon = 0,
                        Status = 0,
                        ActivityName = ActivityName,
                        CardId = "",
                        CreateDate = DateTime.Now
                    });
                    response.Status = 0;
                    response.Message = "存在会员";
                    return JsonHelper.ToJSON(response);
                }
            }
            catch (Exception ex)
            {
                WriteTxt("验证手机是否是新客:手机号=" + mobile + ",异常：" + ex.Message);
                response.Status = 0;// -1;
                response.Message = "系统错误";
                return JsonHelper.ToJSON(response);
            }
        }
        #endregion

        #region 创建潜客
        public void CreateCrmCust(string mobile, string openid)
        {
            ThreadPool.QueueUserWorkItem(a =>
            {
                bool isSuccess = false;
                string msg = "失败";
                try
                {
                    //通知crm创建
                    dt_Dyn_CreateLead_req crreq = new dt_Dyn_CreateLead_req();
                    ZCRMT342_Dyn meber = new ZCRMT342_Dyn();
                    meber.WECHATFOLLOWSTATUS = "1";
                    meber.MOB_NUMBER = mobile;
                    meber.WECHAT = openid;
                    //meber.EMPID = "HMWX001";
                    //meber.DEPTID = AppConfig.DEPTID;//入会门店
                    meber.NAME1_TEXT = AppConfig.CUST_NAME;// "佰草集潜客";//全名
                    meber.DATA_SOURCE = AppConfig.DATA_SOURCE;// "0002";//数据来源
                    meber.LOYALTY_BRAND = AppConfig.LOYALTY_BRAND;// "28";//忠诚度品牌
                    meber.SOURCE_SYSTEM = AppConfig.SOURCE_SYSTEM;// "0003";//来源系统
                    meber.VGROUP = AppConfig.VGROUP;// "C004"; //销售组织
                    meber.CAMPAIGN_ID1 = AppConfig.CAMPAIGN_ID;
                    crreq.INFO_QK = new ZCRMT342_Dyn[] { meber };
                    dt_Dyn_CreateLead_res qianke = WebApiHelp.CreateLead(crreq);
                    if (qianke.WV_RETURN == "Y")
                    {
                        isSuccess = true;
                        msg = "成功";
                    }
                    else
                        msg = "失败：" + qianke.WV_MESSAGE;
                }
                catch (Exception ex)
                {
                    msg = "异常：" + ex.Message;
                }
                finally
                {
                    WXCRMCustLog WXCRMCustLog = new WXCRMCustLog();
                    WXCRMCustLog.OpenId = openid;
                    WXCRMCustLog.Mobile = mobile;
                    WXCRMCustLog.NAME1_TEXT = AppConfig.CUST_NAME;
                    WXCRMCustLog.DATA_SOURCE = AppConfig.DATA_SOURCE;
                    WXCRMCustLog.LOYALTY_BRAND = AppConfig.LOYALTY_BRAND;
                    WXCRMCustLog.SOURCE_SYSTEM = AppConfig.SOURCE_SYSTEM;
                    WXCRMCustLog.VGROUP = AppConfig.VGROUP;
                    WXCRMCustLog.Result = isSuccess ? 1 : 0;
                    WXCRMCustLog.ActivityName = ActivityName;
                    WXCRMCustLog.CreateDate = DateTime.Now;
                    if (msg.Length > 2000)
                        WXCRMCustLog.Remark = msg.Substring(0, 2000);
                    else
                        WXCRMCustLog.Remark = msg;
                    _xyhService.InsertWXCRMCustLog(WXCRMCustLog);
                }
            });

        }
        #endregion

        #region 分享
        public string WXShare(string openid, int type)
        {
            try
            {
                ISystemService sbo = new SystemService();
                WriteTxt("新增分享记录-开始");
                Cust_Old_New old = new Cust_Old_New
                {
                    FromUserName = openid,
                    ToUserName = "xyh_coupon_syjt_h5",
                    ToUserNickName = "分享活动",
                    Jie = type,
                    CreateTime = DateTime.Now
                };

                long ret = sbo.AddWXShare(old);
                if (ret > 0)
                    response.Status = 1;
                else
                    response.Status = 0;
                WriteTxt("新增分享记录-结束，结果" + response.Status);
            }
            catch (Exception ex)
            {
                response.Status = 0;
                response.Message = "操作异常：" + ex.Message;
                WriteTxt("新增分享记录-异常，异常" + ex.Message);
            }

            return JsonHelper.ToJSON(response);
        }
        #endregion

        #region 记录日志
        /// <summary>
        /// 日志
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        public delegate int WriteLogHandler(WXLOG log);
        public bool WriteTxt(string str)
        {
            try
            {
                ISystemService sbo = new SystemService();
                WXLOG log = new WXLOG();
                log.CON = str;
                log.TIME = DateTime.Now;
                //sbo.AddLog(l);
                WriteLogHandler handler = new WriteLogHandler(sbo.AddLog);
                int result = handler.Invoke(log);
            }
            catch (Exception)
            {
                return false;
            }
            return true;

        }
        #endregion

        #region 获取所有请求参数
        /// <summary>
        /// 获取所有请求参数
        /// </summary>
        /// <param name="res"></param>
        /// <returns></returns>
        public IDictionary<string, string> GetRequestStringList(HttpRequest res)
        {
            IDictionary<string, string> _params = new Dictionary<string, string>();
            if (res.Form.AllKeys.Length > 0)
            {
                foreach (string key in res.Form.Keys)
                {
                    if (!_params.ContainsKey(key))
                    {
                        _params.Add(key, res.Form[key]);
                    }
                }
            }
            if (res.QueryString.AllKeys.Length > 0)
            {
                foreach (string key in res.QueryString.Keys)
                {
                    if (!_params.ContainsKey(key))
                    {
                        _params.Add(key, res.QueryString[key]);
                    }
                }
            }
            return _params;
        }
        #endregion

        #region 其它基础配置
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public class activity_response
        {
            public int Status { get; set; }
            public string Message { get; set; } = "";
            public object Data { get; set; }
        }

        //活动截止日期
        public string ActiveEndDate
        {
            get { return ConfigurationManager.AppSettings["ActiveEndDate"]; }
        }
        #endregion
    }
}