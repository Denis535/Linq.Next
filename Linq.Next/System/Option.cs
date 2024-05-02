﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace System;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

public static class Option {

    // Equals
    public static bool Equals<T>(Option<T> v1, Option<T> v2) {
        if (v1.HasValue && v2.HasValue) return EqualityComparer<T>.Default.Equals( v1.Value, v2.Value );
        return EqualityComparer<bool>.Default.Equals( v1.HasValue, v2.HasValue );
    }
    public static bool Equals<T>(Option<T> v1, T v2) {
        if (v1.HasValue) return EqualityComparer<T>.Default.Equals( v1.Value, v2 );
        return false;
    }
    public static bool Equals<T>(T v1, Option<T> v2) {
        if (v2.HasValue) return EqualityComparer<T>.Default.Equals( v1, v2.Value );
        return false;
    }

    // Compare
    public static int Compare<T>(Option<T> v1, Option<T> v2) {
        if (v1.HasValue && v2.HasValue) return Comparer<T>.Default.Compare( v1.Value, v2.Value );
        return Comparer<bool>.Default.Compare( v1.HasValue, v2.HasValue );
    }
    public static int Compare<T>(Option<T> v1, T v2) {
        if (v1.HasValue) return Comparer<T>.Default.Compare( v1.Value, v2 );
        return Comparer<bool>.Default.Compare( false, true );
    }
    public static int Compare<T>(T v1, Option<T> v2) {
        if (v2.HasValue) return Comparer<T>.Default.Compare( v1, v2.Value );
        return Comparer<bool>.Default.Compare( true, false );
    }

    // GetUnderlyingType
    public static Type? GetUnderlyingType(Type type) {
        if (GetUnboundType( type ) == typeof( Option<> )) return type.GetGenericArguments().First();
        return null;
    }

    // Helpers
    private static Type GetUnboundType(Type type) {
        if (type.IsGenericType) {
            return type.IsGenericTypeDefinition ? type : type.GetGenericTypeDefinition();
        } else {
            return type;
        }
    }

}
public static class OptionExtensions {

    public static Option<T> AsOption<T>(this T value) {
        return new Option<T>( value );
    }

}
[Serializable]
public readonly struct Option<T> : IEquatable<Option<T>>, IEquatable<T>, IComparable<Option<T>>, IComparable<T> {

    private readonly bool hasValue;
    private readonly T? value;

    public bool HasValue => hasValue;
    public T Value => hasValue ? value! : throw new InvalidOperationException( "Option must have value" );
    public T? ValueOrDefault => hasValue ? value : default;

    // Constructor
    public Option() {
        this.hasValue = false;
        this.value = default;
    }
    public Option(T value) {
        this.hasValue = true;
        this.value = value;
    }

    // TryGetValue
    public bool TryGetValue([MaybeNullWhen( false )] out T value) {
        if (hasValue) {
            value = this.value!;
            return true;
        }
        value = default;
        return false;
    }

    // Utils
    public override string ToString() {
        if (hasValue) return value?.ToString() ?? "Null";
        return "Nothing";
    }
    public override bool Equals(object? other) {
        if (other is Option<T> other_) return Option.Equals( this, other_ );
        return false;
    }
    public override int GetHashCode() {
        if (hasValue) return value?.GetHashCode() ?? 0;
        return 0;
    }

    // Utils
    public bool Equals(Option<T> other) {
        return Option.Equals( this, other );
    }
    public bool Equals(T other) {
        return Option.Equals( this, other );
    }

    // Utils
    public int CompareTo(Option<T> other) {
        return Option.Compare( this, other );
    }
    public int CompareTo(T other) {
        return Option.Compare( this, other );
    }

    // Utils
    public static bool operator ==(Option<T> left, Option<T> right) {
        return Option.Equals( left, right );
    }
    public static bool operator ==(Option<T> left, T right) {
        return Option.Equals( left, right );
    }
    public static bool operator ==(T left, Option<T> right) {
        return Option.Equals( left, right );
    }
    public static bool operator !=(Option<T> left, Option<T> right) {
        return !Option.Equals( left, right );
    }
    public static bool operator !=(Option<T> left, T right) {
        return !Option.Equals( left, right );
    }
    public static bool operator !=(T left, Option<T> right) {
        return !Option.Equals( left, right );
    }

}
