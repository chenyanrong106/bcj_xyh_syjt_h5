using SPACRM.DataAccess;
using SPACRM.DataAccess.Repository;
using SPACRM.Entity.Entities;
using SPACRM.Entity.PageSearch;
using SPACRM.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;


namespace SPACRM.Business.WXService
{
    public class WXMessageRecordService : IWXMessageRecordService
    {
        private WXMessageRecordRepository _mrr;
        public WXMessageRecordService()
        {
            _mrr = new WXMessageRecordRepository();
        }

        ////查询粉丝发送给商户的消息记录
        //public PagedList<CUST_MSG_RECORD_EX> QueryFansMessages(PageView view, FansMessageSearch fms)
        //{
        //    return _mrr.QueryFansMessages(view, fms.ToUserName, fms.IsStar, fms.IsReturn, fms.SearchText);
        //}

        ////加星 取消加星
        //public int UpdateFansMessageIsStar(CUST_MSG_RECORD_EX message)
        //{
        //    if (message.IS_STAR.HasValue)
        //    {
        //        return _mrr.Update(message);
        //    }
        //    return 0;
        //}

        //public int ReplyMessage(CUST_MSG_RECORD_EX fanMessage, string content)
        //{
        //    var num = 0;
        //    using (TransScope scope = new TransScope())
        //    {
        //        fanMessage.IS_RETURN = true;
        //        _mrr.Update(fanMessage);

        //        var replyMessage = new CUST_MSG_RECORD_EX()
        //        {
        //            TOUSERNAME = fanMessage.TOUSERNAME,
        //            FROMUSERNAME = fanMessage.FROMUSERNAME,
        //            MSGTYPE = "text",
        //            CONTENT = content,
        //            ReturnID = fanMessage.ID,
        //            State = 1,
        //            CREATE_DATE = DateTime.Now
        //        };
        //        num = (int)_mrr.Insert(replyMessage);
        //        scope.Commit();
        //    }
        //    return num;
        //}

        //public List<CUST_MSG_RECORD_EX> QueryReplyMessages(int id)
        //{
        //    return _mrr.QueryReplyMessages(id);
        //}

        //public CUST_MSG_RECORD_EX GetMessage(int id)
        //{
        //    return _mrr.GetMessage(id);
        //}
    }
}
