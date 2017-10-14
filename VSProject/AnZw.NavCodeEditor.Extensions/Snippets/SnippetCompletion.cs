using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Language.Intellisense;
using AnZw.NavCodeEditor.Extensions;

namespace AnZw.NavCodeEditor.Extensions.Snippets
{
    public class SnippetCompletion : Completion
    {

        public Snippet Snippet { get; }
        public int Indent { get; }

        public SnippetCompletion(Snippet snippet, int indent)
        {
            this.Snippet = snippet;
            this.Description = Snippet.Description;
            this.DisplayText = Snippet.Name;
            this.Indent = indent;
        }

        public override string InsertionText
        {
            get
            {
                return Session.Current.SnippetManager.ParseSnippet(this.Snippet, this.Indent, null);
            }
            set { }
        }

    }
}
