using SPACRM.Entity;
using SPACRM.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SPACRM.Interface
{
    public interface IProductService
    {
        /// <summary>
        /// 获取有服务费项目的项目ID
        /// </summary>
        /// <param name="orgID"></param>
        /// <returns></returns>
        PROD_SERVICE GetProdServiceFeeByOrg(int orgID);

        /// <summary>
        /// 判断门店是否有服务费
        /// </summary>
        /// <param name="orgID"></param>
        /// <param name="storeID"></param>
        /// <returns></returns>
        bool HasServiceFee(int orgID, int storeID);

        /// <summary>
        /// 排除服务费的项目
        /// </summary>
        /// <param name="orgID"></param>
        /// <param name="source"></param>
        /// <remarks>create by likui.liu</remarks>
        /// <returns></returns>
        List<PROD_SERVICE> GeProdServiceExceptServiceFee(int orgID, List<PROD_SERVICE> source);

        /// <summary>
        /// 获取可以用积分兑换的产品列表
        /// </summary>
        /// <param name="orgID"></param>
        /// <remarks>create by likui.liu</remarks>
        /// <returns></returns>
        List<PROD_GOODS_EX> GetGoodsForIsGift(int orgID);
    }
}
