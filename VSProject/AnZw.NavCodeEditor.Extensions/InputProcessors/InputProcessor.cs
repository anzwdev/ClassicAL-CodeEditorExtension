using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AnZw.NavCodeEditor.Extensions;

namespace AnZw.NavCodeEditor.Extensions.InputProcessors
{
    public class InputProcessor
    {

        public CALKeyProcessor KeyProcessor { get; }

        public InputProcessor(CALKeyProcessor keyProcessor)
        {
            this.KeyProcessor = keyProcessor;
        }

        public virtual void KeyDown(KeyEventArgs args, KeyStateInfo keyStateInfo)
        {
        }

        public virtual void TextInput(TextCompositionEventArgs args)
        {
        }

    }
}
