using Microsoft.CodeAnalysis;
using System;
using Microsoft.CodeAnalysis;
using System.Diagnostics;

namespace DependencyInjection.AutoInject_SourceGenerator
{
    [Generator]
    public sealed class AutoInjectSourceGenerator : ISourceGenerator
    {
        private const string _fileName = "DependencyInjectionExtensions.g.cs";
        private const string _className = "DependencyInjectionExtensions_G";

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="context"></param>
        public void Initialize(GeneratorInitializationContext context)
        {
            Debugger.Launch();
            context.RegisterForSyntaxNotifications(() => new SyntaxReceiver());
        }

        public void Execute(GeneratorExecutionContext context)
        {
            if (context.SyntaxReceiver is SyntaxReceiver receiver)
            {
                var code = SourceGenerator(receiver, context.Compilation);
                context.AddSource(_fileName, "");
            }
        }

        public string SourceGenerator(SyntaxReceiver receiver, Compilation compilation)
        {
            return null;
        }

    }
}
