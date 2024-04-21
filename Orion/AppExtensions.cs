using Orion.Components;
using System;
using System.Linq;
using System.Reflection;

namespace Orion;

public static class AppExtensions
{
    private static readonly Assembly __a_Orion = typeof(AppExtensions).Assembly;
    private static readonly Assembly[] __customAssemblies = new Assembly[] { Assembly.GetEntryAssembly(), Assembly.GetCallingAssembly(), Assembly.GetExecutingAssembly() }
        .Where(assembly => !assembly.Equals(__a_Orion))
        .ToArray();

    public static App LoadDefaults(this App app)
    {
        app.Services.Clear();
        app.Components.Clear();

        app.RegisterComponent<DependencyHandler>();
        __customAssemblies.ForEach(assembly => app.AddServices(assembly));
        return app;
    }

    public static App RegisterComponent<TComponent>(this App app, Func<TComponent, TComponent>? activator = default, int priority = 0)
    {
        app.Components.Add(new ComponentRegistry<TComponent, TComponent>(activator, priority));
        return app;
    }
    public static App RegisterComponent<TComponent, TImplementation>(this App app, Func<TComponent, TImplementation>? activator = default, int priority = 0) where TImplementation : TComponent
    {
        app.Components.Add(new ComponentRegistry<TComponent, TImplementation>(activator, priority));
        return app;
    }
    public static App RegisterComponent(this App app, Type componentType, Type implementationType, Delegate? activator = default, int priority = 0)
    {
        app.Components.Add(new ComponentRegistry(componentType, implementationType, activator, priority));
        return app;
    }

    public static App AddService<TService>(this App app, ServiceLifetime lifetime = ServiceLifetime.Singleton, Func<TService, TService>? activator = default, int priority = 0)
    {
        app.Services.Add(new ServiceRegistry<TService, TService>(lifetime, activator, priority));
        return app;
    }
    public static App AddService<TService, TImplementation>(this App app, ServiceLifetime lifetime = ServiceLifetime.Singleton, Func<TService, TImplementation>? activator = default, int priority = 0) where TImplementation : TService
    {
        app.Services.Add(new ServiceRegistry<TService, TImplementation>(lifetime, activator, priority));
        return app;
    }
    public static App AddService(this App app, Type serviceType, Type implementationType, ServiceLifetime lifetime = ServiceLifetime.Singleton, Delegate? activator = default, int priority = 0)
    {
        app.Services.Add(new ServiceRegistry(serviceType, implementationType, lifetime, activator, priority));
        return app;
    }
    public static App AddServices(this App app, Assembly assembly)
    {
        var types = assembly.DefinedTypes
            .Where(a => !a.IsInterface)
            .Where(a => !a.IsNotPublic)
            .Where(a => !a.IsNestedPrivate)
            .ToList();

        for (int i = 0; i < types.Count; i++)
        {
            var type = types[i];
            var interfaces = type.GetInterfaces();
            if (interfaces.Length == 0) AddService(app, type, type);
            else interfaces.ForEach(ti => AddService(app, ti, type));
        }

        return app;
    }

    public static App SetEntryPoint(this App app, Action entryPoint)
    {
        app.EntryPoint = entryPoint;
        return app;
    }
    public static App SetEntryPoint<T>(this App app, Action<T> entryPoint)
    {
        app.EntryPoint = () =>
        {
            var s1 = app.GetService<T>()!;
            entryPoint(s1);
        };
        return app;
    }
    public static App SetEntryPoint<T1, T2>(this App app, Action<T1, T2> entryPoint)
    {
        app.EntryPoint = () =>
        {
            var s1 = app.GetService<T1>()!;
            var s2 = app.GetService<T2>()!;
            entryPoint(s1, s2);
        };
        return app;
    }
    public static App SetEntryPoint<T1, T2, T3>(this App app, Action<T1, T2, T3> entryPoint)
    {
        app.EntryPoint = () =>
        {
            var s1 = app.GetService<T1>()!;
            var s2 = app.GetService<T2>()!;
            var s3 = app.GetService<T3>()!;
            entryPoint(s1, s2, s3);
        };
        return app;
    }
    public static App SetEntryPoint<T1, T2, T3, T4>(this App app, Action<T1, T2, T3, T4> entryPoint)
    {
        app.EntryPoint = () =>
        {
            var s1 = app.GetService<T1>()!;
            var s2 = app.GetService<T2>()!;
            var s3 = app.GetService<T3>()!;
            var s4 = app.GetService<T4>()!;
            entryPoint(s1, s2, s3, s4);
        };
        return app;
    }
    public static App SetEntryPoint<T1, T2, T3, T4, T5>(this App app, Action<T1, T2, T3, T4, T5> entryPoint)
    {
        app.EntryPoint = () =>
        {
            var s1 = app.GetService<T1>()!;
            var s2 = app.GetService<T2>()!;
            var s3 = app.GetService<T3>()!;
            var s4 = app.GetService<T4>()!;
            var s5 = app.GetService<T5>()!;
            entryPoint(s1, s2, s3, s4, s5);
        };
        return app;
    }
    public static App SetEntryPoint<T1, T2, T3, T4, T5, T6>(this App app, Action<T1, T2, T3, T4, T5, T6> entryPoint)
    {
        app.EntryPoint = () =>
        {
            var s1 = app.GetService<T1>()!;
            var s2 = app.GetService<T2>()!;
            var s3 = app.GetService<T3>()!;
            var s4 = app.GetService<T4>()!;
            var s5 = app.GetService<T5>()!;
            var s6 = app.GetService<T6>()!;
            entryPoint(s1, s2, s3, s4, s5, s6);
        };
        return app;
    }
    public static App SetEntryPoint<T1, T2, T3, T4, T5, T6, T7>(this App app, Action<T1, T2, T3, T4, T5, T6, T7> entryPoint)
    {
        app.EntryPoint = () =>
        {
            var s1 = app.GetService<T1>()!;
            var s2 = app.GetService<T2>()!;
            var s3 = app.GetService<T3>()!;
            var s4 = app.GetService<T4>()!;
            var s5 = app.GetService<T5>()!;
            var s6 = app.GetService<T6>()!;
            var s7 = app.GetService<T7>()!;
            entryPoint(s1, s2, s3, s4, s5, s6, s7);
        };
        return app;
    }
    public static App SetEntryPoint<T1, T2, T3, T4, T5, T6, T7, T8>(this App app, Action<T1, T2, T3, T4, T5, T6, T7, T8> entryPoint)
    {
        app.EntryPoint = () =>
        {
            var s1 = app.GetService<T1>()!;
            var s2 = app.GetService<T2>()!;
            var s3 = app.GetService<T3>()!;
            var s4 = app.GetService<T4>()!;
            var s5 = app.GetService<T5>()!;
            var s6 = app.GetService<T6>()!;
            var s7 = app.GetService<T7>()!;
            var s8 = app.GetService<T8>()!;
            entryPoint(s1, s2, s3, s4, s5, s6, s7, s8);
        };
        return app;
    }
    public static App SetEntryPoint<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this App app, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> entryPoint)
    {
        app.EntryPoint = () =>
        {
            var s1 = app.GetService<T1>()!;
            var s2 = app.GetService<T2>()!;
            var s3 = app.GetService<T3>()!;
            var s4 = app.GetService<T4>()!;
            var s5 = app.GetService<T5>()!;
            var s6 = app.GetService<T6>()!;
            var s7 = app.GetService<T7>()!;
            var s8 = app.GetService<T8>()!;
            var s9 = app.GetService<T9>()!;
            entryPoint(s1, s2, s3, s4, s5, s6, s7, s8, s9);
        };
        return app;
    }
    public static App SetEntryPoint<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this App app, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> entryPoint)
    {
        app.EntryPoint = () =>
        {
            var s1 = app.GetService<T1>()!;
            var s2 = app.GetService<T2>()!;
            var s3 = app.GetService<T3>()!;
            var s4 = app.GetService<T4>()!;
            var s5 = app.GetService<T5>()!;
            var s6 = app.GetService<T6>()!;
            var s7 = app.GetService<T7>()!;
            var s8 = app.GetService<T8>()!;
            var s9 = app.GetService<T9>()!;
            var s10 = app.GetService<T10>()!;
            entryPoint(s1, s2, s3, s4, s5, s6, s7, s8, s9, s10);
        };
        return app;
    }
    public static App SetEntryPoint<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(this App app, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> entryPoint)
    {
        app.EntryPoint = () =>
        {
            var s1 = app.GetService<T1>()!;
            var s2 = app.GetService<T2>()!;
            var s3 = app.GetService<T3>()!;
            var s4 = app.GetService<T4>()!;
            var s5 = app.GetService<T5>()!;
            var s6 = app.GetService<T6>()!;
            var s7 = app.GetService<T7>()!;
            var s8 = app.GetService<T8>()!;
            var s9 = app.GetService<T9>()!;
            var s10 = app.GetService<T10>()!;
            var s11 = app.GetService<T11>()!;
            entryPoint(s1, s2, s3, s4, s5, s6, s7, s8, s9, s10, s11);
        };
        return app;
    }
    public static App SetEntryPoint<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(this App app, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> entryPoint)
    {
        app.EntryPoint = () =>
        {
            var s1 = app.GetService<T1>()!;
            var s2 = app.GetService<T2>()!;
            var s3 = app.GetService<T3>()!;
            var s4 = app.GetService<T4>()!;
            var s5 = app.GetService<T5>()!;
            var s6 = app.GetService<T6>()!;
            var s7 = app.GetService<T7>()!;
            var s8 = app.GetService<T8>()!;
            var s9 = app.GetService<T9>()!;
            var s10 = app.GetService<T10>()!;
            var s11 = app.GetService<T11>()!;
            var s12 = app.GetService<T12>()!;
            entryPoint(s1, s2, s3, s4, s5, s6, s7, s8, s9, s10, s11, s12);
        };
        return app;
    }
    public static App SetEntryPoint<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(this App app, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> entryPoint)
    {
        app.EntryPoint = () =>
        {
            var s1 = app.GetService<T1>()!;
            var s2 = app.GetService<T2>()!;
            var s3 = app.GetService<T3>()!;
            var s4 = app.GetService<T4>()!;
            var s5 = app.GetService<T5>()!;
            var s6 = app.GetService<T6>()!;
            var s7 = app.GetService<T7>()!;
            var s8 = app.GetService<T8>()!;
            var s9 = app.GetService<T9>()!;
            var s10 = app.GetService<T10>()!;
            var s11 = app.GetService<T11>()!;
            var s12 = app.GetService<T12>()!;
            var s13 = app.GetService<T13>()!;
            entryPoint(s1, s2, s3, s4, s5, s6, s7, s8, s9, s10, s11, s12, s13);
        };
        return app;
    }
    public static App SetEntryPoint<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(this App app, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> entryPoint)
    {
        app.EntryPoint = () =>
        {
            var s1 = app.GetService<T1>()!;
            var s2 = app.GetService<T2>()!;
            var s3 = app.GetService<T3>()!;
            var s4 = app.GetService<T4>()!;
            var s5 = app.GetService<T5>()!;
            var s6 = app.GetService<T6>()!;
            var s7 = app.GetService<T7>()!;
            var s8 = app.GetService<T8>()!;
            var s9 = app.GetService<T9>()!;
            var s10 = app.GetService<T10>()!;
            var s11 = app.GetService<T11>()!;
            var s12 = app.GetService<T12>()!;
            var s13 = app.GetService<T13>()!;
            var s14 = app.GetService<T14>()!;
            entryPoint(s1, s2, s3, s4, s5, s6, s7, s8, s9, s10, s11, s12, s13, s14);
        };
        return app;
    }
    public static App SetEntryPoint<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(this App app, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> entryPoint)
    {
        app.EntryPoint = () =>
        {
            var s1 = app.GetService<T1>()!;
            var s2 = app.GetService<T2>()!;
            var s3 = app.GetService<T3>()!;
            var s4 = app.GetService<T4>()!;
            var s5 = app.GetService<T5>()!;
            var s6 = app.GetService<T6>()!;
            var s7 = app.GetService<T7>()!;
            var s8 = app.GetService<T8>()!;
            var s9 = app.GetService<T9>()!;
            var s10 = app.GetService<T10>()!;
            var s11 = app.GetService<T11>()!;
            var s12 = app.GetService<T12>()!;
            var s13 = app.GetService<T13>()!;
            var s14 = app.GetService<T14>()!;
            var s15 = app.GetService<T15>()!;
            entryPoint(s1, s2, s3, s4, s5, s6, s7, s8, s9, s10, s11, s12, s13, s14, s15);
        };
        return app;
    }
    public static App SetEntryPoint<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(this App app, Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> entryPoint)
    {
        app.EntryPoint = () =>
        {
            var s1 = app.GetService<T1>()!;
            var s2 = app.GetService<T2>()!;
            var s3 = app.GetService<T3>()!;
            var s4 = app.GetService<T4>()!;
            var s5 = app.GetService<T5>()!;
            var s6 = app.GetService<T6>()!;
            var s7 = app.GetService<T7>()!;
            var s8 = app.GetService<T8>()!;
            var s9 = app.GetService<T9>()!;
            var s10 = app.GetService<T10>()!;
            var s11 = app.GetService<T11>()!;
            var s12 = app.GetService<T12>()!;
            var s13 = app.GetService<T13>()!;
            var s14 = app.GetService<T14>()!;
            var s15 = app.GetService<T15>()!;
            var s16 = app.GetService<T16>()!;
            entryPoint(s1, s2, s3, s4, s5, s6, s7, s8, s9, s10, s11, s12, s13, s14, s15, s16);
        };
        return app;
    }

    public static App SetEntryClass<T>(this App app) => SetEntryClass(app, typeof(T));
    public static App SetEntryClass(this App app, Type entryClass)
    {
        AddService(app, entryClass, entryClass, ServiceLifetime.Singleton);
        SetEntryPoint(app, () => App.Current.GetService(entryClass));
        return app;
    }

    public static bool TryGetComponentRegistry<TComponent>(this App app, out ComponentRegistry? componentRegistry) => TryGetComponentRegistry(app, typeof(TComponent), out componentRegistry);
    public static bool TryGetComponentRegistry(this App app, Type componentType, out ComponentRegistry? componentRegistry)
    {
        componentRegistry = app.Components
            .Where(c => c.Implementations.Any(componentType.IsAssignableFrom))
            .OrderByDescending(c => c.Priority)
            .FirstOrDefault();

        return componentRegistry is not null;
    }
    public static bool TryGetComponent<TComponent>(this App app, out TComponent? component)
    {
        component = default;
        if (!TryGetComponentRegistry<TComponent>(app, out var componentRegistry)) return false;
        if (componentRegistry is null) return false;
        if (!componentRegistry.TryActivate(out component)) return false;
        return component is not null;
    }

    public static bool TryGetServiceRegistry<TService>(this App app, out ServiceRegistry? serviceRegistry) => TryGetServiceRegistry(app, typeof(TService), out serviceRegistry);
    public static bool TryGetServiceRegistry(this App app, Type serviceType, out ServiceRegistry? serviceRegistry)
    {
        serviceRegistry = app.Services
            .Where(c => serviceType.IsAssignableFrom(c.ServiceType) || c.Implementations.Any(serviceType.IsAssignableFrom))
            .OrderByDescending(c => c.Priority)
            .FirstOrDefault();

        return serviceRegistry is not null;
    }
    public static bool TryGetService<TService>(this App app, out TService? service)
    {
        service = default;
        if (!TryGetServiceRegistry<TService>(app, out var serviceRegistry)) return false;
        if (serviceRegistry is null) return false;
        if (!serviceRegistry.TryActivate(out service)) return false;
        return service is not null;
    }
    public static bool TryGetService(this App app, Type serviceType, out object? service)
    {
        service = default;
        if (!TryGetServiceRegistry(app, serviceType, out var serviceRegistry)) return false;
        if (serviceRegistry is null) return false;
        if (!serviceRegistry.TryActivate(out service)) return false;
        return service is not null;
    }

    public static T? GetService<T>(this App app) => TryGetService<T>(app, out var service) ? service : default;
    public static object? GetService(this App app, Type serviceType) => TryGetService(app, serviceType, out var service) ? service : default;
}