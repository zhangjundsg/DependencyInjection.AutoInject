using Microsoft.CodeAnalysis;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace DependencyInjection.AutoInject_SourceGenerator
{
    [Generator]
    internal sealed class AutoInjectSourceGenerator : ISourceGenerator
    {
        private const string _attributeName = "Microsoft.Extensions.DependencyInjection.AutoInjectAttribute";
        private const string _fileName = "DependencyInjectionExtensions.g.cs";
        private const string _className = "DependencyInjectionExtensions_G";

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="context"></param>
        public void Initialize(GeneratorInitializationContext context)
        {
            context.RegisterForSyntaxNotifications(() => new SyntaxReceiver());
        }

        public void Execute(GeneratorExecutionContext context)
        {
            if (context.SyntaxReceiver is SyntaxReceiver receiver)
            {
                var code = SourceGenerator(receiver, context.Compilation);
                context.AddSource(_fileName, code);
            }
        }

        public string SourceGenerator(SyntaxReceiver receiver, Compilation compilation)
        {
            var injectAttributeSymbol = compilation.GetTypeByMetadataName(_attributeName);

            if (injectAttributeSymbol == null) return string.Empty;

            var sourceBuilder = new StringBuilder();
            sourceBuilder.AppendLine("using Microsoft.Extensions.DependencyInjection;");
            sourceBuilder.AppendLine($"namespace Microsoft.Extensions.DependencyInjection;");
            sourceBuilder.AppendLine($"public static class {_className}");
            sourceBuilder.AppendLine("{");
            sourceBuilder.AppendLine($"    public static IServiceCollection Add{GetAssemblyName(compilation)}(this IServiceCollection services)");
            sourceBuilder.AppendLine("    {");

            foreach (var item in receiver._classAndRecordNodes)
            {
                // 获取语义模型
                var semanticModel = compilation.GetSemanticModel(item.SyntaxTree);

                if (semanticModel.GetDeclaredSymbol(item) is not INamedTypeSymbol classSymbol) continue;

                foreach (var service in receiver.GetServiceDescriptors(classSymbol, injectAttributeSymbol))
                {
                    if (service.AsSelf)
                        sourceBuilder.AppendLine(AddService(service.LifeTime, service.DeclareType, null));

                    foreach (var interfaces in service.ServiceTypes)
                        sourceBuilder.AppendLine(AddService(service.LifeTime, service.DeclareType, interfaces));
                }
            }

            sourceBuilder.AppendLine("        return services;");
            sourceBuilder.AppendLine("    }");
            sourceBuilder.AppendLine("}");

            return sourceBuilder.ToString();
        }
        private string GetAssemblyName(Compilation compilation)
        {
            var assemblyName = compilation.AssemblyName ?? string.Empty;
            return new string(assemblyName.Where(IsAllowChar).ToArray());

            static bool IsAllowChar(char c)
            {
                return ('0' <= c && c <= '9') || ('A' <= c && c <= 'Z') || ('a' <= c && c <= 'z');
            }
        }

        private string AddService(ServiceLifeTime lifeTime, TypeSymbol DeclareType, TypeSymbol? ServiceType)
        {
            return lifeTime switch
            {
                ServiceLifeTime.Singleton => $"        services.AddSingleton<{(ServiceType == null ? $"{DeclareType}" : $"{ServiceType}, {DeclareType}")}>();",
                ServiceLifeTime.Scoped => $"        services.AddScoped<{(ServiceType == null ? $"{DeclareType}" : $"{ServiceType}, {DeclareType}")}>();",
                ServiceLifeTime.Transient => $"        services.AddTransient<{(ServiceType == null ? $"{DeclareType}" : $"{ServiceType}, {DeclareType}")}>();",
                _ => string.Empty
            };
        }
    }
}
