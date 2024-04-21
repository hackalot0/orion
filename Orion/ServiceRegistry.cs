using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Orion.Components;

namespace Orion;

public class ServiceRegistry<TComponent, TImplementation>(ServiceLifetime lifetime, Func<TComponent, TImplementation>? activator = default, int priority = 0) : ServiceRegistry(typeof(TComponent), typeof(TImplementation), lifetime, activator, priority) { }
public class ServiceRegistry(Type serviceType, Type implementationType, ServiceLifetime lifetime, Delegate? activator = default, int priority = 0) : ContractRegistry(serviceType, implementationType, activator, priority)
{
    public new class Set : Set<ServiceRegistry> { }

    public Type ServiceType => ContractType;
    public ServiceLifetime Lifetime { get; protected set; } = lifetime;

    private readonly object locker = new();

    private object? serviceCache;

    public override bool TryActivate<T>(out T? activatedComponent) where T : default
    {
        lock (locker)
        {
            activatedComponent = default;
            switch (Lifetime)
            {
                case ServiceLifetime.Transient:
                    activatedComponent = (T)InternalActivate()!;
                    break;

                case ServiceLifetime.Singleton:
                    if (serviceCache is not T tService) tService = (T)(serviceCache = InternalActivate())!;
                    activatedComponent = tService;
                    break;
            }
            return activatedComponent is not null;
        }
    }

    protected virtual IDependencyHandler GetDependencyHandler()
    {
        if (!App.Current.TryGetComponent<IDependencyHandler>(out var dependencyHandler) || dependencyHandler is null)
            throw ExceptionHelper.MissingAppComponent(nameof(IDependencyHandler));

        return dependencyHandler;
    }

    protected virtual object? InternalActivate() => (Activator ?? DefaultActivator).DynamicInvoke();
    protected virtual object? DefaultActivator() => GetDependencyHandler().TryResolve(ImplementationType, out var instance) ? instance : null;
}