using SPACRM.Entity;
using SPACRM.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SPACRM.Interface
{
    public interface IAppiService
    {
        //某个技师预约的情况
        List<CUST_BOOKING> QueryBookingTime(string mid, DateTime showday);

        ////提交预约
        //int SaveBookingReq(CUST_BOOKING item, bool force, out string errmsg);

        //获取预约系统提供状态查询接口
        List<CUST_BOOKING_EX> QueryBookingListByUserCard(string usercard, DateTime start, DateTime end);
        List<CUST_BOOKING_EX> QueryBookingListByUserCard(string cust_id);
        List<CUST_BOOKING_EX> QueryBookingListByMobile(string mobile, DateTime start, DateTime end);

        //验证是否为会员
        int CheckIsMember(string phone);

        //验证是否男技师
        int checkMale(string masseurId);
    }
}
