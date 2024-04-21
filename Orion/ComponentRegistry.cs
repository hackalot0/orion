using System;

namespace Orion;

public class ComponentRegistry<TComponent, TImplementation>(Func<TComponent, TImplementation>? activator = default, int priority = 0) : ComponentRegistry(typeof(TComponent), typeof(TImplementation), activator, priority) { }
public class ComponentRegistry(Type componentType, Type implementationType, Delegate? activator = default, int priority = 0) : ContractRegistry(componentType, implementationType, activator, priority)
{
    public new class Set : Set<ComponentRegistry> { }

    public Type ComponentType => ContractType;

    private readonly object locker = new();

    private object? componentCache;

    public override bool TryActivate<T>(out T? activatedComponent) where T : default
    {
        lock (locker)
        {
            if (componentCache is not T tService) tService = (T)(componentCache = InternalActivate())!;
            activatedComponent = tService;
            return activatedComponent is not null;
        }
    }

    protected virtual object? InternalActivate() => (Activator ?? DefaultActivator).DynamicInvoke();
    protected virtual object? DefaultActivator()
    {
        var tI = ImplementationType;
        return tI.Assembly.CreateInstance(tI.FullName);
    }
}