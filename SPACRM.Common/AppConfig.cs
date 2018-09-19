using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SPACRM.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class AppConfig
    {
        /// <summary>
        /// 是否调试模式
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is debug mode; otherwise, <c>false</c>.
        /// </value>
        public static bool IsDebugMode
        {
            get
            {
                string isdebug = GetAppSetting("IsDebugMode");
                if (string.IsNullOrEmpty(isdebug))
                {
                    return false;
                }
                else
                {
                    return string.Compare(isdebug, "true", true) == 0 || isdebug == "1";
                }
            }
        }
        public static bool EnableApiAuth
        {
            get
            {
                string str = GetAppSetting("EnableApiAuth");
                if (string.IsNullOrEmpty(str))
                {
                    return false;
                }
                else
                {
                    return string.Compare(str, "true", true) == 0 || str == "1";
                }
            }
        }

        /// <summary>
        /// 预约是否必须员工排班
        /// </summary>
        public static bool BookNeedSchedule
        {
            get
            {
                string isdebug = GetAppSetting("BookNeedSchedule");
                if (string.IsNullOrEmpty(isdebug))
                {
                    return false;
                }
                else
                {
                    return string.Compare(isdebug, "true", true) == 0 || isdebug == "1";
                }
            }
        }
        /// <summary>
        /// 微信用户注册是否送券
        /// </summary>
        public static bool EnableCustSignupOfferCoupon
        {
            get
            {
                string isdebug = GetAppSetting("EnableCustSignupOfferCoupon");
                if (string.IsNullOrEmpty(isdebug))
                {
                    return false;
                }
                else
                {
                    return string.Compare(isdebug, "true", true) == 0 || isdebug == "1";
                }
            }
        }

        public static bool ShowTop
        {
            get
            {
                string isShowTop = GetAppSetting("ShowTop");
                if (string.IsNullOrEmpty(isShowTop))
                {
                    return false;
                }
                else
                {
                    return string.Compare(isShowTop, "true", true) == 0 || isShowTop == "1";
                }
            }
        }
        public static string MainDbKey
        {
            get
            {
                return "maindb";
            }
        }

        /// <summary>
        /// 开始营业时间
        /// </summary>
        /// <value>
        /// The start hour.
        /// </value>
        public static int StartHour
        {
            get
            {
                return 10;
            }
        }

        /// <summary>
        /// 结束营业时间
        /// </summary>
        /// <value>
        /// The end hour.
        /// </value>
        public static int EndHour
        {
            get
            {
                return 24;
            }
        }

        public static string Get(string key)
        {
            return GetAppSetting(key);

        }


        /// <summary>
        /// 会员开单地址
        /// </summary>
        /// <value>
        /// The card check in URL.
        /// </value>
        public static string CardCheckInUrl
        {
            get
            {
                return Get("CardCheckInUrl");
            }
        }
        /// <summary>
        /// 临时会员开单地址
        /// </summary>
        /// <value>
        /// The cash check in URL.
        /// </value>
        public static string CashCheckInUrl
        {
            get
            {
                return Get("CashCheckInUrl");
            }
        }
        /// <summary>
        /// 会员详细地址
        /// </summary>
        /// <value>
        /// The user edit URL.
        /// </value>
        public static string UserEditUrl
        {
            get
            {
                return Get("UserEditUrl");
            }
        }
        /// <summary>
        /// 会员卡储值地址
        /// </summary>
        /// <value>
        /// The card recharge URL.
        /// </value>
        public static string CardRechargeUrl
        {
            get
            {
                return Get("CardRechargeUrl");
            }
        }

        public static string CardAddUrl
        {
            get
            {
                return Get("CardAddUrl");
            }
        }
        /// <summary>
        /// 疗程卡开卡地址
        /// </summary>
        /// <value>
        /// The create card URL.
        /// </value>
        public static string CreateCardUrl
        {
            get
            {
                return Get("CreateCardUrl");
            }
        }

        public static string CheckInUrl
        {
            get
            {
                return Get("CheckInUrl");
            }
        }
        public static string ViewBillUrl
        {
            get
            {
                return Get("ViewBillUrl");
            }
        }
        public static string SMSContent
        {
            get
            {
                return Get("SMSContent");
            }
        }


        #region private
        private static string GetAppSetting(string key)
        {
            return System.Configuration.ConfigurationManager.AppSettings.Get(key);
        }
        private static string GetConnectionStrings(string key)
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings[key].ConnectionString;
        }
        #endregion

        //public static string CRMServiceUrl { 
        //    get {
        //        return Get("CRMServiceUrl");
        //    } 
        //}
        public static string POSServiceUrl
        {
            get
            {
                return Get("POSServiceUrl");
            }
        }
        public static string CRMSMSRootUrl
        {
            get
            {
                string url = Get("CRMSMSRootUrl");
                if (string.IsNullOrEmpty(url))
                {
                    url = Get("CRMRootUrl");
                }
                return url;
            }
        }
        public static string CRMRootUrl
        {
            get
            {
                return Get("CRMRootUrl");
            }
        }
        public static string CRMPassword
        {
            get
            {
                return Get("CRMPassword");
            }
        }

        public static string CRMUserId
        {
            get
            {
                return Get("CRMUserId");
            }
        }

        public static string POSPassword
        {
            get
            {
                return Get("POSPassword");
            }
        }

        public static string POSUserId
        {
            get
            {
                return Get("POSUserId");
            }
        }
        public static string POSSource
        {
            get
            {
                return Get("POSSource");
            }
        }

        public static int MaxUploadImageSize
        {
            get
            {
                return 10 * 1024 * 1024;
            }
        }

        public static string UploadTMP
        {
            get
            {
                return Get("UploadTMP");
            }
        }

        public static string ImageUrl
        {
            get
            {
                return Get("ImageUrl");
            }
        }
        //会员消费送积分
        public static string Consume_Points
        {
            get
            {
                return Get("Consume_Points");
            }
        }

        public static string TokenUrl
        {
            get
            {
                return Get("TokenUrl");
            }
        }

        public static string OAuthUrl
        {
            get
            {
                return Get("OAuthUrl");
            }
        }

        public static string JsApiTokenUrl
        {
            get { return Get("JsApiTokenUrl"); }
        }


        public static string ActivityWebApp
        {
            get
            {
                return Get("ActivityWebApp");
            }
        }

        public static string CardId
        {
            get
            {
                return Get("CardId");
            }
        }

        public static string WXAppId
        {
            get
            {
                return Get("WXAppId");
            }
        }
        public static string TWOTGCardId_1
        {
            get
            {
                return Get("TWOTGCardId_1");
            }
        }

        public static string TWOTGCardId_2
        {
            get
            {
                return Get("TWOTGCardId_2");
            }
        }

        public static string ActivityStartDate
        {
            get
            {
                return Get("ActivityStartDate");
            }
        }

        public static string ActivityEndDate
        {
            get
            {
                return Get("ActivityEndDate");
            }
        }
        //来源系统
        public static string SOURCE_SYSTEM
        {
            get
            {
                return Get("SOURCE_SYSTEM");
            }
        }
        //销售组织
        public static string VGROUP
        {
            get
            {
                return Get("VGROUP");
            }
        }
        //忠诚度品牌
        public static string LOYALTY_BRAND
        {
            get
            {
                return Get("LOYALTY_BRAND");
            }
        }
        //数据来源
        public static string DATA_SOURCE
        {
            get
            {
                return Get("DATA_SOURCE");
            }
        }

        //潜客名称
        public static string CUST_NAME
        {
            get
            {
                return Get("CUST_NAME");
            }
        }
        
        //成为潜客时的活动编码
        public static string CAMPAIGN_ID
        {
            get
            {
                return Get("CAMPAIGN_ID");
            }
        }

        public static string CardApiTokenUrl
        {
            get { return Get("CardApiTokenUrl"); }
        }
    }
}
