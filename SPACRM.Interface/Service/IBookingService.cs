using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SPACRM.Entity.Entities;
using SPACRM.Entity.PageSearch;
using SPACRM.Entity.SearchEntity;
using SPACRM.Entity;

namespace SPACRM.Interface
{
    /// <summary>
    /// 预约服务接口
    /// by Levin 
    /// 2014-05-06
    /// </summary>
    public interface IBookingService
    {
        /// <summary>+得到技师工作排班信息
        /// </summary>
        /// <param name="type"></param>
        /// <param name="subid"></param>
        /// <param name="mid"></param>
        /// <param name="showdate"></param>
        /// <returns></returns>  
        List<EMPLOYEE_SCHEDULE_EX> GetColModels(int type, int subid, string mid, DateTime showdate);

        /// <summary>+根据门店和技师得到客户预约信息
        /// </summary>
        /// <param name="qStart"></param>
        /// <param name="qEnd"></param>
        /// <param name="masseurId"></param>
        /// <param name="subId"></param>
        /// <returns></returns>
        List<CUST_BOOKING_EX> QueryListByDate(DateTime qStart, DateTime qEnd, string masseurId, int subId);

        /// <summary>+根据门店和技师得到客户预约信息
        /// </summary>
        /// <param name="qStart"></param>
        /// <param name="qEnd"></param>
        /// <param name="masseurId"></param>
        /// <param name="subId"></param>
        /// <returns></returns>
        List<CUST_BOOKING_EX> QueryListByDateForAPI(DateTime qStart, DateTime qEnd, string masseurId, int subId);

        /// <summary>+根据ID 得到预约信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        CUST_BOOKING_EX GetCust_Booking(int id);

        CUST_BOOKING_HOME_EX GetCust_bookingHome(int id);

        /// <summary>
        /// 获取预约服务
        /// </summary>
        /// <param name="bookid"></param>
        /// <returns></returns>
        CUST_BOOKING_SERVICE GetBookingService(int bookid);
        
        //顾客预约信息
        List<CUST_BOOKING_EX> getBookingByCid(int cid);

        /// <summary>+保存预约
        /// </summary>
        /// <param name="booking"></param>
        /// <returns></returns>
        int SaveBooking(CUST_BOOKING_EX booking);
        int SaveBooking(CUST_BOOKING_HOME_EX booking);
        /// <summary>+未约进列表
        /// </summary>
        /// <param name="search"></param>
        /// <param name="view"></param>
        /// <returns></returns>
        PagedList<CUST_BOOKING_EX> QueryFailList(BookingSearch search, PageView view);

        /// <summary>
        /// 预约到家列表
        /// </summary>
        /// <param name="search"></param>
        /// <param name="view"></param>
        /// <returns></returns>
        PagedList<CUST_BOOKING_HOME_EX> QueryFailHomeList(BookingSearch search, PageView view);
        List<CUST_BOOKING_EX> QueryNewList(int storeid);
        /// <summary>+微信列表
        /// </summary>
        /// <param name="search"></param>
        /// <param name="view"></param>
        /// <returns></returns>
        PagedList<CUST_BOOKING_EX> QueryWxList(BookingSearch search, PageView view);

        //修改状态
        int UpdateBookingStatus(int? id, CUST_BOOKING_EX book);
        int UpdateBookingHomeStatus(int? id, CUST_BOOKING_HOME_EX book);
        int UpdateWXBookingStatus(int? id, CUST_BOOKING_EX book);

        List<PROD_SERVICE> GetSerItemByOrdId(int? oid, int? cid);


        //某个技师预约的情况
        List<CUST_BOOKING> QueryBookingTime(string mid, DateTime qstart, DateTime qend);

        PagedList<CUST_BOOKING_EX> QueryListByCustId(string cust_id, DateTime start, DateTime end, PageView view);
        List<CUST_BOOKING_EX> QueryListByCustId(string cust_id, PageView view);

        //验证是否为会员
        int CheckIsMember(string phone);
        //验证是否男技师
        int checkMale(string masseurId);
        int SaveBookingReq(CUST_BOOKING_EX item, bool force, out string errmsg);

        //修改预约状态 取消 确认预约
        int UpdateBookStatus(int? id, int? status);

        //得到预约的服务类型
        CUST_BOOKING_SERVICE GetMainCUST_BOOKING_SERVICE(int bookingId);

        /// <summary>
        /// 是否通过预约冲突检测
        /// </summary>
        /// <param name="cust_id"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns>true通过冲突检测，无冲突；false，未通过冲突检测，存在冲突。</returns>
        bool CheckBookingConflict(int cust_id, DateTime startTime, DateTime endTime);
        bool CheckBookingHomeConflict(int cust_id, DateTime startTime, DateTime endTime);
      

        PROD_CATEGORY GetMainPROD_CATEGORY(string categoryID);

        //ipad 获取门店预约列表  根据日期
        List<CUST_BOOKING_EX> getBookingByStoreId(int storeid, string bookingdate);

        /// <summary>
        /// 获取技师时间段内，是否已经有预约
        /// </summary>
        /// <param name="storeID">门店ID</param>
        /// <param name="orgID">公司ID</param>
        /// <param name="staffID">技师ID</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <remarks>created by likui.liu</remarks>
        /// <returns></returns>
        int GetBookingByStaffID(int storeID, int orgID, int staffID, DateTime beginTime, DateTime endTime);

        /// <summary>
        /// 创建预约订单
        /// </summary>
        /// <param name="buys">购物车</param>
        /// <param name="book">yuyue</param>
        /// <returns></returns>
        int CreateBookingOrder(List<WXBuyCars> buys, CUST_BOOKING_EX book);

        int CreateBookingInfoForMulti(CUST_BOOKING_EX booking, out List<int> bookingIDs);

        int EditBookingInfo(CUST_BOOKING_EX booking);

        List<CUST_BOOKING_SERVICE> GetBookingServiceList(int[] bookingIDs);

        List<ORG_EMPLOYEE> GetFreeEmployees(int storId, int orgid, DateTime beginTime, DateTime endTime, bool? gender, bool needSchedule);

        List<CUST_BOOKING_VIEW> GetByWXOpenID(string orgNO, string wxOpenID);

        int GetBookingCountToday(int storeID);

        int DeleteBookingBatch(int storeID, List<int> idList);

        int CancelBookingBatch(int[] bookingIDs, int orgID);

        CUST_BOOKING_EX GetBookingInfo(int bookingID, int orgID);

        /// <summary>
        /// 获取用户最后一次预约情况
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        CUST_BOOKING GetLastBookingInfoByOpenId(string openid);


        /// <summary>
        /// 小时光预约报表
        /// </summary>
        /// <param name="search"></param>
        /// <param name="view"></param>
        /// <returns></returns>
        PagedList<BookReport_EX> QueryBookReport(BookingSearch search, PageView view);

        /// <summary>
        /// 根据订单id获取预约列表 推送给技师
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        List<CUST_BOOKING_EX> GetBookingListByOrderidForMessage(int id);
    }
}
