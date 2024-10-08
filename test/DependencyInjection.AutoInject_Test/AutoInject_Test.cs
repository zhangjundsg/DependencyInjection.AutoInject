using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace DependencyInjection.AutoInject_Test;
public class AutoInject_Test
{
    [Fact]
    public void Test()
    {
        var services = new ServiceCollection();
        services.AddDependencyInjectionAutoInjectTest();
        var root = services.BuildServiceProvider();

        var service = root.GetService<ServiceD>();
        Assert.NotNull(service);

    }
    [Fact]
    public void Test_Generic()
    {
        var services = new ServiceCollection();
        services.AddDependencyInjectionAutoInjectTest();
        var root = services.BuildServiceProvider();

        var service = root.GetService<IGenreicServicesA<ServiceD>>();
        
        Assert.NotNull(service);
    }
    [Fact]
    public void Test_ServiceKey()
    {
        var services = new ServiceCollection();
        services.AddDependencyInjectionAutoInjectTest();
        var root = services.BuildServiceProvider();

        var serviceA = root.GetKeyedService<IServices>("key1");
        Assert.NotNull(serviceA);

        var serviceB = root.GetKeyedService<IServices>("key2");
        Assert.NotNull(serviceB);

        Assert.NotEqual(serviceA,serviceB);
    }
}