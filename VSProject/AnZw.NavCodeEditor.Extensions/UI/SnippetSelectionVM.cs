using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnZw.NavCodeEditor.Extensions;
using AnZw.NavCodeEditor.Extensions.Snippets;

namespace AnZw.NavCodeEditor.Extensions.UI
{
    public class SnippetSelectionVM : ObservableObject
    {

        public SnippetManager SnippetManager { get; }
        public BindingList<Snippet> Snippets { get; }

        private string _nameFilter;
        public string NameFilter
        {
            get { return _nameFilter; }
            set
            {
                if (SetProperty<string>(ref _nameFilter, value))
                {
                    this.Selected = SelectSnippetByName(_nameFilter);
                }
            }
        }

        private Snippet _selected;
        public Snippet Selected
        {
            get { return _selected; }
            set { SetProperty<Snippet>(ref _selected, value); }
        }

        public SnippetSelectionVM(SnippetManager snippetManager)
        {
            this.SnippetManager = snippetManager;
            this.Snippets = new BindingList<Snippet>();
            foreach (Snippet snippet in this.SnippetManager.Settings.Snippets)
            {
                this.Snippets.Add(snippet);
            }
            foreach (Snippet generatorSnippet in this.SnippetManager.CodeGeneratorSnippets)
            {
                this.Snippets.Add(generatorSnippet);
            }

            this.Selected = this.Snippets.FirstOrDefault();
        }

        protected Snippet SelectSnippetByName(string nameToFind)
        {
            if (String.IsNullOrWhiteSpace(nameToFind))
                return this.Snippets.FirstOrDefault();

            nameToFind = this.NameFilter.ToLower();
            Snippet _nameStartMatches = null;
            Snippet _nameContains = null;

            foreach (Snippet snippet in this.Snippets)
            {
                string snippetName = snippet.Name.ToLower();
                if (snippetName.Equals(nameToFind))
                    return snippet;
                if ((_nameStartMatches == null) && (snippet.Name.StartsWith(this.NameFilter, StringComparison.CurrentCultureIgnoreCase)))
                    _nameStartMatches = snippet;
                if ((_nameContains == null) && (snippet.Name.ToLower().Contains(this.NameFilter.ToLower())))
                    _nameContains = snippet;
            }

            if (_nameStartMatches != null)
                return _nameStartMatches;
            return _nameContains;
        }

    }
}
