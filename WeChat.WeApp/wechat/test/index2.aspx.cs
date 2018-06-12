using SPACRM.Business.ServiceImpl;
using SPACRM.Entity;
using SPACRM.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace SPACRM.WebApp.wechat.test
{
    public partial class index2 : WeiPage
    {

        ISystemService sbo = new SystemService();
        MySmallShopService mss = new MySmallShopService();
        const string Token = "puman";		//与微信平台那边填写的token一致
        protected void Page_Load(object sender, EventArgs e)
        {
            string postStr = "";
            if (Request.HttpMethod.ToLower() == "post")
            {
                Stream s = System.Web.HttpContext.Current.Request.InputStream;
                byte[] b = new byte[s.Length];
                s.Read(b, 0, (int)s.Length);
                postStr = Encoding.UTF8.GetString(b);

                WXLOG log = new WXLOG { CON = postStr + "INDEX2", TIME = DateTime.Now };
                mss.SaveLog(log);
            }
            else
            {

            }
        }
    }
}