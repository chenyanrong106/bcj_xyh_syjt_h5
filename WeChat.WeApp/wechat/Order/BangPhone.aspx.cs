using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SPACRM.Business.ServiceImpl;
using SPACRM.Entity;

namespace SPACRM.WebApp.wechat.Order
{
    public partial class BangPhone : WeiPage
    {
        MySmallShopService mss = new MySmallShopService();
        NewVerService nvbo = new NewVerService();
        public OAauth_Log oa = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["para"] != null && Request.QueryString["para"] == "SendDX")
            {
                Random r = new Random();
                int num = r.Next(100000, 999999);
                string message = "本次获取的验证码是" + num + "。";
                string zt = new SendDX().Send(message, Request.Params["phone"]);
                if (zt == "发送成功")
                {
                    Session["RanPhone"] = Request.Params["phone"];
                    Session["RanNum"] = num.ToString();
                    Response.Write("{\"message\":\"发送成功。\",\"status\":\"" + 0 + "\"}");
                }
                else
                {
                    Response.Write("{\"message\":\"发送失败。\",\"status\":\"" + -1 + "\"}");
                }
                Response.End();
            }
            else if (Request.QueryString["para"] != null && Request.QueryString["para"] == "bang")
            {
                if (Session["RanNum"] == null)
                {
                    Response.Write("{\"message\":\"请先获取验证码。\",\"status\":\"" + -1 + "\"}");
                }
                else if (Request.QueryString["phone"] != Session["RanPhone"].ToString() || Request.QueryString["yzm"] != Session["RanNum"].ToString())
                {
                    Response.Write("{\"message\":\"输入的验证码有误。\",\"status\":\"" + -1 + "\"}");
                }
                else
                {
                    string url = "wo.aspx";
                    OAauth_Log oa1 = mss.GetOAByPhone(Request.QueryString["phone"]);//根据手机号码获取到的信息
                    if ((Request.QueryString["FromUserName"] != null || Session["FromUserName"] != null) && (Request.QueryString["ToUserName"] != null || Session["ToUserName"] != null))
                    {
                        string user = Request.QueryString["FromUserName"] == null ? Session["FromUserName"].ToString() : Request.QueryString["FromUserName"].ToString();
                        string user2 = Request.QueryString["ToUserName"] == null ? Session["ToUserName"].ToString() : Request.QueryString["ToUserName"].ToString();
                        oa = mss.GetOA(user);//根据微信号获取到的信息

                        if (oa != null && !string.IsNullOrEmpty(oa.Phone))
                        {
                            Response.Write("{\"message\":\"你已经绑定。\",\"status\":\"" + -1 + "\"}");
                        }
                        //else if (oa != null && oa1 != null && !string.IsNullOrEmpty(oa1.FromUserName) && oa1.FromUserName != null&&oa1.ToUserName!="web") //根据手机号码获取到的用户信息，微信号码不同
                        //{
                        //    Response.Write("{\"message\":\"该手机已被其他微信绑定。\",\"status\":\"" + -1 + "\"}");
                        //}
                        //可绑定多个账号
                        else
                        {
                            if (oa != null && oa1 != null && oa1.ID != oa.ID) //根据手机号获取到另一个来自网页的绑定的信息，将手机号绑定到有微信号的信息中，删除无手机号的信息
                            {
                                oa.Phone = Request.QueryString["phone"];
                                //mss.DeteleOA(oa1.ID); //保留原有账号不删除，保留web版账号
                            }
                            else if (oa != null)
                            {
                                oa.Phone = Request.QueryString["phone"];
                            }
                            else
                            {
                                oa = new OAauth_Log
                                {
                                    CreateDate = DateTime.Now,
                                    FromUserName = user,
                                    ToUserName = "",
                                    Nickname = "用户",
                                    Phone = Request.QueryString["phone"]
                                };
                            }
                            mss.SaveOA(oa);
                            Session["FromUserName"] = oa.FromUserName;
                            Session["ToUserName"] = oa.ToUserName;
                            if (Session["beforeurl2"] != null)
                                url = Session["beforeurl2"].ToString().Replace("*", "&");
                            Response.Write("{\"message\":\"登录成功\",\"status\":\"" + 1 + "\",\"url\":\"" + url + "\"}");
                        }
                    }
                    else
                    {
                        if (oa1 == null)  //web版登录
                        {
                            oa = new OAauth_Log
                            {
                                CreateDate = DateTime.Now,
                                FromUserName = Guid.NewGuid().ToString("d"),
                                ToUserName = "web",
                                Nickname = "用户",
                                Phone = Request.QueryString["phone"]
                            };
                            mss.SaveOA(oa);
                            Session["FromUserName"] = oa.FromUserName;
                            Session["ToUserName"] = oa.ToUserName;
                            if (Session["beforeurl2"] != null)
                                url = Session["beforeurl2"].ToString().Replace("*", "&");
                            Response.Write("{\"message\":\"登录成功\",\"status\":\"" + 1 + "\",\"url\":\"" + url + "\"}");
                        }
                        else
                        {
                            Session["FromUserName"] = oa1.FromUserName;
                            Session["ToUserName"] = oa1.ToUserName;
                            if (Session["beforeurl2"] != null)
                                url = Session["beforeurl2"].ToString().Replace("*", "&");
                            Response.Write("{\"message\":\"登录成功\",\"status\":\"" + 1 + "\",\"url\":\"" + url + "\"}");
                        }
                    }
                }
                Response.End();
            }
            else
            {
                if (Request.QueryString["beforeurl2"] != null && Session["beforeurl2"] == null)
                    Session["beforeurl2"] = Request.QueryString["beforeurl2"];
                if ((Request.QueryString["FromUserName"] != null || Session["FromUserName"] != null) && (Request.QueryString["ToUserName"] != null || Session["ToUserName"] != null))
                {
                    string user = Request.QueryString["FromUserName"] == null ? Session["FromUserName"].ToString() : Request.QueryString["FromUserName"].ToString();
                    string user2 = Request.QueryString["ToUserName"] == null ? Session["ToUserName"].ToString() : Request.QueryString["ToUserName"].ToString();
                    oa = mss.GetOA(user);//根据微信号获取到的信息
                }
            }
        }
    }
}