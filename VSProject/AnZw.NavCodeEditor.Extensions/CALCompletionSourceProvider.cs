using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Operations;
using Microsoft.VisualStudio.Utilities;

namespace AnZw.NavCodeEditor.Extensions
{

    [ContentType("C/AL"), Name("AZ AL completion source provider"), Order(After = "AL completion source provider"), Export(typeof(ICompletionSourceProvider))]
    public class CALCompletionSourceProvider : ICompletionSourceProvider
    {

        [Import]
        internal ITextStructureNavigatorSelectorService NavigatorService { get; set; }

        public ICompletionSource TryCreateCompletionSource(ITextBuffer textBuffer)
        {
            return new CALCompletionSource(this, textBuffer);
        }


    }
}
