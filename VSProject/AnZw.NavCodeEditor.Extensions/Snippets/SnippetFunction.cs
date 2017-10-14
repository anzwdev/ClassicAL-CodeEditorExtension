using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnZw.NavCodeEditor.Extensions;

namespace AnZw.NavCodeEditor.Extensions.Snippets
{
    public class SnippetFunction : ObservableObject
    {

        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty<string>(ref _name, value); }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set { SetProperty<string>(ref _description, value); }
        }

        public SnippetFunction()
        {
            this.Name = "";
            this.Description = "";
        }

        public virtual string GetValue(string formatString)
        {
            return "";
        }

    }
}
