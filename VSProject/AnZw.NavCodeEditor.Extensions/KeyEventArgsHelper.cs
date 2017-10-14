using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AnZw.NavCodeEditor.Extensions
{
    public class KeyEventArgsHelper
    {

        public static bool Equals(KeyEventArgs args, string shortcut)
        {
            bool ctrl = shortcut.Contains("Ctrl");
            bool shift = shortcut.Contains("Shift");
            bool alt = shortcut.Contains("Alt");

            if (ctrl)
                shortcut = shortcut.Replace("Ctrl", "");
            if (shift)
                shortcut = shortcut.Replace("Shift", "");
            if (alt)
                shortcut = shortcut.Replace("Alt", "");
            shortcut = shortcut.Replace("+", "");

            Key keyValue;
            if (!Enum.TryParse<Key>(shortcut, out keyValue))
                return false;

            bool ctrlDown = (args.KeyboardDevice.IsKeyDown(Key.LeftCtrl) || args.KeyboardDevice.IsKeyDown(Key.RightCtrl));
            bool altDown = (args.KeyboardDevice.IsKeyDown(Key.LeftAlt) || args.KeyboardDevice.IsKeyDown(Key.RightAlt));
            bool shiftDown = (args.KeyboardDevice.IsKeyDown(Key.LeftShift) || args.KeyboardDevice.IsKeyDown(Key.RightShift));

            return ((ctrl == ctrlDown) && (shift == shiftDown) && (alt == altDown) && (keyValue == args.Key));
        }

    }
}
