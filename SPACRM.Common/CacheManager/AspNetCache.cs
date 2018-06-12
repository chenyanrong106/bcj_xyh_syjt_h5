﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Caching;
using System.Collections;
using System.Web;

namespace SPACRM.Common.CacheManager
{
    /// <summary>
    /// 全局级别的缓存
    /// </summary>
    public class AspNetCache : ICache
    {
        #region readonly
        private readonly Cache _cache = HttpRuntime.Cache;
        private readonly string _cacheName;
        #endregion

        #region 字段
        private CacheItemPriority _priority = CacheItemPriority.Default;//默认优先级
        private bool _slidingExpiration = true;//默认  是否 滑动过期

        private bool _enforceTimeToLive = false;//默认 强制统一的过期时间
        private TimeSpan _timeToLive = new TimeSpan(3, 00, 0);//TimeSpan.Zero;//默认3小时过期

        private static AspNetCache _instance = null;
        private static object _lockobject = new object();

        #endregion

        #region 属性
        public bool SlidingExpiration
        {
            get { return _slidingExpiration; }
            set { _slidingExpiration = value; }
        }

        public CacheItemPriority Priority
        {
            get { return _priority; }
            set { _priority = value; }
        }
        public TimeSpan TimeToLive
        {
            get { return _timeToLive; }
            set { _timeToLive = value; }
        }
        #endregion

        #region 构造
        public AspNetCache()
        {
            _cacheName = typeof(AspNetCache).FullName + "[" + this.GetHashCode() + "].";
        }
        #endregion

        #region ICache 成员
        public static AspNetCache CreateInstance()
        {
            if (_instance == null)
            {
                lock (_lockobject)
                {
                    if (_instance == null)
                    {
                        _instance = new AspNetCache();
                    }
                }
            }
            return _instance;
        }


        public int Count
        {
            get
            {
                return Keys.Count;
            }
        }

        public IList<string> Keys
        {
            get
            {
                IList<string> keys = new List<string>();

                foreach (DictionaryEntry entry in _cache)
                {
                    string key = (string)entry.Key;
                    if (key.StartsWith(_cacheName))
                    {
                        keys.Add(key.Substring(_cacheName.Length));
                    }
                }
                return keys;
            }
        }

        public object Get(string key)
        {
            return key == null ? null : _cache.Get(GenerateKey(key));
        }

        public void Remove(string key)
        {
            if (key != null)
            {
                _cache.Remove(GenerateKey(key));
            }
        }



        public void Clear()
        {
            foreach (string key in this.Keys)
            {
                Remove(key);
            }
        }

        public void Insert(string key, object value)
        {

            Insert(key, value, TimeToLive);
        }

        public void Insert(string key, object value, TimeSpan timeToLive)
        {
            if (timeToLive < TimeSpan.Zero)
            {
                timeToLive = TimeSpan.Zero;
            }
            if (_enforceTimeToLive)
            {
                timeToLive = _timeToLive;
            }
            this.DoInsert(key, value, timeToLive, _slidingExpiration, _priority);
        }

        #endregion

        #region 私有
        private void DoInsert(object key, object value, TimeSpan timeToLive, bool slidingExpiration, CacheItemPriority itemPriority)
        {
            if (TimeSpan.Zero < timeToLive) //有过期时间的
            {
                if (slidingExpiration) // 滑动过期
                {
                    TimeSpan slidingExpirationSpan = timeToLive;
                    _cache.Insert(GenerateKey(key), value, null, Cache.NoAbsoluteExpiration, slidingExpirationSpan, itemPriority, null);
                }
                else//绝对过期
                {
                    DateTime absoluteExpiration = DateTime.Now.Add(timeToLive);
                    _cache.Insert(GenerateKey(key), value, null, absoluteExpiration, Cache.NoSlidingExpiration, itemPriority, null);
                }
            }
            else // 不过期
            {
                _cache.Insert(GenerateKey(key), value, null, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, itemPriority, null);
            }
        }
        private string GenerateKey(object key)
        {
            return _cacheName + key;
        }
        #endregion
    }
}
