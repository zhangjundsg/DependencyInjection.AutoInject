using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyInjection.AutoInject_Test
{

    public interface IServices { }
    [AutoInject(ServiceLifetime.Transient, typeof(IServices), "key1", false)]
    public class ServiceA : IServices { }
    [AutoInject(ServiceLifetime.Transient, typeof(IServices), "key2", false)]
    public class ServiceB : IServices { }

    public interface IGenreicServicesA<T> { }

    [AutoInject(ServiceLifetime.Scoped, typeof(IGenreicServicesA<>))]
    public class GenreicServiceA<T> : IGenreicServicesA<T> { }


    public interface IGenreicServicesB<T, D> { }
    [AutoInject(ServiceLifetime.Singleton, typeof(IGenreicServicesB<,>))]
    public class GenreicServiceB<T, D> : IGenreicServicesB<T, D> { }

 

}
