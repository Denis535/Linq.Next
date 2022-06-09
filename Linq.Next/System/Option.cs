namespace System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Linq;

public static class Option {
    public static Option<T> Default<T>() {
        return new Option<T>();
    }
    public static Option<T> Create<T>(T value) {
        return new Option<T>( value );
    }
    public static Option<T> Create<T>(T? value) where T : struct {
        if (value.HasValue) return new Option<T>( value.Value );
        return default;
    }
    public static bool Equals<T>(Option<T> opt1, Option<T> opt2) {
        if ((opt1.HasValue, opt2.HasValue) == (true, true)) return EqualityComparer<T>.Default.Equals( opt1.Value, opt2.Value );
        return EqualityComparer<bool>.Default.Equals( opt1.HasValue, opt2.HasValue );
    }
    public static int Compare<T>(Option<T> opt1, Option<T> opt2) {
        if ((opt1.HasValue, opt2.HasValue) == (true, true)) return Comparer<T>.Default.Compare( opt1.Value, opt2.Value );
        return Comparer<bool>.Default.Compare( opt1.HasValue, opt2.HasValue );
    }
    public static Type? GetUnderlyingType(Type type!!) {
        if (GetUnboundType( type ) == typeof( Option<> )) return type.GetGenericArguments().First();
        return null;
    }
    // Helpers
    private static Type GetUnboundType(Type type!!) {
        if (type.IsGenericType) {
            return type.IsGenericTypeDefinition ? type : type.GetGenericTypeDefinition();
        } else {
            return type;
        }
    }
}
// Note: Don't override true, false operators!
// Note: We need something like [MemberMaybeNullWhen( false, nameof( ValueOrDefault ) )]
// Note: Value is nullable when T is nullable
[Serializable]
public readonly struct Option<T> : IEquatable<Option<T>>, IComparable<Option<T>> {

    private readonly bool hasValue;
    private readonly T value;
    public static Option<T> Default => default;
    public bool HasValue => hasValue;
    public T Value => hasValue ? value : throw new InvalidOperationException( "Option object must have a value" );
    public T? ValueOrDefault => hasValue ? value : default;

    public Option(T value) {
        this.hasValue = true;
        this.value = value;
    }

    // TryGetValue
    public bool TryGetValue([MaybeNullWhen( false )] out T? value) {
        value = hasValue ? this.value : default;
        return hasValue;
    }

    // Utils
    public bool Equals(Option<T> other) {
        return Option.Equals( this, other );
    }
    public int CompareTo(Option<T> other) {
        return Option.Compare( this, other );
    }

    // Utils
    public override string ToString() {
        if (hasValue) return value?.ToString() ?? "";
        return "";
    }
    public override bool Equals(object? other) {
        if (other is Option<T> other_) return Option.Equals( this, other_ );
        return false;
    }
    public override int GetHashCode() {
        if (hasValue) return value?.GetHashCode() ?? 0;
        return 0;
    }

    // Conversions
    public static implicit operator Option<T>(T value) {
        return new Option<T>( value );
    }
    public static explicit operator T(Option<T> value) {
        return value.Value;
    }

    // Operators
    public static bool operator ==(Option<T> left, Option<T> right) {
        return Option.Equals( left, right );
    }
    public static bool operator !=(Option<T> left, Option<T> right) {
        return !Option.Equals( left, right );
    }


}