using Orion.Events;
using System.Collections.Generic;

namespace Orion.Structures;

public class ParentedItemSet<TParent, TItem> : ItemSet<TItem>, IParented<TParent> where TItem : IParented<TParent>
{
    public event ItemChangeEvent<TParent?>.Handler? ParentChanged;

    public TParent? Parent { get => parent; set => EventHelper.Change(ref parent, value, OnParentChanged); }

    private TParent? parent;

    public ParentedItemSet() : this(parent: default) { }
    public ParentedItemSet(IList<TItem> list) : this(parent: default, list) { }
    public ParentedItemSet(TParent? parent) { this.parent = parent; }
    public ParentedItemSet(TParent? parent, IList<TItem> list) : base(list) { this.parent = parent; }

    protected virtual void OnParentChanged(TParent? oldParent, TParent? newParent)
    {
        this.ForEach(item => item.Parent = newParent);
        ParentChanged?.Invoke(new(this, oldParent, newParent));
    }

    protected override void AfterInsert(TItem item, int index)
    {
        base.AfterInsert(item, index);
        item.Parent = Parent;
    }
}