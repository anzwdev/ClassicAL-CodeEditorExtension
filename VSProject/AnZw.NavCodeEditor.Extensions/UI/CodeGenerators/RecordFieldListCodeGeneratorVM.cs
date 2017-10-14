using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnZw.NavCodeEditor.Extensions;
using AnZw.NavCodeEditor.Extensions.LanguageService;
using AnZw.NavCodeEditor.Extensions.Snippets;

namespace AnZw.NavCodeEditor.Extensions.UI.CodeGenerators
{
    public class RecordFieldListCodeGeneratorVM : ObservableObject
    {

        private string _template;
        public string Template
        {
            get { return _template; }
            set { SetProperty<string>(ref _template, value); }
        }

        private string _variableName;
        public string VariableName
        {
            get { return _variableName; }
            set
            {
                if (SetProperty<string>(ref _variableName, value))
                    LoadFields();
            }
        }

        public BindingList<FieldInfo> Fields { get; }
        public List<FieldInfo> SelectedFields { get; }
        public BindingList<TypeInfo> Variables { get; }

        public TypeInfoManager TypeInfoManager { get; }

        public RecordFieldListCodeGeneratorVM(TypeInfoManager typeInfoManager)
        {
            this.Fields = new BindingList<FieldInfo>();
            this.SelectedFields = new List<FieldInfo>();
            this.Variables = new BindingList<TypeInfo>();
            this.TypeInfoManager = typeInfoManager;
            this.VariableName = "";
            this.Template = "";
        }

        protected void LoadFields()
        {
            this.Fields.Clear();

            IEnumerable<FieldInfo> loadedFieldList = this.TypeInfoManager.GetFields(this.VariableName);
            if (loadedFieldList != null)
            {
                foreach (FieldInfo field in loadedFieldList)
                {
                    this.Fields.Add(field);
                }
            }
        }

        public void SetSelectedFields(IList selectedFields)
        {
            this.SelectedFields.Clear();
            foreach (FieldInfo fieldInfo in selectedFields)
            {
                this.SelectedFields.Add(fieldInfo);
            }
        }

    }
}
