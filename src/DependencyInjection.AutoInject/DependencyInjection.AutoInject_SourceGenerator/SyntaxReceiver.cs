using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DependencyInjection.AutoInject_SourceGenerator
{
    sealed class SyntaxReceiver : ISyntaxReceiver
    {
        public readonly List<TypeDeclarationSyntax> _classAndRecordNodes = [];
        public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
        {
            if (syntaxNode is ClassDeclarationSyntax or RecordDeclarationSyntax)
                _classAndRecordNodes.Add((TypeDeclarationSyntax)syntaxNode);
        }

        public IEnumerable<ServiceDescriptor> GetServiceDescriptors(ITypeSymbol classSymbol, INamedTypeSymbol injectAttributeSymbol)
        {
            var attribute = classSymbol.GetAttributes().FirstOrDefault(x => SymbolEqualityComparer.Default.Equals(x.AttributeClass, injectAttributeSymbol));

            if (attribute != null)
            {
                var args = attribute.ConstructorArguments;

                if (args.Length > 0 && args[0].Kind == TypedConstantKind.Enum && args[args.Length - 1].Value is bool asSelf && args[0].Value is int value &&
                    Enum.IsDefined(typeof(ServiceLifeTime), value))
                {
                    var serviceDescriptor = new ServiceDescriptor((ServiceLifeTime)value, new TypeSymbol(classSymbol), (string?)args[args.Length - 2].Value, asSelf);

                    foreach (var arg in args)
                    {
                        if (arg.Value is ITypeSymbol symbol)
                            serviceDescriptor.ServiceTypes.Add(new TypeSymbol(symbol));
                    }

                    yield return serviceDescriptor;
                }
            }

        }
    }
}
