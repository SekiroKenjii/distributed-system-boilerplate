using System.Reflection;

namespace Common.Infrastructure;

/// <summary>
///     Provides a reference to the assembly containing the common infrastructure.
/// </summary>
public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}