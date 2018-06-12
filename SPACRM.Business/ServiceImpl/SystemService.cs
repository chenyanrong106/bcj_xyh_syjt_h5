using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SPACRM.Interface;
using SPACRM.Common;
using SPACRM.Common.CacheManager;
using SPACRM.Common.Exceptions;
using SPACRM.Entity;
using SPACRM.DataAccess.Repository;
using SPACRM.Entity.PageSearch;
using SPACRM.Entity.Entities;
using SPACRM.Entity.Jsons;

namespace SPACRM.Business.ServiceImpl
{
    public class SystemService : ISystemService
    {
        private SystemSetRepository _set;
        public SystemService()
        {
            _set = new SystemSetRepository();
        }
        

        /// <summary>
        /// 添加微信日志
        /// </summary>
        /// <param name="l"></param>
        /// <returns></returns>
        public int AddLog(WXLOG l)
        {
            return _set.AddLog(l);
        }

        /// <summary>
        /// 添加微信消息记录
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public int AddCUST_MSG_HIS(WXCUST_MSG_HIS c)
        {
            return _set.AddCUST_MSG_HIS(c);
        }





        ///// <summary>
        ///// 获取商户列表
        ///// </summary>
        ///// <param name="search"></param>
        ///// <param name="view"></param>
        ///// <returns></returns>
        //public PagedList<ORG_INFO> QueryMerchantsList(RoleSearch search, PageView view)
        //{
        //    return _set.QueryMerchantsList(search, view);
        //}

        ///// <summary>
        ///// 获取商户列表
        ///// </summary>
        ///// <param name="search"></param>
        ///// <param name="view"></param>
        ///// <returns></returns>
        //public List<ORG_INFO> GetMerchantsList()
        //{
        //    return _set.GetMerchantsList();
        //}
        //public List<WXCUST_FANS> GetAlljhfs()
        //{
        //    return _set.GetAlljhfs();
        //}

        /// <summary>
        /// 获取商户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ORG_INFO GetMerchants(int id)
        {
            return _set.GetMerchants(id);
        }
        ///// <summary>
        ///// 获取是否允许会员手机号重复
        ///// </summary>
        ///// <param name="orgID"></param>
        ///// <returns></returns>
        //public bool IsRepeatMobile(int orgID)
        //{
        //    return _set.IsRepeatMobile(orgID);
        //}
        public ORG_INFO GetMerchants(string toUserName)
        {
            return _set.GetMerchants(toUserName);
        }

        /// <summary>
        /// 保存商户
        /// </summary>
        /// <param name="sys"></param>
        /// <returns></returns>
        public int SaveMerchants(ORG_INFO sys)
        {
            if (sys.ID == 0)
            {
                //生成加密Key
                //sys.SECRET_KEY = CryptographyManager.AESEncrypt(sys.ORG_NO);
            }
            return _set.SaveMerchants(sys);
        }

        ///// <summary>
        ///// 保存图文列表
        ///// </summary>
        ///// <param name="list"></param>
        ///// <returns></returns>
        //public int SaveGraphicList(WXGraphicList list)
        //{
        //    return _set.SaveGraphicList(list);
        //}

        ///// <summary>
        ///// 保存图文明细
        ///// </summary>
        ///// <param name="Detail"></param>
        ///// <returns></returns>
        //public int SaveGraphicDetail(WXGraphicDetail Detail)
        //{
        //    return _set.SaveGraphicDetail(Detail);
        //}

        ///// <summary>
        ///// 获取图文
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //public WXGraphicList GetGraphicList(int id)
        //{
        //    return _set.GetGraphicList(id);
        //}

        ///// <summary>
        ///// 获取图文明细
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //public Graphic_Detail_EX GetGraphicDetail(int id)
        //{
        //    return _set.GetGraphicDetail(id); ;
        //}

        ///// <summary>
        ///// 获取图文明细列表
        ///// </summary>
        ///// <param name="search"></param>
        ///// <param name="view"></param>
        ///// <returns></returns>
        //public PagedList<Graphic_Detail_EX> QueryGetGraphicDetail(GraphicSearch search, PageView view)
        //{
        //    return _set.QueryGetGraphicDetail(search, view);
        //}

        /// <summary>
        ///插入粉丝信息
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        public int InsertFans(WXCUST_FANS f)
        {
            return (int)_set.Insert(f);
        }

        /// <summary>
        /// 修改粉丝信息
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        public int UpdateFans(WXCUST_FANS f)
        {
            return _set.Update(f);
        }

        /// <summary>
        /// 获取粉丝信息
        /// </summary>
        /// <param name="FromUserName"></param>
        /// <returns></returns>
        public WXCUST_FANS GetFansByFromUserName(string FromUserName)
        {
            return _set.GetFansByFromUserName(FromUserName);
        }

        /// <summary>
        /// 根据商户微信号获取商户信息
        /// </summary>
        /// <param name="ToUserName"></param>
        /// <returns></returns>
        public ORG_INFO GetMerchantsByToUserName(string ToUserName)
        {
            return _set.GetMerchantsByToUserName(ToUserName);
        }

        /// <summary>
        /// 获取自动回复消息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Information_EX GetInformation(int? id)
        {
            return _set.GetInformation(id);
        }

        /// <summary>
        /// 保存自动回复消息
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public int SaveInformation(WXInformation i)
        {
            return _set.SaveInformation(i);
        }

        ///// <summary>
        ///// 获取自动回复消息列表
        ///// </summary>
        ///// <param name="search"></param>
        ///// <param name="view"></param>
        ///// <returns></returns>
        //public PagedList<Information_EX> QueryInformationList(RoleSearch search, PageView view)
        //{
        //    return _set.QueryInformationList(search, view);
        //}

        /// <summary>
        /// 根据消息类型获取数据条数
        /// </summary>
        /// <param name="id"></param>
        /// <param name="replyType"></param>
        /// <param name="Merchants_ID"></param>
        /// <returns></returns>
        public int GetCountByreplyType(int? id, int? replyType, int? Merchants_ID)
        {
            return _set.GetCountByreplyType(id, replyType, Merchants_ID);
        }

        /// <summary>
        /// 获取微信菜单消息集合
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public List<CustomMenu_EX> GetCustomMenuModelList(string sqlWhere)
        {
            return _set.GetCustomMenuModelList(sqlWhere);
        }

        ///// <summary>
        ///// 获取粉丝列表
        ///// </summary>
        ///// <param name="search"></param>
        ///// <param name="view"></param>
        ///// <returns></returns>
        //public PagedList<CUST_FANS_EX> QueryGetFansDetail(FansSearch search, PageView view)
        //{
        //    return _set.QueryGetFansDetail(search, view);
        //}

        /// <summary>
        /// 根据微信原始ID获取一位粉丝
        /// </summary>
        /// <param name="ToUserName"></param>
        /// <returns></returns>
        public WXCUST_FANS GetOneFans(string ToUserName)
        {
            return _set.GetOneFans(ToUserName);
        }

        /// <summary>
        /// 根据粉丝ID获取粉丝详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CUST_FANS_EX GetFansByID(int? id)
        {
            return _set.GetFansByID(id);
        }

        ///// <summary>
        ///// 获取商户设置
        ///// </summary>
        ///// <param name="Merchants_ID"></param>
        ///// <returns></returns>
        //public MerchantsSeting_EX GetMerchantsSetingByMerchantsID(int? Merchants_ID)
        //{
        //    return _set.GetMerchantsSetingByMerchantsID(Merchants_ID);
        //}

        ///// <summary>
        ///// 保存商户设置
        ///// </summary>
        ///// <param name="seting"></param>
        ///// <returns></returns>
        //public int SaveMerchantsSeting(WXMerchantsSeting seting)
        //{
        //    if (seting.ID == 0)
        //        return (int)_set.Insert(seting);
        //    else
        //        return _set.Update(seting);
        //}

        ///// <summary>
        ///// 获取会员卡
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //public WXMembershipCard GetMembershipCard(int? id)
        //{
        //    return _set.GetMembershipCard(id);
        //}
        ///// <summary>
        ///// 保存会员卡
        ///// </summary>
        ///// <param name="m"></param>
        ///// <returns></returns>
        //public int SaveMembershipCard(WXMembershipCard m)
        //{
        //    return _set.SaveMembershipCard(m);
        //}

        ///// <summary>
        ///// 获取会员卡列表
        ///// </summary>
        ///// <param name="search"></param>
        ///// <param name="view"></param>
        ///// <returns></returns>
        //public PagedList<WXMembershipCard> QueryGetMembershipCard(MembershipCardSearch search, PageView view)
        //{
        //    return _set.QueryGetMembershipCard(search, view);
        //}

        /// <summary>
        /// 获取所以图文
        /// </summary>
        /// <param name="Merchants_ID"></param>
        /// <returns></returns>
        public List<Graphic_Detail_EX> GetAllGraphicDetail(int? list_id)
        {
            return _set.GetAllGraphicDetail(list_id);
        }

        /// <summary>
        /// 获取所以图文
        /// </summary>
        /// <param name="Merchants_ID"></param>
        /// <returns></returns>
        public List<WXGraphicList> GetAllGraphicList(int? Merchants_ID)
        {
            return _set.GetAllGraphicList(Merchants_ID); ;
        }

        /// <summary>
        /// 删除图文
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteTW(int id)
        {
            return _set.DeleteTW(id);
        }

        /// <summary>
        /// 根据顺序获取图文
        /// </summary>
        /// <param name="list_id"></param>
        /// <param name="rowid"></param>
        /// <returns></returns>
        public Graphic_Detail_EX GetGraphic_DetailByRowID(int list_id, int rowid)
        {
            return _set.GetGraphic_DetailByRowID(list_id, rowid);
        }

        /// <summary>
        /// 根据顺序删除图文
        /// </summary>
        /// <param name="list_id"></param>
        /// <param name="rowid"></param>
        /// <returns></returns>
        public int DelGraphic_DetailByRowID(int list_id, int rowid)
        {
            return _set.DelGraphic_DetailByRowID(list_id, rowid);
        }

        ///// <summary>
        ///// 保存微信聊天记录
        ///// </summary>
        ///// <param name="msg"></param>
        ///// <returns></returns>
        //public int SaveCUST_MSG_RECORD(WXCUST_MSG_RECORD msg)
        //{
        //    if (msg.ID == 0)
        //        return (int)_set.Insert(msg);
        //    else
        //        return _set.Update(msg);
        //}

        /////// <summary>
        /////// 获取粉丝聊天记录
        /////// </summary>
        /////// <param name="fansid"></param>
        /////// <returns></returns>
        ////public List<CUST_MSG_RECORD_EX> GetMsgList(int? fansid)
        ////{
        ////    return _set.GetMsgList(fansid);
        ////}

        ///// <summary>
        ///// 微信表情
        ///// </summary>
        ///// <returns></returns>
        //public List<WXBiaoqing> GetBQList()
        //{
        //    return _set.GetBQList(); ;
        //}

        /////// <summary>
        /////// 获取单条微信消息记录
        /////// </summary>
        /////// <param name="id"></param>
        /////// <returns></returns>
        ////public CUST_MSG_RECORD_EX GetMsgByID(int? id)
        ////{
        ////    return _set.GetMsgByID(id);
        ////}

        ///// <summary>
        ///// 保存微信消息记录
        ///// </summary>
        ///// <param name="msg"></param>
        ///// <returns></returns>
        //public int SaveMsg(WXCUST_MSG_RECORD msg)
        //{
        //    return _set.SaveMsg(msg);
        //}

        /////// <summary>
        /////// 获取优惠券
        /////// </summary>
        /////// <param name="id"></param>
        /////// <returns></returns>
        ////public WXTicket GetTicket(int? id)
        ////{
        ////    return _set.GetTicket(id);
        ////}

        ///// <summary>
        ///// 保存优惠券
        ///// </summary>
        ///// <param name="wx"></param>
        ///// <returns></returns>
        //public int SaveTicket(WXTicket wx)
        //{
        //    if (wx.ID == 0)
        //        return (int)_set.Insert(wx);
        //    else
        //        return _set.Update(wx);
        //}

        ///// <summary>
        ///// 获取优惠券列表
        ///// </summary>
        ///// <param name="search"></param>
        ///// <param name="view"></param>
        ///// <returns></returns>
        //public PagedList<WXTicket> QueryTicketList(RoleSearch search, PageView view)
        //{
        //    return _set.QueryTicketList(search, view);
        //}

        ///// <summary>
        ///// 来电记录列表
        ///// </summary>
        ///// <param name="search"></param>
        ///// <param name="view"></param>
        ///// <returns></returns>
        //public PagedList<Call_HIS_EX> QueryCallList(CallHisSearch search, PageView view)
        //{
        //    search.Mobile = Utility.ClearSafeStringParma(search.Mobile);
        //    return _set.QueryCallList(search, view);
        //}

        //public WXPersonInfo GetPersonInfo(string openid)
        //{
        //    return _set.GetPersonInfo(openid);
        //}

        public int SaveWXLog(WXLOG log)
        {
            return (int)_set.Insert(log);
        }

        /// <summary>
        /// 保存模板消息发送记录
        /// </summary>
        /// <param name="temp"></param>
        /// <returns></returns>
        public int SaveTempMessageLog(WD_TemplateMessageLog temp)
        {
            if (temp.ID == 0)
                return (int)_set.Insert(temp);
            else
                return _set.Update(temp);
        }

        /// <summary>
        /// 获取微信消息集合
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public List<Information_EX> GetModelList(string sqlWhere)
        {
            return _set.GetModelList(sqlWhere);
        }

        /// <summary>
        /// 新增分享活动
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public long AddWXShare(Cust_Old_New model)
        {
            return _set.Insert(model);
        }
    }
}
