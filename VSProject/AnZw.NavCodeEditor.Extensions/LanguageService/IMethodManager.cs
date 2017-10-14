using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnZw.NavCodeEditor.Extensions.LanguageService
{
    public interface IMethodManager
    {

        IList<IMethod> Methods { get; }
        IMethod ActiveMethod { get; }

    }
}
