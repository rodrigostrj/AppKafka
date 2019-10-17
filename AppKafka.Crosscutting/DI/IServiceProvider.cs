using System;
using System.Collections.Generic;
using System.Text;

namespace AppKafka.Crosscutting.DI
{
    public interface IServiceProvider
    {
        object GetService(Type serviceType);
    }
}
