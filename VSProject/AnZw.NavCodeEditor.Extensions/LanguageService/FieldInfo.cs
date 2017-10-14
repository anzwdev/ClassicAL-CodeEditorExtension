using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnZw.NavCodeEditor.Extensions.Reflection;

namespace AnZw.NavCodeEditor.Extensions.LanguageService
{
    public class FieldInfo : ReflectionWrapper
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int Width { get; set; }

        public FieldInfo(object source) : base(source)
        {
            this.Id = GetProperty<int>("Id");
            this.Name = GetProperty<string>("Name");
            this.Type = GetProperty<string>("Type");
            this.Width = GetProperty<int>("Width");
        }

    }
}
