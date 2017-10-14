using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AnZw.NavCodeEditor.Extensions
{
    public class KeyStateInfo
    {

        public Key Key { get; set; }
        public bool Control { get; set; }
        public bool Alt { get; set; }
        public bool Shift { get; set; }

        public KeyStateInfo()
        {
            this.Control = false;
            this.Alt = false;
            this.Shift = false;
            this.Key = Key.None;
        }

        public KeyStateInfo(string hotKey) : this()
        {
            SetHotKey(hotKey);
        }

        public void SetHotKey(string hotKey)
        {
            this.Control = hotKey.Contains("Ctrl");
            this.Shift = hotKey.Contains("Shift");
            this.Alt = hotKey.Contains("Alt");

            if (this.Control)
                hotKey = hotKey.Replace("Ctrl", "");
            if (this.Shift)
                hotKey = hotKey.Replace("Shift", "");
            if (this.Alt)
                hotKey = hotKey.Replace("Alt", "");
            hotKey = hotKey.Replace("+", "");

            Key keyValue;
            if (Enum.TryParse<Key>(hotKey, out keyValue))
                this.Key = keyValue;
            else
                this.Key = Key.None;
        }

        public KeyStateInfo(KeyEventArgs args)
        {
            this.Key = args.Key;
            this.Control = (args.KeyboardDevice.IsKeyDown(Key.LeftCtrl) || args.KeyboardDevice.IsKeyDown(Key.RightCtrl));
            this.Alt = (args.KeyboardDevice.IsKeyDown(Key.LeftAlt) || args.KeyboardDevice.IsKeyDown(Key.RightAlt));
            this.Shift = (args.KeyboardDevice.IsKeyDown(Key.LeftShift) || args.KeyboardDevice.IsKeyDown(Key.RightShift));
        }

        public bool Equals(KeyStateInfo value)
        {
            return ((this.Control == value.Control) && (this.Alt == value.Alt) && (this.Shift == value.Shift) && (this.Key == value.Key) &&
                (this.Key != Key.None) && (value.Key != Key.None));
        }

    }
}
