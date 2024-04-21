using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace Orion.Structures;

public class ItemSet<T> : Collection<T>, INotifyCollectionChanged
{
    public event NotifyCollectionChangedEventHandler? CollectionChanged;

    public bool UseRemoveOnClear { get; set; } = Globals.ItemSet.DefaultUseRemoveOnClear;
    public bool UseRemoveAddOnReplace { get; set; } = Globals.ItemSet.DefaultUseRemoveAddOnReplace;

    public ItemSet() { }
    public ItemSet(IList<T> list) : base(list) { }

    protected virtual void OnCleared() => CollectionChanged?.Invoke(this, new(NotifyCollectionChangedAction.Reset));
    protected virtual void OnItemInserted(T item, int index) => CollectionChanged?.Invoke(this, new(NotifyCollectionChangedAction.Add, item, index));
    protected virtual void OnItemRemoved(T item, int index) => CollectionChanged?.Invoke(this, new(NotifyCollectionChangedAction.Remove, item, index));
    protected virtual void OnItemReplaced(T oldItem, T item, int index) => CollectionChanged?.Invoke(this, new(NotifyCollectionChangedAction.Replace, item, oldItem, index));

    protected virtual void BeforeInsert(T item, int index) { }
    protected virtual void AfterInsert(T item, int index) { }
    protected virtual void BeforeRemove(T item, int index) { }
    protected virtual void AfterRemove(T item, int index) { }

    protected override void ClearItems()
    {
        var items = this.ToList();
        items.For(BeforeRemove);
        if (UseRemoveOnClear) items.For(OnItemRemoved);
        else OnCleared();
        base.ClearItems();
        items.For(AfterRemove);
    }
    protected override void InsertItem(int index, T item)
    {
        BeforeInsert(item, index);
        OnItemInserted(item, index);
        base.InsertItem(index, item);
        AfterInsert(item, index);
    }
    protected override void RemoveItem(int index)
    {
        var oldItem = this[index];
        BeforeRemove(oldItem, index);
        OnItemRemoved(oldItem, index);
        base.RemoveItem(index);
        AfterRemove(oldItem, index);
    }
    protected override void SetItem(int index, T item)
    {
        var oldItem = this[index];
        BeforeRemove(oldItem, index);
        BeforeInsert(item, index);
        if (UseRemoveAddOnReplace)
        {
            OnItemRemoved(oldItem, index);
            OnItemInserted(item, index);
        }
        else OnItemReplaced(oldItem, item, index);
        base.SetItem(index, item);
        AfterRemove(oldItem, index);
        AfterInsert(item, index);
    }
}