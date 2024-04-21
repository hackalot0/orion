using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Orion.Components;

public class DependencyHandler : IDependencyHandler
{
    private static readonly BindingFlags _bfCtor = BindingFlags.Public | BindingFlags.Instance;

    public bool TryResolve(Type type, out object? dependency)
    {
        if (type.IsInterface)
        {
            if (!App.Current.TryGetServiceRegistry(type, out var serviceRegistry) || serviceRegistry is null)
                throw ExceptionHelper.ServiceNotFound(type.FullName);
            type = serviceRegistry.ImplementationType;
        }

        dependency = default;

        var ciList = type.GetConstructors(_bfCtor);
        var ciDict = ciList.ToDictionary(key => key, val => val.GetParameters().ToList());

        var ciQueue = new Queue<KeyValuePair<ConstructorInfo, List<ParameterInfo>>>(ciDict.OrderByDescending(ci => ci.Value.Count));

        while (ciQueue.Count > 0)
        {
            var ci = ciQueue.Dequeue();
            var ciArgs = ci.Value.Select(a => a.ParameterType).Distinct().ToList();
            if (ciArgs.Count == 0)
            {
                dependency = Activator.CreateInstance(type);
                return dependency is not null;
            }

            try
            {
                var ciArgsResolved = ciArgs.ToDictionary(type => type, type => TryResolve(type, out var dependency) ? dependency : null);
                var ciArgsArray = ci.Value.Select(pi => ciArgsResolved[pi.ParameterType]).ToArray();
                dependency = ci.Key.Invoke(ciArgsArray);
                return dependency is not null;
            }
            catch
            {
                continue;
            }
        }

        return false;
    }
}