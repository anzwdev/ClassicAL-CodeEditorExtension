using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AnZw.NavCodeEditor.Extensions;
using AnZw.NavCodeEditor.Extensions.Snippets;
using AnZw.NavCodeEditor.Extensions.UI;

namespace AnZw.NavCodeEditor.Extensions.InputProcessors
{
    public class SnippetsInputProcessor : InputProcessor
    {

        public SnippetsInputProcessor(CALKeyProcessor keyProcessor) : base(keyProcessor)
        {
        }

        public override void KeyDown(KeyEventArgs args, KeyStateInfo keyStateInfo)
        {
            if (args.Handled)
                return;

            //open snippets selection window
            if (keyStateInfo.Equals(Session.Current.Settings.SnippetSelectionKeyStateInfo))
            {
                SnippetSelectionVM viewModel = new SnippetSelectionVM(Session.Current.SnippetManager);
                SnippetSelection snippetSelection = new SnippetSelection();
                snippetSelection.ViewModel = viewModel;
                if (snippetSelection.ShowDialog() == true)
                {
                    if (snippetSelection.ViewModel.Selected != null)
                        RunSnippet(snippetSelection.ViewModel.Selected);
                }
                args.Handled = true;
                return;
            }

            //check snippet hot keys
            foreach (Snippet snippet in Session.Current.Settings.Snippets)
            {
                if (!String.IsNullOrWhiteSpace(snippet.HotKey))
                {
                    KeyStateInfo snippetKeyStateInfo = new KeyStateInfo(snippet.HotKey);
                    if (keyStateInfo.Equals(snippetKeyStateInfo))
                    {
                        RunSnippet(snippet);
                        args.Handled = true;
                        return;
                    }
                }
            }
        }

        public void RunSnippet(Snippet snippet)
        {
            //collect current information
            CurrentLineInformation lineInformation = this.KeyProcessor.CurrentLineInformation;
            string selectedText = this.KeyProcessor.SelectedText;
            Session.Current.SnippetManager.SelectedTextFunction.Value = selectedText;

            int indent = lineInformation.CaretColumn;
            if (!String.IsNullOrEmpty(selectedText))
                indent -= selectedText.Length;
            if (indent < 0)
                indent = 0;

            //parse snippet
            string newText = Session.Current.SnippetManager.ParseSnippet(snippet, indent, this.KeyProcessor);

            //clear cached information
            Session.Current.SnippetManager.SelectedTextFunction.Value = "";

            //add snippet text to source editor
            if (!String.IsNullOrEmpty(newText))
                this.KeyProcessor.EditorOperations.InsertText(newText);
        }

    }
}
