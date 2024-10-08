using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

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
            if (((INamedTypeSymbol)_typeSymbol) is { Arity: > 0 } symbol)
            {
                var genericsStr = new StringBuilder("<");
                for (int i = 1; i < symbol.Arity; i++)
                {
                    genericsStr.Append(", ");
                }
                genericsStr.Append('>');

                return Regex.Replace(_typeSymbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat), "<.+>", genericsStr.ToString());
            }

            return _typeSymbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
        }
    }
}
