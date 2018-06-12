using SPACRM.Entity;
using SPACRM.Entity.Entities;
using SPACRM.Entity.PageSearch;
using SPACRM.Entity.SearchEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SPACRM.Interface
{
    /// <summary>
    /// 产品服务接口
    /// by Levin
    /// 2014-05-13
    /// </summary>
    public interface IGoodsService
    {
        PagedList<PROD_GOODS> QueryGoodsList(GoodsSearch search, PageView view, int org_id);

        int SaveGoods(PROD_GOODS goods);

        PROD_GOODS_EX GetGoods(int id,int org_id);
        List<PROD_GOODS_EX> QueryGiftList();
        int DeleteGoods(int id);

       //常用项目 销量前十
        List<PROD_GOODS_EX> QueryGodSerCard(string qText, string type, string storeids);

        ///根据产品ID检索产品详细信息
        ///根据客户ID检索出该会员享受的优惠信息
        /// <param name="proid"></param>
        /// <param name="custid"></param>
        /// <param name="protable"></param>
        /// <returns></returns>
        PROD_GOODS_EX QueryProDetail(string proid, string custid, string protable);

          List<PROD_CATEGORY> GetScateByPid(string orgId, string ptype, string pid);
          PROD_GOODS_EX GetGoods(int id);
          List<PROD_GOODS_EX> QueryChangeList(int cust_id);
    }
}
