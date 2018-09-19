using Hmj.Business;
using HmjNew.Service;
using SPACRM.Business.XYH_Coupon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPACRM.WebApp.wechat.common
{
    public class SMS
    {
        public static int SendMessage(string message,string mobile,string TgId=null)
        {
            XYHCoupon2TGService xYHCoupon = new XYHCoupon2TGService();
            dt_SMSInsert_req req = new dt_SMSInsert_req
            {

            //      dt_SMSInsert_req req = new dt_SMSInsert_req();
            //req.SMS_ITEM = new SMS_ITEM[] { new SMS_ITEM() { CONTENT = message,
            //    MESSAGEID = "0000001", MESSAGETYPE = "BC_WX_SMS",
            //    MOBILE = mobile, MSGFORMAT = "8", SRCNUMBER = "106900291033" } };

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
                long ret = xYHCoupon.InsertMessageHis(new Entity.MessageHis()
                {
                    Mobile = mobile,
                    Content = message,
                    TgId = (TgId == null ? 0 : int.Parse(TgId)),
                    IsSend = 1,
                    SendTime = DateTime.Now,
                    Result = 1
                });
                return 1;
            }
            else
            {
                long ret = xYHCoupon.InsertMessageHis(new Entity.MessageHis()
                {
                    Mobile = mobile,
                    Content = message,
                    TgId = (TgId == null ? 0 : int.Parse(TgId)),
                    IsSend = 1,
                    SendTime = DateTime.Now,
                    Result = 0
                });
                return 0;
            }

        }
    }
}