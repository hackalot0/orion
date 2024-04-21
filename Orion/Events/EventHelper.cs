using System;

namespace Orion.Events;

public static class EventHelper
{
    public static void Change<T>(ref T target, T value, Action<T, T> handler)
    {
        if (Equals(target, value)) return;
        var oldValue = target;
        target = value;
        handler?.Invoke(oldValue, target);
    }
    public static void Change<T>(ref T target, T value, ItemChangeEvent<T>.Handler? handler, object? sender = default)
    {
        if (Equals(target, value)) return;
        var oldValue = target;
        target = value;
        handler?.Invoke(new ItemChangeEvent<T>.Args(sender, oldValue, target));
    }
}