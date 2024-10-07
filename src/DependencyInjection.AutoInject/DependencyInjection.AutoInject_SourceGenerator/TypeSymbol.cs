using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Text;

namespace DependencyInjection.AutoInject_SourceGenerator
{
    sealed class TypeSymbol(ITypeSymbol typeSymbol)
    {
        private readonly ITypeSymbol _typeSymbol = typeSymbol;

        public override bool Equals(object obj)
        {
            return SymbolEqualityComparer.Default.Equals(_typeSymbol, ((TypeSymbol)obj)._typeSymbol);
        }

        public override int GetHashCode() => base.GetHashCode();

        public override string ToString()
        {
            return _typeSymbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
        }
    }
}
