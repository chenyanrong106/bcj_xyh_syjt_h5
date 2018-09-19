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
        public WXCouponGiveInfo GetWXCouponGiveInfoByOpenid(string Openid,string ActivityName)
        {
            string sql = @"select id,ActivityName,Openid,CouponNo,CardId,CreateDate,CouponGetDate,UseDate,UseStoreCode,Mobile,GetCoupon from WXCouponGiveInfo with(nolock) 
                            where Openid=@Openid and GetCoupon=1 and ActivityName=@ActivityName";
            return base.Get<WXCouponGiveInfo>(sql, new { Openid = Openid, ActivityName= ActivityName });
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

        public WXCouponGiveInfo CanGetCoupon(string OpenId, string cardId,string ActivityName)
        {
            string sql = @"select ActivityName,Openid,CouponNo,[Status],CardId,GetCoupon from WXCouponGiveInfo with(nolock) 
                            where Openid=@OpenId  and cardId=@cardId and GetCoupon=1 and ActivityName=@ActivityName";
            return base.Get<WXCouponGiveInfo>(sql, new { OpenId = OpenId, cardId = cardId, ActivityName= ActivityName });
        }

        public int UpdateWXCouponGiveInfoIsHX(string CouponNo,string ActivityName)
        {
            string sql = "update WXCouponGiveInfo set Status=2 ,UseDate=getdate() where  CouponNo=@CouponNo and ActivityName=@ActivityName";
            return base.Excute(sql, new { CouponNo = CouponNo, ActivityName= ActivityName });
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
        public List<WXCouponGiveInfo> GetWXCouponGiveInfo(string openid, string cardid,string ActivityName)
        {
            string sql = @"select Openid,ActivityName,CouponNo,Status,CardId,CreateDate,CouponGetDate,UseDate,UseStoreCode,Mobile,GetCoupon from WXCouponGiveInfo with(nolock)  
                            where Openid=@openid and CardId=@cardid and ActivityName=@ActivityName";

            return base.Query<WXCouponGiveInfo>(sql, new { openid = openid, cardid = cardid , ActivityName = ActivityName });
        }

        public WXCouponGiveInfo GetWXCouponGiveInfoByMobile(string Mobile, string ActivityName)
        {
            string sql = @"select Id,ActivityName, Openid, Mobile, GetCoupon, CouponNo, Status, CardId, CreateDate, CouponGetDate, UseDate, UseStoreCode from WXCouponGiveInfo with(nolock) 
                            where Mobile = @Mobile and ActivityName=@ActivityName";
            return base.Get<WXCouponGiveInfo>(sql, new { Mobile = Mobile, ActivityName= ActivityName });
        }

        public WXCouponGiveInfo GetWXCouponGiveInfoByMobile(string Mobile,string CardId,string ActivityName)
        {
            string sql = @"select 
                                Id, 
                                ActivityName,
                                Openid, 
                                Mobile, 
                                GetCoupon, 
                                CouponNo, 
                                Status, 
                                CardId, 
                                CreateDate, 
                                CouponGetDate, 
                                UseDate, 
                                UseStoreCode 
                           from WXCouponGiveInfo with(nolock) 
                           where Mobile = @Mobile and CardId=@CardId and ActivityName=@ActivityName";
            return base.Get<WXCouponGiveInfo>(sql, new { Mobile = Mobile, CardId= CardId , ActivityName = ActivityName });
        }

   

    }
}
