using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnZw.NavCodeEditor.Extensions
{
    public class CurrentLineInformation
    {

        public string LineText { get; set; }
        public int LineStartPosition { get; set; }
        public int CaretPosition { get; set; }
        public int CaretColumn { get; set; }

        public CurrentLineInformation()
        {
        }

    }
}
