using SPACRM.DataAccess.XYH_Coupon;
using SPACRM.Entity;
using SPACRM.Interface.XYH_Coupon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SPACRM.Business.XYH_Coupon
{
    public class XHYCouponService: IXHYCouponService
    {
        private XHYCouponRepository _repo;

        public XHYCouponService()
        {
            _repo = new XHYCouponRepository();
        }
        
        public long AddLodInfo(Tb_log log)
        {
            return _repo.Insert(log);
        }

        public WXCouponGiveInfo GetWXCouponGiveInfoByOpenid(string Openid,string ActivityName)
        {
            return _repo.GetWXCouponGiveInfoByOpenid(Openid, ActivityName);
        }


        public List<WXCouponNoInfo> QueryWXCouponNoInfo(string cardId)
        {
            return _repo.QueryWXCouponNoInfo(cardId);
        }
        public int UpdateWXCouponNoInfoIsImport(long id)
        {
            return _repo.UpdateWXCouponNoInfoIsImport(id);
        }

        public WXCouponGiveInfo CanGetCoupon(string OpenId, string cardId, string ActivityName)
        {
            return _repo.CanGetCoupon(OpenId, cardId, ActivityName);
        }

        public int UpdateWXCouponGiveInfoIsHX(string CouponNo, string ActivityName)
        {
            return _repo.UpdateWXCouponGiveInfoIsHX(CouponNo, ActivityName);
        }

        public CardApiTicket GetModelCardApi()
        {
            return _repo.GetModelCardApi();
        }

        public long AddCardApi(CardApiTicket model)
        {
            return _repo.Insert(model);
        }

        /// <summary>
        /// 查询用户某卡券获取资格
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        public List<WXCouponGiveInfo> GetWXCouponGiveInfo(string openid, string cardid, string ActivityName)
        {
            return _repo.GetWXCouponGiveInfo(openid, cardid, ActivityName);
        }

        public long AddWXCouponGiveInfo(WXCouponGiveInfo model)
        {
            return _repo.Insert(model);
        }

        public WXCouponGiveInfo GetWXCouponGiveInfoByMobile(string Mobile, string ActivityName)
        {
            return _repo.GetWXCouponGiveInfoByMobile(Mobile, ActivityName);
        }

        public WXCouponGiveInfo GetWXCouponGiveInfoByMobile(string Mobile, string CardId, string ActivityName)
        {
            return _repo.GetWXCouponGiveInfoByMobile(Mobile, CardId, ActivityName);
        }

        public long InsertWXCRMCustLog(WXCRMCustLog model)
        {
            return _repo.Insert(model);
        }
    }
}
