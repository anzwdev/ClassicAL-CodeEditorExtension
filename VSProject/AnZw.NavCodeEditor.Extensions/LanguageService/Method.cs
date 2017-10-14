using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Text;
using AnZw.NavCodeEditor.Extensions.Reflection;

namespace AnZw.NavCodeEditor.Extensions.LanguageService
{
    public class Method : ReflectionWrapper, IMethod
    {

        public int Id { get; }
        public string Name { get; }
        public string Signature { get; }

        public Method(object source) : base(source)
        {
            this.Id = GetProperty<int>("Id");
            this.Name = GetProperty<string>("Name");
            this.Signature = GetProperty<string>("Signature");
        }

        public IEnumerable<string> GetLines()
        {
            return CallMethod<IEnumerable<string>>("GetLines");
        }

        public Tuple<SnapshotPoint, SnapshotPoint> GetCodeInterval()
        {
            object value = CallMethod("GetCodeInterval");
            Tuple<SnapshotPoint, SnapshotPoint> retVal = (Tuple<SnapshotPoint, SnapshotPoint>)value;
            return retVal;
        }


    }
}
