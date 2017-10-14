using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Threading.Tasks;
using AnZw.NavCodeEditor.Extensions;

namespace AnZw.NavCodeEditor.Extensions.Snippets
{
    public class SnippetVariable : SnippetFunction
    {

        private string _value;
        public string Value
        {
            get { return _value; }
            set { SetProperty<string>(ref _value, value); }
        }

        public SnippetVariable()
        {
            this.Value = "";
        }

        public SnippetVariable(SnippetVariable source) : this()
        {
            CopyFrom(source);
        }

        public void CopyFrom(SnippetVariable source)
        {
            this.Name = source.Name;
            this.Description = source.Description;
            this.Value = source.Value;
        }

        public override string GetValue(string formatString)
        {
            return this.Value;
        }

    }
}
