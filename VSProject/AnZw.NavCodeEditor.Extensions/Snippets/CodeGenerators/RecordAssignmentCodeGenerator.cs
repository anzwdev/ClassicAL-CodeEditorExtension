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
    public class RecordAssignmentCodeGenerator : CodeGeneratorSnippet
    {

        public RecordAssignmentCodeGenerator()
        {
            this.Name = "Records Assignment Generator";
            this.Description = "Assigns values of fields from one record variable to another";
        }

        public override string Run(CALKeyProcessor keyProcessor)
        {
            RecordAssignmentCodeGeneratorVM viewModel = new RecordAssignmentCodeGeneratorVM();
            RecordAssignmentCodeGeneratorWindow window = new RecordAssignmentCodeGeneratorWindow();

            AddRecordTypes(keyProcessor, viewModel.Variables);

            window.DataContext = viewModel;
            if (window.ShowDialog() == true)
            {
                TypeInfoManager typeInfoManager = keyProcessor.NavConnector.TypeInfoManager;

                IEnumerable<FieldInfo> sourceFieldList = typeInfoManager.GetFields(viewModel.SourceVariableName);
                IEnumerable<FieldInfo> destFieldList = typeInfoManager.GetFields(viewModel.DestVariableName);

                //collect source fields
                Dictionary<int, FieldInfo> sourceFieldById = null;
                Dictionary<string, FieldInfo> sourceFieldByName = null;
                if (viewModel.MatchByName)
                {
                    sourceFieldByName = new Dictionary<string, FieldInfo>();
                    foreach (FieldInfo sourceField in sourceFieldList)
                    {
                        sourceFieldByName.Add(sourceField.Name, sourceField);
                    }
                } else
                {
                    sourceFieldById = new Dictionary<int, FieldInfo>();
                    foreach (FieldInfo sourceField in sourceFieldList)
                    {
                        sourceFieldById.Add(sourceField.Id, sourceField);
                    }

                }

                StringBuilder builder = new StringBuilder();

                //process fields
                foreach (FieldInfo destField in destFieldList)
                {
                    FieldInfo sourceField = null;
                    if ((viewModel.MatchByName) && (sourceFieldByName.ContainsKey(destField.Name)))
                        sourceField = sourceFieldByName[destField.Name];
                    else if ((!viewModel.MatchByName) && (sourceFieldById.ContainsKey(destField.Id)))
                        sourceField = sourceFieldById[destField.Id];

                    string sourceFieldText = "";
                    if (sourceField != null)
                        sourceFieldText = viewModel.SourceVariableName + ".\"" + sourceField.Name + "\"";

                    if ((sourceField != null) || (viewModel.AllFields))
                    {
                        builder.Append(viewModel.DestVariableName);
                        if (viewModel.WithValidation)
                        {
                            builder.Append(".VALIDATE(\"");
                            builder.Append(destField.Name);
                            builder.Append("\", ");
                        } else
                        {
                            builder.Append(".\"");
                            builder.Append(destField.Name);
                            builder.Append("\" := ");
                        }
                        builder.Append(sourceFieldText);
                        if (viewModel.WithValidation)
                            builder.Append(");\n");
                        else
                            builder.Append(";\n");
                    }
                }

                return builder.ToString();
            }
            return null;
        }

    }
}
