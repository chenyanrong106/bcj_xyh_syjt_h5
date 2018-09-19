using Hmj.Business;
using HmjNew.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SPACRM.WebApp.wechat.XYH_Coupon_H5.wxcard
{
    public partial class test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                Random r = new Random();
                int num = r.Next(100000, 999999);
                string message = string.Format("您的验证码为："
                    + num + "此验证码10分钟内有效，如非本人操作，请联系上海家化华美家微信后台。");

                dt_SMSInsert_req req = new dt_SMSInsert_req
                {
                    SMS_ITEM = new SMS_ITEM[] { new SMS_ITEM() {
                        CONTENT = message,
                        MESSAGEID = "0000001",
                        MESSAGETYPE = "BC_WX_SMS",
                        MOBILE = "13661450526",
                        MSGFORMAT = "8",
                        SRCNUMBER = "1069048560003"
                        }
                    }
                };

                dt_SMSInsert_res res = WebApiHelp.SMSInsert(req, true);

                if (res != null && res.zstatus == "1")
                {
                    this.TextBox1.Text = res.zstatus == "1" ? "成功" : "失败";
                }
            }
            catch(Exception ex) {
                this.TextBox1.Text = ex.Message;
            }
        }
    }
}