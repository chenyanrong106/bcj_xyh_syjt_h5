using SPACRM.DataAccess.Repository;
using SPACRM.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SPACRM.DataAccess.XYH_Coupon
{
    public class XHYCouponRepository : BaseRepository
    {
        public WXCouponGiveInfo GetWXCouponGiveInfoByOpenid(string Openid)
        {
            string sql = "select id,Openid,CouponNo,CardId,CreateDate,CouponGetDate,UseDate,UseStoreCode,Mobile,GetCoupon from WXCouponGiveInfo with(nolock) where Openid=@Openid";
            return base.Get<WXCouponGiveInfo>(sql, new { Openid = Openid });
        }


        public List<WXCouponNoInfo> QueryWXCouponNoInfo(string cardId)
        {
            string sql = "select id,CouponNo from WXCouponNoInfo where isnull(IsImport,0)=0 and cardId=@cardId";
            return base.Query<WXCouponNoInfo>(sql, new { cardId= cardId });
        }

        public int UpdateWXCouponNoInfoIsImport(long id)
        {
            string sql = "update WXCouponNoInfo set IsImport=1 ,ImportDate=getdate() where  id=@id ";
            return base.Excute(sql, new { id = id });
        }

        public WXCouponGiveInfo CanGetCoupon(string OpenId, string cardId)
        {
            string sql = "select Openid,CouponNo,[Status],CardId from WXCouponGiveInfo with(nolock) where Openid=@OpenId and isnull(Status,0)=0 and cardId=@cardId and GetCoupon=1";
            return base.Get<WXCouponGiveInfo>(sql, new { OpenId = OpenId, cardId = cardId });
        }

        public int UpdateWXCouponGiveInfoIsHX(string CouponNo)
        {
            string sql = "update WXCouponGiveInfo set Status=2 ,UseDate=getdate() where  CouponNo=@CouponNo ";
            return base.Excute(sql, new { CouponNo = CouponNo });
        }

        public CardApiTicket GetModelCardApi()
        {
            string sql = "select  top 1 Id,Cardapi_Ticket,GetTicketTime from CardApiTicket Order by GetTicketTime desc";
            return base.Get<CardApiTicket>(sql, new { });
        }

        /// <summary>
        /// 查询用户某卡券获取资格
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        public List<WXCouponGiveInfo> GetWXCouponGiveInfo(string openid, string cardid)
        {
            string sql = @"select Openid,CouponNo,Status,CardId,CreateDate,CouponGetDate,UseDate,UseStoreCode,Mobile,GetCoupon from WXCouponGiveInfo with(nolock)  where Openid=@openid and CardId=@cardid";

            return base.Query<WXCouponGiveInfo>(sql, new { openid = openid, cardid = cardid });
        }

        public WXCouponGiveInfo GetWXCouponGiveInfoByMobile(string Mobile)
        {
            string sql = "select Id, Openid, Mobile, GetCoupon, CouponNo, Status, CardId, CreateDate, CouponGetDate, UseDate, UseStoreCode from WXCouponGiveInfo with(nolock) where Mobile = @Mobile";
            return base.Get<WXCouponGiveInfo>(sql, new { Mobile = Mobile });
        }
        
    }
}
