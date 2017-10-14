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
    public class Snippet : ObservableObject
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

        private string _content;
        public string Content
        {
            get { return _content; }
            set { SetProperty<string>(ref _content, value); }
        }

        private string _hotKey;
        public string HotKey
        {
            get { return _hotKey; }
            set { SetProperty(ref _hotKey, value); }
        }

        private bool _selectionTransformationSnippet;
        /// <summary>
        /// Selection transformation snippet
        /// </summary>
        public bool SelectionTransformationSnippet
        {
            get { return _selectionTransformationSnippet; }
            set { SetProperty<bool>(ref _selectionTransformationSnippet, value); }
        }

        public Snippet()
        {
        }

        public Snippet(Snippet source) : this()
        {
            CopyFrom(source);
        }

        public void CopyFrom(Snippet source)
        {
            this.Name = source.Name;
            this.Description = source.Description;
            this.Content = source.Content;
            this.HotKey = source.HotKey;
            this.SelectionTransformationSnippet = source.SelectionTransformationSnippet;
        }

        public virtual string Run(CALKeyProcessor keyProcessor)
        {
            return this.Content;
        }

    }
}

