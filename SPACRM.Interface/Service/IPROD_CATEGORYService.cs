using SPACRM.Entity;
using SPACRM.Entity.Entities;
using SPACRM.Entity.PageSearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SPACRM.Interface
{
    public interface IPROD_CATEGORYService
    {
        /// <summary>
        /// 获取产品类别列表
        /// </summary>
        /// <param name="type"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        List<PROD_CATEGORY> GetAllPROD_CATEGORY(int orgId,int type);

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="cuObj"></param>
        /// <returns></returns>
        int SavePROD_CATEGORY(PROD_CATEGORY cuObj);

         
        PagedList<PROD_CATEGORY> GetPROD_CATEGORYByParentId(Guid pId, PageView view,int type,int org_id);

        List<PROD_CATEGORY_EX> GetORG_DICTByPId(Guid pId);

        PROD_CATEGORY_EX GetPROD_CATEGORYById(Guid id);

        int DeletePROD_CATEGORY(Guid id);

    }

}
