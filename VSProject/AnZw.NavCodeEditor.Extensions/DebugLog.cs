using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnZw.NavCodeEditor.Extensions
{
    public static class DebugLog
    {

        public static void WriteLogEntry(string messageText)
        {
            System.IO.StreamWriter w = new System.IO.StreamWriter("c:\\temp\\log.txt", true);
            w.WriteLine(messageText);
            w.Close();
            w.Dispose();
        }


    }
}
