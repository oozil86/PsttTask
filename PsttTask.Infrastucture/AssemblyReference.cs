using System.Reflection;

namespace PsttTask.Infrastucture;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
