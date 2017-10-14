using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnZw.NavCodeEditor.Extensions.Reflection;

namespace AnZw.NavCodeEditor.Extensions.LanguageService
{
    public class TypeInfoCache : ReflectionWrapper
    {

        public TypeInfoCache(object source) : base(source)
        {
        }

        public IList<TypeInfo> Get(object key)
        {
            ReflectionWrapper reflectionWrapperKey = key as ReflectionWrapper;
            if (reflectionWrapperKey != null)
                key = reflectionWrapperKey.ReflectionWrapperSourceObject;

            IEnumerable sourceList = CallMethod<IEnumerable>("Get", key);
            List<TypeInfo> typeList = new List<TypeInfo>();
            foreach (object sourceTypeInfo in sourceList)
            {
                typeList.Add(new TypeInfo(sourceTypeInfo));
            }
            return typeList;
        }

    }
}
