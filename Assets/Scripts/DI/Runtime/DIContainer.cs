using System;
using System.Collections.Generic;
using System.Linq;
using DI.Attributes;
using DI.Utils;
using UnityEngine;
using Object = UnityEngine.Object;

namespace DI
{
    public class DIContainer
    {
        private readonly DIContainer parent;
        private readonly Dictionary<Type, Binding> bindings = new();
        private readonly HashSet<Type> resolutions = new();

        public DIContainer(DIContainer parent = null)
        {
            this.parent = parent;
        }

        public void Bind<T>(Func<DIContainer, T> factory)
        {
            Bind(factory, true);
        }

        public void BindTransient<T>(Func<DIContainer, T> factory)
        {
            Bind(factory, false);
        }

        public void BindInstance<T>(T instance)
        {
            var type = typeof(T);
            
            if (bindings.ContainsKey(type))
            {
                throw new Exception($"Factory with type {type.FullName} is already registered");
            }

            var binding = new Binding
            {
                IsSingleton = true,
                Instance = instance
            };
            
            Inject(instance);
            
            bindings.Add(type, binding);
        }

        public object Resolve(Type type)
        {
            if (resolutions.Contains(type))
            {
                throw new Exception($"Cyclic dependency for type {type.FullName}");
            }

            resolutions.Add(type);

            try
            {
                if (bindings.TryGetValue(type, out var registration))
                {
                    if (registration.IsSingleton)
                    {
                        var factory = registration.Factory;

                        if (registration.Instance == null && factory != null)
                        {
                            registration.Instance = factory(this);
                        }

                        return registration.Instance;
                    }

                    return registration.Instance;
                }

                if (parent != null)
                {
                    return parent.Resolve(type);
                }
            }
            finally
            {
                resolutions.Remove(type);
            }

            throw new Exception($"Couldn't find dependency for type: {type.FullName}");
        }

        public T Resolve<T>()
        {
            var type = typeof(T);

            return (T)Resolve(type);
        }

        public void Inject(object target)
        {
            var type = target.GetType();
            
            // Field injection
            var injectableFields = type.GetFields(Settings.BINDING_FLAGS)
                .Where(member => Attribute.IsDefined(member, typeof(InjectAttribute)));

            foreach (var injectableField in injectableFields) {
                if (injectableField.GetValue(target) != null) {
                    Debug.LogWarning($"Field '{injectableField.Name}' of class '{type.FullName}' is already set.");
                    continue;
                }
                var fieldType = injectableField.FieldType;
                var resolvedInstance = Resolve(fieldType);
                if (resolvedInstance == null) {
                    throw new Exception($"Failed to inject dependency into field '{injectableField.Name}' of class '{type.Name}'.");
                }
                
                injectableField.SetValue(target, resolvedInstance);
            }
            
            // Property injection
            var injectableProperties = type.GetProperties(Settings.BINDING_FLAGS)
                .Where(member => Attribute.IsDefined(member, typeof(InjectAttribute)));
            foreach (var injectableProperty in injectableProperties) {
                var propertyType = injectableProperty.PropertyType;
                var resolvedInstance = Resolve(propertyType);
                if (resolvedInstance == null) {
                    throw new Exception($"Failed to inject dependency into property '{injectableProperty.Name}' of class '{type.Name}'.");
                }

                injectableProperty.SetValue(target, resolvedInstance);
            }
            
            // Method injection
            var injectableMethods = type.GetMethods(Settings.BINDING_FLAGS)
                .Where(member => Attribute.IsDefined(member, typeof(InjectAttribute)));

            foreach (var injectableMethod in injectableMethods) {
                var requiredParameters = injectableMethod.GetParameters()
                    .Select(parameter => parameter.ParameterType)
                    .ToArray();
                var resolvedInstances = requiredParameters.Select(Resolve).ToArray();
                if (resolvedInstances.Any(resolvedInstance => resolvedInstance == null)) {
                    throw new Exception($"Failed to inject dependencies into method '{injectableMethod.Name}' of class '{type.Name}'.");
                }
                
                injectableMethod.Invoke(target, resolvedInstances);
            }
        }

        public T Instantiate<T>(T obj) where T : MonoBehaviour
        {
            var instance = Object.Instantiate(obj);

            foreach (var component in instance.GetComponentsInChildren<MonoBehaviour>())
            {
                Inject(component);
            }

            return instance;
        }

        private void Bind<T>(Func<DIContainer, T> factory, bool isSingleton)
        {
            var type = typeof(T);
            
            if (bindings.ContainsKey(type))
            {
                throw new Exception($"Factory with type: {type.FullName} is already registered");
            }

            var binding = new Binding
            {
                Factory = container =>
                {
                    var o = factory(container);
                    Inject(o);
                    
                    return o;
                },
                IsSingleton = isSingleton
            };
            
            bindings.Add(type, binding);
        }
    }
}
