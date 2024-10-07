using Microsoft.Extensions.DependencyInjection;

namespace DependencyInjection.AutoInject_Test;
public class AutoInject_Test
{
    [Fact]
    public void Test1()
    {
        var services = new ServiceCollection();
        services.AddDependencyInjectionAutoInjectTest();

        var root = services.BuildServiceProvider();

        var service = root.GetRequiredService<Iservices>();

        Assert.NotNull(service);
    }
    [Fact]
    public void Test2()
    {
        var services = new ServiceCollection();
        services.AddDependencyInjectionAutoInjectTest();

        var root = services.BuildServiceProvider();

        var service = root.GetRequiredService<DeviceService>();

        Assert.NotNull(service);
    }
}