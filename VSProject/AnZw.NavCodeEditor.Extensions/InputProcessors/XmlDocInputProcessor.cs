using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Operations;
using AnZw.NavCodeEditor.Extensions;
using AnZw.NavCodeEditor.Extensions.LanguageService;

namespace AnZw.NavCodeEditor.Extensions.InputProcessors
{
    public class XmlDocInputProcessor : InputProcessor
    {

        public XmlDocInputProcessor(CALKeyProcessor keyProcessor) : base(keyProcessor)
        {
        }

        public override void KeyDown(KeyEventArgs args, KeyStateInfo keyStateInfo)
        {
            if ((args.Handled) || (!Session.Current.Settings.EnableXmlDocumentation))
                return;

            if ((args.Key == Key.Enter) || (args.Key == Key.Return))
            {
                CurrentLineInformation lineInformation = this.KeyProcessor.CurrentLineInformation;

                if ((lineInformation.LineText.StartsWith("///")) && (lineInformation.CaretColumn > 2))
                {
                    this.KeyProcessor.EditorOperations.InsertNewLine();
                    this.KeyProcessor.EditorOperations.InsertText("/// ");
                    args.Handled = true;
                }
            }

        }

        public override void TextInput(TextCompositionEventArgs args)
        {
            if ((args.Handled) || (!Session.Current.Settings.EnableXmlDocumentation))
                return;

            if (args.Text != "/")
                return;

            CurrentLineInformation lineInformation = this.KeyProcessor.CurrentLineInformation;

            if ((lineInformation.LineText == "//") && (lineInformation.CaretColumn == 2))
            {
                IMethod activeMethod = this.KeyProcessor.MethodManager.ActiveMethod;
                if (activeMethod != null)
                {
                    Tuple<SnapshotPoint, SnapshotPoint> methodInterval = activeMethod.GetCodeInterval();
                    int size = lineInformation.CaretPosition - methodInterval.Item1.Position;
                    //we don't want to process too long texts, let's assume that max. indent is 30 characters and it doc. shoudl always start in the first line
                    if (size == 2)
                    {
                        IEnumerable<string> methodLines = activeMethod.GetLines();
                        if (CanGenerateXmlDocumentation(methodLines))
                        {
                            int moveUp = 2;

                            this.KeyProcessor.EditorOperations.InsertText("/ <summary>");
                            this.KeyProcessor.EditorOperations.InsertNewLine();
                            this.KeyProcessor.EditorOperations.InsertText("/// ");
                            this.KeyProcessor.EditorOperations.InsertNewLine();
                            this.KeyProcessor.EditorOperations.InsertText("/// </summary>");
                            this.KeyProcessor.EditorOperations.InsertNewLine();

                            //try to find current method
                            try
                            {
                                List<SignatureInfo> signatures = this.KeyProcessor.NavConnector.TypeInfoManager.GetSignatures(activeMethod.Name).ToList();

                                if (signatures.Count > 0)
                                {
                                    SignatureInfo signature = signatures[0];
                                    foreach (ParameterInfo parameterInfo in signature.Parameters)
                                    {
                                        this.KeyProcessor.EditorOperations.InsertText($"/// <param name=\"{parameterInfo.ParameterName}\"></param>");
                                        this.KeyProcessor.EditorOperations.InsertNewLine();
                                        moveUp++;
                                    }
                                    if ((signature.ReturnType != null) && (!String.IsNullOrWhiteSpace(signature.ReturnType.TypeName)))
                                    {
                                        this.KeyProcessor.EditorOperations.InsertText("/// <returns></returns>");
                                        this.KeyProcessor.EditorOperations.InsertNewLine();
                                        moveUp++;
                                    }
                                }
                            }
                            catch (Exception e)
                            {
                                DebugLog.WriteLogEntry(e.Message);
                                DebugLog.WriteLogEntry(e.Source);
                                DebugLog.WriteLogEntry(e.StackTrace);
                            }

                            for (int i = 0; i < moveUp; i++)
                                this.KeyProcessor.EditorOperations.MoveLineUp(false);
                            this.KeyProcessor.EditorOperations.MoveToEndOfLine(false);

                            args.Handled = true;
                        }
                    }
                }
            }
        }

        protected bool CanGenerateXmlDocumentation(IEnumerable<string> lines)
        {
            foreach (string line in lines)
            {
                string trimLine = line.TrimStart();
                if (!String.IsNullOrWhiteSpace(trimLine))
                    return (!line.StartsWith("///"));
            }
            return true;
        }


    }
}
