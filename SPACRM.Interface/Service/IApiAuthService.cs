using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SPACRM.Interface
{
    public interface IApiAuthService
    {
        /// <summary>
        /// OAuth认证
        /// </summary>
        /// <param name="appID">商户ID</param>
        /// <param name="timestamp">时间戳</param>
        /// <param name="signature">签名</param>
        /// <param name="ip">客户端IP</param>
        /// <param name="errorMsg">返回消息</param>
        /// <remarks>create by likui.liu</remarks>
        /// <returns>是否认证成功</returns>
        bool Auth(string appID, string timestamp, string signature, string ip, out string errorMsg);
    }
}
