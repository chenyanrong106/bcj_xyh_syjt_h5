using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using SPACRM.Common;
using ServiceStack.Text;
using SPACRM.Entity.Entities;
using SPACRM.Entity;
using StructureMap.Attributes;
using SPACRM.Interface;
using SPACRM.Entity.Jsons;

namespace SPACRM.Extension
{
    /// <summary>
    /// Controller 基类，包装一些同样的方法和属性
    /// </summary>
    public class MyControllerBase : Controller
    {
        

        ///// <summary>
        ///// 当前登录用户信息
        ///// </summary>
        ///// <value>The current user.</value>
        //protected virtual IdentityUser CurrentUser
        //{
        //    get
        //    {
        //        return MyContext.CurrentUser;
        //    }
        //}
        

        /// <summary>
        /// 判断用户是否属于某个角色
        /// </summary>
        /// <param name="roleCode">角色标识</param>
        /// <returns>
        /// 	<c>true</c> 是否属于某个角色 <c>false</c>.
        /// </returns>
        ////protected virtual bool IsInRole(string roleCode)
        ////{
        ////    return MyContext.IsInRole(roleCode);
        ////}


        ///// <summary>
        ///// 获取当前登录用户ID
        ///// </summary>
        ///// <value>The LoginUserId ID.</value>
        //protected virtual int LoginUserId
        //{
        //    get
        //    {
        //        return CurrentLoginUser == null ? 0 : CurrentLoginUser.ID;
        //    }
        //}

       
      
        /// <summary>
        /// 处理Controller异常
        /// </summary>
        /// <param name="filterContext"></param>
        //protected override void OnException(ExceptionContext filterContext)
        //{
        //    //异常
        //    var exception = filterContext.Exception;
        //    var request = filterContext.RequestContext.HttpContext.Request;

        //    string path = string.Format("{0}", request.Path);
        //    string queryString = string.Format("{0}", request.Url.Query);
        //    string delimiter = "-----------------------------------------------------------------------------------------------------";

        //    //记录日志
        //    try
        //    {
        //        string errorMsg = string.Format(" 异常OnException {0} \r\n Message = {1}\r\n StackTrace = {2}\r\n Path = {3}\r\n QueryString = {4}",
        //                                        delimiter, exception.Message, exception.StackTrace, path, queryString);

        //        LogService.Error(errorMsg);
        //    }
        //    catch (Exception)
        //    {
        //        //记日志报错，还能做什么！？
        //    }
        //    base.OnException(filterContext);

        //    if (request.IsAjaxRequest())
        //    {
        //        JsonSMsg rMsg = new JsonSMsg();
        //        rMsg.Status = -9999;
        //        rMsg.Message = "异常：" + exception.Message;
        //        filterContext.Result = base.Json(rMsg);
        //    }
        //    else
        //    {
        //        string url = Url.Action("Error", "Home", new { ReturnUrl = path });
        //        filterContext.Result = new RedirectResult(url);
        //        //filterContext.RequestContext.HttpContext.Response.Redirect(url);
        //    }
        //    filterContext.ExceptionHandled = true;
        //}

        protected FilePathResult EmptyImage()
        {
            string emptyFile = Server.MapPath("~/assets/img/icon/s.gif");   //404.png
            return File(emptyFile, "image/gif");
        }
    

        //重写基类的方法
        #region 重写基类的方法
        protected override JsonResult Json(object data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new ServiceStackJsonResult
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding
            };
        }
        #endregion
    }

    public class ServiceStackJsonResult : JsonResult
    {
        public override void ExecuteResult(ControllerContext context)
        {
            HttpResponseBase response = context.HttpContext.Response;
            response.ContentType = !String.IsNullOrEmpty(ContentType) ? ContentType : "application/json";

            if (ContentEncoding != null)
            {
                response.ContentEncoding = ContentEncoding;
            }

            if (Data != null)
            {
                response.Write(Data.ToJson());
            }
        }


    }

}
