﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyInjection.AutoInject_Test
{
    [AutoInject(ServiceLifetime.Scoped, typeof(Iservices))]
    public class Services : Iservices
    {

    }
    [AutoInject(ServiceLifetime.Scoped,true)]
    public record DeviceService(string name,int age);

    public interface Iservices { }
}
