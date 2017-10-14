using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnZw.NavCodeEditor.Extensions;
using AnZw.NavCodeEditor.Extensions.Snippets;
using AnZw.NavCodeEditor.Extensions.LanguageService;

namespace AnZw.NavCodeEditor.Extensions.UI.CodeGenerators
{
    public class RecordAssignmentCodeGeneratorVM : ObservableObject
    {

        private string _destVariableName;
        public string DestVariableName
        {
            get { return _destVariableName; }
            set { SetProperty<string>(ref _destVariableName, value); }
        }

        private string _sourceVariableName;
        public string SourceVariableName
        {
            get { return _sourceVariableName; }
            set { SetProperty<string>(ref _sourceVariableName, value); }
        }

        private bool _withValidation;
        public bool WithValidation
        {
            get { return _withValidation; }
            set { SetProperty<bool>(ref _withValidation, value); }
        }

        private bool _allFields;
        public bool AllFields
        {
            get { return _allFields; }
            set { SetProperty<bool>(ref _allFields, value); }
        }

        private bool _matchByName;
        public bool MatchByName
        {
            get { return _matchByName; }
            set { SetProperty<bool>(ref _matchByName, value); }
        }

        public BindingList<TypeInfo> Variables { get; }

        public RecordAssignmentCodeGeneratorVM()
        {
            this.Variables = new BindingList<TypeInfo>();
            this.DestVariableName = "";
            this.SourceVariableName = "";
            this.WithValidation = true;
            this.AllFields = true;
            this.MatchByName = true;
        }

    }
}
