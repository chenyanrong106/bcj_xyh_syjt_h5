using SPACRM.Common;
using SPACRM.DataAccess;
using SPACRM.DataAccess.Repository;
using SPACRM.Entity;
using SPACRM.Entity.Entities;
using SPACRM.Entity.PageSearch;
using SPACRM.Interface;
using StructureMap.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace SPACRM.Business.ServiceImpl
{
    public class OrderService : BaseService, IOrderService
    {
        OrderRepository _repo;
        public OrderService()
        {
            _repo = new OrderRepository();
        }

        /// <summary>
        /// 保存老带新纪录
        /// </summary>
        /// <param name="old"></param>
        /// <returns></returns>
        public int SaveCustOldNew(Cust_Old_New old)
        {
            return _repo.SaveCustOldNew(old);
        }
        
    }
}
