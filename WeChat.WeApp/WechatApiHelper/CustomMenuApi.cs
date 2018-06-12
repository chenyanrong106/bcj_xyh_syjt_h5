using Newtonsoft.Json;
using SPACRM.Business.ServiceImpl;
using SPACRM.Interface;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace SPACRM.WebApp.WechatApiHelper
{
    public class CustomMenuApi : BasePage //: WechatApi
    {
        private ICustomMenuService _cms;
        public CustomMenuApi()
        {
            _cms = new CustomMenuService();
        }

        public Result CreateMenu()
        {
            var str = string.Empty;
            if (GetCreateJson(ref str))
            {
                var accessToken = base.Token();
                var postUrl = RequestUrlHelper.CustomMenu.Create(accessToken);
                return JsonConvert.DeserializeObject<Result>(base.HttpXmlPostRequest(postUrl, str, Encoding.UTF8));
            }
            else
            {
                var result = new Result() { errcode = -1, errmsg = str };
                return result;
            }
            //return null;
        }

        public bool GetCreateJson(ref string str)
        {
            var 所有菜单 = _cms.GetCustomMenuList(CurrentMerchants.ID);
            var 微信零级菜单 = new Wechat_Level0Menu();
            var 微信一级菜单列表 = new List<Wechat_Level1Menu>();
            //var i = 1;
            //var path = "D:/menulog.txt";
            //File.AppendAllText(path, "\r\n" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + " ");

            foreach (var 一级菜单 in 所有菜单.Where(cm => cm.Depth == 1).OrderBy(cm => cm.OrderNum))
            {
                var 二级菜单集合 = 所有菜单.Where(cm => cm.ParentID == 一级菜单.ID).OrderBy(cm => cm.OrderNum);
                if (二级菜单集合.Count() > 0)
                {
                    var 微信一级菜单 = new Wechat_Level1Menu_hasChild();
                    微信一级菜单.name = 一级菜单.Name;
                    var 微信二级菜单集合 = new List<Wechat_Level2Menu>();

                    foreach (var 二级菜单 in 二级菜单集合)
                    {
                        if (!二级菜单.Type.HasValue)
                        {
                            str = string.Format("{0}-{1}没有配置响应动作。", 一级菜单.Name, 二级菜单.Name);
                            return false;//
                        }

                        else if (二级菜单.Type == 7) //多客服
                        {
                            var wechat_Level2Menu = new Wechat_Level2Menu_click();
                            wechat_Level2Menu.name = 二级菜单.Name;
                            wechat_Level2Menu.key = "dkf";
                            微信二级菜单集合.Add(wechat_Level2Menu);
                        }
                        else if (二级菜单.Type != 3) //不是外链
                        {
                            var wechat_Level2Menu = new Wechat_Level2Menu_click();
                            wechat_Level2Menu.name = 二级菜单.Name;
                            wechat_Level2Menu.key = 二级菜单.ID.ToString();
                            微信二级菜单集合.Add(wechat_Level2Menu);
                        }
                        else
                        {
                            var wechat_Level2Menu = new Wechat_Level2Menu_view();
                            wechat_Level2Menu.name = 二级菜单.Name;
                            wechat_Level2Menu.type = "view";
                            wechat_Level2Menu.url = 二级菜单.Url;

                            if (二级菜单.Url.Contains(ConfigurationManager.AppSettings["ServerIP"]))
                            {
                                if (二级菜单.Url.Contains("?"))
                                    二级菜单.Url = 二级菜单.Url + "&ToUserName=" + CurrentMerchants.ToUserName;
                                else
                                    二级菜单.Url = 二级菜单.Url + "?ToUserName=" + CurrentMerchants.ToUserName;

                                wechat_Level2Menu.url = string.Format("https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri={1}&response_type=code&scope=snsapi_base&state=STATE#wechat_redirect",
                                    GetAppid(), 二级菜单.Url);
                                ;
                            }
                            微信二级菜单集合.Add(wechat_Level2Menu);

                        }
                        //File.AppendAllText(path, i++.ToString() + ",");
                    }
                    微信一级菜单.sub_button = 微信二级菜单集合.ToArray();
                    微信一级菜单列表.Add(微信一级菜单);
                    //File.AppendAllText(
                }
                else
                {
                    if (!一级菜单.Type.HasValue)
                    {
                        str = string.Format("{0}没有配置响应动作。", 一级菜单.Name);
                        return false;
                    }

                    else if (一级菜单.Type == 7) //多客服
                    {
                        var 微信一级菜单 = new Wechat_Level1Menu_noChild_click();
                        微信一级菜单.name = 一级菜单.Name;
                        微信一级菜单.key = "dkf";
                        微信一级菜单列表.Add(微信一级菜单);
                    }
                    else if (一级菜单.Type != 3) //不是外链
                    {
                        var 微信一级菜单 = new Wechat_Level1Menu_noChild_click();
                        微信一级菜单.name = 一级菜单.Name;
                        微信一级菜单.key = 一级菜单.ID.ToString();
                        微信一级菜单列表.Add(微信一级菜单);
                    }
                    else
                    {
                        var 微信一级菜单 = new Wechat_Level1Menu_noChild_view();
                        微信一级菜单.name = 一级菜单.Name;
                        微信一级菜单.type = "view";
                        微信一级菜单.url = 一级菜单.Url;
                        微信一级菜单列表.Add(微信一级菜单);
                    }
                }
            }
            微信零级菜单.button = 微信一级菜单列表.ToArray();
            str = JsonConvert.SerializeObject(微信零级菜单);
            return true;
        }

        #region wechat obj

        public class Wechat_Level0Menu
        {
            public Wechat_Level1Menu[] button;
        }

        public class Wechat_Level1Menu
        {
            public string name;
        }

        public class Wechat_Level1Menu_noChild : Wechat_Level1Menu
        {

        }

        public class Wechat_Level1Menu_noChild_click : Wechat_Level1Menu_noChild
        {
            public string type = "click";
            public string key;
        }

        public class Wechat_Level1Menu_noChild_view : Wechat_Level1Menu_noChild
        {
            public string type = "view";
            public string url;
        }

        public class Wechat_Level1Menu_hasChild : Wechat_Level1Menu
        {
            public Wechat_Level2Menu[] sub_button;
        }

        public class Wechat_Level2Menu
        {
            public string name;
        }

        public class Wechat_Level2Menu_click : Wechat_Level2Menu
        {
            public string type = "click";
            public string key;
        }

        public class Wechat_Level2Menu_view : Wechat_Level2Menu
        {
            public string type = "view";
            public string url;
        }

        public class Result
        {
            public int errcode;
            public string errmsg;
        }

        #endregion
    }
}