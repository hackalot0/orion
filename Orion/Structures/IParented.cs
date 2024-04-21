namespace Orion.Structures;

public interface IParented<TParent> : IHasParent<TParent>
{
    new TParent? Parent { get; set; }
}