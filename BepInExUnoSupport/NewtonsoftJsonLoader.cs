using System.Reflection;
using System.Runtime.Loader;

namespace BepInExUnoSupport;

internal static class NewtonsoftJsonLoader
{
    private static readonly Assembly _assembly = Assembly.GetExecutingAssembly();

    public static void Load()
    {
        byte[]? buffer = GetEmbeddedBytes("Newtonsoft.Json.dll", false);
        if (buffer == null) return;

        AssemblyLoadContext.Default.LoadFromStream(new MemoryStream(buffer));

        Console.WriteLine("Loaded embedded assembly Newtonsoft.Json.dll");
    }

    private static byte[]? GetEmbeddedBytes(string name, bool throwIfNotFound)
    {
        string? path = _assembly.GetManifestResourceNames().FirstOrDefault(n => n.Contains(name));
        if (path == null) return !throwIfNotFound ? null : throw new FileNotFoundException($"Embedded resource {name} not found");

        Stream manifestResourceStream = _assembly.GetManifestResourceStream(path)!;
        return manifestResourceStream.ReadFully();
    }

    private static byte[] ReadFully(this Stream stream)
    {
        using MemoryStream ms = new();
        stream.CopyTo(ms);
        return ms.ToArray();
    }
}
