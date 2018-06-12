using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SPACRM.Entity;
using SPACRM.Entity.Entities;
using SPACRM.Entity.PageSearch;
using SPACRM.Entity.Jsons;

namespace SPACRM.DataAccess.Repository
{
    public class SystemSetRepository : BaseRepository
    {

        /// <summary>
        /// 添加微信日志
        /// </summary>
        /// <param name="l"></param>
        /// <returns></returns>
        public int AddLog(WXLOG l)
        {
            return (int)base.Insert(l);
        }

        /// <summary>
        /// 添加微信消息记录
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public int AddCUST_MSG_HIS(WXCUST_MSG_HIS c)
        {
            return (int)base.Insert(c);
        }

        ///// <summary>
        ///// 获取商户列表
        ///// </summary>
        ///// <param name="search"></param>
        ///// <param name="view"></param>
        ///// <returns></returns>
        //public PagedList<ORG_INFO> QueryMerchantsList(RoleSearch search, PageView view)
        //{
        //    //string sql = "select * from Merchants";
        //    return base.PageGet<ORG_INFO>(view, @"*", "ORG_INFO", " and id=" + search.ORG_ID, "id", "");
        //}

        /// <summary>
        /// 获取商户列表
        /// </summary>
        /// <param name="search"></param>
        /// <param name="view"></param>
        /// <returns></returns>
        public List<ORG_INFO> GetMerchantsList()
        {
            return base.Query<ORG_INFO>(@"select * from ORG_INFO", null);
        }
        public bool IsRepeatMobile(int orgID)
        {
            string sql = "select ISNULL(IS_REPEAT_MOBILE, 0) from ORG_INFO where ID = " + orgID;
            return base.Get<bool>(sql, null);
        }
        /// <summary>
        /// 获取商户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ORG_INFO GetMerchants(int id)
        {
            string sql = @"select * from ORG_INFO where id=" + id;
            return base.Get<ORG_INFO>(sql, null);
        }
        public ORG_INFO GetOrgIsPointByStoreID(int storeID)
        {

            string sql = @"select OI.ID, OI.IS_POINT from ORG_INFO OI
                        inner join ORG_STORE OS on os.ORG_ID = OI.ID
                        where OS.ID = @ID ";
            return base.Get<ORG_INFO>(sql, new { ID = storeID });
        }
        public ORG_INFO GetMerchants(string toUserName)
        {
            string sql = @"select * from ORG_INFO where tousername=@ToUserName";
            return base.Get<ORG_INFO>(sql, new { ToUserName = toUserName });
        }

        /// <summary>
        /// 保存商户
        /// </summary>
        /// <param name="sys"></param>
        /// <returns></returns>
        public int SaveMerchants(ORG_INFO sys)
        {
            if (sys.ID == 0)
                return (int)base.Insert(sys);
            else
                return base.Update(sys);
        }

        /// <summary>
        /// 保存图文列表
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public int SaveGraphicList(WXGraphicList list)
        {
            if (list.ID == 0)
                return (int)base.Insert(list);
            else
                return base.Update(list);
        }

        /// <summary>
        /// 保存图文明细
        /// </summary>
        /// <param name="Detail"></param>
        /// <returns></returns>
        public int SaveGraphicDetail(WXGraphicDetail Detail)
        {
            if (Detail.ID == 0)
                return (int)base.Insert(Detail);
            else
                return base.Update(Detail);
        }

        /// <summary>
        /// 获取图文
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public WXGraphicList GetGraphicList(int id)
        {
            return Get<WXGraphicList>("select * from wxGraphicList where ID=" + id, null);
        }

        /// <summary>
        /// 获取图文明细
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Graphic_Detail_EX GetGraphicDetail(int id)
        {
            return Get<Graphic_Detail_EX>("select d.*,l.title Name from wxGraphicDetail d left join wxgraphiclist l on d.list_id=l.id where d.ID=" + id, null);
        }

        ///// <summary>
        ///// 获取图文明细列表
        ///// </summary>
        ///// <param name="search"></param>
        ///// <param name="view"></param>
        ///// <returns></returns>
        //public PagedList<Graphic_Detail_EX> QueryGetGraphicDetail(GraphicSearch search, PageView view)
        //{

        //    return base.PageGet<Graphic_Detail_EX>(view, "d.*,l.Title Name", "wxGraphicDetail d left join wxGraphicList l on d.List_ID=l.ID", " and l.Merchants_ID=" + search.Merchants_ID, "d.id", "");

        //}

        /// <summary>
        /// 获取粉丝信息
        /// </summary>
        /// <param name="FromUserName"></param>
        /// <returns></returns>
        public WXCUST_FANS GetFansByFromUserName(string FromUserName)
        {
            return base.Get<WXCUST_FANS>("select * from wxcust_fans where fromusername='" + FromUserName + "'", null);
        }

        /// <summary>
        /// 根据商户微信号获取商户信息
        /// </summary>
        /// <param name="ToUserName"></param>
        /// <returns></returns>
        public ORG_INFO GetMerchantsByToUserName(string ToUserName)
        {
            string sql = @"select * from ORG_INFO where ToUserName='" + ToUserName + "'";
            return base.Get<ORG_INFO>(sql, null);
        }

        /// <summary>
        /// 获取自动回复消息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Information_EX GetInformation(int? id)
        {
            string sql = "select * from wxInformation where id=" + id;
            return base.Get<Information_EX>(sql, null);
        }

        /// <summary>
        /// 保存自动回复消息
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public int SaveInformation(WXInformation i)
        {
            if (i.ID == 0)
                return (int)Insert(i);
            else
                return Update(i);
        }

//        /// <summary>
//        /// 获取自动回复消息列表
//        /// </summary>
//        /// <param name="search"></param>
//        /// <param name="view"></param>
//        /// <returns></returns>
//        public PagedList<Information_EX> QueryInformationList(RoleSearch search, PageView view)
//        {
//            string sqlwhere = " and Merchants_ID=" + search.ORG_ID;
//            if (search.replyType != 0)
//                sqlwhere += " and replyType=" + search.replyType;
//            return base.PageGet<Information_EX>(view, @"ID,CreateTime,case MsgType when 'text' then '文本' else '图文' end MsgType,Content,
//case replyType when 1 then '关键字回复' when 2 then '被关注回复' else '自动回复' end XXType,
//case replyType when 1 then ( case matchingType when 0 then '模糊匹配' else '全部匹配' end)  else '无' end PPType, case replyType when 1 then KeyWords else '无' end KeyWords", "wxInformation", sqlwhere, "id", "");
//        }

        /// <summary>
        /// 根据消息类型获取数据条数
        /// </summary>
        /// <param name="id"></param>
        /// <param name="replyType"></param>
        /// <param name="Merchants_ID"></param>
        /// <returns></returns>
        public int GetCountByreplyType(int? id, int? replyType, int? Merchants_ID)
        {
            string sql = string.Format("select COUNT(1) from wxinformation where replyType={1} and ID<>{0} and Merchants_ID={2}", id, replyType, Merchants_ID);
            return base.Get<int>(sql, null);
        }

        /// <summary>
        /// 获取微信菜单消息集合
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public List<CustomMenu_EX> GetCustomMenuModelList(string sqlWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select c.*,d.Title,d.Describe Description,d.pic PicUrl,d.url FulltextUrl,
d.id did,case c.type when 0 then 'text' when 1 then 'news' end MsgType,d.IsURL from wxCustomMenu c left join wxGraphicList l on c.Graphic_ID=l.ID
left join wxGraphicDetail d on l.ID=d.List_ID ");
            if (strSql.ToString().Trim() != "")
            {
                strSql.Append(" where " + sqlWhere.ToString());
            }
            return base.Query<CustomMenu_EX>(strSql.ToString(), null);
        }

        ///// <summary>
        ///// 获取粉丝列表
        ///// </summary>
        ///// <param name="search"></param>
        ///// <param name="view"></param>
        ///// <returns></returns>
        //public PagedList<CUST_FANS_EX> QueryGetFansDetail(FansSearch search, PageView view)
        //{
        //    string iszc = "";
        //    if (search.ISZC == "已注册")
        //        iszc = " and p.name is not null";
        //    if (search.ISZC == "未注册")
        //        iszc = " and p.name is null";
        //    if (search.State != null && search.State != -1)
        //        iszc += " and w.STATUS=" + search.State;
        //    if (search.Name != null && search.Name != "")
        //        iszc += " and (w.name like '%" + search.Name + "%' or p.name like '%" + search.Name + "%' or p.phone like '%" + search.Name + "%')";
        //    return base.PageGet<CUST_FANS_EX>(view, @"w.*,p.Name xm,p.Phone,case status when 0 then '已取消' else '未取消' end qx,case gender when 1 then '男' else '女' end xb", "wxCUST_FANS w LEFT JOIN dbo.WXPersonInfo p ON w.FROMUSERNAME=p.FromUserName", " and w.ToUserName='" + search.ToUserName + "'  " + iszc, "w.id", "");
        //}

        /// <summary>
        /// 根据微信原始ID获取一位粉丝
        /// </summary>
        /// <param name="ToUserName"></param>
        /// <returns></returns>
        public WXCUST_FANS GetOneFans(string ToUserName)
        {
            return base.Get<WXCUST_FANS>("select top 1 * from wxCUST_FANS where ToUserName='" + ToUserName + "'", null);
        }

        /// <summary>
        /// 根据粉丝ID获取粉丝详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CUST_FANS_EX GetFansByID(int? id)
        {
            return Get<CUST_FANS_EX>(@"select f.*,case status when 0 then '已取消关注' else '未取消关注' end qx,case gender when 1 then '男' else '女' end xb,
ISNULL(MEM_LEVEL,'未注册/绑定') MEM_LEVEL,p.name XM,p.phone,birthday,yzm from wxcust_fans f left join wxpersoninfo p on f.fromusername=p.fromusername
where f.ID=" + id, null);
        }

        ///// <summary>
        ///// 获取商户设置
        ///// </summary>
        ///// <param name="Merchants_ID"></param>
        ///// <returns></returns>
        //public MerchantsSeting_EX GetMerchantsSetingByMerchantsID(int? Merchants_ID)
        //{
        //    return Get<MerchantsSeting_EX>("select * from wxMerchantsSeting where Merchants_ID=" + Merchants_ID, null);
        //}

        ///// <summary>
        ///// 获取会员卡
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //public WXMembershipCard GetMembershipCard(int? id)
        //{
        //    return Get<WXMembershipCard>("select * from wxMembershipCard where id=" + id, null);
        //}
        ///// <summary>
        ///// 保存会员卡
        ///// </summary>
        ///// <param name="m"></param>
        ///// <returns></returns>
        //public int SaveMembershipCard(WXMembershipCard m)
        //{
        //    if (m.ID == 0)
        //        return (int)Insert(m);
        //    else
        //        return Update(m);
        //}

        ///// <summary>
        ///// 获取会员卡列表
        ///// </summary>
        ///// <param name="search"></param>
        ///// <param name="view"></param>
        ///// <returns></returns>
        //public PagedList<WXMembershipCard> QueryGetMembershipCard(MembershipCardSearch search, PageView view)
        //{
        //    return base.PageGet<WXMembershipCard>(view, @"m.ID,m.Type,m.Title,s.NAME Explain,m.CreateDate,m.Merchants_ID,m.SID", "WXMembershipCard m left join ORG_STORE s on m.SID=s.ID", " and m.Merchants_ID=" + search.Merchants_ID, "m.id", "");
        //}

        /// <summary>
        /// 获取所以图文
        /// </summary>
        /// <param name="Merchants_ID"></param>
        /// <returns></returns>
        public List<Graphic_Detail_EX> GetAllGraphicDetail(int? list_id)
        {
            return Query<Graphic_Detail_EX>(@"select d.*,g.title Name,g.CreateDate from dbo.wxGraphicList g left join dbo.wxGraphicDetail d on g.id=d.list_id
where d.list_id=" + list_id + " and d.id is not null order by g.id", null);
        }

        /// <summary>
        /// 获取所以图文
        /// </summary>
        /// <param name="Merchants_ID"></param>
        /// <returns></returns>
        public List<WXGraphicList> GetAllGraphicList(int? Merchants_ID)
        {
            return Query<WXGraphicList>(@"select * from dbo.wxGraphicList where merchants_id=" + Merchants_ID + " order by id desc", null);
        }

        /// <summary>
        /// 删除图文
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteTW(int id)
        {
            string sql = string.Format(@"delete wxGraphicList where ID={0}
delete wxGraphicDetail where list_id={0}", id);
            return base.Excute(sql, null);
        }

        /// <summary>
        /// 根据顺序获取图文
        /// </summary>
        /// <param name="list_id"></param>
        /// <param name="rowid"></param>
        /// <returns></returns>
        public Graphic_Detail_EX GetGraphic_DetailByRowID(int list_id, int rowid)
        {
            string sql = string.Format(@"select * from (select ROW_NUMBER()over(order by g.id) rid, d.*,g.title Name,g.CreateDate from dbo.wxGraphicList g left join dbo.wxGraphicDetail d on g.id=d.list_id
where d.list_id={0} and d.id is not null )t where rid={1}", list_id, rowid);
            return Get<Graphic_Detail_EX>(sql, null);
        }

        /// <summary>
        /// 根据顺序删除图文
        /// </summary>
        /// <param name="list_id"></param>
        /// <param name="rowid"></param>
        /// <returns></returns>
        public int DelGraphic_DetailByRowID(int list_id, int rowid)
        {
            string sql = string.Format(@"delete wxGraphicDetail where ID in(
select ID from (select ROW_NUMBER()over(order by g.id) rid, d.*,g.title Name,g.CreateDate from dbo.wxGraphicList g left join dbo.wxGraphicDetail d on g.id=d.list_id
where d.list_id={0} and d.id is not null )t where rid={1})", list_id, rowid);
            return base.Excute(sql, null);
        }

//        /// <summary>
//        /// 获取粉丝聊天记录 前20条
//        /// </summary>
//        /// <param name="fansid"></param>
//        /// <returns></returns>
//        public List<CUST_MSG_RECORD_EX> GetMsgList(int? fansid)
//        {
//            string sql = @"select TOP 20  m.ID,isnull(m.TOUSERNAME,s.tousername) tousername,isnull(m.FROMUSERNAME,f.fromusername) fromusername,m.MSGTYPE,case m.GraphicID when 0 then m.CONTENT else '[图文][<a href='''+case isurl when 1 then url else '/GraphicDisplay.aspx?id='+CONVERT(varchar, w.id) end +''' target=''_Blank''>'+w.title+'</a>]'+ISNULL(CONTENT,'') end CONTENT,m.GraphicID,m.ReturnID,m.IS_RETURN,m.IS_STAR,m.State,m.CREATE_DATE,case m.State when 0 then  f.NAME else s.ORG_NAME end name,case m.State when 0 then  f.IMAGE else '/assets/images/jqr.jpg' end image,f.NAME fname,F.ID FID from wxcust_fans f left join wxcust_msg_record m on f.fromusername=m.fromusername
//left join ORG_INFO s on f.TOUSERNAME=s.TOUSERNAME
//left join (select MIN(id) id from dbo.WXGraphicDetail group by list_id) d on m.graphicid=d.id
//left join WXGraphicDetail w on d.id=w.id
//where f.id=" + fansid + " order by m.CREATE_DATE desc";
//            return Query<CUST_MSG_RECORD_EX>(sql, null);
//        }

        ///// <summary>
        ///// 微信表情
        ///// </summary>
        ///// <returns></returns>
        //public List<WXBiaoqing> GetBQList()
        //{
        //    return Query<WXBiaoqing>("select * from wxbiaoqing", null);
        //}

//        /// <summary>
//        /// 获取单条微信消息记录
//        /// </summary>
//        /// <param name="id"></param>
//        /// <returns></returns>
//        public CUST_MSG_RECORD_EX GetMsgByID(int? id)
//        {
//            return Get<CUST_MSG_RECORD_EX>(@"select m.*,case m.State when 0 then  f.NAME else s.ORG_NAME end name,case m.State when 0 then  f.IMAGE else 'http://182.254.139.183/favicon.ico' end image,f.NAME fname from wxcust_fans f left join wxcust_msg_record m on f.fromusername=m.fromusername
//left join ORG_INFO s on f.TOUSERNAME=s.TOUSERNAME
//where m.id=" + id, null);
//        }

        ///// <summary>
        ///// 保存微信消息记录
        ///// </summary>
        ///// <param name="msg"></param>
        ///// <returns></returns>
        //public int SaveMsg(WXCUST_MSG_RECORD msg)
        //{
        //    if (msg.ID == 0)
        //        return (int)Insert(msg);
        //    else
        //        return Update(msg);
        //}

        ///// <summary>
        ///// 获取优惠券
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //public WXTicket GetTicket(int? id)
        //{
        //    return Get<WXTicket>("select * from wxticket where id=" + id, null);
        //}

        ///// <summary>
        ///// 获取优惠券列表
        ///// </summary>
        ///// <param name="search"></param>
        ///// <param name="view"></param>
        ///// <returns></returns>
        //public PagedList<WXTicket> QueryTicketList(RoleSearch search, PageView view)
        //{
        //    return base.PageGet<WXTicket>(view, "*", "WXTicket", " and merchants_id=" + search.ORG_ID, "id", "");
        //}

        ///// <summary>
        ///// 来电记录列表
        ///// </summary>
        ///// <param name="search"></param>
        ///// <param name="view"></param>
        ///// <returns></returns>
        //public PagedList<Call_HIS_EX> QueryCallList(CallHisSearch search, PageView view)
        //{
        //    string where = " and h.ORG_ID = " + search.OrgID;
        //    if (search.STORE_ID > 0)
        //    {
        //        where += " and h.STORE_ID = " + search.STORE_ID;
        //    }
        //    if (!string.IsNullOrEmpty(search.Mobile))
        //    {
        //        where += " and h.PHONE_NO like '%" + search.Mobile + "%'";
        //    }
        //    return base.PageGet<Call_HIS_EX>(view, "h.*,c.NAME", "dbo.CALL_HIS h LEFT JOIN dbo.CALL_HIS_GUEST c ON c.ID=h.GUEST_ID AND c.ORG_ID=h.ORG_ID", where, "h.id desc", "");
        //}


        //public WXPersonInfo GetPersonInfo(string openid)
        //{
        //    return Get<WXPersonInfo>("select * from wxpersoninfo WHERE FromUserName='" + openid + "' ", null);
        //}

        //public List<WXCUST_FANS> GetAlljhfs()
        //{
        //    return Query<WXCUST_FANS>("SELECT * FROM dbo.WXCUST_FANS WHERE ToUserName='gh_2d78b1f7266f'  --AND( NAME='韩奔' OR NAME LIKE '%luffy%' OR NAME='再见理想' OR name LIKE'%Jason （俊岭）%' OR FROMUSERNAME='oLT6UjuVxkTmOic5IhIu5jVWjYHc' OR FROMUSERNAME='oLT6UjkRg9Go-6D_q-ZjnMFmJ2R4')", null);
        //}

        /// <summary>
        /// 获取微信消息集合
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public List<Information_EX> GetModelList(string sqlWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select i.*,d.Title,d.Describe Description,d.pic PicUrl,d.url FulltextUrl,
d.id did,d.IsURL from wxinformation i left join dbo.wxGraphicList l on i.Graphic_ID=l.id
left join dbo.wxGraphicDetail d on d.list_id=l.id
left join ORG_INFO m on i.Merchants_ID=m.ID ");
            if (strSql.ToString().Trim() != "")
            {
                strSql.Append(" where " + sqlWhere.ToString());
            }
            return base.Query<Information_EX>(strSql.ToString(), null);
        }
        
    }
}
