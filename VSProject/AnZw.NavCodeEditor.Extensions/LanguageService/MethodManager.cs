using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnZw.NavCodeEditor.Extensions.Reflection;

namespace AnZw.NavCodeEditor.Extensions.LanguageService
{
    public class MethodManager : ReflectionWrapper, IMethodManager
    {

        public MethodManager(object source) : base(source)
        {
        }

        public IList<IMethod> Methods
        {
            get
            {
                List<IMethod> methodList = new List<IMethod>();
                IEnumerable sourceMethodList = GetProperty<IEnumerable>("Methods");
                foreach (object sourceMethod in sourceMethodList)
                {
                    methodList.Add(new Method(sourceMethod));
                }
                return methodList;
            }
        }

        public IMethod ActiveMethod
        {
            get
            {
                object sourceMethod = GetProperty("ActiveMethod");
                if (sourceMethod == null)
                    return null;
                return new Method(sourceMethod);
            }
        }

    }
}
