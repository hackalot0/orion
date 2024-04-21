namespace Orion.Events;

public class ItemEvent<TKey, TItem> : SenderEvent
{
    public new delegate void Handler(Args args);

    public new class Args(object? sender, TKey key, TItem item) : SenderEvent.Args(sender)
    {
        public TKey Key { get; set; } = key;
        public TItem Item { get; set; } = item;
    }
}

public class ItemEvent<T> : SenderEvent
{
    public new delegate void Handler(Args args);

    public new class Args(object? sender, T item) : SenderEvent.Args(sender)
    {
        public T Item { get; set; } = item;
    }
}