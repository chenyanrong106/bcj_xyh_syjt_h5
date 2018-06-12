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
    public interface IPromotionService
    {
        /// <summary>
        /// 二维码列表
        /// </summary>
        /// <param name="view"></param>
        /// <param name="CName"></param>
        /// <returns></returns>
        PagedList<ORG_WX_QRCode_EX> GetQrcodeList(PageView view);

        /// <summary>
        /// 保存二维码
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        int SaveQrcode(ORG_WX_QRCode code);

        /// <summary>
        /// 获取二维码
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ORG_WX_QRCode GetQrcode(int id);

        /// <summary>
        /// 获取微信优惠券列表
        /// </summary>
        /// <param name="search"></param>
        /// <param name="view"></param>
        /// <param name="ORG_ID"></param>
        /// <returns></returns>
        PagedList<ORG_WX_Discount> QueryDiscountList(PROMOTION_INFOSearch search, PageView view);

        /// <summary>
        /// 获取微信优惠券详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ORG_WX_Discount GetDiscount(int id);

        /// <summary>
        /// 保存微信优惠券
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        int SaveDiscount(ORG_WX_Discount d);

        PagedList<PROMOTION_INFO_EX> QueryPROMOTION_INFOList(int type, PROMOTION_INFOSearch search, PageView view, int ORG_ID);

        PagedList<ORG_Crowd_Info> QueryCrowdList(PROMOTION_INFOSearch search, PageView view);

        ORG_Crowd_Info GetCrowd(int id);

        int SaveCrowd(ORG_Crowd_Info crowd);

        PROMOTION_INFO_EX GetPROMOTION_INFO_EX(int id);

        int SavePROMOTION_INFO(PROMOTION_INFO_EX cuObj);

        //List<PROD_GOODS> GetPROD_GOODSByOrgIdAndType(int orgId, int type);
        List<REL_PROMOTION_PROD_EX> GetPRODSByCondition(int orgId, int prodtype, int objtype);

        List<REL_PROMOTION_PROD_EX> GetRPPByPrId(int promotionId);

        int DeletePROMOTION_INFO(int promotionId);

        /// <summary>
        /// 根据客户分组获得组内成员数量
        /// </summary>
        /// <param name="groupId">分组编号</param>
        /// <returns></returns>
        int GetCustomerCountByGroupId(int groupId);

        PagedList<PROMOTION_VOUCHER_EX> QueryPROMOTION_VOUCHERList(PromotionVSearch search, PageView view);

        /// <summary>
        /// 获得公司的所有门店，门店编号_门店名称
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        string GetOrgStores(int orgId);

        int SavePROMOTION_VOUCHER(PROMOTION_VOUCHER_EX cuObj);

        /// <summary>
        /// 发放和作废代金券
        /// </summary>
        /// <param name="cuObj"></param>
        /// <returns></returns>
        int UpdateVoucherManager(PROMOTION_VOUCHER_EX cuObj);

        List<CUST_INFO> QueryCustomer(string qText, int orgId);

        /// <summary>
        /// 获得代金券
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        PROMOTION_VOUCHER GetPROMOTION_VOUCHER(string VOUCHER_NO);

        /// <summary>
        /// 根据面值获得数量
        /// </summary>
        /// <param name="faceValue"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        int GetVOUCHERCountByV(decimal faceValue, int orgId);

        /// <summary>
        /// 发放代金券
        /// </summary>
        /// <param name="cuObj"></param>
        /// <returns></returns>
        int GrantPROMOTION_INFO(PROMOTION_INFO_EX cuObj,int STORE_ID);

        List<ORG_INFO_EX> QueryORG_REGIONTree(int ORG_ID, string cuUserType, int cuUserStoreId);

         /// <summary>
        /// 获取所有顾客数量 added by cyr
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        int GetAllCustomerCount(int ORG_ID);

        PROMOTION_VOUCHER_EX GetPROMOTION_VOUCHER_EX(string VOUCHER_NO);

        /// <summary>
        /// 兑换代金券时，指定顾客
        /// </summary>
        /// <param name="info"></param>
        /// <remarks>create by likui.liu</remarks>
        /// <returns></returns>
        int UpdateCustIDByNO(PROMOTION_VOUCHER info, int custCardID, decimal balance,out string errMsg);

        /// <summary>
        /// 批量删除未使用的代金券
        /// </summary>
        /// <param name="orgID"></param>
        /// <param name="ids"></param>
        /// <returns></returns>
        int DeleteVoucherBatchByIDs(int orgID, string ids);
    }
}
