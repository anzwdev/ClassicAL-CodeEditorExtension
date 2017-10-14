using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnZw.NavCodeEditor.Extensions.Snippets.CodeGenerators;

namespace AnZw.NavCodeEditor.Extensions.Snippets
{

    public class SnippetManager
    {

        public Session Session { get; }

        private SessionSettings _localSettings;
        public SessionSettings Settings
        {
            get
            {
                if (this.Session != null)
                    return this.Session.Settings;
                return _localSettings;
            }
            set
            {
                _localSettings = value;
            }
        }

        /// <summary>
        /// list of snippet functions
        /// </summary>
        public Dictionary<string, SnippetFunction> Functions { get; set; }
        
        /// <summary>
        /// snippet function returning text selected in editor
        /// </summary>
        public SnippetVariable SelectedTextFunction { get; private set; }

        public BindingList<SnippetVariable> Variables
        {
            get
            {
                return this.Settings.Variables;
            }
        }

        public List<CodeGeneratorSnippet> CodeGeneratorSnippets { get; }

        public SnippetManager()
        {
            _localSettings = null;
            this.Session = null;
            this.Functions = new Dictionary<string, SnippetFunction>();
            this.CodeGeneratorSnippets = new List<CodeGeneratorSnippet>();
            CreateFunctions();
            CreateCodeGenerators();
        }

        public SnippetManager(Session session) : this()
        {
            this.Session = session;
        }

        public SnippetManager(SessionSettings settings) : this()
        {
            _localSettings = settings;
        }

        protected void AddFunction(SnippetFunction function)
        {
            this.Functions.Add(function.Name, function);
        }

        protected void CreateFunctions()
        {
            this.SelectedTextFunction = new SnippetVariable() { Name = "SelectedText", Description = "Selected text" };
            AddFunction(this.SelectedTextFunction);
            AddFunction(new SnippetDateTimeFunction());
        }

        protected void CreateCodeGenerators()
        {
            this.CodeGeneratorSnippets.Add(new RecordFieldListCodeGenerator());
            this.CodeGeneratorSnippets.Add(new RecordAssignmentCodeGenerator());
        }

        public string ParseSnippet(Snippet snippet, int indent, CALKeyProcessor keyProcessor)
        {
            string content = snippet.Run(keyProcessor);
            if (content == null)
                return null;

            int pos = content.IndexOf("{{");
            if (pos >= 0)
            {
                StringBuilder builder = new StringBuilder();
                int startPos = 0;
                while (pos >= 0)
                {
                    if (content.Substring(pos, 4) == "{{{{")
                    {
                        builder.Append(content.Substring(startPos, pos - startPos + 2));
                        startPos = pos + 4;
                        pos = startPos;
                    }
                    else
                    {
                        int endPos = content.IndexOf("}}", pos);
                        if (endPos > 0)
                        {
                            string variableName = content.Substring(pos + 2, endPos - pos - 2).Trim();
                            string variableValue = GetVariableValue(variableName);

                            //check if text has to be indented
                            int prevNewLinePos = content.LastIndexOf('\n', pos);
                            if (prevNewLinePos >= 0)
                                variableValue = IndentText(variableValue, pos - prevNewLinePos - 1);

                            //add text to string builder
                            builder.Append(content.Substring(startPos, pos - startPos));
                            builder.Append(variableValue);
                            startPos = endPos + 2;

                            //move pos
                            pos = startPos;
                        }
                    }
                    pos = content.IndexOf("{{", pos);
                }
                if (startPos < content.Length)
                    builder.Append(content.Substring(startPos));
                content = builder.ToString();
            }

            return IndentText(content, indent);
        }

        protected string IndentText(string text, int indent)
        {
            if (indent <= 0)
                return text;
            string padText = "\n".PadRight(indent + 1);
            return text.Replace("\n", padText);
        }

        protected SnippetVariable FindVariable(string name)
        {
            SessionSettings settings = this.Settings;

            for (int i=0; i<settings.Variables.Count;i++)
            {
                if (settings.Variables[i].Name == name)
                    return settings.Variables[i];
            }
            return null;
        }

        protected string GetVariableValue(string variableName)
        {
            int pos = variableName.IndexOf(':');
            string formatString = "";
            if (pos > 0)
            {
                formatString = variableName.Substring(pos + 1);
                variableName = variableName.Substring(0, pos);
            }

            if (String.IsNullOrWhiteSpace(variableName))
                return "";

            SnippetVariable snippetVariable = FindVariable(variableName);
            if (snippetVariable != null)
                return snippetVariable.GetValue(formatString);

            //predefined variables
            if (this.Functions.ContainsKey(variableName))
                return this.Functions[variableName].GetValue(formatString);

            return "";
        }

    }

}
