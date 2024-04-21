using System;
using System.Threading.Tasks;

namespace Orion;

public class App
{
    public static App Current => _current;
    private static readonly App _current = new();

    public ComponentRegistry.Set Components { get; } = [];
    public ServiceRegistry.Set Services { get; } = [];

    public Action? EntryPoint { get; set; }

    public static App Create() => Current.LoadDefaults();

    public void Run()
    {
        if (EntryPoint is null) throw ExceptionHelper.Null(nameof(EntryPoint));
        EntryPoint();
    }

    public async Task RunAsync() => await Task.Run(Run);
}