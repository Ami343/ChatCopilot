namespace Domain.Primitives;

public class Maybe<T> : IEquatable<Maybe<T>>
{
    private readonly T? _value;

    private Maybe(T? value)
        => _value = value;

    public static Maybe<T> From(T value)
        => new Maybe<T>(value);

    public static Maybe<T> None
        => new Maybe<T>(default);

    public bool HasValue
        => _value is not null;

    public T Value => HasValue
        ? _value!
        : throw new InvalidOperationException("Value does not exist.");


    public bool Equals(Maybe<T>? other)
    {
        if (other is null) return false;

        if (!HasValue && !other.HasValue) return true;

        if (!HasValue || !other.HasValue) return false;

        return Value!.Equals(other.Value!);
    }

    public override bool Equals(object? obj)
        => obj switch
        {
            null => false,
            T value => Equals(new Maybe<T>(value)),
            Maybe<T> maybe => Equals(maybe),
            _ => false
        };

    public override int GetHashCode()
        => HasValue ? Value!.GetHashCode() : 0;
}