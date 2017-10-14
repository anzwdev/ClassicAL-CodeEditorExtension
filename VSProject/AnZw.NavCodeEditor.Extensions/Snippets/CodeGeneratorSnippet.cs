using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnZw.NavCodeEditor.Extensions.LanguageService;
using AnZw.NavCodeEditor.Extensions.Snippets;
using AnZw.NavCodeEditor.Extensions.UI.CodeGenerators;

namespace AnZw.NavCodeEditor.Extensions.Snippets
{

    /// <summary>
    /// Snippet generating code
    /// </summary>
    public class CodeGeneratorSnippet : Snippet
    {

        public CodeGeneratorSnippet()
        {
        }

        public override string Run(CALKeyProcessor keyProcessor)
        {
            return null;
        }

        protected IList<TypeInfo> GetLocalTypes(CALKeyProcessor keyProcessor)
        {
            return keyProcessor.NavConnector.Context.LanguageService.Locals.Get(keyProcessor.MethodManager.ActiveMethod);
        }

        protected void AddRecordTypes(CALKeyProcessor keyProcessor, IList<TypeInfo> destList)
        {
            IList<TypeInfo> types = GetLocalTypes(keyProcessor);
            foreach (TypeInfo typeInfo in types)
            {
                if (typeInfo.DataTypeName == "Record")
                    destList.Add(typeInfo);
            }
        }

    }

}
