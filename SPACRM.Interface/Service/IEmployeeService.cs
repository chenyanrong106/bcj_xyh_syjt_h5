using SPACRM.Entity;
using SPACRM.Entity.PageSearch;
using SPACRM.Entity.SearchEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SPACRM.Entity.Entities;
using SPACRM.Interface.Service;

namespace SPACRM.Interface
{
    public interface IEmployeeService
    {
        //员工管理接口
        PagedList<ORG_EMPLOYEE_EX> QueryEmployeeList(EmployeeSearch search, PageView view);
        int SaveEmployee(ORG_EMPLOYEE employee);
        ORG_EMPLOYEE GetEmployee(int id);
        ORG_EMPLOYEE_EX GetEmployeeEX(int id);
        int DeleteEmployee(int id);
        int SaveEmployeeXcyfl(ORG_EMPLOYEE employee);
        string GetEditPost(int orgId, int? port_id);

        #region 员工账户
        /// <summary>
        /// 查询权限内组织机构菜单
        /// </summary>
        /// <param name="cuUserOrgId"></param>
        /// <param name="cuUserType"></param>
        /// <param name="cuUserStoreId"></param>
        /// <returns></returns>
        List<ORG_INFO_EX> QueryORG_REGIONTree(int cuUserOrgId, string cuUserType, int cuUserStoreId);

        /// <summary>
        /// 查询登陆账户
        /// </summary>
        /// <param name="search"></param>
        /// <param name="view"></param>
        /// <returns></returns>
        PagedList<USER_INFO_EX> QueryUSER_INFO_EXList(UserInfoSearch search, PageView view);
        USER_INFO_EX GetUSER_INFO_EX(int id);

        /// <summary>
        /// 获取用户选择的区域
        /// </summary>
        /// <param name="userType">查询的用户类别</param>
        /// <param name="ORG_ID">当前的公司</param>
        /// <param name="cuUserREGION_ID">当前用户的所属区域</param>
        /// <param name="cuUserType">当前用户的类别</param>
        /// <returns></returns>
        List<IResource> GetUserREGION(string userType, int ORG_ID, int cuUserREGION_ID, string cuUserType);
        /// <summary>
        /// 添加登陆信息
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        int SaveEmployeeDlzh(USER_INFO_EX CUUSER_INFO);
        /// <summary>
        /// 删除登陆账户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int DeleteUserInfo(int id);
        List<ORG_EMPLOYEE_EX> GetEmployeeByType(string userType, int storeId, int orgId);
        /// <summary>
        /// 获取数据权限
        /// </summary> 
        /// <returns></returns>
        List<ORG_STORE_EX> QueryStoreData(int orgId, string userType, int REGION_ID);
        #endregion

        //佣金管理       
        PagedList<COMMISSION_EX> QueryCOMMISSIONList(CommissionSearch search, PageView view, int orgid);
        COMMISSION_EX GetCOMMISSION_EX(int id);
        int DeleteCOMMISSION(int id);
        int SaveCOMMISSION(COMMISSION obj);

        //佣金详细：服务项目
        List<COMMISSION_DETAIL_EX> GetCOMMISSION_DETAILList(CommissionDetailSearch cds);
        int SaveCOMMISSION_DETAILs(List<COMMISSION_DETAIL> objs);
        int SaveSomeCOMMISSION_DETAILs(List<COMMISSION_DETAIL> objs);

        //职位管理接口
        PagedList<ORG_POST_EX> QueryPostList(PostSearch sObj, PageView view);
        int SavePost(ORG_POST post);
        ORG_POST GetPost(int id);
        int DeletePost(int id);

        #region 业绩佣金方案

        /// <summary>
        /// 获取添加时的业绩汇总
        /// </summary>
        /// <param name="code"></param>
        /// <param name="PROD_TYPE"></param>
        /// <param name="commisionId"></param>
        /// <returns></returns>
        List<PROD_CATEGORY_EX> GetCategorys(string code, int PROD_TYPE, int commisionId);
        List<PROD_CATEGORY_EX> GetAddCategorys(int commissionId);

        /// <summary>
        /// 保存业绩汇总
        /// </summary>
        /// <param name="values"></param>
        /// <param name="pTypes"></param>
        /// <param name="commisionId"></param>
        /// <returns></returns>
        int SavePFMCCommission(string values, string pTypes, int commisionId, string creatUser, int groupId);

        /// <summary>
        /// 查询业绩提成详细
        /// </summary>
        /// <param name="commisionId"></param>
        /// <returns></returns>
        List<COMMISSION_PERFORMANCE_DETAIL_EX> GetCPD(int commisionId);
        int DeleteEMPCOMMISSION(int groupId);

        #endregion

        //佣金计算 获取存储过程
        PagedList<EMPLOYEE_COMMISSION_EX> QueryCountCOMMISSIONList(string month, int? storeID, PageView view);
        
        //调用存储过程 佣金计算
        List<COUNT_COMMISSION_EX> queryCommission(string ymonth, int? storeid);
        //获取未发布的佣金列表
        List<EMPLOYEE_COMMISSION_EX> QueryValidCommission(PageView view,int valid,int orgid);
        int UpdateValid(int store_id,string valid);
        //判断佣金方案有没有被使用
        int COMMISSIONWhetherToBeUsed(int ORG_ID, int ID);
       
        

        /// <summary>
        /// 获取显示预约的员工列表
        /// </summary>
        /// <param name="storeID">门店ID</param>
        /// <param name="orgID">公司ID</param>
        /// <remarks>create by likui.liu</remarks>
        /// <returns></returns>
        List<ORG_EMPLOYEE> GetCanBookingEmp(int storeID, int orgID);
        List<PLAT_VERSION> GetVersionById(int platform);
    }
}
