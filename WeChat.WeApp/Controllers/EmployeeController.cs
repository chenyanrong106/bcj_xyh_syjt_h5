using SPACRM.Entity;
using SPACRM.Entity.Jsons;
using SPACRM.Entity.PageSearch;
using SPACRM.Entity.SearchEntity;
using SPACRM.Extension;
using SPACRM.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SPACRM.Entity.Entities;
using System.Text;

namespace WeChat.WebApp.Controllers
{
    public class EmployeeController : WXMyControllerBase
    {
        
        private IEmployeeService _eservice;
        private ISystemService _service;
        public EmployeeController(IEmployeeService eService, ISystemService service)
        {
            _eservice = eService;
            _service = service;
            ViewData["ORG_ID"] = base.CurrentUserOrgId;
            ViewData["ISADMIN"] = base.CurrentIsOrgAdmin;
            ViewData["USER_TYPE"] = base.CurrentUserType;

            ViewBag.StoreId = base.CurrentUserStoreId.ToString();
            ViewBag.OrgId = base.CurrentUserOrgId.ToString();
        }

        #region 员工管理 maya

        public ActionResult Index()
        {
            return View(new EmployeeSearch()
            {
                ORG_ID = base.CurrentUserOrgId,
                UserType = ""
            });
        }

        /// <summary>
        /// 查询左边的数据树值
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult QueryLeft(FormCollection form)
        {
            List<JsonTreeNode> treelist = new List<JsonTreeNode>();
            List<ORG_INFO> cuList = _service.GetMerchantsList();
            if (cuList != null && cuList.Count > 0)
            {

                foreach (var p in cuList)
                {
                    JsonTreeNode nodeM = new JsonTreeNode();
                    nodeM.hasChildren = false;
                    nodeM.id = p.ID.ToString();
                    nodeM.text = p.ORG_NAME;
                    nodeM.value = p.ID.ToString();
                    nodeM.data = null;// new Dictionary<string, string>() { { "orgLevel", p.ORG_LEVEL } };
                    nodeM.isexpand = true;
                    //SetTreeChildree(cuList, ref nodeM, p.ID);
                    nodeM.complete = true;
                    treelist.Add(nodeM);
                }
            }

            return Json(treelist);
        }

        void SetTreeChildree(List<ORG_INFO_EX> cuAllList, ref JsonTreeNode cuJsonTreeNode, int pid)
        {
            //var childList = cuAllList.Where(c => c.PARENT_ID == pid);

            //foreach (var p in childList)
            //{
            //    JsonTreeNode node = new JsonTreeNode();
            //    node.id = p.ID.ToString() + "_" + p.PARENT_ID.ToString();
            //    node.text = p.ORG_NAME;
            //    node.value = p.ID.ToString();
            //    node.isexpand = false;
            //    node.data = new Dictionary<string, string>() { { "orgLevel", p.ORG_LEVEL } };
            //    if (p.ORG_LEVEL != "2")
            //    {
            //        node.hasChildren = (cuAllList.Count(r => r.PARENT_ID == p.ID) > 0);
            //        SetTreeChildree(cuAllList, ref node, p.ID);
            //    }
            //    else
            //        node.hasChildren = false;
            //    node.complete = true;
            //    cuJsonTreeNode.ChildNodes.Add(node);
            //}
        }

        //列表
        [HttpPost]
        public JsonResult List(EmployeeSearch search, FormCollection form)
        {
            if (search == null)
            {
                search = new EmployeeSearch();
            }
            search.ORG_ID = base.CurrentUserOrgId;
            search.CuEmpId = base.CurrentUserEmployeeId;
            search.UserType = form["USER_TYPE"] == null ? "" : form["USER_TYPE"].ToString();
            PageView view = new PageView(form);
            string colkey = form["colkey"];
            string colsinfo = form["colsinfo"];
            //员工列表           
            PagedList<ORG_EMPLOYEE_EX> pList = _eservice.QueryEmployeeList(search, view);
            JsonQTable fdata = JsonQTable.ConvertFromPagedList<ORG_EMPLOYEE_EX>(pList, colkey, colsinfo.Split(','));
            return Json(fdata);
        }

        //删除
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            JsonSMsg rMsg = new JsonSMsg();
            try
            {
                if (id <= 0)
                {
                    rMsg.Status = -1;
                    rMsg.Message = "参数错误！";
                    return Json(rMsg);
                }

                int ret = _eservice.DeleteEmployee(id);
                if (ret > 0)
                {
                    rMsg.Status = 0;
                    rMsg.Data = ret;
                }
                else
                {
                    rMsg.Status = -1;
                    rMsg.Message = "意外错误，请稍后重试";
                }
            }
            catch (Exception ex)
            {
                rMsg.Status = -1;
                rMsg.Message = ex.Message;
            }
            return Json(rMsg);
        }

        //验证
        private static bool ValidateEMP(ORG_EMPLOYEE employee, out string errmsg)
        {
            errmsg = "";
            if (employee == null)
            {
                errmsg = "参数错误！";
                return false;
            }
            if (string.IsNullOrEmpty(employee.NAME))
            {
                errmsg = "员工姓名不能为空！";
                return false;
            }

            if (string.IsNullOrEmpty(employee.EMPLOYEE_NO))
            {
                errmsg = "员工编号不能为空！";
                return false;
            }

            if (employee.ORG_ID == 0)
            {
                errmsg = "员工没有所属公司！";
                return false;
            }

            if (employee.STORE_ID == 0)
            {
                errmsg = "员工所属门店不能为空！";
                return false;
            }

            return true;
        }

        //修改
        //[HttpPost]
        public ActionResult EditInfo(string userType, int? storeId, int? id)
        {
            try
            {
                if (id.HasValue)
                {
                    ORG_EMPLOYEE_EX employee = _eservice.GetEmployeeEX(id.Value);
                    if (null != employee)
                    {
                        return View(employee);
                    }
                    else
                    {
                        return View(new ORG_EMPLOYEE_EX()
                        {
                            SALARY_TYPE = 1,
                            MIN_SALARY = 0,
                            STATUS = 1,
                            ORG_ID = base.CurrentUserOrgId,
                            USER_TYPE = userType,
                            STORE_ID = storeId.Value
                        });
                    }
                }
                else
                {
                    return View(new ORG_EMPLOYEE_EX()
                    {
                        SALARY_TYPE = 1,
                        MIN_SALARY = 0,
                        STATUS = 1,
                        ORG_ID = base.CurrentUserOrgId,
                        USER_TYPE = userType,
                        STORE_ID = storeId.Value
                    });
                }
            }
            catch (Exception)
            {
                return View(new ORG_EMPLOYEE_EX() { SALARY_TYPE = 1, MIN_SALARY = 0, STATUS = 1, ORG_ID = base.CurrentUserOrgId });
            }

        }

        //保存基础信息
        [HttpPost]
        public ActionResult SaveJcxx(ORG_EMPLOYEE_EX CuORG_EMPLOYEE)
        {
            JsonSMsg rMsg = new JsonSMsg();
            try
            {
                string errmsg = "";
                //CuORG_EMPLOYEE.STORE_ID = base.CurrentIsOrgAdmin ? CuORG_EMPLOYEE.STORE_ID : base.CurrentUserStoreId;
                CuORG_EMPLOYEE.ORG_ID = base.CurrentUserOrgId;

                DateTime od = DateTime.Now;
                if (!string.IsNullOrEmpty(CuORG_EMPLOYEE.ENTRY_DATE_Str))
                {
                    DateTime.TryParse(CuORG_EMPLOYEE.ENTRY_DATE_Str, out od);
                }
                CuORG_EMPLOYEE.ENTRY_DATE = od;

                DateTime cd = DateTime.Now;
                if (!string.IsNullOrEmpty(CuORG_EMPLOYEE.DISM_DATE_Str))
                {
                    DateTime.TryParse(CuORG_EMPLOYEE.DISM_DATE_Str, out cd);
                    CuORG_EMPLOYEE.DISM_DATE = cd;
                }
                else CuORG_EMPLOYEE.DISM_DATE = DateTime.MaxValue;


                if (!ValidateEMP(CuORG_EMPLOYEE, out errmsg))
                {
                    rMsg.Status = -1;
                    rMsg.Message = errmsg;
                    return Json(rMsg);
                }

                int ret = _eservice.SaveEmployee(CuORG_EMPLOYEE);
                if (ret > 0)
                {
                    rMsg.Status = 0;
                    rMsg.Data = CuORG_EMPLOYEE.ID > 0 ? CuORG_EMPLOYEE.ID : ret;
                }
                else if (ret == -2)
                {
                    rMsg.Status = -2;
                    rMsg.Message = "员工工号重复，请重新填写";
                }
                else
                {
                    rMsg.Status = -1;
                    rMsg.Message = "意外错误，请稍后重试";
                }
            }
            catch (Exception ex)
            {
                rMsg.Status = -1;
                rMsg.Message = ex.Message;
            }
            return Json(rMsg);
        }

        //保存薪酬与福利
        [HttpPost]
        public ActionResult SaveXcyfl(ORG_EMPLOYEE_EX CuORG_EMPLOYEE, FormCollection form)
        {
            JsonSMsg rMsg = new JsonSMsg();
            try
            {
                CuORG_EMPLOYEE.ID = Int32.Parse(form["ID1"]);
                //if (!ValidateEMP(CuORG_EMPLOYEE, out errmsg))
                //{
                //    rMsg.Status = -1;
                //    rMsg.Message = errmsg;
                //    return Json(rMsg);
                //}

                int ret = _eservice.SaveEmployeeXcyfl(CuORG_EMPLOYEE);
                if (ret > 0)
                {
                    rMsg.Status = 0;
                    rMsg.Data = CuORG_EMPLOYEE.ID;
                }
                else
                {
                    rMsg.Status = -1;
                    rMsg.Message = "意外错误，请稍后重试";
                }
            }
            catch (Exception ex)
            {
                rMsg.Status = -1;
                rMsg.Message = ex.Message;
            }
            return Json(rMsg);
        }

        //获取修改时的职位
        [HttpPost]
        public ActionResult GetEditPost(FormCollection form)
        {
            JsonSMsg rMsg = new JsonSMsg();
            try
            {
                var post_id = form["post_id"];
                var ret = _eservice.GetEditPost(base.CurrentUserOrgId, (post_id == null || post_id == "" ? 0 : Int32.Parse(post_id)));
                rMsg.Status = 0;
                rMsg.Data = ret;

            }
            catch (Exception ex)
            {
                rMsg.Status = -1;
                rMsg.Message = ex.Message;
            }
            return Json(rMsg);
        }

        #endregion

        #region 登陆账户

        public ActionResult UserInfo()
        {
            return View(new USER_INFO_EX() { ORG_ID = base.CurrentUserOrgId });
        }
        //修改
        public ActionResult EditUserInfo(int id)
        {
            var jmsg = new JsonSMsg();
            var cuObj = _eservice.GetUSER_INFO_EX(id);
            jmsg.Status = 0;
            jmsg.Data = cuObj;
            return Json(jmsg);
        }

        //保存登陆账户
        [HttpPost]
        public ActionResult SaveDlzh(USER_INFO_EX CUUSER_INFO, FormCollection form)
        {
            JsonSMsg rMsg = new JsonSMsg();
            try
            {
                CUUSER_INFO.ORG_ID = base.CurrentUserOrgId;
                int ret = _eservice.SaveEmployeeDlzh(CUUSER_INFO);
                if (ret > 0)
                {
                    rMsg.Status = 0;
                    rMsg.Data = ret;
                }
                else if (ret == -2)
                {
                    rMsg.Status = -2;
                    rMsg.Message = "登录名已被注册，请重新填写";
                }
                else
                {
                    rMsg.Status = -1;
                    rMsg.Message = "意外错误，请稍后重试";
                }
            }
            catch (Exception ex)
            {
                rMsg.Status = -1;
                rMsg.Message = ex.Message;
            }
            return Json(rMsg);
        }

        //登陆账户列表
        [HttpPost]
        public JsonResult ListDlzh(FormCollection form)
        {
            UserInfoSearch search = new UserInfoSearch();

            search.ORG_ID = base.CurrentUserOrgId;
            search.USER_NO = form["USER_NO1"].ToString();
            search.USER_TYPE = form["USER_TYPES"].ToString();//当前用户的用户类型 STORE_ID
            search.STORE_ID = form["STORE_IDS"] == null ? 0 : int.Parse(form["STORE_IDS"].ToString());
            search.CUUSER_ID = base.CurrentLoginUserId;

            PageView view = new PageView(form);
            search.ORG_ID = base.CurrentUserOrgId;

            string colkey = form["colkey"];
            string colsinfo = form["colsinfo"];
            //账户列表           
            PagedList<USER_INFO_EX> pList = _eservice.QueryUSER_INFO_EXList(search, view);
            JsonQTable fdata = JsonQTable.ConvertFromPagedList<USER_INFO_EX>(pList, colkey, colsinfo.Split(','));
            return Json(fdata);
        }

        //根据“用户类别”获取“区域”的下拉框数据
        [HttpPost]
        public ActionResult GetUserREGION(FormCollection form)
        {
            JsonSMsg rMsg = new JsonSMsg();
            try
            {
                var USER_TYPE = form["USER_TYPE"];
                var STORE_ID = form["STORE_ID"];
                var ret = _eservice.GetUserREGION(USER_TYPE.ToString(), base.CurrentUserOrgId, base.CurrentUserStoreId, base.CurrentUserType);
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("<option value=\"0\">请选择区域...</option>");
                foreach (var item in ret)
                {
                    if (STORE_ID.ToString() == item.Value.ToString())
                        sb.AppendLine("<option value=\"" + item.Value + "\" selected=\"selected\">" + item.Name + "</option>");
                    else sb.AppendLine("<option value=\"" + item.Value + "\">" + item.Name + "</option>");
                }
                rMsg.Status = 0;
                rMsg.Data = sb.ToString();

            }
            catch (Exception ex)
            {
                rMsg.Status = -1;
                rMsg.Message = ex.Message;
            }
            return Json(rMsg);
        }

        //根据所属“分店区域”获取“数据权限”
        //[HttpPost]
        //public ActionResult GetStoreData(FormCollection form)
        //{
        //    JsonSMsg rMsg = new JsonSMsg();
        //    try
        //    {
        //        var USER_TYPE = form["USER_TYPE"].ToString();
        //        var REGION_ID = Int32.Parse(form["REGION_ID"].ToString());
        //        var ret = _eservice.QueryStoreData(base.CurrentUserOrgId, USER_TYPE, REGION_ID);
        //        rMsg.Status = 0;
        //        rMsg.Data = ret;
        //    }
        //    catch (Exception ex)
        //    {
        //        rMsg.Status = -1;
        //        rMsg.Message = ex.Message;
        //    }
        //    return Json(rMsg);
        //}

        //根据所属“分店区域 ”获取“所属员工”的下拉框数据
        [HttpPost]
        public ActionResult GetEmployeeByType(FormCollection form)
        {
            JsonSMsg rMsg = new JsonSMsg();
            try
            {
                var USER_TYPE = form["USER_TYPE"];
                var STORE_ID = form["STORE_ID"];
                var EMPLOYEE_ID = form["EMPLOYEE_ID"];
                var ret = _eservice.GetEmployeeByType(USER_TYPE.ToString(), Int32.Parse(STORE_ID.ToString()), base.CurrentUserOrgId);
                StringBuilder sb = new StringBuilder();

                foreach (var item in ret)
                {
                    if (item.ID.ToString() == EMPLOYEE_ID.ToString())
                        sb.AppendLine("<option value=\"" + item.ID + "\" selected=\"selected\">" + item.NAME + "</option>");
                    else sb.AppendLine("<option value=\"" + item.ID + "\">" + item.NAME + "</option>");
                }
                rMsg.Status = 0;
                rMsg.Data = sb.ToString();

            }
            catch (Exception ex)
            {
                rMsg.Status = -1;
                rMsg.Message = ex.Message;
            }
            return Json(rMsg);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteUserInfo(int id)
        {
            JsonSMsg rMsg = new JsonSMsg();
            try
            {
                if (id <= 0)
                {
                    rMsg.Status = -1;
                    rMsg.Message = "参数错误！";
                    return Json(rMsg);
                }

                int ret = _eservice.DeleteUserInfo(id);
                if (ret > 0)
                {
                    rMsg.Status = 0;
                    rMsg.Data = ret;
                }
                else
                {
                    rMsg.Status = -1;
                    rMsg.Message = "意外错误，请稍后重试";
                }
            }
            catch (Exception ex)
            {
                rMsg.Status = -1;
                rMsg.Message = ex.Message;
            }
            return Json(rMsg);
        }

        #endregion

        #region 职位管理 maya
        ///<summary>
        ///Index
        /// </summary>
        //public ActionResult Post()
        //{
        //    return View(new ORG_POST() { ORG_ID = base.CurrentUserOrgId });
        //}
        ///// <summary>
        ///// 列表
        ///// </summary>
        ///// <param name="form"></param>
        ///// <returns></returns>
        //[HttpPost]
        //public JsonResult PostList(FormCollection form)
        //{
        //    int pt = 0;
        //    if (form["POST_TYPES"] != null)
        //        int.TryParse(form["POST_TYPES"].ToString(), out pt);
        //    var search = new PostSearch()
        //    {
        //        POST_TYPE = "",
        //        ORG_ID = base.CurrentUserOrgId
        //    };
        //    if (form["POST_TYPES"] != null)
        //        search.POST_TYPE = form["post_types"].ToString();
        //    PageView view = new PageView(form);
        //    string colkey = form["colkey"];
        //    string colsinfo = form["colsinfo"];

        //    PagedList<ORG_POST_EX> pList = _eservice.QueryPostList(search, view);
        //    JsonQTable fdata = JsonQTable.ConvertFromPagedList<ORG_POST_EX>(pList, colkey, colsinfo.Split(','));
        //    return Json(fdata);
        //}
        ///// <summary>
        ///// 修改
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //public ActionResult PostEdit(int? id)
        //{

        //    ORG_POST post = null;
        //    var jmsg = new JsonSMsg();
        //    try
        //    {
        //        if (id.HasValue)
        //        {
        //            post = _eservice.GetPost(id.Value);
        //        }
        //        else
        //        {
        //            post = new ORG_POST() { ORG_ID = base.CurrentUserOrgId };
        //        }

        //        jmsg.Status = 0;
        //        jmsg.Data = post;
        //    }
        //    catch (Exception ex)
        //    {
        //        jmsg.Status = -1;
        //        jmsg.Message = ex.Message;
        //    }
        //    return Json(jmsg);
        //}

        ///// <summary>
        ///// 保存
        ///// </summary>
        ///// <param name="id"></param>
        ///// <param name="post"></param>
        ///// <param name="collection"></param>
        ///// <returns></returns>
        //[HttpPost]
        //public JsonResult PostSave(ORG_POST post, FormCollection collection)
        //{
        //    JsonSMsg rMsg = new JsonSMsg();
        //    try
        //    {
        //        string errmsg = "";
        //        if (!ValidatePOS(post, out errmsg))
        //        {
        //            rMsg.Status = -1;
        //            rMsg.Message = errmsg;
        //            return Json(rMsg);
        //        }
        //        post.ORG_ID = base.CurrentUserOrgId;
        //        if (post.ID > 0)
        //            post.CREATE_USER = base.CurrentUserNO;
        //        post.LAST_MODI_USER = base.CurrentUserNO;
        //        int ret = _eservice.SavePost(post);
        //        if (ret > 0)
        //        {
        //            rMsg.Status = 0;
        //            rMsg.Data = ret;
        //        }
        //        else if (ret == -2)
        //        {
        //            rMsg.Status = -1;
        //            rMsg.Message = "级别编号重复，请重新填写。";
        //        }
        //        else
        //        {
        //            rMsg.Status = -1;
        //            rMsg.Message = "意外错误，请稍后重试";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        rMsg.Status = -1;
        //        rMsg.Message = ex.Message;
        //    }
        //    return Json(rMsg);
        //}

        ///// <summary>
        ///// 删除
        ///// </summary>
        ///// <param name="id"></param>
        ///// <param name="collection"></param>
        ///// <returns></returns>
        //[HttpPost]
        //public ActionResult PostDelete(int id)
        //{
        //    JsonSMsg rMsg = new JsonSMsg();
        //    try
        //    {
        //        if (id <= 0)
        //        {
        //            rMsg.Status = -1;
        //            rMsg.Message = "参数错误！";
        //            return Json(rMsg);
        //        }

        //        int ret = _eservice.DeletePost(id);
        //        if (ret > 0)
        //        {
        //            rMsg.Status = 0;
        //            rMsg.Data = ret;
        //        }
        //        else if (ret == -2)
        //        {
        //            rMsg.Status = -1;
        //            rMsg.Message = "此级别下有员工，不能删除。";
        //        }
        //        else
        //        {
        //            rMsg.Status = -1;
        //            rMsg.Message = "意外错误，请稍后重试";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        rMsg.Status = -1;
        //        rMsg.Message = ex.Message;
        //    }
        //    return Json(rMsg);
        //}

        ///// <summary>
        ///// 验证
        ///// </summary>
        ///// <param name="post"></param>
        ///// <param name="errmsg"></param>
        ///// <returns></returns>
        //private static bool ValidatePOS(ORG_POST post, out string errmsg)
        //{
        //    errmsg = "";
        //    if (post == null)
        //    {
        //        errmsg = "参数错误！";
        //        return false;
        //    }
        //    if (string.IsNullOrEmpty(post.POST_NAME))
        //    {
        //        errmsg = "职位名称不能为空！";
        //        return false;
        //    }
        //    return true;
        //}

        #endregion

        #region 佣金方案管理 maya

        public ActionResult Commission()
        {
            return View();
        }

        //[HttpPost]
        //public ActionResult CommissionList(FormCollection form)
        //{
        //    string fn = form["CNAME"];
        //    PageView view = new PageView(form);

        //    string colkey = form["colkey"];
        //    string colsinfo = form["colsinfo"];
        //    PagedList<COMMISSION_EX> pList = _eservice.QueryCOMMISSIONList(fn, view);
        //    JsonQTable fdata = JsonQTable.ConvertFromPagedList<COMMISSION_EX>(pList, colkey, colsinfo.Split(','));
        //    return Json(fdata);
        //}

        //public ActionResult EditCommission(int? id)
        //{
        //    try
        //    {
        //        if (id.HasValue)
        //        {
        //            COMMISSION_EX obj = _eservice.GetCOMMISSION_EX(id.Value);
        //            if (null != obj)
        //            {
        //                return View(obj);
        //            }
        //            else
        //            {
        //                return View(new COMMISSION_EX());
        //            }
        //        }
        //        else
        //        {
        //            return View(new COMMISSION_EX());
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return View(new COMMISSION_EX());
        //    }
        //}

        ///// <summary>
        ///// 保存方案详细
        ///// </summary>
        ///// <param name="CuCOMMISSION"></param>
        ///// <returns></returns>
        //[HttpPost]
        //public ActionResult SaveCommissionFaxx(COMMISSION_EX CuCOMMISSION, FormCollection form)
        //{
        //    JsonSMsg rMsg = new JsonSMsg();
        //    try
        //    {
        //        CuCOMMISSION.CMODE = Int32.Parse(form["VIWEPAGE"].ToString());
        //        CuCOMMISSION.ORG_ID = base.CurrentUserOrgId;
        //        CuCOMMISSION.CREATE_DATE = DateTime.Now;
        //        CuCOMMISSION.CREATE_USER = base.CurrentLoginUserId.ToString();
        //        string errmsg;
        //        DateTime od = DateTime.Now;
        //        if (!string.IsNullOrEmpty(CuCOMMISSION.BEGIN_DATE_Str))
        //        {
        //            DateTime.TryParse(CuCOMMISSION.BEGIN_DATE_Str, out od);
        //        }
        //        CuCOMMISSION.BEGIN_DATE = od;

        //        DateTime cd = DateTime.Now;
        //        if (!string.IsNullOrEmpty(CuCOMMISSION.END_DATE_Str))
        //        {
        //            DateTime.TryParse(CuCOMMISSION.END_DATE_Str, out cd);
        //            CuCOMMISSION.END_DATE = cd;
        //        }
        //        else CuCOMMISSION.END_DATE = DateTime.MaxValue;

        //        if (!ValidateCommission(CuCOMMISSION, out errmsg))
        //        {
        //            rMsg.Status = -1;
        //            rMsg.Message = errmsg;
        //            return Json(rMsg);
        //        }

        //        int ret = _eservice.SaveCOMMISSION(CuCOMMISSION);
        //        if (ret > 0)
        //        {
        //            rMsg.Status = 0;
        //            rMsg.Data = CuCOMMISSION.ID > 0 ? CuCOMMISSION.ID : ret;
        //        }
        //        else if (ret == -2)
        //        {
        //            rMsg.Status = -2;
        //            rMsg.Message = "佣金名称重复，请重新填写";
        //        }
        //        else
        //        {
        //            rMsg.Status = -1;
        //            rMsg.Message = "意外错误，请稍后重试";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        rMsg.Status = -1;
        //        rMsg.Message = ex.Message;
        //    }
        //    return Json(rMsg);
        //}

        //private static bool ValidateCommission(COMMISSION_EX CuCOMMISSION, out string errmsg)
        //{
        //    errmsg = "";
        //    if (CuCOMMISSION == null)
        //    {
        //        errmsg = "参数错误！";
        //        return false;
        //    }
        //    if (string.IsNullOrEmpty(CuCOMMISSION.NAME))
        //    {
        //        errmsg = "名称不能为空！";
        //        return false;
        //    }
        //    if (CuCOMMISSION.BEGIN_DATE.Year == 0001 || CuCOMMISSION.BEGIN_DATE.Year == 9999)
        //    {
        //        errmsg = "开始时间不对，请重新选择！";
        //        return false;
        //    }
        //    if (CuCOMMISSION.END_DATE.Year == 0001)
        //    {
        //        CuCOMMISSION.END_DATE = DateTime.MaxValue;
        //    }

        //    return true;
        //}

        //[HttpPost]
        //public ActionResult DeleteCommission(int id, FormCollection collection)
        //{

        //    JsonSMsg rMsg = new JsonSMsg();
        //    try
        //    {
        //        if (id <= 0)
        //        {
        //            rMsg.Status = -1;
        //            rMsg.Message = "参数错误！";
        //            return Json(rMsg);
        //        }

        //        int ret = _eservice.DeleteCOMMISSION(id);
        //        if (ret > 0)
        //        {
        //            rMsg.Status = 0;
        //            rMsg.Data = ret;
        //        }
        //        else
        //        {
        //            rMsg.Status = -1;
        //            rMsg.Message = "意外错误，请稍后重试";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        rMsg.Status = -1;
        //        rMsg.Message = ex.Message;
        //    }
        //    return Json(rMsg);
        //}

        #endregion

        #region 佣金方案详细 maya

        //private static int CheckState(COMMISSION_DETAIL_EX item)
        //{
        //    if (item.IsChecked)
        //    {
        //        return 1;
        //    }
        //    else
        //    {
        //        return 0;
        //    }
        //}

        //[HttpPost]
        //public ActionResult QueryCommissionDETAILList(FormCollection form)
        //{
        //    CommissionDetailSearch cds = new CommissionDetailSearch();
        //    cds.OrgId = base.CurrentUserOrgId;
        //    cds.SType = Int32.Parse(form["SType"].ToString());
        //    cds.CommissionId = Int32.Parse(form["CommissionId"].ToString());

        //    PageView view = new PageView(form);
        //    string colkey = form["colkey"];
        //    string colsinfo = form["colsinfo"];

        //    List<COMMISSION_DETAIL_EX> pList = _eservice.GetCOMMISSION_DETAILList(cds);

        //    JsonQTable fdata = JsonQTable.ConvertFromList<COMMISSION_DETAIL_EX>(pList, colkey, colsinfo.Split(','), CheckState);
        //    return Json(fdata);
        //}

        //[HttpPost]
        //public ActionResult SaveCommissionDETAIL(FormCollection form)
        //{
        //    JsonSMsg rMsg = new JsonSMsg();
        //    try
        //    {
        //        var formdata = form["formdata"];
        //        string cId = form["cId"].ToString();

        //        List<COMMISSION_DETAIL> cds = new List<COMMISSION_DETAIL>();
        //        if (!string.IsNullOrEmpty(formdata))
        //        {
        //            var rts = formdata.Split(',');

        //            foreach (var item in rts)
        //            {
        //                var vd = item.Split('_');
        //                var cuCd = new COMMISSION_DETAIL();

        //                cuCd.ID = int.Parse(vd[0]);
        //                cuCd.COMM_TYPE = Int32.Parse(vd[3]);
        //                if (cuCd.COMM_TYPE == 0)
        //                    continue;

        //                cuCd.PROD_ID = int.Parse(vd[1]);
        //                cuCd.COMMISSION_ID = Int32.Parse(cId);
        //                cuCd.PROD_TYPE = int.Parse(vd[2]);

        //                cuCd.CREATE_DATE = DateTime.Now;
        //                cuCd.CREATE_USER = base.CurrentUserNO;

        //                if (cuCd.COMM_TYPE != 1)
        //                {
        //                    cuCd.COMMISSION = decimal.Parse(vd[4].Replace("%", ""));
        //                    cuCd.COMM_DK = decimal.Parse(vd[5].Replace("%", ""));
        //                }
        //                else cuCd.COMMISSION = 0; cuCd.COMM_DK = 0;
        //                cuCd.PROD_NAME = vd[6];
        //                cds.Add(cuCd);
        //            }
        //        }
        //        if (cds.Count == 0)
        //        {
        //            rMsg.Status = -1;
        //            rMsg.Message = "无数据需要保存！";
        //        }
        //        else
        //        {
        //            int ret = _eservice.SaveCOMMISSION_DETAILs(cds);
        //            if (ret > 0)
        //            {
        //                rMsg.Status = 0;
        //                rMsg.Data = ret;
        //            }
        //            else
        //            {
        //                rMsg.Status = -1;
        //                rMsg.Message = "意外错误，请稍后重试";
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        rMsg.Status = -1;
        //        rMsg.Message = ex.Message;
        //    }
        //    return Json(rMsg);
        //}

        //[HttpPost]
        //public ActionResult PostAllSet(ORG_POST post, FormCollection form)
        //{
        //    JsonSMsg rMsg = new JsonSMsg();
        //    try
        //    {
        //        var PROD_TYPE = Int32.Parse(form["type"].ToString());
        //        var id = Int32.Parse(form["cId"].ToString());
        //        var COMM_TYPE = Int32.Parse(form["tcType"].ToString());
        //        var COMMISSION = decimal.Parse(form["COMMISSION"].ToString().Replace("%", ""));
        //        var COMM_DK = decimal.Parse(form["COMM_DK"].ToString().Replace("%", ""));
        //        var formdata = form["formdata"].ToString();

        //        List<COMMISSION_DETAIL> cds = new List<COMMISSION_DETAIL>();
        //        if (COMM_TYPE == 0)
        //        {
        //            var cuCd = new COMMISSION_DETAIL()
        //            {
        //                COMM_DK = COMM_DK,
        //                COMM_TYPE = COMM_TYPE,
        //                COMMISSION = COMMISSION,
        //                COMMISSION_ID = id,
        //                PROD_NAME = "",
        //                PROD_TYPE = PROD_TYPE
        //            };
        //            cds.Add(cuCd);
        //        }
        //        else
        //        {
        //            if (!string.IsNullOrEmpty(formdata))
        //            {
        //                var rts = formdata.Split(',');

        //                foreach (var item in rts)
        //                {
        //                    var vd = item.Split('_');
        //                    var cuCd = new COMMISSION_DETAIL();

        //                    cuCd.ID = 0;
        //                    cuCd.COMM_TYPE = COMM_TYPE;

        //                    cuCd.PROD_ID = int.Parse(vd[1]);
        //                    cuCd.COMMISSION_ID = id;
        //                    cuCd.PROD_TYPE = PROD_TYPE;

        //                    cuCd.CREATE_DATE = DateTime.Now;
        //                    cuCd.CREATE_USER = base.CurrentUserNO;

        //                    if (cuCd.COMM_TYPE != 1)
        //                    {
        //                        cuCd.COMMISSION = COMMISSION;
        //                        cuCd.COMM_DK = COMM_DK;
        //                    }
        //                    else cuCd.COMMISSION = 0; cuCd.COMM_DK = 0;
        //                    cuCd.PROD_NAME = vd[6];
        //                    cds.Add(cuCd);
        //                }
        //            }
        //        }

        //        if (cds.Count == 0)
        //        {
        //            rMsg.Status = -1;
        //            rMsg.Message = "无数据需要保存！";
        //        }
        //        else
        //        {
        //            int ret = _eservice.SaveSomeCOMMISSION_DETAILs(cds);
        //            if (ret > 0)
        //            {
        //                rMsg.Status = 0;
        //                rMsg.Data = ret;
        //            }
        //            else
        //            {
        //                rMsg.Status = -1;
        //                rMsg.Message = "意外错误，请稍后重试";
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        rMsg.Status = -1;
        //        rMsg.Message = ex.Message;
        //    }
        //    return Json(rMsg);
        //}

        #endregion

        #region 业绩佣金方案管理
        //修改
        //public ActionResult EditPFMCCommission(int? id)
        //{
        //    try
        //    {
        //        if (id.HasValue)
        //        {
        //            COMMISSION_EX obj = _eservice.GetCOMMISSION_EX(id.Value);
        //            if (null != obj)
        //            {
        //                return View(obj);
        //            }
        //            else
        //            {
        //                return View(new COMMISSION_EX());
        //            }
        //        }
        //        else
        //        {
        //            return View(new COMMISSION_EX());
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return View(new COMMISSION_EX());
        //    }
        //}

        //获取添加时的业绩汇总
        //[HttpPost]
        //public ActionResult GetCategorys(FormCollection form)
        //{
        //    JsonSMsg rMsg = new JsonSMsg();
        //    try
        //    {
        //        var code = form["code"];
        //        var proType = form["proType"];
        //        var id = form["id"];

        //        var ret = _eservice.GetCategorys(code.ToString(), Int32.Parse(proType.ToString()), Int32.Parse(id.ToString()));

        //        rMsg.Status = 0;
        //        rMsg.Data = ret;

        //    }
        //    catch (Exception ex)
        //    {
        //        rMsg.Status = -1;
        //        rMsg.Message = ex.Message;
        //    }
        //    return Json(rMsg);
        //}

        //获取添加时的业绩项
        //[HttpPost]
        //public ActionResult GetAddCategorys(FormCollection form)
        //{
        //    JsonSMsg rMsg = new JsonSMsg();
        //    try
        //    {
        //        var id = form["id"];
        //        var ret = _eservice.GetAddCategorys(Int32.Parse(id));
        //        StringBuilder sb = new StringBuilder();
        //        var pItems = ret.Where(p => p.PARENT_ID == Guid.Empty).ToList();
        //        foreach (var item in pItems)
        //        {

        //            sb.AppendLine("<optgroup label=\"" + item.CATE_NAME + "\">");
        //            var pChildIs = ret.Where(p => p.PARENT_ID == item.ID);
        //            foreach (var cItem in pChildIs)
        //            {
        //                //var pChildIs1 = ret.Where(p => p.PARENT_ID == cItem.ID);
        //                //if (pChildIs1 != null && pChildIs1.Count() > 0)
        //                //{
        //                //    sb.AppendLine("<optgroup label=\"" + cItem.CATE_NAME + "\">");
        //                //    foreach (var cItem1 in pChildIs1)
        //                //    {
        //                //        sb.AppendLine("<option value=\"" + cItem.CATE_NO + "_" + cItem1.CATE_NO + "\">" + cItem1.CATE_NAME + "</option>");
        //                //    }

        //                //    sb.AppendLine("</optgroup>");
        //                //}
        //                //else
        //                sb.AppendLine("<option value=\"" + item.CATE_NO + "_" + cItem.CATE_NO + "\">" + cItem.CATE_NAME + "</option>");
        //            }
        //            sb.AppendLine("</optgroup>");

        //        }
        //        rMsg.Status = 0;
        //        rMsg.Data = sb.ToString();

        //    }
        //    catch (Exception ex)
        //    {
        //        rMsg.Status = -1;
        //        rMsg.Message = ex.Message;
        //    }
        //    return Json(rMsg);
        //}

        //保存业绩汇总
        [HttpPost]
        public JsonResult SavePFMCCommission(FormCollection form)
        {
            JsonSMsg rMsg = new JsonSMsg();
            try
            {
                var values = form["values"];
                var id = form["id"];
                var types = form["types"];
                var groupId = form["groupId"];
                if (!ValidatePFMCCommission(values.ToString()))
                {
                    rMsg.Status = -2;
                    rMsg.Message = "数据填写有误，请填写正确。";
                    return Json(rMsg);
                }

                int ret = _eservice.SavePFMCCommission(values.ToString(), types, Int32.Parse(id), base.CurrentUserNO, Int32.Parse(groupId.ToString()) + 1);
                if (ret > 0)
                {
                    rMsg.Status = 0;
                    rMsg.Data = ret;
                }
                else
                {
                    rMsg.Status = -1;
                    rMsg.Message = "意外错误，请稍后重试";
                }
            }
            catch (Exception ex)
            {
                rMsg.Status = -1;
                rMsg.Message = ex.Message;
            }
            return Json(rMsg);
        }

        //保存验证
        bool ValidatePFMCCommission(string values)
        {
            bool isSuccess = true;
            int sV = 0;
            int eV = 0;
            float pv = 0;
            var cuos = values.Split(',');
            if (cuos.Count() < 1)
                isSuccess = false;
            else
            {
                foreach (var item in cuos)
                {
                    var cuVs = item.Split('_');
                    if (!Int32.TryParse(cuVs[0], out sV) || !Int32.TryParse(cuVs[1], out eV) || !float.TryParse(cuVs[1], out pv))
                    {
                        isSuccess = false;
                        break;
                    }
                }
            }
            return isSuccess;
        }

        //查询业绩提成详细
        //[HttpPost]
        //public ActionResult GetCPD(FormCollection form)
        //{
        //    JsonSMsg rMsg = new JsonSMsg();
        //    try
        //    {
        //        var id = form["id"];
        //        var ret = _eservice.GetCPD(Int32.Parse(id.ToString()));
        //        rMsg.Status = 0;
        //        rMsg.Data = ret;
        //    }
        //    catch (Exception ex)
        //    {
        //        rMsg.Status = -1;
        //        rMsg.Message = ex.Message;
        //    }
        //    return Json(rMsg);
        //}

        ////删除
        //[HttpPost]
        //public ActionResult DeleteEMPCommission(int groupId, FormCollection collection)
        //{
        //    JsonSMsg rMsg = new JsonSMsg();
        //    try
        //    {
        //        if (groupId <= 0)
        //        {
        //            rMsg.Status = -1;
        //            rMsg.Message = "参数错误！";
        //            return Json(rMsg);
        //        }

        //        int ret = _eservice.DeleteEMPCOMMISSION(groupId);
        //        if (ret > 0)
        //        {
        //            rMsg.Status = 0;
        //            rMsg.Data = ret;
        //        }
        //        else
        //        {
        //            rMsg.Status = -1;
        //            rMsg.Message = "意外错误，请稍后重试";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        rMsg.Status = -1;
        //        rMsg.Message = ex.Message;
        //    }
        //    return Json(rMsg);
        //}

        #endregion

        #region 佣金方案计算
        //佣金方案页面
        public ActionResult CountCommission()
        {
            return View();
        }

        //[HttpPost]
        //public ActionResult CountCommission(FormCollection form)
        //{
        //    JsonSMsg rMsg = new JsonSMsg();
        //    try
        //    {
        //        rMsg.Status = 0;
        //        rMsg.Data = 1;

        //    }
        //    catch (Exception ex)
        //    {
        //        rMsg.Status = -1;
        //        rMsg.Message = ex.Message;
        //    }
        //    return Json(rMsg);
        //}

        //[HttpPost]
        //public ActionResult CountCommissionList(string ymonth, int? storeid, FormCollection form)
        //{
        //    string fn = form["CNAME"];
        //    PageView view = new PageView(form);

        //    string colkey = form["colkey"];
        //    string colsinfo = form["colsinfo"];

        //    PagedList<EMPLOYEE_COMMISSION_EX> pList = _eservice.QueryCountCOMMISSIONList(ymonth, storeid, view);
        //    JsonQTable fdata = JsonQTable.ConvertFromPagedList<EMPLOYEE_COMMISSION_EX>(pList, colkey, colsinfo.Split(','));
        //    return Json(fdata);
        //}
        [HttpPost]
        public JsonResult queryCommission(string ymonth,int? storeid)
        {
            JsonSMsg rMsg = new JsonSMsg();
            if (string.IsNullOrEmpty(ymonth) || !storeid.HasValue)
            {
                rMsg.Status = -1;
                rMsg.Message = "参数错误！";
                return Json(rMsg);
            }
            List<COUNT_COMMISSION_EX> commssion = _eservice.queryCommission(ymonth, storeid);
            rMsg.Status = commssion[0].STATUS;
            rMsg.Message = commssion[0].MESSAGE;
            return Json(rMsg);
        }
        #endregion
    }
}
