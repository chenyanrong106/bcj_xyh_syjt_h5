using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SPACRM.Entity;
using SPACRM.Entity.Entities;
using SPACRM.Entity.PageSearch;
using SPACRM.Entity.SearchEntity;

namespace SPACRM.Interface
{
    public interface IReportService
    {
        List<PASSENGER_EX> QueryPassenger(ReportSearch search);

        List<ORG_STORE> GetStoreByPname(string orgId, string pname,string storeid,string regionid);

        List<PROD_CATEGORY> GetScateByPname(string orgId, string ptype, string pid);

        List<PROD_CATEGORY> GetScateByPid(string orgId, string ptype, string pid);

        List<BUSINESS_EX> queryBusiness(int org_id,int? regionid, int? storeid, DateTime starttime, DateTime endtime);
        List<FINANCIAL_EX> queryFinancial(int org_id, int? regionid, int? storeid, DateTime starttime, DateTime endtime);

        //员工提成报表
        List<COMMISSION_GOODS_EX> queryCommissionGoods(int org_id, int? regionid, int? storeid, DateTime starttime, DateTime endtime, string categoryid, string empid, int? prodid,ReportSearch search);
        List<COMMISSION_SERVICE_EX> queryCommissionService(int org_id, int? regionid, int? storeid, DateTime starttime, DateTime endtime, string categoryid, string empid, int? prodid, ReportSearch search);
        List<COMMISSION_CARD_EX> queryCommissionCard(int org_id, int? regionid, int? storeid, DateTime starttime, DateTime endtime, string empid, int? prodid, ReportSearch search);
        List<COMMISSION_SERVICE_EX> queryCommissionServiceSummary(int org_id, int? regionid, int? storeid, DateTime starttime, DateTime endtime, string empid, ReportSearch search);
        ORG_EMPLOYEE GetEmployee(string name);

        //商品销售明细表
        List<SALES_GOODS_EX> querySalesGoods(int org_id, int? regionid, int? storeid, DateTime starttime, DateTime endtime, string categoryid, int? prodid);
        List<SALES_SERVICE_EX> querySalesService(int org_id, int? regionid, int? storeid, DateTime starttime, DateTime endtime, string categoryid, int? prodid);
        List<SALES_CARD_EX> querySalesCard(int org_id, int? regionid, int? storeid, DateTime starttime, DateTime endtime, int? prodid);

        //商品销售汇总表
        List<SALES_GOODS_EX> querySalesGoodsSummary(int org_id, int? regionid, int? storeid, DateTime starttime, DateTime endtime, string categoryid);
        List<SALES_SERVICE_EX> querySalesServiceSummary(int org_id, int? regionid, int? storeid, DateTime starttime, DateTime endtime, string categoryid);
        List<SALES_CARD_EX> querySalesCardSummary(int org_id, int? regionid, int? storeid, DateTime starttime, DateTime endtime, int? prodid);

        //会员存款明细表
        List<DEPOSIT_EX> queryDeposit(int org_id, int? regionid, int? storeid, DateTime starttime, DateTime endtime, string custname);
        //会员消费明细表
        List<CONSUME_DETAIL_EX> queryConsumeDetail(int org_id, int? regionid, int? storeid, DateTime starttime, DateTime endtime, string custname);
        //会员项目消费明细表
        List<ORDER_DETAIL_EX> queryMemberItemDetail(int org_id, int? regionid, int? storeid, DateTime starttime, DateTime endtime, string custname);

        //客流量统计表
        List<ORDER_HEAD_EX> queryPassengerFlowSummary(int org_id, int? regionid, int? storeid, DateTime starttime, DateTime endtime);
        //客流量明细表
        List<ORDER_HEAD_EX> queryPassengerFlowDetail(int org_id, int? regionid, int? storeid, DateTime starttime, DateTime endtime);

        /// <summary>
        /// 促销报表
        /// </summary>
        /// <param name="org_id"></param>
        /// <param name="regionid"></param>
        /// <param name="storeid"></param>
        /// <param name="starttime"></param>
        /// <param name="endtime"></param>
        /// <param name="lx"></param>
        /// <param name="hd"></param>
        /// <returns></returns>
        List<SalesPromotion_EX> querySalesPromotion(int org_id, int? regionid, string storeid, DateTime starttime, DateTime endtime, string lx,string hd);

        /// <summary>
        /// 会员消费汇总表
        /// </summary>
        /// <param name="storeid"></param>
        /// <param name="starttime"></param>
        /// <param name="endtime"></param>
        /// <returns></returns>
        List<Comsumption_EX> queryComsumption(string storeid, DateTime starttime, DateTime endtime, string custname);

        //积分兑换列表
        PagedList<CUST_CONSUME_DETAIL_EX> GetPointsList(PointsSearch search, PageView view);
        //会员积分明细表
        List<CUST_CONSUME_DETAIL_EX> queryPointsDetail(int org_id, int? regionid, int? storeid, DateTime starttime, DateTime endtime, string custname);

        ORG_INFO_EX GetBegAndEndTime(int orgid);

        /// <summary>
        /// 根据活动类型获取活动列表
        /// </summary>
        /// <param name="lx"></param>
        /// <returns></returns>
        List<PROMOTION_INFO> getPROMOTION(string lx,int orgid);

        List<CUST_CDETAIL_EX> queryCoursePackage(int org_id, int? regionid, int? storeid, string custname, int? serviceid);
        //会员流失预警表
        List<CUST_INFO_EX> queryMemberRunOff(int org_id, int? regionid, int? storeid, int daynum, decimal balance);
    }
}
