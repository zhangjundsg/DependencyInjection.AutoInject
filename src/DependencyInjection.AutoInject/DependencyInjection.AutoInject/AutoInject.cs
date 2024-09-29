using System;

namespace Microsoft.Extensions.DependencyInjection
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public sealed class AutoInjectAttribute : Attribute
    {
        /// <summary>
        /// 注册当前服务类型
        /// </summary>
        /// <param name="serviceLifetime">生命周期</param>
        public AutoInjectAttribute(ServiceLifetime serviceLifetime) { }
        /// <summary>
        /// 注册服务
        /// </summary>
        /// <param name="serviceLifetime">生命周期</param>
        /// <param name="type">服务类型</param>
        public AutoInjectAttribute(ServiceLifetime serviceLifetime, Type type)
        {

        }
        /// <summary>
        /// 注册服务
        /// </summary>
        /// <param name="serviceLifetime">生命周期</param>
        /// <param name="type1">服务类型1</param>
        /// <param name="type2">服务类型2</param>
        public AutoInjectAttribute(ServiceLifetime serviceLifetime, Type type1, Type type2)
        {

        }
        /// <summary>
        /// 注册服务
        /// </summary>
        /// <param name="serviceLifetime">生命周期</param>
        /// <param name="type1">服务类型1</param>
        /// <param name="type2">服务类型2</param>
        /// <param name="type3">服务类型3</param>
        public AutoInjectAttribute(ServiceLifetime serviceLifetime, Type type1, Type type2, Type type3)
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
        public AutoInjectAttribute(ServiceLifetime serviceLifetime, Type type1, Type type2, Type type3, Type type4)
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
        public AutoInjectAttribute(ServiceLifetime serviceLifetime, Type type1, Type type2, Type type3, Type type4, Type type5)
        {

        }
    }
}
