using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SPACRM.WebApp.wechat.test
{
    public partial class GetNewsList : WeiPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string url = "https://api.weixin.qq.com/cgi-bin/material/batchget_material?access_token="+Token(mjuserid);
            string jason="{\"type\":\"news\",\"offset\":0, \"count\":20}";
            string jg = HttpXmlPostRequest(url, jason, Encoding.UTF8);
        }
    }
}