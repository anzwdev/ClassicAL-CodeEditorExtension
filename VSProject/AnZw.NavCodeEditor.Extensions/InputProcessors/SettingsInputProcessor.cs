using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AnZw.NavCodeEditor.Extensions;

namespace AnZw.NavCodeEditor.Extensions.InputProcessors
{
    public class SettingsInputProcessor : InputProcessor
    {
        public SettingsInputProcessor(CALKeyProcessor keyProcessor) : base(keyProcessor)
        {
        }

        public override void KeyDown(KeyEventArgs args, KeyStateInfo keyStateInfo)
        {
            if (args.Handled)
                return;

            if (keyStateInfo.Equals(Session.Current.Settings.SettingsKeyStateInfo))
            {
                Session.Current.ShowSettings();
                args.Handled = true;
            }

        }

    }
}
