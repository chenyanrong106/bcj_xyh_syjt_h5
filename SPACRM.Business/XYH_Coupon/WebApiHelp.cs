
using HmjNew.Service;
using log4net;
using SPACRM.Business.ServiceImpl;
using System;
using System.Web.Script.Serialization;

namespace Hmj.Business
{
    /// <summary>
    /// 调用web service
    /// </summary>
    public class WebApiHelp
    {
        //static Stopwatch sw = new Stopwatch();
        static JavaScriptSerializer js = new JavaScriptSerializer();
        private static ILog logger = LogManager.GetLogger("loginfo");

        //private static ILogService datalog = new LogService();


        /// <summary>
        /// 会员快速查询
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public static dt_Dyn_DispMemQuick_res DispMemQuick(dt_Dyn_DispMemQuick_req req)
        {
            DateTime timeStart = DateTime.Now;
            SystemService _set = new SystemService();
            try
            {
                HmjNew.Service.si_Dyn_DispMemQuick_obService s2 = new HmjNew.Service.si_Dyn_DispMemQuick_obService();

                string reqStr = js.Serialize(req);

                //logger.Info("会员信息快速查询si_Dyn_DispMemQuick_ob请求信息"+ timeStart + "：" + reqStr);
                
                _set.AddLog(new SPACRM.Entity.WXLOG()
                {
                    CON = "会员信息快速查询si_Dyn_DispMemQuick_ob请求信息" + timeStart + "：" + reqStr,
                    TIME = DateTime.Now
                });
                dt_Dyn_DispMemQuick_res dt2 = s2.si_Dyn_DispMemQuick_ob(req);
                string resStr = js.Serialize(dt2);
                DateTime timeEnd = DateTime.Now;
                
                _set.AddLog(new SPACRM.Entity.WXLOG()
                {
                    CON = "会员信息快速查询响应信息" + timeStart + "：" + resStr + "\r\n",
                    TIME = DateTime.Now
                });

                //datalog.DataInfo(reqStr, resStr, timeStart, timeEnd, 0, "si_Dyn_DispMemQuick_ob", "会员信息快速查询");
                return dt2;
            }
            catch (Exception ex)
            {
                _set.AddLog(new SPACRM.Entity.WXLOG()
                {
                    CON = "会员信息快速查询响应信息" + timeStart + "：" + ex.Message + "\r\n",
                    TIME = DateTime.Now
                });
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public static dt_SMSInsert_res SMSInsert(dt_SMSInsert_req req,
            bool isdebug = false)
        {
            string reqStr = js.Serialize(req);
            string resStr = string.Empty;
            DateTime? timeStart = null;
            DateTime? timeEnd = null;
            string isok = "OK";
            SystemService _set = new SystemService();
            try
            {
                timeStart = DateTime.Now;
                _set.AddLog(new SPACRM.Entity.WXLOG()
                {
                    CON = "发送短信si_SMSInsert_real_obService请求信息" + timeStart + "：" + reqStr,
                    TIME = DateTime.Now
                });
                si_SMSInsert_real_obService web = new si_SMSInsert_real_obService();

                dt_SMSInsert_res res = web.si_SMSInsert_real_ob(req);

                resStr = js.Serialize(res);
                timeEnd = DateTime.Now;
                _set.AddLog(new SPACRM.Entity.WXLOG()
                {
                    CON = "发送短信响应信息" + timeStart + "：" + resStr + "\r\n",
                    TIME = DateTime.Now
                });
                if (res == null)
                {
                    isok = "NO";
                }

                return res;
            }
            catch (Exception ex)
            {
                resStr = ex.Message;
                isok = "NO";
                _set.AddLog(new SPACRM.Entity.WXLOG()
                {
                    CON = "发送短信响应信息" + timeStart + "：" + resStr + "\r\n",
                    TIME = DateTime.Now
                });
                throw ex;
            }
            finally
            {
                if (!isdebug)
                {
                    //datalog.DataInfo(reqStr, resStr, timeStart, timeEnd, 0, SystemCode.SMSInsert.ToString(), isok);
                }
            }

        }

        /// <summary>
        /// 潜客创建
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public static dt_Dyn_CreateLead_res CreateLead(dt_Dyn_CreateLead_req req,
            bool isdebug = false)
        {
            string reqStr = js.Serialize(req);
            string resStr = string.Empty;
            DateTime? timeStart = null;
            DateTime? timeEnd = null;
            string isok = "OK";
            SystemService _set = new SystemService();
            try
            {
                timeStart = DateTime.Now;
                _set.AddLog(new SPACRM.Entity.WXLOG()
                {
                    CON = "潜客创建si_Dyn_CreateLead_obService请求信息" + timeStart + "：" + reqStr,
                    TIME = DateTime.Now
                });
                si_Dyn_CreateLead_obService web = new si_Dyn_CreateLead_obService();

                dt_Dyn_CreateLead_res res = web.si_Dyn_CreateLead_ob(req);

                resStr = js.Serialize(res);
                timeEnd = DateTime.Now;
                _set.AddLog(new SPACRM.Entity.WXLOG()
                {
                    CON = "潜客创建响应信息" + timeStart + "：" + resStr,
                    TIME = DateTime.Now
                });
                if (res.WV_RETURN == "N")
                {
                    isok = "NO";
                }

                return res;
            }
            catch (Exception ex)
            {
                resStr = ex.Message;
                isok = "NO";
                _set.AddLog(new SPACRM.Entity.WXLOG()
                {
                    CON = "潜客创建响应信息" + timeStart + "：异常" + resStr,
                    TIME = DateTime.Now
                });
                return null;
            }
            finally
            {

            }

        }


    }
}
