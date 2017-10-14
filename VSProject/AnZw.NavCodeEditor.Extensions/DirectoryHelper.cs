using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace AnZw.NavCodeEditor.Extensions
{
    public class DirectoryHelper
    {

        public static string CurrentAssemblyPath()
        {
            return GetAssemblyPath(typeof(DirectoryHelper));
        }

        public static string GetAssemblyPath(Type type)
        {
            return Path.GetDirectoryName(type.Assembly.Location);
        }

        public static string GetApplicationDataPath()
        {
            string dataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string applicationDirectoryName = "AnZw.NavCodeEditor.Extensions";
            string applicationDataPath = Path.Combine(dataPath, applicationDirectoryName);
            if (!Directory.Exists(applicationDataPath))
                Directory.CreateDirectory(applicationDataPath);
            return applicationDataPath;
        }

    }
}
