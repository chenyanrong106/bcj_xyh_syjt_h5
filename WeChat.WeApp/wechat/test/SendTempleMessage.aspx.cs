using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SPACRM.WebApp.wechat.test
{
    public partial class SendTempleMessage :WeiPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string token = Token(mjuserid);
            var temp = new
            {
                first = new { value = "我们正在为您配送手工半湿粮", color = "#173177" },
                keyword1 = new { value = "PETKIN手工半湿粮", color = "#173177" },
                keyword2 = new { value =(TextBox2.Text==""? "圆通快递":TextBox2.Text), color = "#173177" },
                keyword3 = new { value = "准备发货", color = "#173177" },
                keyword4 = new { value = "准备发货", color = "#173177" },
                remark = new { value = "\n" +(TextBox3.Text==""? "亲，您好您抢拍的PETKIN手工半湿粮，收货地址缺少省份和地区，请直接回复详细地址谢谢":TextBox3.Text), color = "#CD0000" }
            };
            string message = SendTemplateMessage(token, TextBox1.Text, "EVr5hLKdp7ltgADIg2pXXoOWmn-gmoS9uQviHlX7nuo", "#FF0000", temp, (RadioButton1.Checked ? "http://www.wechat.petkin.cn/wechat/test/tz2.aspx?tousername=" : "http://www.wechat.petkin.cn/wechat/test/tz.aspx?tousername=") + mjuserid);
            if (message.Contains("ok"))
            {
                Response.Write(TextBox1.Text + "  发送成功");
               // Response.End();
            }
            else
            {
                Response.Write(TextBox1.Text+ message);
               // Response.End();
            }
        }
    }
}