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

                if (!string.IsNullOrEmpty(code))
                    context.AddSource(_fileName, code);

            }
        }

        public string SourceGenerator(SyntaxReceiver receiver, Compilation compilation)
        {
            var injectAttributeSymbol = compilation.GetTypeByMetadataName(_attributeName);

            if (injectAttributeSymbol == null) return string.Empty;

            var sourceBuilder = new StringBuilder();
            sourceBuilder
                .AppendLine("using Microsoft.Extensions.DependencyInjection;")
                .AppendLine("namespace Microsoft.Extensions.DependencyInjection")
                .AppendLine("{")
                .AppendLine($"  public static class {_className}")
                .AppendLine("  {")
                .AppendLine($"      public static IServiceCollection Add{GetAssemblyName(compilation)}(this IServiceCollection services)")
                .AppendLine("      {");

            foreach (var item in receiver._classAndRecordNodes)
            {
                // 获取语义模型
                var semanticModel = compilation.GetSemanticModel(item.SyntaxTree);

                if (semanticModel.GetDeclaredSymbol(item) is not INamedTypeSymbol classSymbol) continue;

                foreach (var service in receiver.GetServiceDescriptors(classSymbol, injectAttributeSymbol))
                {
                    if (service.AsSelf)
                        sourceBuilder.AppendLine(AddService(service.LifeTime, service.DeclareType, null, service.ServiceKey));

                    foreach (var interfaces in service.ServiceTypes)
                        sourceBuilder.AppendLine(AddService(service.LifeTime, service.DeclareType, interfaces, service.ServiceKey));
                }
            }

            sourceBuilder
                .AppendLine("           return services;")
                .AppendLine("      }")
                .AppendLine("  }")
                .AppendLine("}");

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

        private string AddService(ServiceLifeTime LifeTime, TypeSymbol DeclareType, TypeSymbol? ServiceType, string? ServiceKey)
        {
            switch (LifeTime, ServiceKey)
            {
                case var (life, key) when life == ServiceLifeTime.Singleton && key == null:
                    {
                        if (ServiceType == null)
                            return $"           services.Add(ServiceDescriptor.Singleton(typeof({DeclareType}),typeof({DeclareType})));";
                        else
                            return $"           services.Add(ServiceDescriptor.Singleton(typeof({ServiceType}),typeof({DeclareType})));";
                    }
                case var (life, key) when life == ServiceLifeTime.Singleton && key != null:
                    {
                        if (ServiceType == null)
                            return $@"           services.Add(ServiceDescriptor.KeyedSingleton(typeof({DeclareType}),""{key}"",typeof({DeclareType})));";
                        else
                            return $@"           services.Add(ServiceDescriptor.KeyedSingleton(typeof({ServiceType}),""{key}"",typeof({DeclareType})));";
                    }
                case var (life, key) when life == ServiceLifeTime.Scoped && key == null:
                    {
                        if (ServiceType == null)
                            return $"           services.Add(ServiceDescriptor.Scoped(typeof({DeclareType}),typeof({DeclareType})));";
                        else
                            return $"           services.Add(ServiceDescriptor.Scoped(typeof({ServiceType}),typeof({DeclareType})));";
                    }
                case var (life, key) when life == ServiceLifeTime.Scoped && key != null:
                    {
                        if (ServiceType == null)
                            return $@"           services.Add(ServiceDescriptor.KeyedScoped(typeof({DeclareType}),""{key}"",typeof({DeclareType})));";
                        else
                            return $@"           services.Add(ServiceDescriptor.KeyedScoped(typeof({ServiceType}),""{key}"",typeof({DeclareType})));";
                    }
                case var (life, key) when life == ServiceLifeTime.Transient && key == null:
                    {
                        if (ServiceType == null)
                            return $"           services.Add(ServiceDescriptor.Transient(typeof({DeclareType}),typeof({DeclareType})));";
                        else
                            return $"           services.Add(ServiceDescriptor.Transient(typeof({ServiceType}),typeof({DeclareType})));";
                    }
                case var (life, key) when life == ServiceLifeTime.Transient && key != null:
                    {
                        if (ServiceType == null)
                            return $@"           services.Add(ServiceDescriptor.KeyedTransient(typeof({DeclareType}),""{key}"",typeof({DeclareType})));";
                        else
                            return $@"           services.Add(ServiceDescriptor.KeyedTransient(typeof({ServiceType}),""{key}"",typeof({DeclareType})));";
                    }
                default:
                    return string.Empty;
            }
        }
    }
}
