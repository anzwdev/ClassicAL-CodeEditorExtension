using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnZw.NavCodeEditor.Extensions.LanguageService;
using AnZw.NavCodeEditor.Extensions.Snippets;
using AnZw.NavCodeEditor.Extensions.UI.CodeGenerators;

namespace AnZw.NavCodeEditor.Extensions.Snippets.CodeGenerators
{
    public class RecordFieldListCodeGenerator : CodeGeneratorSnippet
    {

        public RecordFieldListCodeGenerator()
        {
            this.Name = "Record Field List Generator";
            this.Description = "Inserts multiple record fields into source code.";
        }

        public override string Run(CALKeyProcessor keyProcessor)
        {
            string defaultTemplate = "{{VariableName}}.{{FieldName}} := ;";
            RecordFieldListCodeGeneratorVM viewModel = new RecordFieldListCodeGeneratorVM(keyProcessor.NavConnector.TypeInfoManager);
            RecordFieldListCodeGeneratorWindow window = new RecordFieldListCodeGeneratorWindow();
            viewModel.Template = defaultTemplate;
            AddRecordTypes(keyProcessor, viewModel.Variables);
            window.DataContext = viewModel;
            if (window.ShowDialog() == true)
            {
                if ((String.IsNullOrWhiteSpace(viewModel.VariableName)) || (viewModel.Fields.Count == 0))
                    return null;

                string template = viewModel.Template;
                if (String.IsNullOrWhiteSpace(template))
                    template = defaultTemplate;

                StringBuilder builder = new StringBuilder();
                IEnumerable<FieldInfo> fieldList = viewModel.Fields;
                if (viewModel.SelectedFields.Count != 0)
                    fieldList = viewModel.SelectedFields;

                template = template.Replace("{{VariableName}}", viewModel.VariableName);

                foreach (FieldInfo field in fieldList)
                {
                    string fieldName = "\"" + field.Name + "\"";
                    string line = template.Replace("{{FieldName}}", fieldName);
                    builder.Append(line);
                    builder.Append("\n");
                }

                return builder.ToString();
            }
            return null;
        }

    }
}
