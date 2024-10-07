using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace DependencyInjection.AutoInject_Test
{
    public class AutoInject_Test
    {
        [Fact]
        public void Test1()
        {
            var services = new ServiceCollection();
            services.AddScoped<global::System.IEquatable<global::DependencyInjection.AutoInject_Test.DeviceService>, global::DependencyInjection.AutoInject_Test.DeviceService>();
        }
    }
}