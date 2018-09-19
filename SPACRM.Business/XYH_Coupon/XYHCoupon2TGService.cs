using SPACRM.DataAccess.XYH_Coupon;
using SPACRM.Entity;
using SPACRM.Entity.Entities;
using SPACRM.Interface.XYH_Coupon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SPACRM.Business.XYH_Coupon
{
   public class XYHCoupon2TGService: IXYHCoupon2TGService
    {
        private XYHCoupon2TGRepository _repo;

        public XYHCoupon2TGService()
        {
            _repo = new XYHCoupon2TGRepository();
        }

        public long AddLodInfo(Tb_log log)
        {
            return _repo.Insert(log);
        }

        public WXCouponGiveInfo GetWXCouponGiveInfoByOpenid(string Openid, string ActivityName)
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

        public WXTG_INFO_EX GetWXTG_INFO(string mobile)
        {
            return _repo.GetWXTG_INFO(mobile);
        }

        public long AddWXTG_INFO(WXTG_INFO model)
        {
            return _repo.Insert(model);
        }

        public WXTG_INFO GetWXTG_INFOById(string tgid)
        {
            return _repo.GetWXTG_INFOById(tgid);
        }

        public int UpdateWXTG_INFOById(string tgid, string JoinOpenId, string JoinMobile)
        {
            return _repo.UpdateWXTG_INFOById(tgid, JoinOpenId, JoinMobile);
        }

        public WXTG_INFO_EX GetWXTG_INFOByOpenid(string OpenId)
        {
            return _repo.GetWXTG_INFOByOpenid(OpenId);
        }

        public WXTG_INFO_EX GetWXTG_INFOBytgid(string tgid, string CardId1, string CardId2, string OpenId)
        {
            return _repo.GetWXTG_INFOBytgid(tgid, CardId1, CardId2, OpenId);
        }

        /// <summary>
        /// 根据id查询拼团记录
        /// </summary>
        /// <param name="OpenId"></param>
        /// <returns></returns>
        public WXTG_INFO GetWXTG_INFOBytgid(string tgid)
        {
            return _repo.GetWXTG_INFOById(tgid);
        }

        public long InsertMessageHis(MessageHis his)
        {
            return _repo.Insert(his);
        }
    }
}
