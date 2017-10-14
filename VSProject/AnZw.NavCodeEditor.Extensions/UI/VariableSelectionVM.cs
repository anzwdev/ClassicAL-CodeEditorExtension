using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using AnZw.NavCodeEditor.Extensions;
using AnZw.NavCodeEditor.Extensions.Snippets;

namespace AnZw.NavCodeEditor.Extensions.UI
{
    public class VariableSelectionVM : ObservableObject
    {

        public SnippetManager SnippetManager { get; }
        public BindingList<SnippetFunction> Variables { get; set; }

        private SnippetFunction _selected;
        public SnippetFunction Selected
        {
            get { return _selected; }
            set { SetProperty<SnippetFunction>(ref _selected, value); }
        }

        public VariableSelectionVM(SnippetManager snippetManager)
        {
            this.SnippetManager = snippetManager;
            this.Variables = new BindingList<SnippetFunction>();
            foreach (SnippetVariable variable in this.SnippetManager.Variables)
            {
                this.Variables.Add(variable);
            }
            foreach (SnippetFunction function in this.SnippetManager.Functions.Values)
            {
                this.Variables.Add(function);
            }
            _selected = this.Variables.FirstOrDefault();
        }

    }
}
