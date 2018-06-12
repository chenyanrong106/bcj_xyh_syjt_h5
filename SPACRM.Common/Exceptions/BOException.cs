﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SPACRM.Common.Exceptions
{
    public class BOException : Exception
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message"></param>
        public BOException(string message)
            : base(message)
        { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public BOException(string message, Exception ex)
            : base(message, ex)
        { }
    }
}
