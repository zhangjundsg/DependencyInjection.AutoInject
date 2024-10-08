using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Text;

namespace DependencyInjection.AutoInject_SourceGenerator
{
    sealed class ServiceDescriptor(ServiceLifeTime lifeTime, TypeSymbol typeSymbol, string? serviceKey, bool asSelf)
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
        /// 是否注册自身服务
        /// </summary>
        public readonly bool AsSelf = asSelf;
        /// <summary>
        /// KeyedService key
        /// </summary>
        public string? ServiceKey { get; set; } = serviceKey;
        /// <summary>
        /// 服务类型
        /// </summary>
        public HashSet<TypeSymbol> ServiceTypes { get; } = [];
    }
}
