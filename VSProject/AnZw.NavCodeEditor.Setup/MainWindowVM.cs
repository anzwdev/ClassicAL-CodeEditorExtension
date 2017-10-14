using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnZw.NavCodeEditor.Extensions;

namespace AnZw.NavCodeEditor.Setup
{
    public class MainWindowVM
    {

        public Session Session { get; }

        public MainWindowVM()
        {
            this.Session = Session.Current;
        }

    }
}
