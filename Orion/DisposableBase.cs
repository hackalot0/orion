using System;

namespace Orion;

public abstract class DisposableBase : IDisposable
{
    private bool disposedValue;

    protected virtual void DisposeManaged() { }
    protected virtual void DisposeUnmanaged() { }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing) DisposeManaged();
            DisposeUnmanaged();
            disposedValue = true;
        }
    }

    ~DisposableBase() => Dispose(disposing: false);

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
