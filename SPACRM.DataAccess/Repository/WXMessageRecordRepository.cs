using SPACRM.Entity.Entities;
using SPACRM.Entity.PageSearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SPACRM.DataAccess.Repository
{
    public class WXMessageRecordRepository : BaseRepository
    {
 //       //查询粉丝发送给商户的消息记录
 //       public PagedList<CUST_MSG_RECORD_EX> QueryFansMessages(PageView view, string toUserName, bool? isStar, bool? isReturn, string searchText)
 //       {
 //           StringBuilder sqlWhere = new StringBuilder(" AND CMR.State = 0");
 //           if (!string.IsNullOrEmpty(toUserName))
 //               sqlWhere.Append(string.Format(" AND CMR.TOUSERNAME ='{0}'", toUserName));
 //           if (isStar.HasValue)
 //               sqlWhere.Append(string.Format(" AND CMR.IS_STAR = '{0}'", isStar.Value));
 //           if (isReturn.HasValue)
 //           {
 //               if (isReturn.Value)
 //                   sqlWhere.Append(string.Format(" AND CMR.IS_RETURN = '{0}'", isReturn.Value));
 //               else
 //               {
 //                   sqlWhere.Append(string.Format(" AND (CMR.IS_RETURN = '{0}' or CMR.IS_RETURN IS NULL)", isReturn.Value));
 //               }
 //           }
 //           if (!string.IsNullOrEmpty(searchText))
 //               sqlWhere.Append(string.Format(" AND (CF.NAME LIKE '%{0}%' OR PI.NAME LIKE '%{0}%')", searchText));

 //           return base.PageGet<CUST_MSG_RECORD_EX>(view, @" CMR.*,CF.NAME AS Fname,CF.IMAGE,isnull(PI.NAME,'未注册') NAME",
 //               "[WXCUST_MSG_RECORD] CMR LEFT JOIN [WXCUST_FANS] CF ON CMR.FROMUSERNAME = CF.[FROMUSERNAME] LEFT JOIN [WXPersonInfo] PI ON CMR.FROMUSERNAME = PI.[FROMUSERNAME]",
 //               sqlWhere.ToString(), "cmr.ID", "ORDER BY CMR.CREATE_DATE DESC");
 //       }

 //       public CUST_MSG_RECORD_EX GetMessage(int id)
 //       {
 //           var sql = @"SELECT * FROM [WXCUST_MSG_RECORD] WHERE ID=@ID";
 //           return base.Get<CUST_MSG_RECORD_EX>(sql, new { ID = id });
 //       }

 //       //得到一条消息和它的所有回复
 //       public List<CUST_MSG_RECORD_EX> QueryReplyMessages(int id)
 //       {
 //           var sql = @" SELECT CMR.*,CF.NAME as Fname,CF.IMAGE
 //FROM [WXCUST_MSG_RECORD] CMR 
 //LEFT JOIN [WXCUST_FANS] CF ON CMR.FROMUSERNAME = CF.FROMUSERNAME 
 //WHERE CMR.ID=@ID OR ReturnID=@ID
 //ORDER BY CMR.State ASC,CMR.CREATE_DATE DESC";
 //           return base.Query<CUST_MSG_RECORD_EX>(sql, new { ID = id });
 //       }
    }
}
