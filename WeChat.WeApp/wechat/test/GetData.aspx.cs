using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SPACRM.WebApp.wechat.test
{
    public partial class GetData : WeiPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string url = "https://a.dper.com/shops#/shops/all?data=%7B%22dynamicCondition%22%3A%7B%7D%2C%22condition%22%3A%7B%22mainCategory%22%3A45%2C%22category%22%3A147%2C%22mainRegion%22%3A-1%2C%22region%22%3A-1%2C%22ownerType%22%3A0%2C%22city%22%3A1%7D%2C%22pagination%22%3A%7B%22isRequested%22%3Afalse%2C%22isRequesting%22%3Afalse%2C%22isEnd%22%3Afalse%2C%22index%22%3A319%2C%22size%22%3A10%7D%7D&_k=4vr0d9";
            Response.Write(PostRequest(url));
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string url = "https://a.dper.com/shop/cascade/query";
            var data = new { mainCategory = -1, category = -1, mainRegion = -1, region = -1, ownerType = 0, city = 1, pageIndex = 5000, pageSize = 10 };
            string ret = HttpXmlPostRequest(url, JsonConvert.SerializeObject(data), System.Text.Encoding.UTF8);
            Response.Write(ret);
        }
    }
}