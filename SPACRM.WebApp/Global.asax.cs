using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using SPACRM.Extension;
using Vulcan.Framework.DBConnectionManager;

namespace SPACRM.WebApp
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var config = GlobalConfiguration.Configuration;
            var jqueryFormatter = config.Formatters.FirstOrDefault(x => x.GetType() == typeof(System.Web.Http.ModelBinding.JQueryMvcFormUrlEncodedFormatter));

            config.Formatters.Remove(config.Formatters.JsonFormatter);
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            config.Formatters.Remove(config.Formatters.FormUrlEncodedFormatter);
            config.Formatters.Remove(jqueryFormatter);

            config.Formatters.Insert(0, new ServiceStackTextFormatter(ServiceStack.Text.JsonDateHandler.TimestampOffset));

            //new Timer();

            //AreaRegistration.RegisterAllAreas(); 
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            //默认的数据库管理
            DbConnectionFactory.Default = new SqlConnectionFactory();

            //依赖注入
            Bootstrapper.Restart();

            ControllerBuilder.Current.SetControllerFactory(new StructureMapControllerFactory()); 
        }
    }
}