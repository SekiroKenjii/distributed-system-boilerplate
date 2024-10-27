using FluentValidation;

namespace Common.Core.Validation;

/// <summary>
///     Provides custom validation logic for objects of type <typeparamref name="T" />.
/// </summary>
/// <typeparam name="T">The type of the object to validate.</typeparam>
public class CustomValidator<T> : AbstractValidator<T> { }