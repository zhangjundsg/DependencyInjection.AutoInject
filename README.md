# DependencyInjection.AutoInject

使用source generator的di注入扩展，支持aot

## 如何使用

### 1、nuget安装

```xml
<PackageReference Include="DependencyInjection.AutoInject" Version="1.0.1" />
```

### 2、参数

```c#
public AutoInjectAttribute(ServiceLifetime serviceLifetime, Type type, string? serviceKey = null, bool asSelf = true)
```

- serviceLifetime生命周期  默认`Scoped`
- type  服务类型
- serviceKey   默认为空不注册为KeyedService
- asSelf 是否注册自身服务 默认`true`
  多个重载F12查看

### 3、Attribute标记

注册自身

```c#
[AutoInject]
public class ServiceA { }
```

注册接口

```c#
[AutoInject(ServiceLifetime.Transient, typeof(IServices))]
public class ServiceA : IServices { }
```

支持泛型

```c#
[AutoInject(ServiceLifetime.Scoped, typeof(IGenreicServicesA<>))]
public class GenreicServiceA<T> : IGenreicServicesA<T> { }
```

支持KeyedService

```c#
[AutoInject(ServiceLifetime.Transient, typeof(IServices), "key1", false)]
public class ServiceA : IServices { }
[AutoInject(ServiceLifetime.Transient, typeof(IServices), "key2", false)]
public class ServiceB : IServices { }
```

### 4、服务注册

```
services.Add{AssemblyName}();
```

AssemblyName为服务所在的程序集名称 如：

```c#
var services = new ServiceCollection();
services.AddConsoleApp2();
```

### 分析器查看生成文件

```c#
using Microsoft.Extensions.DependencyInjection;
namespace Microsoft.Extensions.DependencyInjection;
public static class DependencyInjectionExtensions_G
{
    public static IServiceCollection AddConsoleApp2(this IServiceCollection services)
    {
	services.Add(ServiceDescriptor.KeyedTransient(typeof(global::DependencyInjection.AutoInject_Test.IServices),"key1",typeof(global::DependencyInjection.AutoInject_Test.ServiceA)));
	services.Add(ServiceDescriptor.KeyedTransient(typeof(global::DependencyInjection.AutoInject_Test.IServices),"key2",typeof(global::DependencyInjection.AutoInject_Test.ServiceB)));
        services.Add(ServiceDescriptor.Scoped(typeof(global::ConsoleApp2.Service),typeof(global::ConsoleApp2.Service)));
        services.Add(ServiceDescriptor.Scoped(typeof(global::ConsoleApp2.IService),typeof(global::ConsoleApp2.Service)));
        services.Add(ServiceDescriptor.Scoped(typeof(global::ConsoleApp2.IGenreicService<>),typeof(global::ConsoleApp2.GenreicService<>)));
        return services;
    }
}
```