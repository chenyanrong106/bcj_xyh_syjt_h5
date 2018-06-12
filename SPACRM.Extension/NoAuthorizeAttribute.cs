using System;

namespace SPACRM.Extension
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class NoAuthorizeAttribute : Attribute
    {
    }
}
