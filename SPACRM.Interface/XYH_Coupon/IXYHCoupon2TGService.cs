using SPACRM.Entity;
using SPACRM.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SPACRM.Interface.XYH_Coupon
{
    public interface IXYHCoupon2TGService
    {
        long AddLodInfo(Tb_log log);

        WXCouponGiveInfo GetWXCouponGiveInfoByOpenid(string Openid, string ActivityName);

        List<WXCouponNoInfo> QueryWXCouponNoInfo(string cardId);

        int UpdateWXCouponNoInfoIsImport(long id);

        WXCouponGiveInfo CanGetCoupon(string OpenId, string cardId, string ActivityName);

        int UpdateWXCouponGiveInfoIsHX(string CouponNo, string ActivityName);

        CardApiTicket GetModelCardApi();

        long AddCardApi(CardApiTicket model);

        /// <summary>
        /// 查询用户某卡券获取资格
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        List<WXCouponGiveInfo> GetWXCouponGiveInfo(string openid, string cardid, string ActivityName);

        long AddWXCouponGiveInfo(WXCouponGiveInfo model);

        WXCouponGiveInfo GetWXCouponGiveInfoByMobile(string Mobile, string ActivityName);

        WXCouponGiveInfo GetWXCouponGiveInfoByMobile(string Mobile, string CardId, string ActivityName);

        WXTG_INFO_EX GetWXTG_INFO(string mobile);
        long AddWXTG_INFO(WXTG_INFO model);

        WXTG_INFO GetWXTG_INFOById(string tgid);

        int UpdateWXTG_INFOById(string tgid, string JoinOpenId, string JoinMobile);

        WXTG_INFO_EX GetWXTG_INFOByOpenid(string OpenId);

        WXTG_INFO_EX GetWXTG_INFOBytgid(string tgid, string CardId1, string CardId2, string OpenId);

        /// <summary>
        /// 根据id查询拼团记录
        /// </summary>
        /// <param name="OpenId"></param>
        /// <returns></returns>
        WXTG_INFO GetWXTG_INFOBytgid(string tgid);

        long InsertMessageHis(MessageHis his);
    }
}
