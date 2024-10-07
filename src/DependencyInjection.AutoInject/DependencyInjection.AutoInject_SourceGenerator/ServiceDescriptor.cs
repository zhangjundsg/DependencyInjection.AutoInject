using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Text;

namespace DependencyInjection.AutoInject_SourceGenerator
{
    sealed class ServiceDescriptor(ServiceLifeTime lifeTime, TypeSymbol typeSymbol)
    {
        /// <summary>
        /// 生命周期
        /// </summary>
        public readonly ServiceLifeTime LifeTime = lifeTime;
        /// <summary>
        /// 声明类型
        /// </summary>
        public readonly TypeSymbol DeclareType = typeSymbol;
        /// <summary>
        /// 服务类型
        /// </summary>
        public HashSet<TypeSymbol> ServiceTypes { get; } = [];
    }
}
