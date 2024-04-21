using Orion.Events;

namespace Orion.Structures;

public interface IHasParent<TParent>
{
    event ItemChangeEvent<TParent?>.Handler? ParentChanged;

    TParent? Parent { get; }
}