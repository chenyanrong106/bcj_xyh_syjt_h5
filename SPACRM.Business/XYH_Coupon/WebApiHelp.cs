
using Hmj.WebService;
using log4net;
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
            try
            {
                si_Dyn_DispMemQuick_obService s2 = new si_Dyn_DispMemQuick_obService();

                string reqStr = js.Serialize(req);

                logger.Info("会员信息快速查询si_Dyn_DispMemQuick_ob请求信息"+ timeStart + "：" + reqStr);

                dt_Dyn_DispMemQuick_res dt2 = s2.si_Dyn_DispMemQuick_ob(req);
                string resStr = js.Serialize(dt2);
                DateTime timeEnd = DateTime.Now;

                logger.Info("响应信息" + timeStart + "：" + resStr + "\r\n");


                //datalog.DataInfo(reqStr, resStr, timeStart, timeEnd, 0, "si_Dyn_DispMemQuick_ob", "会员信息快速查询");
                return dt2;
            }
            catch (Exception ex)
            {
                logger.Info("响应信息" + timeStart + "：" + ex.Message + "\r\n");
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

            try
            {
                timeStart = DateTime.Now;
                logger.Info("发送短信si_SMSInsert_real_obService请求信息" + timeStart + "：" + reqStr);
                si_SMSInsert_real_obService web = new si_SMSInsert_real_obService();

                dt_SMSInsert_res res = web.si_SMSInsert_real_ob(req);

                resStr = js.Serialize(res);
                timeEnd = DateTime.Now;
                logger.Info("响应信息" + timeStart + "：" + resStr + "\r\n");

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
                logger.Info("响应信息" + timeStart + "：" + resStr + "\r\n");
                return null;
            }
            finally
            {
                if (!isdebug)
                {
                    //datalog.DataInfo(reqStr, resStr, timeStart, timeEnd, 0, SystemCode.SMSInsert.ToString(), isok);
                }
            }

        }

    }
}
