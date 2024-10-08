using System;

namespace Microsoft.Extensions.DependencyInjection;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
public sealed class AutoInjectAttribute : Attribute
{
    /// <summary>
    /// 注册当前服务类型
    /// </summary>
    /// <param name="serviceLifetime">生命周期</param>
    /// <param name="serviceKey">KeyedService</param>
    /// <param name="asSelf">是否注册自身服务</param>
    public AutoInjectAttribute(ServiceLifetime serviceLifetime = ServiceLifetime.Scoped, string? serviceKey = null, bool asSelf = true) { }
    /// <summary>
    /// 注册服务
    /// </summary>
    /// <param name="serviceLifetime">生命周期</param>
    /// <param name="type">服务类型</param>
    /// <param name="serviceKey">KeyedService</param>
    /// <param name="asSelf">是否注册自身服务</param>
    public AutoInjectAttribute(ServiceLifetime serviceLifetime, Type type, string? serviceKey = null, bool asSelf = true)
    {

    }
    /// <summary>
    /// 注册服务
    /// </summary>
    /// <param name="serviceLifetime">生命周期</param>
    /// <param name="type1">服务类型1</param>
    /// <param name="type2">服务类型2</param>
    /// <param name="serviceKey">KeyedService</param>
    /// <param name="asSelf">是否注册自身服务</param>
    public AutoInjectAttribute(ServiceLifetime serviceLifetime, Type type1, Type type2, string? serviceKey = null, bool asSelf = true)
    {

    }
    /// <summary>
    /// 注册服务
    /// </summary>
    /// <param name="serviceLifetime">生命周期</param>
    /// <param name="type1">服务类型1</param>
    /// <param name="type2">服务类型2</param>
    /// <param name="type3">服务类型3</param>
    /// <param name="serviceKey">KeyedService</param>
    /// <param name="asSelf">是否注册自身服务</param>
    public AutoInjectAttribute(ServiceLifetime serviceLifetime, Type type1, Type type2, Type type3, string? serviceKey = null, bool asSelf = true)
    {

    }
    /// <summary>
    /// 注册服务
    /// </summary>
    /// <param name="serviceLifetime">生命周期</param>
    /// <param name="type1">服务类型1</param>
    /// <param name="type2">服务类型2</param>
    /// <param name="type3">服务类型3</param>
    /// <param name="type4">服务类型4</param>
    /// <param name="serviceKey">KeyedService</param>
    /// <param name="asSelf">是否注册自身服务</param>
    public AutoInjectAttribute(ServiceLifetime serviceLifetime, Type type1, Type type2, Type type3, Type type4, string? serviceKey = null, bool asSelf = true)
    {

    }
    /// <summary>
    /// 注册服务
    /// </summary>
    /// <param name="serviceLifetime">生命周期</param>
    /// <param name="type1">服务类型1</param>
    /// <param name="type2">服务类型2</param>
    /// <param name="type3">服务类型3</param>
    /// <param name="type4">服务类型4</param>
    /// <param name="type5">服务类型5</param>
    /// <param name="serviceKey">KeyedService</param>
    /// <param name="asSelf">是否注册自身服务</param>
    public AutoInjectAttribute(ServiceLifetime serviceLifetime, Type type1, Type type2, Type type3, Type type4, Type type5, string? serviceKey = null, bool asSelf = true)
    {

    }
}
