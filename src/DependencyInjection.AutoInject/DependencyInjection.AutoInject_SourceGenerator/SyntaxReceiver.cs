using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;

namespace DependencyInjection.AutoInject_SourceGenerator
{
    public sealed class SyntaxReceiver : ISyntaxReceiver
    {
        private const string _attributeName = "Microsoft.Extensions.DependencyInjection.AutoInjectAttribute";
        public readonly List<TypeDeclarationSyntax> _classAndRecordNodes = [];
        public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
        {
            if (syntaxNode is ClassDeclarationSyntax or RecordDeclarationSyntax)
                _classAndRecordNodes.Add((TypeDeclarationSyntax)syntaxNode);
        }
    }
}
