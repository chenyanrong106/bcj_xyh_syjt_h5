using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SPACRM.WebApp
{
    public partial class SendTmpMessage : WeiPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
             
                if (Request.QueryString["para"] == "send")
                {
                    object obj = HttpContext.Current.Request.Params;
                    string openid = Request.QueryString["openid"];
                    string temid= Request.QueryString["temid"];
                    string first = Request.QueryString["first"];
                    string keyword1 = Request.QueryString["keyword1"];
                    string keyword2 = Request.QueryString["keyword2"];
                    string keyword3 = Request.QueryString["keyword3"];
                    string keyword4 = Request.QueryString["keyword4"];
                    string keyword5 = Request.QueryString["keyword5"];
                    string keyword6 = Request.QueryString["keyword6"];
                    string keyword7 = Request.QueryString["keyword7"];
                    string url = Request.QueryString["url"];
                    string color = Request.QueryString["color"];
                    if (string.IsNullOrWhiteSpace(color))
                        color = "#ac2102";

                    string token = Token(mjuserid);
                    var temp = new
                    {
                        first = new { value = first, color = color },
                        keyword1 = keyword1 == null ? null : new { value = keyword1, color = color },
                        keyword2 = keyword2 == null ? null : new { value = keyword2, color = color },
                        keyword3 = keyword3 == null ? null : new { value = keyword3, color = color },
                        keyword4 = keyword4 == null ? null : new { value = keyword4, color = color },
                        keyword5 = keyword5 == null ? null : new { value = keyword5, color = color },
                        keyword6 = keyword6 == null ? null : new { value = keyword6, color = color },
                        keyword7 = keyword7 == null ? null : new { value = keyword7, color = color },

                    };
                    string message = SendTemplateMessage(token, openid, temid, "#FF0000", temp, url);//Seapage2

                }
            }
        }
    }
}