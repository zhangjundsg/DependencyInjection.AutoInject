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
                var typeSymbol = new TypeSymbol(classSymbol);

                var args = attribute.ConstructorArguments;
                if (args.Length > 0 && args[0].Kind == TypedConstantKind.Enum && args[0].Value is int value &&
                    Enum.IsDefined(typeof(ServiceLifeTime), value))
                {
                    var serviceDescriptor = new ServiceDescriptor((ServiceLifeTime)value, typeSymbol);

                    foreach (var @interface in classSymbol.AllInterfaces)
                    {
                        serviceDescriptor.ServiceTypes.Add(new TypeSymbol(@interface));
                    }

                    yield return serviceDescriptor;
                }
            }

        }
    }
}
