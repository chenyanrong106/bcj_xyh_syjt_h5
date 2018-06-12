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
   public interface IOrderMemCardService
    {
        /// <summary>
        /// 列表 会员卡订单
        /// </summary>
        /// <param name="search"></param>
        /// <param name="view"></param>
        /// <returns></returns>
       PagedList<ORDER_DETAIL_EX> QueryOrderMemCardList(OrderMemCardSearch search, PageView view);
    }
}
