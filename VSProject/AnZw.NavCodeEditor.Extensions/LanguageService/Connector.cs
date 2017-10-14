using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Language.Intellisense;
using System.Reflection;
using Microsoft.VisualStudio.Text.Editor;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Microsoft.VisualStudio.Text.Operations;
using AnZw.NavCodeEditor.Extensions.Reflection;

namespace AnZw.NavCodeEditor.Extensions.LanguageService
{
    public class Connector : ReflectionWrapper
    {

        public Connector(ITextView textView)
        {
                string connectorTypeName = "Microsoft.Dynamics.Nav.Prod.CodeEditor.Intellisense.Connector";
                Type connectorType = Type.GetType(connectorTypeName);
                if (connectorType == null)
                {
                    //load dynamics nav editor assemblies
                    Assembly sourceNavAssembly = Assembly.LoadFrom(Path.Combine(DirectoryHelper.CurrentAssemblyPath(), "Microsoft.Dynamics.Nav.CodeEditor.Intellisense.dll"));
                    connectorType = sourceNavAssembly.GetType(connectorTypeName);
                }

                //find connector
                MethodInfo getConnector = connectorType.GetMethod("GetConnector", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
                object[] getConnectorParameters = { textView };
                object connector = getConnector.Invoke(null, getConnectorParameters);

                Initialize(connector, connectorType);

        }

        private Context _context = null;
        public Context Context
        {
            get
            {
                if (_context == null)
                    _context = new Context(GetProperty("Context"));
                return _context;
            }
        }

        private TypeInfoManager _typeInfoManager = null;
        public TypeInfoManager TypeInfoManager
        {
            get
            {
                if (_typeInfoManager == null)
                    _typeInfoManager = new TypeInfoManager(GetProperty("TypeInfoManager"));
                return _typeInfoManager;
            }
        }

        private IMethodManager _methodManager = null;
        public IMethodManager MethodManager
        {
            get
            {
                if (_methodManager == null)
                    _methodManager = new MethodManager(GetProperty("MethodManager"));
                return _methodManager;
            }
        }

    }
}
