using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SPACRM.Common.CacheManager
{
   public class CacheFactory
    {
       private static CacheFactory _instance = new CacheFactory();
       public static CacheFactory Instance
        {
            get
            {
                return _instance;
            }
        }

        public ICache CreateCoreCacheInstance()
       {
           //return new AspNetCache();
           return AspNetCache.CreateInstance();
        }

        public ICache CreateSessionCacheInstance()
        {
            return new SessionCache();
        }
    }
}
