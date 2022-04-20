using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace GraphQLGeneration
{
    [Generator]
    public class ModelGenerator : ISourceGenerator
    {

        public void GeneratePoJo(GeneratorExecutionContext context, AdditionalText file)
        {
            var content = file.GetText(context.CancellationToken);
            var jsonDocument = JsonSerializer.Deserialize<JsonObject>(
                content?.ToString() ??
                throw new ArgumentNullException());
            var schemas = jsonDocument?["components"]?["schemas"]?.AsObject();

            if (schemas == null) return;
            foreach (var obj in schemas)
            {
                var objectName = obj.Key;
                var objectType = obj.Value?["type"];
                    
                var generatedProperties = new List<string>();
                var properties = obj.Value?["properties"]?.AsObject();

                if (properties == null) continue;
                    
                foreach (var property in properties)
                {
                    var propertyName = property.Key;
                    var propertyType = property.Value?["type"]?.ToString();

                    var result = $"public {propertyName} {propertyType} {{ get; set; }}";
                    generatedProperties.Add(result);
                }

                var generatedPropertiesText = string.Join("\n", generatedProperties);
                    
                    
                context.AddSource(
                    $"{objectName}.g.cs",
                    SourceText.From($@"// Auto-generated Code
using System;

namespace GraphQLCodeGenerationPoC 
{{
    public class {objectName}
    {{
        {generatedPropertiesText}
    }}
}}", Encoding.UTF8)
                );
            }
        }

        public void Initialize(GeneratorInitializationContext context)
        {
            #if DEBUG
            if (!Debugger.IsAttached)
            {
                Debugger.Launch();
            }
            #endif
        }

        public void Execute(GeneratorExecutionContext context)
        {
            var syntaxTrees = context.Compilation.SyntaxTrees;
            //throw new System.NotImplementedException();
        }
    }
}