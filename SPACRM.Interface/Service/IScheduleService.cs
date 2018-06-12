using SPACRM.Entity;
using SPACRM.Entity.Entities;
using SPACRM.Entity.PageSearch;
using SPACRM.Entity.SearchEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SPACRM.Interface
{
    public interface IScheduleService
    {
        //排班视图
        List<EMPLOYEE_SCHEDULE_EX> QuerySchedule(string date,int storeId);
        
        //排班员工姓名
        List<EMPLOYEE_SCHEDULE_EX> QuerySchedule_EName(string date,int storeId);

         //员工班次信息
        List<SCHEDULE_INFO_EX> GetSchedule_Info(string orgId);

        //门店员工列表
        List<ORG_EMPLOYEE_EX> QueryEmployee_EName(string orgId);

        //保存排班
        int SaveSchedule(List<EMPLOYEE_SCHEDULE> scheduleList,int scheduleId);
        
        //修改员工排班时间
        int EditSchedule(EMPLOYEE_SCHEDULE_EX schedule);

        //班次列表
        PagedList<SCHEDULE_INFO_EX> QueryScheduleSetList(ScheduleInfoSearch search, PageView view);

        //班次详情
        SCHEDULE_INFO_EX GetSchedule(int? id);

        //保存班次设置 
        int SaveScheduleSet(SCHEDULE_INFO_EX schedule);

        int DeleteScheduleSet(int? id);

        int DeleteEmpSchedule(int empId, DateTime schedule);
        
    }
}
