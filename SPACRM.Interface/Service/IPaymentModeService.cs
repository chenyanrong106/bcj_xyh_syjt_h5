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
    public interface IPaymentModeService
    {

        PagedList<PAYMENT_MODE_EX> QueryPostList(PaymentModeSearch sObj, PageView view, int ORG_ID);
        int SavePost(PAYMENT_MODE post);
        PAYMENT_MODE GetPost(int id);
        int DeletePost(int id);

        

        /// <summary>
        /// 根据公司ID 获取支付方式
        /// </summary>
        /// <param name="orgID">公司ID</param>
        /// <remarks>create by likui.liu</remarks>
        /// <returns></returns>
        List<PAYMENT_MODE_EX> GetPayModeByOrgID(int orgID);

        /// <summary>
        /// 获取会员的会员卡级列表
        /// </summary>
        /// <param name="custid"></param>
        /// <returns></returns>
        List<CustPayMode_EX> GetCustPayMode(int custid);

        /// <summary>
        /// 获取会员的优惠券列表
        /// </summary>
        /// <param name="custid"></param>
        /// <returns></returns>
        List<CustPayMode_EX> GetCustPayModeV(int custid);


        /// <summary>
        /// 获取用于退卡的支付方式
        /// </summary>
        /// <param name="orgID">公司ID</param>
        /// <returns></returns>
        List<PAYMENT_MODE> GetPayModeForBackCard(int orgID);
    }
}
