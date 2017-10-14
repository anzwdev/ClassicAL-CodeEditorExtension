using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnZw.NavCodeEditor.Extensions.Snippets
{
    public class SnippetDateTimeFunction : SnippetFunction
    {

        public SnippetDateTimeFunction()
        {
            this.Name = "DateTime";
            this.Description = "Returns current date time. Value can be formatted by providing format string after : character (i.e. DateTime:dd-MM-yyy";
        }

        public override string GetValue(string formatString)
        {
            DateTime dateTime = DateTime.Now;
            if (String.IsNullOrWhiteSpace(formatString))
                return dateTime.ToString();
            return dateTime.ToString(formatString);
        }

    }
}
