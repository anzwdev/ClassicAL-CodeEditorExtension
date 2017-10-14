using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnZw.NavCodeEditor.Extensions.Reflection
{
    public class ReflectionTypeProxy
    {

        public Type ObjectType { get; }
        
        public Dictionary<string, MethodInfo> MethodsCache { get; }
        public Dictionary<string, PropertyInfo> PropertiesCache { get; }

        public ReflectionTypeProxy(Type objectType)
        {
            this.ObjectType = objectType;
            this.MethodsCache = new Dictionary<string, MethodInfo>();
            this.PropertiesCache = new Dictionary<string, PropertyInfo>();
        }

        protected MethodInfo GetMethod(string methodName)
        {
            if (this.MethodsCache.ContainsKey(methodName))
                return this.MethodsCache[methodName];
            MethodInfo methodInfo = this.ObjectType.GetMethod(methodName, BindingFlags.Instance |
                BindingFlags.NonPublic |
                BindingFlags.Public);
            this.MethodsCache.Add(methodName, methodInfo);
            return methodInfo;
        }

        public object CallMethod(object sourceObject, string methodName, params object[] parameters)
        {
            MethodInfo methodInfo = GetMethod(methodName);
            return methodInfo.Invoke(sourceObject, parameters);
        }

        protected PropertyInfo GetProperty(string propertyName)
        {
            if (this.PropertiesCache.ContainsKey(propertyName))
                return this.PropertiesCache[propertyName];
            PropertyInfo propertyInfo = this.ObjectType.GetProperty(propertyName, BindingFlags.Instance |
                        BindingFlags.NonPublic |
                        BindingFlags.Public);
            this.PropertiesCache.Add(propertyName, propertyInfo);
            return propertyInfo;
        }

        public object GetProperty(object sourceObject, string propertyName)
        {
            PropertyInfo propertyInfo = GetProperty(propertyName);
            return propertyInfo.GetValue(sourceObject);
        }

        public void SetProperty(object sourceObject, string propertyName, object newValue)
        {
            PropertyInfo propertyInfo = GetProperty(propertyName);
            propertyInfo.SetValue(sourceObject, newValue);
        }

    }
}
