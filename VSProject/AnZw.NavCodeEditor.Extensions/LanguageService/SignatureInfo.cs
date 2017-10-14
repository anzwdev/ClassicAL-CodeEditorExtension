using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnZw.NavCodeEditor.Extensions.Reflection;

namespace AnZw.NavCodeEditor.Extensions.LanguageService
{
    public class SignatureInfo : ReflectionWrapper
    {

        public string ParentTypeName { get; set; }
        public string MethodName { get; set; }
        public ParameterInfo ReturnType { get; set; }
        public List<ParameterInfo> Parameters { get; }
        public string FormattedValue { get; set; }

        public SignatureInfo(object source) : base(source)
        {
            this.Parameters = new List<ParameterInfo>();

            //copy properties
            this.ParentTypeName = GetProperty<string>("ParentTypeName");
            this.MethodName = GetProperty<string>("MethodName");
            this.FormattedValue = GetProperty<string>("FormattedValue");

            object sourceReturnType = GetProperty("ReturnType");
            if (sourceReturnType == null)
                this.ReturnType = null;
            else
                this.ReturnType = new ParameterInfo(sourceReturnType);

            IEnumerable sourceParameters = GetProperty<IEnumerable>("Parameters");
            if (sourceParameters != null)
            {
                foreach (object sourceParameter in sourceParameters)
                {
                    this.Parameters.Add(new ParameterInfo(sourceParameter));
                }
            }
        }

}
}
