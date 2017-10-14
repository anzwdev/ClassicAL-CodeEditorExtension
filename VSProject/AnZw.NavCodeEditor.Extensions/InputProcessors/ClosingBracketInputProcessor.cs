using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AnZw.NavCodeEditor.Extensions;

namespace AnZw.NavCodeEditor.Extensions.InputProcessors
{
    public class ClosingBracketInputProcessor : InputProcessor
    {

        protected Dictionary<string, string> ClosingTexts { get; }

        public ClosingBracketInputProcessor(CALKeyProcessor keyProcessor) : base(keyProcessor)
        {
            this.ClosingTexts = new Dictionary<string, string>();
            this.ClosingTexts.Add("(", " )");   //we have to add space before closing bracket to make intellisense work
            this.ClosingTexts.Add("[", " ]");   //the same as above
            this.ClosingTexts.Add("{", "}");
            this.ClosingTexts.Add("'", "'");
            this.ClosingTexts.Add("\"", "\"");
        }

        public override void TextInput(TextCompositionEventArgs args)
        {
            if ((args.Handled) || (!Session.Current.Settings.AutoCloseElements))
                return;

            if (this.ClosingTexts.ContainsKey(args.Text))
            {
                string closingText = this.ClosingTexts[args.Text];

                this.KeyProcessor.EditorOperations.InsertText(args.Text);
                this.KeyProcessor.EditorOperations.InsertText(closingText);
                for (int i=0; i<closingText.Length;i++)
                    this.KeyProcessor.EditorOperations.MoveToPreviousCharacter(false);
                args.Handled = true;
            }

        }


    }
}
