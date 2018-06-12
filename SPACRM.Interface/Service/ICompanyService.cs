using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SPACRM.Entity;
using SPACRM.Entity.Entities;
using SPACRM.Entity.PageSearch;
using SPACRM.Entity.SearchEntity;

namespace SPACRM.Interface
{
    public interface ICompanyService
    {
        #region 公司维护

        /// <summary>
        /// Queries the company list.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="view">The view.</param>
        /// <returns></returns>
        PagedList<ORG_INFO_EX> QueryCompanyList(CompanySearch search, PageView view, int id);

        /// <summary>
        /// 获取修改公司信息
        /// </summary>
        /// <param name="p">The p.</param>
        /// <returns></returns>
        ORG_INFO_EX GetCompanyById(int id);
        ORG_INFO GetCompany(int id);
        /// <summary>
        /// 保存公司信息
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        int SaveCompanyInfo(ORG_INFO_EX company);

        /// <summary>
        /// 根据公司编号得到公司管理员角色的功能权限
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        List<SYS_RIGHT_EX> GetRIGHTSByOrgId(int orgId);
        #endregion

        #region 块区维护
        ORG_INFO_EX GetORG_REGIONById(int id);
        int DeleteORG_REGION(int id);
        int SaveORG_REGION(ORG_INFO_EX company);
        List<ORG_INFO_EX> QueryORG_REGIONTree(int ORG_ID, string cuUserType, int cuUserStoreId);
        //List<ORG_INFO_EX> QueryORG_REGIONTree(int ORG_ID);
        List<ORG_INFO_EX> QueryORG_REGIONList(ORG_REGIONSearch search);
        #endregion

        #region 运营后台公司管理
        PagedList<REGISTER_COMPANY_EX> QueryRegisterCompanyList(CompanySearch search, PageView view);

        REGISTER_COMPANY_EX GetRegisterCompanyById(int id);

        List<REGISTER_STORE> GetRegisterStoreByREGISTER_COMPANY_ID(int REGISTER_COMPANY_ID);

        int SaveRegisterCompanyInfo(REGISTER_COMPANY_EX cuObj);

        int CheckRegisterCompanyInfo(REGISTER_COMPANY_EX cuObj, int ID);

        List<REGISTER_STORE> AddRegisterStore(REGISTER_STORE REGISTER_STORE);

        int CheckCompanyCodeHasExists(int id, string code);

        List<REGISTER_STORE> RemoveRegisterStore(int id, int REGISTER_COMPANY_ID);

        List<ORG_INFO> GetAllORG_INFO();

        List<ORG_INFO_EX> QueryORG_REGIONTree();

        /// <summary>
        /// 初始化公司数据
        /// </summary>
        /// <param name="org_id"></param>
        /// <returns></returns>
        int Init_Company_Data(int org_id);

        ORG_INFO_EX GetOrgInfo(int orgID);
        #endregion
    }
}
