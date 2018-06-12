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
    public interface IStoreService
    {
        PagedList<ORG_STORE_EX> QueryAllStoreDate(StoreSearch search, PageView view);
     
        ORG_STORE_EX QueryStoreById(int id);

        ORG_STORE_EX QueryStoreById_Ex(int id);
        int SaveStore(ORG_STORE store);
        int DeleteStore(int id);

        List<ORG_STORE_EX> QueryStoreByOrgId(int orgId);
        List<ORG_STORE_EX> QueryStoreByOrgIdInApp(int orgId);//在app中显示门店列表
        List<ORG_DICT_EX> GetServicetype(string servicetype, int org_id);

        PagedList<ORG_STORE_EX> QueryAllStoreDateEx(StoreSearch search, PageView view);

        List<ORG_INFO_EX> GetORG_INFO_BY_ID(int ORG_ID);

        //获取服务类型
        List<ORG_DICT> GetServicetype(string servicetype);

        WXStore GetStore(int id);

        int UpdateStore(WXStore s);

        List<WXStore> GetStores(int merchants_ID);

        int AddStore(WXStore s);

        /// <summary>
        /// 获取公司下的所有未关闭的门店
        /// </summary>
        /// <param name="orgId">公司Id</param>
        /// <remarks>create by likui.liu</remarks>
        /// <returns>门店列表集合</returns>
        List<ORG_STORE_EX> GetStoreListByOrgId(string openid);

        /// <summary>
        /// 根据时间查询门店排班
        /// </summary>
        /// <param name="StoreID"></param>
        /// <param name="Date"></param>
        /// <returns></returns>
        List<EMPLOYEE_SCHEDULE> GetScheduleList(int StoreID, string Date, int jssex);

        /// <summary>
        /// 查询门店指定预约情况
        /// </summary>
        /// <param name="StoreID"></param>
        /// <param name="Date"></param>
        /// <returns></returns>
        List<CUST_BOOKING> GetBookDateList(int StoreID, string Date);

        /// <summary>
        /// 获取门店评价数
        /// </summary>
        /// <param name="storeid"></param>
        /// <returns></returns>
        int GetStorePJCount(int storeid);
    }
}
