using SPACRM.DataAccess.Repository;
using SPACRM.Entity;
using SPACRM.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SPACRM.DataAccess.XYH_Coupon
{
    public class XYHCoupon2TGRepository: BaseRepository
    {

        public WXCouponGiveInfo GetWXCouponGiveInfoByOpenid(string Openid, string ActivityName)
        {
            string sql = @"select id,ActivityName,Openid,CouponNo,CardId,CreateDate,CouponGetDate,UseDate,UseStoreCode,Mobile,GetCoupon from WXCouponGiveInfo with(nolock) 
                            where Openid=@Openid and GetCoupon=1 and ActivityName=@ActivityName";
            return base.Get<WXCouponGiveInfo>(sql, new { Openid = Openid, ActivityName = ActivityName });
        }


        public List<WXCouponNoInfo> QueryWXCouponNoInfo(string cardId)
        {
            string sql = "select id,CouponNo from WXCouponNoInfo where isnull(IsImport,0)=0 and cardId=@cardId";
            return base.Query<WXCouponNoInfo>(sql, new { cardId = cardId });
        }

        public int UpdateWXCouponNoInfoIsImport(long id)
        {
            string sql = "update WXCouponNoInfo set IsImport=1 ,ImportDate=getdate() where  id=@id ";
            return base.Excute(sql, new { id = id });
        }

        public WXCouponGiveInfo CanGetCoupon(string OpenId, string cardId, string ActivityName)
        {
            string sql = @"select ActivityName,Openid,CouponNo,[Status],CardId,GetCoupon from WXCouponGiveInfo with(nolock) 
                            where Openid=@OpenId  and cardId=@cardId and GetCoupon=1 and ActivityName=@ActivityName";
            return base.Get<WXCouponGiveInfo>(sql, new { OpenId = OpenId, cardId = cardId, ActivityName = ActivityName });
        }

        public int UpdateWXCouponGiveInfoIsHX(string CouponNo, string ActivityName)
        {
            string sql = "update WXCouponGiveInfo set Status=2 ,UseDate=getdate() where  CouponNo=@CouponNo and ActivityName=@ActivityName";
            return base.Excute(sql, new { CouponNo = CouponNo, ActivityName = ActivityName });
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
        public List<WXCouponGiveInfo> GetWXCouponGiveInfo(string openid, string cardid, string ActivityName)
        {
            string sql = @"select Openid,ActivityName,CouponNo,Status,CardId,CreateDate,CouponGetDate,UseDate,UseStoreCode,Mobile,GetCoupon from WXCouponGiveInfo with(nolock)  
                            where Openid=@openid and CardId=@cardid and ActivityName=@ActivityName";

            return base.Query<WXCouponGiveInfo>(sql, new { openid = openid, cardid = cardid, ActivityName = ActivityName });
        }

        public WXCouponGiveInfo GetWXCouponGiveInfoByMobile(string Mobile, string ActivityName)
        {
            string sql = @"select Id,ActivityName, Openid, Mobile, GetCoupon, CouponNo, Status, CardId, CreateDate, CouponGetDate, UseDate, UseStoreCode from WXCouponGiveInfo with(nolock) 
                            where Mobile = @Mobile and ActivityName=@ActivityName";
            return base.Get<WXCouponGiveInfo>(sql, new { Mobile = Mobile, ActivityName = ActivityName });
        }

        public WXCouponGiveInfo GetWXCouponGiveInfoByMobile(string Mobile, string CardId, string ActivityName)
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
            return base.Get<WXCouponGiveInfo>(sql, new { Mobile = Mobile, CardId = CardId, ActivityName = ActivityName });
        }
        public WXTG_INFO_EX GetWXTG_INFO(string mobile)
        {
            string sql = @"select top 1 
                            a.Id,
                            a.OpenId,
                            a.Mobile,
                            a.JoinOpenId,
                            a.JoinMobile,
                            a.Status,
                            a.JoinTime,
                            a.CloseTime,
                            a.Remark,
                            a.OpenTime,
                            DATEADD(hour,24,a.OpenTime) EndTime ,
                            b.headimgurl OpenHeadimgurl,
                            c.headimgurl JoinHeadimgurl
                            from WXTG_INFO a with(nolock)
                            left join OAauth_Log b with(nolock) on a.OpenId=b.FromUserName
                            left join OAauth_Log c with(nolock) on a.JoinOpenId=c.FromUserName
                           where Status in (0,1) and (Mobile=@mobile or JoinMobile=@mobile)";
            return base.Get<WXTG_INFO_EX>(sql, new { mobile = mobile });
        }

        public WXTG_INFO GetWXTG_INFOById(string tgid)
        {
            string sql = @"select 
                            Id,
                            OpenId,
                            Mobile,
                            JoinOpenId,
                            JoinMobile,
                            Status,
                            JoinTime,
                            CloseTime,
                            Remark,
                            OpenTime
                            from WXTG_INFO  with(nolock) where id=@id";
            return base.Get<WXTG_INFO>(sql, new { id=tgid });
        }

        public int UpdateWXTG_INFOById(string tgid, string JoinOpenId, string JoinMobile)
        {
            string sql = @"Update WXTG_INFO set 
                            status=1 ,
                            JoinTime=getdate(),
                            JoinOpenId=@JoinOpenId,
                            JoinMobile=@JoinMobile 
                            where id=@id and status=0";
            return base.Excute(sql, new { JoinOpenId = JoinOpenId, JoinMobile = JoinMobile, id = tgid });
        }

        /// <summary>
        /// 根据查询拼团中或拼团完成的记录
        /// </summary>
        /// <param name="OpenId"></param>
        /// <returns></returns>
        public WXTG_INFO_EX GetWXTG_INFOByOpenid(string OpenId)
        {
            string sql = @"select top 1
                            a.Id,
                            a.OpenId,
                            a.Mobile,
                            a.JoinOpenId,
                            a.JoinMobile,
                            a.Status,
                            a.JoinTime,
                            a.CloseTime,
                            a.Remark,
                            a.OpenTime,
                            DATEADD(hour, 24, a.OpenTime) EndTime 
                            from WXTG_INFO a with(nolock) where (a.OpenId=@OpenId or a.JoinOpenId=@OpenId) and Status in (0,1)";
            return base.Get<WXTG_INFO_EX>(sql, new { OpenId= OpenId });
        }

        public WXTG_INFO_EX GetWXTG_INFOBytgid(string tgid,string CardId1,string CardId2,string OpenId)
        {
            string sql = @"select top 1 
                            a.Id,
                            a.OpenId,
                            a.Mobile,
                            a.JoinOpenId,
                            a.JoinMobile,
                            a.Status,
                            a.JoinTime,
                            a.CloseTime,
                            a.Remark,
                            a.OpenTime,
                            DATEADD(hour,24,a.OpenTime) EndTime ,
                            b.headimgurl OpenHeadimgurl,
                            c.headimgurl JoinHeadimgurl,
                            (select top 1 Status from WXCouponGiveInfo where Openid=@OpenId and CardId=@CardId1) Card1Status,
							(select top 1 Status from WXCouponGiveInfo where Openid=@OpenId and CardId=@CardId2) Card2Status
							--d.Status Card1Status,
							--e.Status Card2Status
                            from WXTG_INFO a with(nolock)
                            left join OAauth_Log b with(nolock) on a.OpenId=b.FromUserName
                            left join OAauth_Log c with(nolock) on a.JoinOpenId=c.FromUserName
							--left join WXCouponGiveInfo d with(nolock) on a.OpenId=d.Openid and d.CardId=@CardId1
							--left join WXCouponGiveInfo e with(nolock) on a.OpenId=d.Openid and e.CardId=@CardId2
                           where a.id=@tgid";
            return base.Get<WXTG_INFO_EX>(sql, new { tgid = tgid, CardId1= CardId1, CardId2= CardId2, OpenId= OpenId });
        }

        /// <summary>
        /// 根据id查询拼团记录
        /// </summary>
        /// <param name="OpenId"></param>
        /// <returns></returns>
        public WXTG_INFO GetWXTG_INFOBytgid(string tgid)
        {
            string sql = @"select 
                            a.Id,
                            a.OpenId,
                            a.Mobile,
                            a.JoinOpenId,
                            a.JoinMobile,
                            a.Status,
                            a.JoinTime,
                            a.CloseTime,
                            a.Remark,
                            a.OpenTime,
                            DATEADD(hour, 24, a.OpenTime) EndTime 
                            from WXTG_INFO a with(nolock) where id=@id";
            return base.Get<WXTG_INFO>(sql, new { id = tgid });
        }
    }
}
