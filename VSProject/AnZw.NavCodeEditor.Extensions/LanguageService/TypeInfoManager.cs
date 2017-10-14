using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnZw.NavCodeEditor.Extensions.Reflection;

namespace AnZw.NavCodeEditor.Extensions.LanguageService
{
    public class TypeInfoManager : ReflectionWrapper
    {

        public TypeInfoManager(object source) : base(source)
        {
        }

        public IEnumerable<SignatureInfo> GetSignatures(string methodName)
        {
            IEnumerable sourceSignatureList = CallMethod<IEnumerable>("GetSignatures", methodName);
            if (sourceSignatureList == null)
                return null;

            List<SignatureInfo> signatures = new List<SignatureInfo>();
            foreach (object sourceSignature in sourceSignatureList)
            {
                signatures.Add(new SignatureInfo(sourceSignature));
            }

            return signatures;
        }

        public IEnumerable<FieldInfo> GetFields(string owner)
        {
            IEnumerable sourceFieldList = CallMethod<IEnumerable>("GetFields", owner);
            if (sourceFieldList == null)
                return null;

            List<FieldInfo> fields = new List<FieldInfo>();
            foreach (object sourceField in sourceFieldList)
            {
                fields.Add(new FieldInfo(sourceField));
            }

            return fields;
        }

    }
}
