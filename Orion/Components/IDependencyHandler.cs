using System;

namespace Orion.Components;

public interface IDependencyHandler
{
    bool TryResolve(Type type, out object? dependency);
}