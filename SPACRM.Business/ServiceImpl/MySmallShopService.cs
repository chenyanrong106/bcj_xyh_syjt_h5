using SPACRM.DataAccess;
using SPACRM.DataAccess.Repository;
using SPACRM.Entity;
using SPACRM.Entity.Entities;
using SPACRM.Interface.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SPACRM.Business.ServiceImpl
{
    public class MySmallShopService : IMySmallShopService
    {
        private MySmallShopRepository _set;

        public MySmallShopService()
        {
            _set = new MySmallShopRepository();
        }



        /// <summary>
        /// 获取门店城市
        /// </summary>
        /// <param name="ToUserName"></param>
        /// <returns></returns>
        public List<string> GetCityList(string ToUserName)
        {
            return _set.GetCityList(ToUserName);
        }



        /// <summary>
        /// 判断用户编号与用户名是否已存在
        /// </summary>
        /// <param name="MERCHANT_NO"></param>
        /// <param name="USER_NAME"></param>
        /// <returns></returns>
        public ORG_INFO GetWDByUserNo(string MERCHANT_NO, string USER_NAME)
        {
            return _set.GetWDByUserNo(MERCHANT_NO, USER_NAME);
        }

        public int SaveMD(ORG_INFO m)
        {
            if (m.ID == 0)
                return (int)_set.Insert(m);
            else
                return _set.Update(m);
        }

        /// <summary>
        /// 保存授权信息
        /// </summary>
        /// <param name="oa"></param>
        /// <returns></returns>
        public int SaveOAtuh(OAauth_Log oa)
        {
            return _set.SaveOAtuh(oa);
        }

        /// <summary>
        /// 获取授权信息
        /// </summary>
        /// <param name="FromUserName"></param>
        /// <returns></returns>
        public OAauth_Log GetOA(string FromUserName)
        {
            return _set.GetOA(FromUserName);
        }

        /// <summary>
        /// 根据绑定手机获取用户信息
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public OAauth_Log GetOAByPhone(string phone)
        {
            return _set.GetOAByPhone(phone);
        }

        /// <summary>
        /// 删除用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeteleOA(int id)
        {
            return _set.DeteleOA(id);
        }

        public int SaveLog(WXLOG log)
        {
            return (int)_set.Insert(log);
        }

        /// <summary>
        /// 获取商户信息  供pos接口
        /// </summary>
        /// <param name="ToUserName"></param>
        /// <returns></returns>
        public ORG_INFO GetWD(string ToUserName)
        {
            return _set.GetWD(ToUserName);
        }

        /// <summary>
        /// 获取商户信息  供pos接口
        /// </summary>
        /// <param name="ToUserName"></param>
        /// <returns></returns>
        public ORG_INFO GetWD(int mid)
        {
            return _set.GetWD(mid);
        }


        /// <summary>
        /// 获取商户信息  用于验证重复授权
        /// </summary>
        /// <param name="ToUserName"></param>
        /// <returns></returns>
        public ORG_INFO GetWD(string ToUserName, int id)
        {
            return _set.GetWD(ToUserName, id);
        }

        ///// <summary>
        ///// 保存短信发送记录
        ///// </summary>
        ///// <param name="log"></param>
        ///// <returns></returns>
        //public int SaveDX(WXDXLog log)
        //{
        //    return (int)_set.Insert(log);
        //}

        /// <summary>
        /// 保存用户信息
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        public int SaveOA(OAauth_Log log)
        {
            if (log.ID == 0)
                return (int)_set.Insert(log);
            else
                return (int)_set.Update(log);
        }


        public int GetCountPage()
        {
            return _set.GetCountPage();
        }


        /// <summary>
        /// 删除配置信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteStoreSet(int id)
        {
            return _set.DeleteStoreSet(id);
        }


        ///// <summary>
        ///// 获取绑定会员
        ///// </summary>
        ///// <param name="openid"></param>
        ///// <returns></returns>
        //public CUST_INFO GetCust(string openid)
        //{
        //    return _set.GetCust(openid);
        //}



        /// <summary>
        /// 获取授权信息
        /// </summary>
        /// <param name="FromUserName"></param>
        /// <returns></returns>
        public OAauth_Log GetOA(int id)
        {
            return _set.GetOA(id);
        }

        /// <summary>
        /// 查询是否有授权记录
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        public int GetISOA(string openid)
        {
            return _set.GetISOA(openid);
        }

        /// <summary>
        /// 查询当天是否有授权记录
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        public int GetOAByDay(string openid)
        {
            return _set.GetOAByDay(openid);
        }

        ///// <summary>
        ///// 保存用户坐标
        ///// </summary>
        ///// <param name="loc"></param>
        ///// <returns></returns>
        //public int SaveWXLocation(WX_Location loc)
        //{
        //    if (loc.ID == 0)
        //        return (int)_set.Insert(loc);
        //    else
        //        return _set.Update(loc);
        //}

        ///// <summary>
        ///// 保存扫码纪录
        ///// </summary>
        ///// <param name="qr"></param>
        ///// <returns></returns>
        //public int SaveQRLog(ORG_WX_QRLog qr)
        //{
        //    if (qr.ID == 0)
        //        return (int)_set.Insert(qr);
        //    else return _set.Update(qr);
        //}

       

        /// <summary>
        /// 获取所有粉丝
        /// </summary>
        /// <returns></returns>
        public List<WXCUST_FANS> GetAllFans()
        {
            return _set.GetAllFans();
        }

        ///// <summary>
        ///// 获取微信排名
        ///// </summary>
        ///// <param name="mjuserid"></param>
        ///// <param name="openid"></param>
        ///// <returns></returns>
        //public List<WXFansRank> HuoQuTop5(string mjuserid)
        //{
        //    return _set.HuoQuTop5(mjuserid);
        //}

        public int UpdateCustOldNewJie(string OpenID)
        {
            return _set.UpdateCustOldNewJie(OpenID);
        }

        public int InsertPVData(PVData pv)
        {
            return _set.InsertPVData(pv);
        }
    }
}
