using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnZw.NavCodeEditor.Extensions.Reflection
{
    public class ReflectionTypeProxyCache
    {

        private static Dictionary<string, ReflectionTypeProxy> _proxyCache = new Dictionary<string, ReflectionTypeProxy>();

        public static ReflectionTypeProxy GetReflectionTypeProxy(Type objectType)
        {
            string key = objectType.FullName;
            if (_proxyCache.ContainsKey(key))
                return _proxyCache[key];
            ReflectionTypeProxy proxyObject = new ReflectionTypeProxy(objectType);
            _proxyCache.Add(key, proxyObject);
            return proxyObject;
        }

    }
}
