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
using Microsoft.VisualStudio.Text.Editor;

namespace AnZw.NavCodeEditor.Extensions
{

    [TextViewRole("INTERACTIVE"), ContentType("C/AL"), Name("AL2IntellisensePreKeyProcessor"), Order(Before = "ALEditorKeyProcessor"), Export(typeof(IKeyProcessorProvider))]
    public class CALCompletionHandlerProvider : IKeyProcessorProvider
    {

        [Import]
        internal IIntellisenseSessionStackMapService StackMapService
        {
            get;
            set;
        }

        [Import]
        internal IEditorOperationsFactoryService EditorOperationsFactoryService = null;

        public KeyProcessor GetAssociatedProcessor(IWpfTextView textView)
        {
            if (textView == null)
            {
                throw new ArgumentNullException("textView");
            }
            IIntellisenseSessionStack stackForTextView = this.StackMapService.GetStackForTextView(textView);
            IEditorOperations editorOperations = EditorOperationsFactoryService.GetEditorOperations(textView);
            return new CALKeyProcessor(textView, editorOperations);
        }
    
    }

}
