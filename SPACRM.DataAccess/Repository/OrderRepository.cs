using SPACRM.Entity;
using SPACRM.Entity.PageSearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SPACRM.Common;

namespace SPACRM.DataAccess.Repository
{
    public class OrderRepository : BaseRepository
    {

        /// <summary>
        /// 保存老带新纪录
        /// </summary>
        /// <param name="old"></param>
        /// <returns></returns>
        public int SaveCustOldNew(Cust_Old_New old)
        {
            if (old.ID == 0)
                return (int)Insert(old);
            else
                return Update(old);
        }
        
    }
}
