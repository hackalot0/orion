using Orion.Structures;
using System;
using System.Collections.Generic;

namespace Orion;

public abstract class ContractRegistry : DisposableBase
{
    public class Set : Set<ContractRegistry> { }
    public class Set<T> : ItemSet<T> where T : ContractRegistry { }

    public Type ContractType { get; protected set; }
    public Type ImplementationType { get; protected set; }
    public Delegate? Activator { get; protected set; }
    public int Priority { get; set; }

    public IReadOnlyCollection<Type> Implementations => implementations;

    private readonly List<Type> implementations;

    public ContractRegistry(Type contractType, Type implementationType, Delegate? activator = default, int priority = 0)
    {
        ContractType = contractType;
        ImplementationType = implementationType;
        Activator = activator;
        Priority = priority;

        implementations = [.. ImplementationType.GetInterfaces()];
    }

    public abstract bool TryActivate<T>(out T? activatedComponent);
}