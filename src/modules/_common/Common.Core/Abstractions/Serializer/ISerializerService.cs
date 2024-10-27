using Common.Core.Abstractions.System;

namespace Common.Core.Abstractions.Serializer;

/// <summary>
///     Provides methods for serializing and deserializing objects.
/// </summary>
public interface ISerializerService : ITransientService
{
    /// <summary>
    ///     Serializes the specified object to a string.
    /// </summary>
    /// <typeparam name="T">The type of the object to serialize.</typeparam>
    /// <param name="obj">The object to serialize.</param>
    /// <returns>A string representation of the serialized object.</returns>
    string Serialize<T>(T obj);

    /// <summary>
    ///     Serializes the specified object to a string using the specified type.
    /// </summary>
    /// <typeparam name="T">The type of the object to serialize.</typeparam>
    /// <param name="obj">The object to serialize.</param>
    /// <param name="type">The type to use for serialization.</param>
    /// <returns>A string representation of the serialized object.</returns>
    string Serialize<T>(T obj, Type type);

    /// <summary>
    ///     Deserializes the specified string to an object of type <typeparamref name="T" />.
    /// </summary>
    /// <typeparam name="T">The type of the object to deserialize.</typeparam>
    /// <param name="text">The string to deserialize.</param>
    /// <returns>An object of type <typeparamref name="T" />.</returns>
    T Deserialize<T>(string text);
}