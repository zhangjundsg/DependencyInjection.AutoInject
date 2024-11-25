using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyInjection.AutoInject_Test
{

    public interface IServices { }
    [AutoInject(ServiceLifetime.Transient, typeof(IServices), "key1", true)]
    public class ServiceA : IServices { }
    [AutoInject(ServiceLifetime.Transient, typeof(IServices), "key2", true)]
    public class ServiceB : IServices { }




}
