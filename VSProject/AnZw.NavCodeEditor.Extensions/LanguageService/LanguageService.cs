using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Text.Editor;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Microsoft.VisualStudio.Text.Operations;
using AnZw.NavCodeEditor.Extensions.Reflection;

namespace AnZw.NavCodeEditor.Extensions.LanguageService
{
    public class LanguageService : ReflectionWrapper
    {

        public LanguageService(object source) : base(source)
        {
        }

        private TypeInfoCache _locals = null;
        public TypeInfoCache Locals
        {
            get
            {
                if (_locals == null)
                    _locals = new TypeInfoCache(GetProperty("Locals"));
                return _locals;
            }
        }

        private TypeInfoCache _globals = null;
        public TypeInfoCache Globals
        {
            get
            {
                if (_globals == null)
                    _globals = new TypeInfoCache(GetProperty("Globals"));
                return _locals;
            }
        }

    }
}
