using System;

namespace DI
{
    public class Binding
    {
        public Func<DIContainer, object> Factory;
        public bool IsSingleton;
        public object Instance;
    }
}