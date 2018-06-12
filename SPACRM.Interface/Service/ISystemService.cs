using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SPACRM.Entity;
using SPACRM.Entity.Entities;
using SPACRM.Entity.PageSearch;
using SPACRM.Entity.Jsons;

namespace SPACRM.Interface
{
    public interface ISystemService
    {
        /// <summary>
        /// 添加微信日志
        /// </summary>
        /// <param name="l"></param>
        /// <returns></returns>
        int AddLog(WXLOG l);

        /// <summary>
        /// 添加微信消息记录
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        int AddCUST_MSG_HIS(WXCUST_MSG_HIS c);




        ///// <summary>
        ///// 获取商户列表
        ///// </summary>
        ///// <param name="search"></param>
        ///// <param name="view"></param>
        ///// <returns></returns>
        //PagedList<ORG_INFO> QueryMerchantsList(RoleSearch search, PageView view);

        ///// <summary>
        ///// 获取商户列表
        ///// </summary>
        ///// <param name="search"></param>
        ///// <param name="view"></param>
        ///// <returns></returns>
        //List<ORG_INFO> GetMerchantsList();
        //List<WXCUST_FANS> GetAlljhfs();

        /// <summary>
        /// 获取商户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ORG_INFO GetMerchants(int id);
        ///// <summary>
        ///// 获取是否允许会员手机号重复
        ///// </summary>
        ///// <param name="orgID"></param>
        ///// <returns></returns>
        //bool IsRepeatMobile(int orgID);
        //ORG_INFO GetMerchants(string toUserName);
        ORG_INFO GetMerchants(string toUserName);

        /// <summary>
        /// 保存商户
        /// </summary>
        /// <param name="sys"></param>
        /// <returns></returns>
        int SaveMerchants(ORG_INFO sys);

        ///// <summary>
        ///// 保存图文列表
        ///// </summary>
        ///// <param name="list"></param>
        ///// <returns></returns>
        //int SaveGraphicList(WXGraphicList list);

        ///// <summary>
        ///// 保存图文明细
        ///// </summary>
        ///// <param name="Detail"></param>
        ///// <returns></returns>
        //int SaveGraphicDetail(WXGraphicDetail Detail);

        ///// <summary>
        ///// 获取图文
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //WXGraphicList GetGraphicList(int id);

        ///// <summary>
        ///// 获取图文明细
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //Graphic_Detail_EX GetGraphicDetail(int id);

        ///// <summary>
        ///// 获取图文明细列表
        ///// </summary>
        ///// <param name="search"></param>
        ///// <param name="view"></param>
        ///// <returns></returns>
        //PagedList<Graphic_Detail_EX> QueryGetGraphicDetail(GraphicSearch search, PageView view);

        /// <summary>
        ///插入粉丝信息
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        int InsertFans(WXCUST_FANS f);

        /// <summary>
        /// 修改粉丝信息
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        int UpdateFans(WXCUST_FANS f);

        /// <summary>
        /// 获取粉丝信息
        /// </summary>
        /// <param name="FromUserName"></param>
        /// <returns></returns>
        WXCUST_FANS GetFansByFromUserName(string FromUserName);

        /// <summary>
        /// 根据商户微信号获取商户信息
        /// </summary>
        /// <param name="ToUserName"></param>
        /// <returns></returns>
        ORG_INFO GetMerchantsByToUserName(string ToUserName);

        /// <summary>
        /// 获取自动回复消息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Information_EX GetInformation(int? id);

        /// <summary>
        /// 保存自动回复消息
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        int SaveInformation(WXInformation i);

        ///// <summary>
        ///// 获取自动回复消息列表
        ///// </summary>
        ///// <param name="search"></param>
        ///// <param name="view"></param>
        ///// <returns></returns>
        //PagedList<Information_EX> QueryInformationList(RoleSearch search, PageView view);

        /// <summary>
        /// 根据消息类型获取数据条数
        /// </summary>
        /// <param name="id"></param>
        /// <param name="replyType"></param>
        /// <param name="Merchants_ID"></param>
        /// <returns></returns>
        int GetCountByreplyType(int? id, int? replyType, int? Merchants_ID);

        /// <summary>
        /// 获取微信菜单消息集合
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        List<CustomMenu_EX> GetCustomMenuModelList(string sqlWhere);

        ///// <summary>
        ///// 获取粉丝列表
        ///// </summary>
        ///// <param name="search"></param>
        ///// <param name="view"></param>
        ///// <returns></returns>
        //PagedList<CUST_FANS_EX> QueryGetFansDetail(FansSearch search, PageView view);

        /// <summary>
        /// 根据微信原始ID获取一位粉丝
        /// </summary>
        /// <param name="ToUserName"></param>
        /// <returns></returns>
        WXCUST_FANS GetOneFans(string ToUserName);

        /// <summary>
        /// 根据粉丝ID获取粉丝详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        CUST_FANS_EX GetFansByID(int? id);

        ///// <summary>
        ///// 获取商户设置
        ///// </summary>
        ///// <param name="Merchants_ID"></param>
        ///// <returns></returns>
        //MerchantsSeting_EX GetMerchantsSetingByMerchantsID(int? Merchants_ID);

        ///// <summary>
        ///// 保存商户设置
        ///// </summary>
        ///// <param name="seting"></param>
        ///// <returns></returns>
        //int SaveMerchantsSeting(WXMerchantsSeting seting);

        ///// <summary>
        ///// 获取会员卡
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //WXMembershipCard GetMembershipCard(int? id);
        ///// <summary>
        ///// 保存会员卡
        ///// </summary>
        ///// <param name="m"></param>
        ///// <returns></returns>
        //int SaveMembershipCard(WXMembershipCard m);

        ///// <summary>
        ///// 获取会员卡列表
        ///// </summary>
        ///// <param name="search"></param>
        ///// <param name="view"></param>
        ///// <returns></returns>
        //PagedList<WXMembershipCard> QueryGetMembershipCard(MembershipCardSearch search, PageView view);

        /// <summary>
        /// 获取所以图文
        /// </summary>
        /// <param name="Merchants_ID"></param>
        /// <returns></returns>
        List<Graphic_Detail_EX> GetAllGraphicDetail(int? list_id);

        /// <summary>
        /// 获取所以图文
        /// </summary>
        /// <param name="Merchants_ID"></param>
        /// <returns></returns>
        List<WXGraphicList> GetAllGraphicList(int? Merchants_ID);

        /// <summary>
        /// 删除图文
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int DeleteTW(int id);

        /// <summary>
        /// 根据顺序获取图文
        /// </summary>
        /// <param name="list_id"></param>
        /// <param name="rowid"></param>
        /// <returns></returns>
        Graphic_Detail_EX GetGraphic_DetailByRowID(int list_id, int rowid);

        /// <summary>
        /// 根据顺序删除图文
        /// </summary>
        /// <param name="list_id"></param>
        /// <param name="rowid"></param>
        /// <returns></returns>
        int DelGraphic_DetailByRowID(int list_id, int rowid);

        ///// <summary>
        ///// 保存微信聊天记录
        ///// </summary>
        ///// <param name="msg"></param>
        ///// <returns></returns>
        //int SaveCUST_MSG_RECORD(WXCUST_MSG_RECORD msg);

        ///// <summary>
        ///// 获取粉丝聊天记录
        ///// </summary>
        ///// <param name="fansid"></param>
        ///// <returns></returns>
        //List<CUST_MSG_RECORD_EX> GetMsgList(int? fansid);

        ///// <summary>
        ///// 微信表情
        ///// </summary>
        ///// <returns></returns>
        //List<WXBiaoqing> GetBQList();

        ///// <summary>
        ///// 获取单条微信消息记录
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //CUST_MSG_RECORD_EX GetMsgByID(int? id);

        ///// <summary>
        ///// 保存微信消息记录
        ///// </summary>
        ///// <param name="msg"></param>
        ///// <returns></returns>
        //int SaveMsg(WXCUST_MSG_RECORD msg);

        ///// <summary>
        ///// 获取优惠券
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //WXTicket GetTicket(int? id);

        ///// <summary>
        ///// 保存优惠券
        ///// </summary>
        ///// <param name="wx"></param>
        ///// <returns></returns>
        //int SaveTicket(WXTicket wx);

        ///// <summary>
        ///// 获取优惠券列表
        ///// </summary>
        ///// <param name="search"></param>
        ///// <param name="view"></param>
        ///// <returns></returns>
        //PagedList<WXTicket> QueryTicketList(RoleSearch search, PageView view);

        ///// <summary>
        ///// 来电记录列表
        ///// </summary>
        ///// <param name="search"></param>
        ///// <param name="view"></param>
        ///// <returns></returns>
        //PagedList<Call_HIS_EX> QueryCallList(CallHisSearch search, PageView view);

        //WXPersonInfo GetPersonInfo(string openid);

        int SaveWXLog(WXLOG log);

        /// <summary>
        /// 保存模板消息发送记录
        /// </summary>
        /// <param name="temp"></param>
        /// <returns></returns>
        int SaveTempMessageLog(WD_TemplateMessageLog temp);

        /// <summary>
        /// 获取微信消息集合
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        List<Information_EX> GetModelList(string sqlWhere);

        /// <summary>
        /// 新增分享活动
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        long AddWXShare(Cust_Old_New model);
    }
}


