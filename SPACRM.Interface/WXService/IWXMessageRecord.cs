using SPACRM.Entity.Entities;
using SPACRM.Entity.PageSearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SPACRM.Interface
{
    public interface IWXMessageRecordService
    {
        ////查询粉丝发送给商户的消息记录
        //PagedList<CUST_MSG_RECORD_EX> QueryFansMessages(PageView view, FansMessageSearch fms);

        //加星 取消加星
        //int UpdateFansMessageIsStar(CUST_MSG_RECORD_EX message);

        //List<CUST_MSG_RECORD_EX> QueryReplyMessages(int id);

        //int ReplyMessage(CUST_MSG_RECORD_EX fanMessage, string content);

        //CUST_MSG_RECORD_EX GetMessage(int id);
    }
}
