using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnZw.NavCodeEditor.Extensions.Reflection
{
    public class ReflectionWrapper
    {

        private object _source;
        private Type _sourceType;
        private ReflectionTypeProxy _proxy;

        public object ReflectionWrapperSourceObject
        {
            get { return _source; }
        }

        public ReflectionWrapper()
        {
            _source = null;
            _sourceType = null;
            _proxy = null;
        }

        public ReflectionWrapper(object source)
        {
            Initialize(source);
        }

        protected void Initialize(object source)
        {
            Initialize(source, source.GetType());
        }

        protected void Initialize(object source, Type sourceType)
        {
            _source = source;
            _sourceType = sourceType;
            _proxy = ReflectionTypeProxyCache.GetReflectionTypeProxy(_sourceType);
        }

        public object GetProperty(string propertyName)
        {
            return _proxy.GetProperty(_source, propertyName);
        }

        public T GetProperty<T>(string propertyName)
        {
            object value = GetProperty(propertyName);
            return (T)value;
        }

        public object CallMethod(string methodName, params object[] parameters)
        {
            return _proxy.CallMethod(_source, methodName, parameters);
        }

        public T CallMethod<T>(string methodName, params object[] parameters)
        {
            object retVal = CallMethod(methodName, parameters);
            return (T)retVal;
        }

    }
}
