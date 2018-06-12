using SPACRM.Interface;
using StructureMap.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace SPACRM.Business
{
    public class BaseService
    {

      


        /// <summary>
        /// 异步执行
        /// </summary>
        /// <param name="action"></param>
        public void RunAsync(Action action)
        {
            try
            {
                Task.Factory.StartNew(action);
            }
            catch(Exception ex)
            {
                //LogService.Error("异步异常：" + ex.Message);
            }
        }
    }
}
