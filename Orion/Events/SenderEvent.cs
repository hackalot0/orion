using System;

namespace Orion.Events;

public class SenderEvent
{
    public delegate void Handler(Args args);

    public class Args(object? sender) : EventArgs
    {
        public object? Sender { get; set; } = sender;
    }
}