using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SPACRM.Entity;
using SPACRM.Entity.PageSearch;
using SPACRM.Entity.SearchEntity;
using SPACRM.Entity.Entities;

namespace SPACRM.Interface
{
    public interface ICardService
    {

        PagedList<PROD_CARD> QueryCardList(CardSearch search, PageView view);


        List<PrdouctCardItem> QueryCardServiceList(CardItemSearch search);
        List<PrdouctCardItem> QueryCardProductList(CardItemSearch search);

        List<ORG_STORE_EX> QueryAllStoreCard(CardSearch search, PageView view, decimal pprice);
        List<ORG_STORE_EX> QueryAllStoreCard(CardSearch search, PageView view);
        void SaveStoreList(int id, int type, List<REL_STORE_PROD> slist);

        ProductCard GetCard(int? id, int orgId);
        PROD_CARD GetCardById(int id);
        
        int SaveCard(PROD_CARD card);
        int NewSaveCard(PROD_CARD card);

        int DeleteCard(int id);

        void SaveServiceList(int id, int type, List<REL_CARD_GS> slist);

        #region 会员卡级变更
        /// <summary>
        /// 查询会员卡级变更
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        PagedList<CUST_CARD_CHANGE_EX> QueryCUST_CARD_CHANGEList(PageView view, string StrWhere);

        /// <summary>
        /// 查询会员卡级变更
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        CUST_CARD_CHANGE_EX QueryCUST_CARD_CHANGE(int id);

        /// <summary>
        /// 会员卡级变更
        /// </summary>
        /// <param name="Group_ID"></param>
        /// <returns></returns>
        int DeleteCUST_CARD_CHANGE(int ID);

        CUST_CARD_CHANGE_EX GetCustExpandLevelOrder(int? cid, int? orderid);

        /// <summary>
        /// 根据客户id查询会员卡级
        /// </summary>
        /// <param name="Cust_ID"></param>
        /// <returns></returns>
        List<CUST_CARD_EX> GetChangeCardLevelByCust_ID(int Cust_ID);

        /// <summary>
        /// 查询在购买卡级时是否有赠送过项目
        /// </summary>
        /// <param name="cust_id"></param>
        /// <param name="Cust_Card_Id"></param>
        /// <returns></returns>
        bool CustCardHasSendProc(int cust_id, int Cust_Card_Id);

         /// <summary>
        /// 
        /// </summary>
        /// <param name="card"></param>
        /// <param name="order"></param>
        /// <param name="dataArray">0 优惠金额 1销售员 2卡级名 3支付方式ID 4支付金额 5公司卡类型</param>
        /// <returns></returns>
        int SaveBuyCard(CUST_CARD_CHANGE cuObj, CUST_CARD card, ORDER_HEAD order, string[] dataArray, List<CUST_CDETAIL> cdList);

        #endregion

        /// <summary>
        /// 会员转账
        /// </summary>
        /// <param name="Cust_ID"></param>
        /// <returns></returns>
        List<CUST_CARD_EX> GetChangeCardLevelByCard_NO(string Card_NO);

        CUST_CARD_EX GetCard(int CUST_CARD_ID);
        CUST_CARD_EX GetCardInfoByID(int CUST_CARD_ID);
        /// <summary>
        /// 修改顾客卡的过期时间
        /// </summary>
        /// <param name="info">卡信息</param>
        /// <remarks>create by likui.liu</remarks>
        /// <returns></returns>
        int UpdateEndDateByCustCardID(CUST_CARD info);
        CUST_CARD GetCustCardByID(int custCardID);

        int SaveCustCard(CUST_CARD card);

        List<PROD_CARD> GetProdVoucherInStore(int storeID, int orgID);
    }
}
