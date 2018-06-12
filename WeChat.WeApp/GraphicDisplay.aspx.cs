using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SPACRM.Business.ServiceImpl;
using SPACRM.Interface;
using SPACRM.Entity;

namespace SPACRM.WebApp
{
    public partial class GraphicDisplay : System.Web.UI.Page
    {
        ISystemService sbo = new SystemService();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    WXGraphicDetail d = sbo.GetGraphicDetail(int.Parse(Request.QueryString["id"].ToString()));
                   
                    if (d != null)
                    {
                        WXGraphicList l = sbo.GetGraphicList(int.Parse(d.List_ID.ToString()));
                        if (l != null)
                        {
                            ORG_INFO o = sbo.GetMerchants(int.Parse(l.Merchants_ID.ToString()));
                            if (o != null)
                            {
                                Title.Text = d.Title;
                                divbody.InnerHtml=string.Format(@"<h2 class='rich_media_title'>{0}</h2>
  <div class='rich_media_meta_list'>
       <em id='post-date' class='rich_media_meta text'>{1}</em>
<em id='post-date' class='rich_media_meta text'>{4}</em>
       <a class='rich_media_meta ' href='javascript:void(0);' id='post-user'>{2}</a>
  </div><div class='rich_media_content'>{3}</div>", d.Title, DateTime.Parse(l.CreateDate.ToString()).ToString("yyyy-MM-dd"),o.ORG_NAME,d.Body,d.Author);
                                //divbody.InnerHtml = "<h1>" + d.Title + "</h1>" + DateTime.Parse(l.CreateDate.ToString()).ToString("yyyy-MM-dd")+"&nbsp;"+o.ORG_NAME + "<br>" + d.Body;
                            }
                        }
                    }
                }
            }
        }
    }
}