using SPACRM.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SPACRM.Interface.XYH_Coupon
{
    public interface IXHYCouponService
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

        long InsertWXCRMCustLog(WXCRMCustLog model);
    }
}
