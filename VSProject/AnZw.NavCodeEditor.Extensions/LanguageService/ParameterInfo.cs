using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnZw.NavCodeEditor.Extensions.Reflection;

namespace AnZw.NavCodeEditor.Extensions.LanguageService
{
    public class ParameterInfo : ReflectionWrapper
    {

        public string TypeName { get; }
        public string ParameterName { get; }

        public ParameterInfo(object source) : base(source)
        {
            this.TypeName = GetProperty<string>("TypeName");
            this.ParameterName = GetProperty<string>("ParameterName");
        }

    }
}
