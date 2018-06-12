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
    public interface IFollowService
    {
        PagedList<CUST_FOLLOW_EX> QueryOrderList(OrderSearch search, PageView view);

        CUST_FOLLOW_EX GetFollowByOID(int? id);

        PagedList<ORDER_DETAIL> QueryOrderInfo(OrderSearch search, PageView view);
    }
}
