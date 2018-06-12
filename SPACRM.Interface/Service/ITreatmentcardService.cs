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
    public interface ITreatmentcardService
    {

        PagedList<PROD_CARD> QueryCardList(CardSearch search, PageView view, int ORG_ID);

        CARD_TREAT_SERVICE_EX GetCard(int id);

        //List<PROD_CDETAIL> GetProdCdetailByCardId(int cardId);

        int SaveCard(CARD_TREAT_SERVICE_EX card);

        int DeleteCard(int id);

        int DeleteProdCdetail(int cardId);

        List<PROD_SERVICE> GetServiceBycid(string orgId, string pid);

        /// <summary>
        /// 根据大类获取项目小类
        /// </summary>
        /// <param name="ORG_ID"></param>
        /// <param name="PARENT_ID"></param>
        /// <returns></returns>
        List<PROD_CATEGORY_EX> GetProdTypeByPid(int ORG_ID, string PARENT_ID);

        /// <summary>
        /// 根据项目大小类获取项目信息
        /// </summary>
        /// <param name="orgId">公司id</param>
        /// <param name="pid">项目大类</param>
        /// <param name="pid2">项目小类</param>
        /// <returns></returns>
        List<PROD_SERVICE> GetServiceByPID(string orgId, string pid, string pid2);
    }
}
