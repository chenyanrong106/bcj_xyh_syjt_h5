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
    public interface ICustomerService
    {
        /// <summary>
        /// Queries the customer list.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="view">The view.</param>
        /// <returns></returns>
        PagedList<CUST_INFO_EX> QueryCustomerList(CustomerSearch search, PageView view, int org_id, int region_id);
        List<CUST_CARD_EX> GetCustCards(List<int> ids, int orgID);
        /// <summary>
        /// 顾客360页面tab，根据顾客ID获取预约列表
        /// </summary>
        /// <param name="view"></param>
        /// <param name="cust_id"></param>
        /// <returns></returns>
        PagedList<CUST_BOOKING_EX> QueryCustBookingList(PageView view, int cust_id);
        /// <summary>
        /// 顾客360页面tab，根据顾客ID获取订单列表
        /// </summary>
        /// <param name="view"></param>
        /// <param name="cust_id"></param>
        /// <returns></returns>
        PagedList<ORDER_HEAD> GetCustOrderList(PageView view, int cust_id);

        //List<VIEW_CUST_HIS> GetCustDynamicList(int cust_id);

        /// <summary>
        /// 获取顾客信息
        /// </summary>
        /// <param name="p">The p.</param>
        /// <returns></returns>
        /// 
        CUST_INFO_EX GetCustomerDetail(int id);
        CUST_INFO_EX GetCustomer(int id);
        CUST_INFO_EX GetCustomerEx(string custname, int storeid, int orgid);
        //CUST_INFO GetCustomer(string custname);
        CUST_INFO GetCustomerByMobile(string mobile);
        CUST_INFO GetBasicInfoByID(int id, int orgID);
        int SaveSmsMessage(SMS_MESSAGE_EX sms);
        /// <summary>
        /// 保存顾客信息
        /// </summary>
        /// <param name="customer">The customer.</param>
        /// <returns></returns>
        int SaveCustomer(CUST_INFO customer);

        int SaveCustLog(CUST_MODIFY_LOG custlog);
        /// <summary>
        /// Deletes the customer.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        int DeleteCustomer(int id);

        /// <summary>
        /// 2014-05-04 by Levin
        /// </summary>
        /// <param name="qText"></param>
        /// <returns></returns>
        List<CUST_INFO_EX> QueryCustomer(string qText, int storeid, int org_id);
        List<CUST_INFO_EX> QueryCustomerEX(string qText, int storeid, int org_id);
        //List<CUST_INFO> QueryCustomer(string qText);
        //查询会员列表 用于ipad接口
        List<CUST_INFO_EX> QueryCustomer(CUST_INFO cust, int page, int pagesize);
        // 查询会员列表总数 用于ipad接口
        int QueryCustomerCount(CUST_INFO cust);
        /// <summary>
        /// 获取顾客360对象
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        Customer360 GetCustomer360ByCustId(string cid, int orgid);

        /// <summary>
        /// 获取顾客最近一条备注信息
        /// </summary>
        /// <returns></returns>
        CUST_NOTES_HIS GetCustomerLastNote(string cid);

        /// <summary>
        /// 获取顾客备忘录列表
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        List<CUST_NOTES_HIS_EX> GetCustNoteList(string cid);

        /// <summary>
        /// 保存顾客备注信息
        /// </summary>
        /// <param name="customer">The customer.</param>
        /// <returns></returns>
        int SaveRemark(CUST_NOTES_HIS note);

        /// <summary>
        /// 获取顾客卡包列表
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        List<CUST_CARD_EX> GetCustCardList(string cid);

        /// <summary>
        /// 获取会员积分列表
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        List<CUST_CONSUME_DETAIL_EX> GetMemberPointsList(string cid);

        /// <summary>
        /// 获取会员积分列表(带查询)
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        PagedList<CUST_CONSUME_DETAIL_EX> GetMemberPointsList(PageView view, int cust_id);

        /// <summary>
        /// 获取礼券列表(带查询)
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        PagedList<PROMOTION_VOUCHER_EX> GetCertiFicateList(PageView view, int cust_id);

        /// <summary>
        /// 获取会员积分信息
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        MEMBER_INFO GetMemberPointsInfoByCustId(string cid);

        /// <summary>
        /// 顾客360页面tab，根据顾客ID获取附件列表
        /// </summary>
        /// <param name="view"></param>
        /// <param name="cust_id"></param>
        /// <returns></returns>
        PagedList<Cust_Files_EX> QueryFilesList(PageView view, int cust_id);
        List<Cust_Files_EX> GetCustFiles(int customerID);

        FILES GetUploadFile(int id);

        int DeleteFile(int id);

        #region 客户分组
        /// <summary>
        /// 查询客户
        /// </summary>
        /// <param name="conditions">查询条件</param>
        /// <param name="view">分页参数</param>
        /// <returns></returns>
        PagedList<CUST_INFO_EX> QueryCustInfoList(List<CUST_GROUP_CONDITION_EX> conditions, PageView view, int ORG_ID);

        /// <summary>
        /// 保存客户
        /// </summary>
        /// <param name="conditions">客户查询条件</param>
        /// <param name="createUser">保存人</param>
        /// <returns></returns>
        int SaveCustGoup(CUST_GROUP_EX cuGroup, List<CUST_GROUP_CONDITION_EX> conditions, int ORG_ID);

        /// <summary>
        /// 查询顾客分组
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        PagedList<CUST_GROUP_EX> QeryCustomerGroupList(PageView view, string StrWhere);

        /// <summary>
        /// 查询顾客分组
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        CUST_GROUP_EX GetCustGroupInfo(int id);
        #endregion

        ///发卡
        int SaveFaCard(CUST_INFO custcard);

        /// <summary>
        /// 会员卡信息
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        CUST_INFO_EX getCard(string cardNo, string pwd);
        //转账
        int CardTransferSave(int outcid, int incid, double TransferAmt, CUST_CARD_TRANSFER_HISTORY model);

        //挂失 解冻 补卡
        int SaveMakeUpCard(int? cid, CUST_INFO_EX custinfo, string cardno, string oldpwd);

        //日结统计
        List<DailyCheck_EX> GetDailyIncome(int? storeId, string sDate, string eDate, int? orgid);

        //日结统计
        List<DailyCheck_EX> GetDailyIncome2(int? storeId, string sDate, string eDate, int? orgid);
        //验证日结 未付款订单
        int InvalidDailyCheck(int storeId);
        //日结保存
        int SaveDailyCheck(string userid, int storeId);
        //获取会员特征
        List<ORG_DICT_EX> GetApparent(int org_id);

        int DeleteNote(int id);

        /// <summary>
        /// 积分兑换
        /// </summary>
        /// <param name="custInfo"></param>
        /// <param name="point"></param>
        /// <param name="prodId"></param>
        /// <param name="prodType">0：产品；1：电子券</param>
        /// <returns></returns>
        int SavePoints(CUST_INFO_EX custInfo, int? point, int prodId, int prodType);

        int SavePoints(MEMBER_INFO_EX custInfo, int? point, int goodid, PROD_CARD prodCard);

        //根据会员卡号查找会员

        CUST_INFO GetCustomerByUserCard(string cardno);

        /// <summary>
        /// 查询客户
        /// </summary>
        /// <param name="conditions">查询条件</param>
        /// <param name="view">分页参数</param>
        /// <returns></returns>
        PagedList<CUST_GROUP_DETAIL_EX> SelectCustInfoList(int Group_Id, PageView view);

        List<CUST_GROUP_CONDITION_EX> QueryGroupCondition(int Group_ID);

        /// <summary>
        /// 删除客户分组
        /// </summary>
        /// <param name="Group_ID"></param>
        /// <returns></returns>
        int DeleteCustGroup(int ID);

        /// <summary>
        /// 查询客户疗程信息 不显示已用完的疗程
        /// </summary>
        /// <param name="CUST_CARD_ID"></param>
        /// <returns></returns>
        List<CUST_CDETAIL_EX> QueryCust_CDETAIL(int CUST_ID);

        /// <summary>
        /// 查询客户疗程信息 显示已用完的疗程
        /// </summary>
        /// <param name="CUST_CARD_ID"></param>
        /// <returns></returns>
        List<CUST_CDETAIL_EX> QueryCust_CDETAIL_ShowAll(int CUST_ID);

        CUST_INFO GetCustomerByFromUserName(string fromUserName);

        #region 会员扩展项
        /// <summary>
        /// 根据父级编号获得子数据字典
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        PagedList<CUST_EXINFO_CONFIG_EX> GetCUST_EXINFO_CONFIGByParentId(Guid pId, PageView view, int ORG_ID);

        /// <summary>
        /// 获取一个数据
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        List<CUST_EXINFO_CONFIG_EX> GetAllCUST_EXINFO_CONFIG(int orgId);

        /// <summary>
        /// 根据字典编号获得对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        CUST_EXINFO_CONFIG_EX GetCUST_EXINFO_CONFIGById(Guid id);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="cuObj"></param>
        /// <returns></returns>
        int DeleteCUST_EXINFO_CONFIG(Guid id);

        int SaveCUST_EXINFO_CONFIG(CUST_EXINFO_CONFIG cuObj);

        /// <summary>
        /// 获取会员扩展信息设置
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>

        List<ExpandItem> GetMemberCustExpandInfo(int ORG_ID, int cust_id);


        List<CUST_EXINFO_CONFIG_EX> GetFirstCUST_EXINFO(int orgId);
        List<CUST_EXINFO_CONFIG_EX> GetSecondCUST_EXINFO(string parentid, int orgId);
        CUST_EXINFO_CONFIG_EX GetParentCUST_EXINFO(string id, int orgId);
        int SaveCUST_EXINFO_DATA(CUST_EXINFO_DATA data);
        List<CUST_EXINFO_DATA> GetEXINFO_DATA(int custid);
        int DeleteEXINFO_DATA(int custid);



        #endregion

        CUST_INFO GetCustomerByMobile(string mobile, string org_id);

        ORG_INFO GetOrgInfo();
        List<CUST_CONSUME_DETAIL_EX> QueryCustConsume(string custid, string cardid, string orderstatus);
        List<PROMOTION_VOUCHER> GetCertiFicateList(string cid, string status);
        CUST_INFO LoginByMobile(string mobile, string pwd);
        //保存会员上传头像
        int SaveFiles(FILES file);

        /// <summary>
        /// 判断是否是散客
        /// </summary>
        /// <param name="cust_id"></param>
        /// <returns>true散客 false会员</returns>
        bool IS_VISITOR(int CUST_ID);

        // 获取客户完整信息
        CUST_INFO_EX GetCustomerFullInfoById(string cid, int orgid);

        /// <summary>
        /// 检查是否有重复卡号
        /// </summary>
        /// <param name="cardNO"></param>
        /// <param name="orgID"></param>
        /// <returns></returns>
        int CheckNewCardNo(string cardNO, int orgID);

        /// <summary>
        /// 保存顾客信息（事务提交 防止并发）
        /// </summary>
        /// <param name="customer">顾客详细</param>
        /// <param name="errMsg">错误消息</param>
        /// <remarks>created by likui.liu</remarks>
        /// <returns></returns>
        int SaveCustomerInfo(CUST_INFO customer, out string errMsg);

        /// <summary>
        /// 获取顾客消费信息
        /// </summary>
        /// <param name="custID">顾客ID</param>
        /// <remarks>created by likui.liu</remarks>
        /// <returns></returns>
        /// <returns></returns>
        CUST_SUMMARY GetCustSummaryById(string custID);

        /// <summary>
        /// 统计顾客的消费信息
        /// </summary>
        /// <param name="custID"></param>
        /// <returns></returns>
        CUST_SUMMARY StatisticsCustConsumeInfo(string custID);

        /// <summary>
        /// 新增顾客基本信息
        /// </summary>
        /// <param name="custInfo">顾客实体信息</param>
        /// <param name="errMsg">错误消息</param>
        /// <remarks>created by likui.liu</remarks>
        /// <returns></returns>
        int CreateCustomer(CUST_INFO custInfo, out string errMsg);

        /// <summary>
        /// 修改顾客基本信息
        /// </summary>
        /// <param name="custInfo">顾客实体信息</param>
        /// <param name="errMsg">错误消息</param>
        /// <remarks>created by likui.liu</remarks>
        /// <returns></returns>
        int UpdateCustomerInfoByID(CUST_INFO custInfo, out string errMsg);

        /// <summary>
        /// 修改顾客的微信openid
        /// </summary>
        /// <param name="custInfo">顾客实体信息</param>
        /// <remarks>created by likui.liu</remarks>
        /// <returns></returns>
        int UpdateCustOpenIDByID(CUST_INFO custInfo);


        int GetCustCountToday(int storeID);

        /// <summary>
        /// 查询消费的顾客列表
        /// </summary>
        /// <param name="search"></param>
        /// <param name="pageView"></param>
        /// <remarks>created by likui.liu</remarks>
        /// <returns></returns>
        PagedList<CUST_INFO_EX> GetConsumeCustList(CustomerSearch search, PageView pageView);

        /// <summary>
        /// 查询在时间段内没有消费的顾客列表
        /// </summary>
        /// <param name="search"></param>
        /// <param name="pageView"></param>
        /// <param name="months"></param>
        /// <remarks>created by likui.liu</remarks>
        /// <returns></returns>
        PagedList<CUST_INFO_EX> GetUnConsumeCustListInMonths(CustomerSearch search, PageView pageView, int months);

        CUST_INFO_EX GetCustByCardNO(string cardNO, int orgID);

        int SaveCustCardMerge(CUST_CARD outCardInfo, CUST_CARD inCardInfo, int orgID);
        int BuLCCard(CUST_CARD cardInfo, List<CUST_CDETAIL> list);

        PagedList<CUST_INFO_EX> QueryCustBirthdayList(CustomerSearch search, PageView view);

        int QueryCustBirthdayCount(CustomerSearch search);

        List<CUST_INFO_EX> QueryCustBirthdayListExcel(CustomerSearch search, PageView view);

        int UpdateCustInfoByID(CUST_INFO entity);

        /// <summary>
        /// 修改顾客卡级的状态
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        int UpdateCustCardStatus(CUST_CARD card);

        /// <summary>
        /// 根据手机号获取客户信息
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="orgNO"></param>
        /// <returns></returns>
        CUST_INFO GetCustByMobile(string mobile, string orgNO);
        /// <summary>
        /// 修改客户微信OpenID
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="orgNO"></param>
        /// <returns></returns>
        int UpdateCustFromUserNameByID(CUST_INFO entity, string orgNO);
        /// <summary>
        /// 微信注册
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="orgNO"></param>
        /// <returns></returns>
        int CustSignupForWechat(CUST_INFO entity, string orgNO);

        int CustOfferCoupon(string wxOpenID, int prodCardId, string orgNO);

        void CustBirthdayNotify();
        /// <summary>
        /// 赠送电子券
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        int SaveGivingVoucher(CUST_CARD item);
    }
}
