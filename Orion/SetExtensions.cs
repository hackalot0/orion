using Orion.Structures;
using System;
using System.Collections.Generic;

namespace Orion;

public static class SetExtensions
{
    public static IEnumerable<T> ForEach<T>(this IEnumerable<T> source, Action<T> action)
    {
        switch (source)
        {
            case IList<T> list:
                for (int i = 0; i < list.Count; i++) action(list[i]);
                break;

            default:
                foreach (T item in source) action(item);
                break;
        }
        return source;
    }
    public static IEnumerable<T> For<T>(this IEnumerable<T> source, Action<T, int> action)
    {
        int i = 0;
        switch (source)
        {
            case IList<T> list:
                while (i < list.Count) action(list[i], i++);
                break;

            default:
                foreach (T item in source) action(item, i++);
                break;
        }
        return source;
    }

    public static SetOperationResult AddOrSkip<T>(this ICollection<T> target, T item)
    {
        if (target.Contains(item)) return SetOperationResult.Skipped;
        target.Add(item);
        return SetOperationResult.Inserted;
    }
    public static SetOperationResult AddOrSkip<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, TValue value)
    {
        if (dict.ContainsKey(key)) return SetOperationResult.Skipped;
        dict.Add(key, value);
        return SetOperationResult.Inserted;
    }
    public static SetOperationResult AddOrReplace<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, TValue value)
    {
        if (dict.ContainsKey(key))
        {
            dict[key] = value;
            return SetOperationResult.Replaced;
        }
        dict.Add(key, value);
        return SetOperationResult.Inserted;
    }

    public static ICollection<T> AddRange<T>(this ICollection<T> target, IEnumerable<T> items)
    {
        items.ForEach(target.Add);
        return target;
    }
    public static ICollection<T> RemoveRange<T>(this ICollection<T> target, IEnumerable<T> items)
    {
        items.ForEach(item => target.Remove(item));
        return target;
    }

    public static IEnumerable<T> AddTo<T>(this IEnumerable<T> source, ICollection<T> target)
    {
        source.ForEach(target.Add);
        return source;
    }
    public static IEnumerable<T> AddOrSkipTo<T>(this IEnumerable<T> source, ICollection<T> target)
    {
        source.ForEach(item => target.AddOrSkip(item));
        return source;
    }

    public static IEnumerable<TValue> AddTo<TKey, TValue>(this IEnumerable<TValue> source, IDictionary<TKey, TValue> target, Func<TValue, TKey> keySelector)
    {
        source.ForEach(item => target.Add(keySelector(item), item));
        return source;
    }
    public static IEnumerable<TValue> AddOrSkipTo<TKey, TValue>(this IEnumerable<TValue> source, IDictionary<TKey, TValue> target, Func<TValue, TKey> keySelector)
    {
        source.ForEach(item => target.AddOrSkip(keySelector(item), item));
        return source;
    }
}