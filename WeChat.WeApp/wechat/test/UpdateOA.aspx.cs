using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SPACRM.Business.ServiceImpl;
using SPACRM.Entity;
using Newtonsoft.Json;


namespace SPACRM.WebApp.wechat.test
{
    public partial class UpdateOA : WeiPage
    {
        NewVerService nvbo = new NewVerService();
        MySmallShopService mss = new MySmallShopService();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            List<string> list = nvbo.GetOrderOpenidNotInOA();
            int total = list.Count;
            int cg = 0;
            foreach (string o in list)
            {
                try
                {

                OAauth_Log oa = new OAauth_Log();
                oa.CreateDate = DateTime.Now;
                oa.FromUserName = o;
                oa.ToUserName = tousername;
                string url = "https://api.weixin.qq.com/cgi-bin/user/info?access_token=" + Token(mjuserid) + "&openid=" + o + "&lang=zh_CN";
                string token = PostRequest(url);
                OpenInfo autho = JsonConvert.DeserializeObject<OpenInfo>(token);
                if (autho.subscribe == 1)
                {
                    oa.headimgurl = autho.headimgurl;
                    oa.Nickname = autho.nickname;
                    oa.sex = autho.sex;
                    oa.country = autho.country;
                    oa.province = autho.province;
                    oa.city = autho.city;

                    DownHeadImage(oa);
                    mss.SaveOAtuh(oa);
                    cg++;
                }

                }
                catch (Exception)
                {

                }
            }
            Response.Write("共计"+total+",成功"+cg);
        }
    }
}