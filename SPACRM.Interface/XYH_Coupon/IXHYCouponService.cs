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
        List<WXCouponNoInfo> QueryWXCouponNoInfo();
        
        int UpdateWXCouponNoInfoIsImport(long id);

        WXCouponGiveInfo CanGetCoupon(string OpenId, string cardId);

        int UpdateWXCouponGiveInfoIsHX(string CouponNo);

        CardApiTicket GetModelCardApi();

        long AddCardApi(CardApiTicket model);

        /// <summary>
        /// 查询用户某卡券获取资格
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        List<WXCouponGiveInfo> GetWXCouponGiveInfo(string openid, string cardid);

        long AddWXCouponGiveInfo(WXCouponGiveInfo model);
    }
}
