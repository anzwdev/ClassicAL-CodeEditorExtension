using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnZw.NavCodeEditor.Extensions.Reflection;

namespace AnZw.NavCodeEditor.Extensions.LanguageService
{
    public class Context : ReflectionWrapper
    {

        public Context(object source) : base(source)
        {
        }

        private LanguageService _languageService = null;
        public LanguageService LanguageService
        {
            get
            {
                if (_languageService == null)
                    _languageService = new LanguageService(GetProperty("LanguageService"));
                return _languageService;
            }
        }

    }
}
