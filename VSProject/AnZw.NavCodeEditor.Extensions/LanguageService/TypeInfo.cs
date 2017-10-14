using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnZw.NavCodeEditor.Extensions.Reflection;

namespace AnZw.NavCodeEditor.Extensions.LanguageService
{
    public class TypeInfo : ReflectionWrapper
    {

        public string VariableName { get; }
        public string ObjectName { get; }
        public string DataTypeName { get; }

        public TypeInfo(object source) : base(source)
        {
            this.VariableName = GetProperty<string>("VariableName");
            this.ObjectName = GetProperty<string>("ObjectName");
            this.DataTypeName = GetProperty<string>("DataTypeName");

        }

}
}
