using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Orion.Structures;

public class NotifySetAdapter<TCol, TItem> : DisposableBase where TCol : ICollection<TItem>, INotifyCollectionChanged
{
    public Action? Cleared { get; set; }

    public Action<TItem>? ItemAdded { get; set; }
    public Action<TItem>? ItemRemoved { get; set; }

    public Action<TItem, int>? IndexedItemAdded { get; set; }
    public Action<TItem, int>? IndexedItemRemoved { get; set; }

    public TCol Collection { get; }

    public NotifySetAdapter(TCol collection)
    {
        Collection = collection;
        Register();
    }

    protected override void DisposeManaged()
    {
        Cleared = null;
        ItemAdded = null;
        ItemRemoved = null;
        IndexedItemAdded = null;
        IndexedItemRemoved = null;

        Unregister();

        base.DisposeManaged();
    }

    protected virtual void Register()
    {
        Collection.CollectionChanged += Collection_CollectionChanged;
    }
    protected virtual void Unregister()
    {
        Collection.CollectionChanged -= Collection_CollectionChanged;
    }

    private void RaiseCleared() => Cleared?.Invoke();
    private void RaiseItemAdded(IList itemList, int startingIndex)
    {
        for (int i = 0; i < itemList.Count; i++)
        {
            if (itemList[i] is not TItem item) continue;
            IndexedItemAdded?.Invoke(item, i + startingIndex);
        }
    }
    private void RaiseItemRemoved(IList itemList, int startingIndex)
    {
        for (int i = 0; i < itemList.Count; i++)
        {
            if (itemList[i] is not TItem item) continue;
            IndexedItemRemoved?.Invoke(item, i + startingIndex);
        }
    }

    private void Collection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        switch (e.Action)
        {
            case NotifyCollectionChangedAction.Reset: RaiseCleared(); break;
            case NotifyCollectionChangedAction.Add: RaiseItemAdded(e.NewItems, e.NewStartingIndex); break;
            case NotifyCollectionChangedAction.Remove: RaiseItemRemoved(e.OldItems, e.OldStartingIndex); break;
            case NotifyCollectionChangedAction.Replace:
                RaiseItemRemoved(e.OldItems, e.OldStartingIndex);
                RaiseItemAdded(e.NewItems, e.NewStartingIndex);
                break;
        }
    }
}
