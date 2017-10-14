using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Text.Editor;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Operations;

using AnZw.NavCodeEditor.Extensions.LanguageService;
/*
using Microsoft.Dynamics.Nav.Prod.CodeEditor.Intellisense;
using Microsoft.Dynamics.Nav.Prod.CodeEditor.Interop;
*/

using AnZw.NavCodeEditor.Extensions.InputProcessors;

namespace AnZw.NavCodeEditor.Extensions
{
    public class CALKeyProcessor : KeyProcessor
    {

        public List<InputProcessor> InputProcessors { get; }
        public ITextView TextView { get; }
        public IEditorOperations EditorOperations { get; }


        public IMethodManager MethodManager { get; }
        //public LanguageService LanguageService { get; }
        public Connector NavConnector { get; }

        protected bool _zoomUpdated;

        #region Processing cache variables

        private CurrentLineInformation _currentLineInformation = null;
        public CurrentLineInformation CurrentLineInformation
        {
            get
            {
                if (_currentLineInformation == null)
                {
                    _currentLineInformation = new CurrentLineInformation();

                    ITextSnapshot snapshot = this.TextView.TextSnapshot;
                    SnapshotPoint carretPoint = this.TextView.Caret.Position.BufferPosition;
                    ITextSnapshotLine line = carretPoint.GetContainingLine();

                    _currentLineInformation.LineText = line.GetText();
                    _currentLineInformation.LineStartPosition = line.Start.Position;
                    _currentLineInformation.CaretPosition = carretPoint.Position;
                    _currentLineInformation.CaretColumn = _currentLineInformation.CaretPosition - _currentLineInformation.LineStartPosition;
                }
                return _currentLineInformation;
            }
        }

        private string _selectedText = null;
        public string SelectedText
        {
            get
            {
                if (_selectedText == null)
                   _selectedText = this.TextView.Selection.StreamSelectionSpan.GetText();
                if (_selectedText == null)
                    _selectedText = "";
                return _selectedText;
            }
        }

        public void ClearCacheData()
        {
            _currentLineInformation = null;
            _selectedText = null;
        }

        #endregion

        public CALKeyProcessor(ITextView textView, IEditorOperations editorOperations)
        {
            _zoomUpdated = false;

            this.InputProcessors = new List<InputProcessor>();
            this.TextView = textView;
            this.EditorOperations = editorOperations;

            this.NavConnector = new Connector(textView);
            this.MethodManager = this.NavConnector.MethodManager;

            CreateInputProcessors();
        }

        protected void CreateInputProcessors()
        {
            this.InputProcessors.Add(new ClosingBracketInputProcessor(this));
            this.InputProcessors.Add(new SettingsInputProcessor(this));
            this.InputProcessors.Add(new SnippetsInputProcessor(this));
            this.InputProcessors.Add(new XmlDocInputProcessor(this));
        }

        protected void UpdateZoom()
        {
            if (!_zoomUpdated)
            {
                if ((Session.Current.Settings.SetZoom) && (Session.Current.Settings.Zoom > 0))
                    this.EditorOperations.ZoomTo(Session.Current.Settings.Zoom);
                _zoomUpdated = true;
            }
        }

        public override void KeyDown(KeyEventArgs args)
        {
            UpdateZoom();

            if (args == null)
                throw new ArgumentNullException("args");
            if (args.Handled)
                return;

            KeyStateInfo keyStateInfo = new KeyStateInfo(args);

            for (int i=0; i<this.InputProcessors.Count;i++)
            {
                this.InputProcessors[i].KeyDown(args, keyStateInfo);
                if (args.Handled)
                {
                    ClearCacheData();
                    return;
                }
            }
            ClearCacheData();

            base.KeyDown(args);
        }

        public override void TextInput(TextCompositionEventArgs args)
        {
            if (args.Handled)
                return;

            for (int i = 0; i < this.InputProcessors.Count; i++)
            {
                this.InputProcessors[i].TextInput(args);
                if (args.Handled)
                {
                    ClearCacheData();
                    return;
                }
            }
            ClearCacheData();

            base.TextInput(args);
        }

    }
}
