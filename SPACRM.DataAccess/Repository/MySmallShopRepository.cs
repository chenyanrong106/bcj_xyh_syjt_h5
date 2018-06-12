using SPACRM.Entity;
using SPACRM.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SPACRM.DataAccess.Repository
{
    public class MySmallShopRepository : BaseRepository
    {

        /// <summary>
        /// 获取门店城市
        /// </summary>
        /// <param name="ToUserName"></param>
        /// <returns></returns>
        public List<string> GetCityList(string ToUserName)
        {
            string sql = "SELECT s.CITY FROM dbo.WD_STORE s LEFT JOIN dbo.ORG_INFO m ON s.MERCHANT_ID=m.ID WHERE ToUserName=@ToUserName GROUP BY s.CITY";
            return base.Query<string>(sql, new { ToUserName = ToUserName });
        }



        /// <summary>
        /// 判断用户编号与用户名是否已存在
        /// </summary>
        /// <param name="MERCHANT_NO"></param>
        /// <param name="USER_NAME"></param>
        /// <returns></returns>
        public ORG_INFO GetWDByUserNo(string MERCHANT_NO, string USER_NAME)
        {
            string sql = "SELECT ID,ORG_NAME,ToUserName,AppID,Appsecret,Access_token,OneOpenID,JSapi_Ticket,GetTicketTime FROM dbo.ORG_INFO WHERE USER_NAME=@USER_NAME and MERCHANT_NO=@MERCHANT_NO";
            return Get<ORG_INFO>(sql, new { USER_NAME = USER_NAME, MERCHANT_NO = MERCHANT_NO });
        }

        /// <summary>
        /// 保存授权信息
        /// </summary>
        /// <param name="oa"></param>
        /// <returns></returns>
        public int SaveOAtuh(OAauth_Log oa)
        {
            if (oa.ID == 0)
                return (int)Insert(oa);
            else return Update(oa);
        }

        /// <summary>
        /// 获取授权信息
        /// </summary>
        /// <param name="FromUserName"></param>
        /// <returns></returns>
        public OAauth_Log GetOA(string FromUserName)
        {
            string sql = "SELECT * FROM dbo.OAauth_Log WHERE FromUserName=@openid";
            return Get<OAauth_Log>(sql, new { openid = FromUserName });
        }

        /// <summary>
        /// 根据绑定手机获取用户信息
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public OAauth_Log GetOAByPhone(string phone)
        {
            string sql = "SELECT * FROM dbo.OAauth_Log WHERE phone=@phone";
            return Get<OAauth_Log>(sql, new { phone = phone });
        }

        /// <summary>
        /// 删除用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeteleOA(int id)
        {
            string sql = "delete oaauth_log where id=@id";
            return Excute(sql, new { id = id });
        }

        /// <summary>
        /// 获取商户信息  供pos接口
        /// </summary>
        /// <param name="ToUserName"></param>
        /// <returns></returns>
        public ORG_INFO GetWD(string ToUserName)
        {
            //string sql = "select ID,ORG_NAME,ToUserName,AppID,Appsecret,Access_token,OneOpenID,JSapi_Ticket,GetTicketTime from ORG_INFO where tousername=@tousername";
            //return Get<ORG_INFO>(sql, new { tousername = ToUserName });
            if (ToUserName != null)
            {
                string sql = "select ID,ORG_NAME,ToUserName,AppID,Appsecret,Access_token,OneOpenID,JSapi_Ticket,GetTicketTime from ORG_INFO where tousername=@tousername";
                return Get<ORG_INFO>(sql, new { tousername = ToUserName });
            }
            else
            {
                string sql = "select top 1 ID,ORG_NAME,ToUserName,AppID,Appsecret,Access_token,OneOpenID,JSapi_Ticket,GetTicketTime from ORG_INFO";
                return Get<ORG_INFO>(sql, null);
            }
        }

        /// <summary>
        /// 获取商户信息  供pos接口
        /// </summary>
        /// <param name="ToUserName"></param>
        /// <returns></returns>
        public ORG_INFO GetWD(int mid)
        {
            string sql = "select ID,ORG_NAME,ToUserName,AppID,Appsecret,Access_token,OneOpenID,JSapi_Ticket,GetTicketTime from ORG_INFO where  id=@tousername";
            return Get<ORG_INFO>(sql, new { tousername = mid });
        }

        /// <summary>
        /// 获取商户信息  用于验证重复授权
        /// </summary>
        /// <param name="ToUserName"></param>
        /// <returns></returns>
        public ORG_INFO GetWD(string ToUserName, int id)
        {
            string sql = "select ID,ORG_NAME,ToUserName,AppID,Appsecret,Access_token,OneOpenID,JSapi_Ticket,GetTicketTime from ORG_INFO where tousername=@tousername and id<>@id";
            return Get<ORG_INFO>(sql, new { tousername = ToUserName, id = id });
        }

        public int GetCountPage()
        {
            string sql = "SELECT COUNT(*) FROM dbo.WX_PageLog WHERE Page LIKE 'http://www.meijiewd.com/wechat/mysmallshop/test.aspx%'";
            return Get<int>(sql, null);
        }

        /// <summary>
        /// 删除配置信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteStoreSet(int id)
        {
            string sql = "delete WD_STORE_SET where id=@id";
            return base.Excute(sql, new { id = id });
        }


        /// <summary>
        /// 删除所有门店信息
        /// </summary>
        /// <returns></returns>
        public int deleteWD_STORE()
        {
            string sql = "delete WD_STORE";
            return base.Excute(sql, null);
        }

        ///// <summary>
        ///// 获取绑定会员
        ///// </summary>
        ///// <param name="openid"></param>
        ///// <returns></returns>
        //public CUST_INFO GetCust(string openid)
        //{
        //    string sql = "select * from CUST_INFO where FROM_USER_NAME=@openid";
        //    return Get<CUST_INFO>(sql, new { openid = openid });
        //}

        /// <summary>
        /// 获取授权信息
        /// </summary>
        /// <param name="FromUserName"></param>
        /// <returns></returns>
        public OAauth_Log GetOA(int id)
        {
            string sql = "SELECT * FROM dbo.OAauth_Log WHERE id=@id";
            return Get<OAauth_Log>(sql, new { id = id });
        }

        /// <summary>
        /// 查询是否有授权记录
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        public int GetISOA(string openid)
        {
            return Get<int>("SELECT COUNT(1) FROM dbo.OAauth_Log WHERE FromUserName=@openid", new { openid = openid });
        }

        /// <summary>
        /// 查询当天是否有授权记录
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        public int GetOAByDay(string openid)
        {
            return Get<int>("SELECT COUNT(1) FROM dbo.OAauth_Log WHERE FromUserName=@openid and DATEDIFF(dd,GETDATE(),CreateDate)=0", new { openid = openid });
        }

        ///// <summary>
        ///// 保存微信模板消息发送记录
        ///// </summary>
        ///// <param name="mlog"></param>
        ///// <returns></returns>
        //public int SaveMessageLog(ORG_WXMessage_Log mlog)
        //{
        //    if (mlog.ID == 0)
        //        return (int)Insert(mlog);
        //    else
        //        return Update(mlog);
        //}

        
        /// <summary>
        /// 获取所有粉丝
        /// </summary>
        /// <returns></returns>
        public List<WXCUST_FANS> GetAllFans()
        {
            return Query<WXCUST_FANS>("SELECT * FROM dbo.WXCUST_FANS", null);
        }

        ///// <summary>
        ///// 获取微信排名
        ///// </summary>
        ///// <param name="mjuserid"></param>
        ///// <param name="openid"></param>
        ///// <returns></returns>
        //public List<WXFansRank> HuoQuTop5(string mjuserid)
        //{
        //    string sql = @"select 
        //                    ROW_NUMBER() OVER(order by count(distinct a.ToUserName) desc  ,max(a.CreateTime)  asc) rowNumber,
        //                    b.Nickname,
        //                    b.headimgurl,
        //                    b.FromUserName,
        //                    count(distinct a.ToUserName) InviteCnt ,
        //                    max(a.CreateTime) CreateTime
        //                    from OAauth_Log  b with(nolock)
        //                    left join Cust_Old_New a with(nolock) on a.FromUserName=b.FromUserName  and Jie=2   
        //                    where b.ToUserName=@ToUserName and isnull(SeaImg,'')!=''
        //                    group by b.FromUserName,b.Nickname,b.headimgurl
        //                     order by rowNumber asc";

        //    return base.Query<WXFansRank>(sql, new { ToUserName = mjuserid });
        //}


        public int UpdateCustOldNewJie(string OpenID)
        {

            //update Cust_Old_New set jie = -1 where ToUserName = '' and jie = 2  //老带新关系
            //update Cust_Old_New set jie = -1 where FromUserName = '' and ToUserName = 'fx' //分享关系
            string sql = "update Cust_Old_New set jie = -1 where ToUserName = @OpenID and jie = 2";
           int ret= base.Excute(sql, new { OpenID = OpenID });
            if (ret > 0)
            {
                sql = "update Cust_Old_New set jie = -1 where FromUserName = @OpenID  and ToUserName = 'fx'";
                ret = base.Excute(sql, new { OpenID = OpenID });
            }
            return ret;

        }

        public int InsertPVData(PVData pv)
        {
            string sql = @"insert into pv_data_collect(DOMAIN,URL,TITLE,REFERRER,SH,SW,CD,ACCOUNT,LTIME)
                            values(@DOMAIN,@URL,@TITLE,@REFERRER,@SH,@SW,@CD,@ACCOUNT,@LTIME)";
            return base.Excute(sql, new {
                DOMAIN =pv.DOMAIN,
                URL=pv.URL,
                TITLE=pv.TITLE,
                REFERRER=pv.REFERRER,
                SH=pv.SH,
                SW=pv.SW,
                CD=pv.CD,
                ACCOUNT=pv.ACCOUNT,
                LTIME=pv.LTIME
            });
        }
    }
}
