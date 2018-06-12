﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SPACRM.Common
{
    /// <summary>
    /// 公共的帮助类
    /// </summary>
    public class Utility
    {
        public static string ClearSafeStringParma(string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                return input.Replace("--", "").Replace("'", "").Replace(";", "；");
            }
            return "";
        }
        public static string Base64EnCode(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return "";
            }
            byte[] b = Encoding.UTF8.GetBytes(input);
            return Convert.ToBase64String(b);
        }
        public static string Base64Decode(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return "";
            }
            byte[] b = Convert.FromBase64String(input);
            return Encoding.UTF8.GetString(b);
        }
    }
}
