using System;

namespace Orion;

public static class ExceptionHelper
{
    public static ArgumentNullException Null(string name) => new($"The value for \"{name}\" was null!");
    public static NotImplementedException NotImplemented(string name) => new($"There is no implementation for \"{name}\"!");
    public static NotImplementedException NotImplemented(string name, object? value) => new($"There is no implementation for \"{name}\" with value \"{value}\"!");
    public static Exception MissingAppComponent(string name) => new($"Missing App Component <{name}>!");
    public static Exception ServiceNotFound(string name) => new($"Could not find service for type <{name}>!");
}