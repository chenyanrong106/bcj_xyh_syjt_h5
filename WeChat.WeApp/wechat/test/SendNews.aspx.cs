using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SPACRM.Business.ServiceImpl;
using SPACRM.Entity;
using System.Text;

namespace SPACRM.WebApp.wechat.test
{
    public partial class SendNews : WeiPage
    {
        NewVerService nvbo = new NewVerService();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if ((Request.QueryString["FromUserName"] != null || Session["FromUserName"] != null) && (Request.QueryString["ToUserName"] != null || Session["ToUserName"] != null))
                {
                    string user = Request.QueryString["FromUserName"] == null ? Session["FromUserName"].ToString() : Request.QueryString["FromUserName"].ToString();
                    string user2 = Request.QueryString["ToUserName"] == null ? Session["ToUserName"].ToString() : Request.QueryString["ToUserName"].ToString();
                    Session["FromUserName"] = user;
                    Session["ToUserName"] = user2;

                }
                //List<Pet_Group> list = nvbo.GetAllGroupList();
                //foreach (Pet_Group g in list)
                //{
                //    DropDownList1.Items.Add(new ListItem(g.Name + "["+g.Count+"人]", g.ID.ToString()));
                //}
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string url = "https://api.weixin.qq.com/cgi-bin/message/mass/sendall?access_token="+Token(mjuserid);
            string jason = "{\"filter\":{\"is_to_all\":false\"group_id\":\"{0}\"},\"news\":{\"media_id\":\"6gLDXsSjCm9WUtH_uTeiS1G5IVMnPVg26Fk1deQ5PqM\"},\"msgtype\":\"news\"}";
            jason = jason.Replace("{0}", DropDownList1.SelectedValue);

            //预览
           // url = "https://api.weixin.qq.com/cgi-bin/message/mass/preview?access_token="+Token(mjuserid);
           // jason = "{   \"touser\":\"o0UMCj6Y9wcOeRVu0hJ0yx2MxCZY\",    \"mpnews\":{    \"media_id\":\"Bb4siJ41sKfRosN6soMm5cOVrmo_AUnJd74F0YzwxS0\"    }, \"msgtype\":\"mpnews\" }";

            jason = "{\"filter\":{\"group_id\":\"" + DropDownList1.SelectedValue + "\"}, \"mpnews\":{\"media_id\":\"6gLDXsSjCm9WUtH_uTeiS1G5IVMnPVg26Fk1deQ5PqM\" }, \"msgtype\":\"mpnews\"}";
            

            
            string jg = HttpXmlPostRequest(url, jason, Encoding.UTF8,"application/json");



            Response.Write(DropDownList1.SelectedItem.Text+ jg);
        }
    }
}