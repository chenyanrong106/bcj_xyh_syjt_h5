using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SPACRM.Interface
{
    public interface ILogService
    {
        /// <summary>
        /// Debugs the specified MSG.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        void Debug(string msg);
        /// <summary>
        /// Traces the specified MSG.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        void Trace(string msg);

        void Info(string msg);
        /// <summary>
        /// Warns the specified MSG.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        void Warn(string msg);
        /// <summary>
        /// Errors the specified MSG.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        void Error(string msg);
        /// <summary>
        /// Fatals the specified MSG.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        void Fatal(string msg);
       
    }
}
