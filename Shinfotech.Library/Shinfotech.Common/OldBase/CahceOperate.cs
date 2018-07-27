using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Caching;
using System.Collections;

namespace ShInfoTech.Common
{
    /// <summary>
    /// 缓存处理类
    /// </summary>
    public class CahceOperate
    {

        /// <summary>
        /// 判断缓存是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool CheckCache(string key)
        {
            if (HttpRuntime.Cache[key] == null)
            {
                return false;
            }
            return true;
        }


        /// <summary>
        /// 清除所有的缓存
        /// </summary>
        public static void ClearAllCache()
        {
            Cache _cache = HttpRuntime.Cache;
            IDictionaryEnumerator CacheEnum = _cache.GetEnumerator();
            ArrayList al = new ArrayList();
            while (CacheEnum.MoveNext())
            {
                al.Add(CacheEnum.Key);
            }
            foreach (string key in al)
            {
                _cache.Remove(key);
            }
        }


        /// <summary>
        /// 得到所有的缓存
        /// </summary>
        /// <returns></returns>
        public static string GetAllCache()
        {
            StringBuilder strTxt = new StringBuilder();
            Cache _cache = HttpRuntime.Cache;
            IDictionaryEnumerator CacheEnum = _cache.GetEnumerator();
            ArrayList al = new ArrayList();
            while (CacheEnum.MoveNext())
            {
                al.Add(CacheEnum.Key);
            }
            foreach (string key in al)
            {
                strTxt.Append(_cache[key] as string);
            }
            return strTxt.ToString();
        }

        /// <summary>
        /// 从缓存中取值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object GetFromCache(string key)
        {
            return HttpRuntime.Cache[key];
        }


        /// <summary>
        /// 从缓存中移除
        /// </summary>
        /// <param name="key"></param>
        public static void RemoveToCache(string key)
        {
            HttpRuntime.Cache.Remove(key);
        }


        /// <summary>
        /// 写入缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SaveToCache(string key, object value)
        {
            HttpRuntime.Cache.Insert(key, value, null, DateTime.Now.AddMinutes(10.0), TimeSpan.Zero);
        }
    }
}
