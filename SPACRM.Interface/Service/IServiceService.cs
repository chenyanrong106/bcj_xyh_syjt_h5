using SPACRM.Entity;
using SPACRM.Entity.PageSearch;
using SPACRM.Entity.SearchEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SPACRM.Entity.Entities;

namespace SPACRM.Interface
{
    /// <summary>
    /// 项目服务接口
    /// by Levin
    /// 2014-05-13
    /// </summary>
   public interface IServiceService
    {
        PagedList<PROD_SERVICE> QueryServiceList(ServiceSearch search, PageView view);

        //PagedList<PROD_SERVICE_EX> QueryServiceCategoryList(ServiceSearch search, PageView view);
        List<PROD_SERVICE_EX> QueryServiceCategoryList(ServiceSearch search, PageView view,int org_id);

        int SaveService(PROD_SERVICE goods);

        PROD_SERVICE_EX GetService(int id,int org_id);

        int DeleteService(int id);

       //检索公司下所有项目
        List<PROD_SERVICE> QueryServiceItem(string category_id,int storeid);

       //服务项目类别
        List<PROD_CATEGORY> QuerySerCatrgoryItem(string orgid);

        List<PROD_CATEGORY> GetScateByPid(string orgId, string ptype, string pid);
        List<PROD_CATEGORY> GetScateByPname(string orgId, string ptype, string pname);

       /// <summary>
       /// 根据大类及门店获取相应项目
       /// </summary>
       /// <param name="category_id"></param>
       /// <param name="storeid"></param>
       /// <returns></returns>
        List<PROD_SERVICE> GetSerivceList(string category_id, int storeid);

        /// <summary>
        /// 根据大类获取相应项目
        /// </summary>
        /// <param name="category_id"></param>
        /// <param name="storeid"></param>
        /// <returns></returns>
        List<PROD_SERVICE> GetSerivceList(string category_id);


        /// <summary>
        /// 获取项目详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        PROD_SERVICE GetService(int id);

        /// <summary>
        /// 获取项目详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        PROD_SERVICE GetServiceByStore(int id,int storeid);

        /// <summary>
        /// 根据微信编号获取购物车
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        List<WXBuyCars> GetCarsByOpenid(string openid);

        /// <summary>
        /// 根据id获取购物车单条信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        WXBuyCars GetCarByID(int id);

        /// <summary>
        /// 保存购物车
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        int SaveCar(WXBuyCars c);

        /// <summary>
        /// 根据微信编号清空购物车
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        int DeleteCarsByOpenid(string openid);

       /// <summary>
       /// 根据id删除购物车
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
        int DeleteCarByID(int id);

        /// <summary>
        /// 根据微信编号插入订单编号购物车
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        int BuyCarsByOpenid(string openid, int orderid);

        
    }
}
