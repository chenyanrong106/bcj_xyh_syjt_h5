using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SPACRM.Common.Utils
{
    public class JsonHelper
    {
        public static string ToJSON(object o)
        { 

            IsoDateTimeConverter timeConverter = new IsoDateTimeConverter();
            //这里使用自定义日期格式，如果不使用的话，默认是ISO8601格式     
            timeConverter.DateTimeFormat = "yyyy'-'MM'-'dd' 'HH':'mm':'ss";
            //listUser是准备转换的对象
            return JsonConvert.SerializeObject(o, timeConverter);
        }
        public static T DeserializeObject<T>(string json) where T : class
        {
            T t = JsonConvert.DeserializeObject<T>(json);
            return t;
        }
    }
}
