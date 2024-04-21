namespace Orion.Events;

public class ItemChangeEvent<TKey, TItem> : ItemEvent<TKey, TItem>
{
    public new delegate void Handler(Args args);

    public new class Args(object? sender, TKey key, TItem oldItem, TItem item) : ItemEvent<TKey, TItem>.Args(sender, key, item)
    {
        public TItem OldItem { get; set; } = oldItem;
    }
}
public class ItemChangeEvent<T> : ItemEvent<T>
{
    public new delegate void Handler(Args args);

    public new class Args(object? sender, T oldItem, T item) : ItemEvent<T>.Args(sender, item)
    {
        public T OldItem { get; set; } = oldItem;
    }
}