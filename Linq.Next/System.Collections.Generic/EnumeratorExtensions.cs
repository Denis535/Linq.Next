namespace System.Collections.Generic;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;

// Enumerator
public static class EnumeratorExtensions {


    // As/Stateful
    public static StatefulEnumerator<T> AsStateful<T>(this IEnumerator<T> enumerator) {
        return new StatefulEnumerator<T>( enumerator );
    }
    // As/Peekable
    public static PeekableEnumerator<T> AsPeekable<T>(this IEnumerator<T> enumerator) {
        return new PeekableEnumerator<T>( enumerator );
    }


    // Take/While
    public static IEnumerable<T> TakeWhile<T>(this IEnumerator<T> enumerator, Predicate<T> predicate) {
        // [true, true], break, false
        while (enumerator.TryTakeIf( predicate, out var current )) {
            yield return current;
        }
    }
    // Take/Until
    public static IEnumerable<T> TakeUntil<T>(this IEnumerator<T> enumerator, Predicate<T> predicate) {
        // [false, false], break, true
        while (enumerator.TryTakeIfNot( predicate, out var current )) {
            yield return current;
        }
    }


    // Take/Try
    public static bool TryTakeIf<T>(this IEnumerator<T> enumerator, Predicate<T> predicate, [MaybeNullWhen( false )] out T current) {
        return enumerator.TakeIf( predicate ).TryGetValue( out current );
    }
    public static bool TryTakeIfNot<T>(this IEnumerator<T> enumerator, Predicate<T> predicate, [MaybeNullWhen( false )] out T current) {
        return enumerator.TakeIfNot( predicate ).TryGetValue( out current );
    }
    public static bool TryTake<T>(this IEnumerator<T> enumerator, [MaybeNullWhen( false )] out T current) {
        return enumerator.Take().TryGetValue( out current );
    }


    // Take
    public static Option<T> TakeIf<T>(this IEnumerator<T> enumerator, Predicate<T> predicate) {
        var current = enumerator.Take();
        if (current.HasValue && predicate( current.Value )) return current;
        return default;
    }
    public static Option<T> TakeIfNot<T>(this IEnumerator<T> enumerator, Predicate<T> predicate) {
        var current = enumerator.Take();
        if (current.HasValue && !predicate( current.Value )) return current;
        return default;
    }
    public static Option<T> Take<T>(this IEnumerator<T> enumerator) {
        if (enumerator.MoveNext()) return enumerator.Current;
        return default;
    }


}

// Enumerator/Stateful
public static class StatefulEnumeratorExtensions {


    // Take/While
    public static IEnumerable<T> TakeWhile<T>(this StatefulEnumerator<T> enumerator, Predicate<T> predicate) {
        // [true, true], break, false
        while (enumerator.TryTakeIf( predicate, out var current )) {
            yield return current;
        }
    }
    // Take/Until
    public static IEnumerable<T> TakeUntil<T>(this StatefulEnumerator<T> enumerator, Predicate<T> predicate) {
        // [false, false], break, true
        while (enumerator.TryTakeIfNot( predicate, out var current )) {
            yield return current;
        }
    }


    // Take/Try
    public static bool TryTakeIf<T>(this StatefulEnumerator<T> enumerator, Predicate<T> predicate, [MaybeNullWhen( false )] out T current) {
        return enumerator.TakeIf( predicate ).TryGetValue( out current );
    }
    public static bool TryTakeIfNot<T>(this StatefulEnumerator<T> enumerator, Predicate<T> predicate, [MaybeNullWhen( false )] out T current) {
        return enumerator.TakeIfNot( predicate ).TryGetValue( out current );
    }
    public static bool TryTake<T>(this StatefulEnumerator<T> enumerator, [MaybeNullWhen( false )] out T current) {
        return enumerator.Take().TryGetValue( out current );
    }


    // Take
    public static Option<T> TakeIf<T>(this StatefulEnumerator<T> enumerator, Predicate<T> predicate) {
        var current = enumerator.Take();
        if (current.HasValue && predicate( current.Value )) return current;
        return default;
    }
    public static Option<T> TakeIfNot<T>(this StatefulEnumerator<T> enumerator, Predicate<T> predicate) {
        var current = enumerator.Take();
        if (current.HasValue && !predicate( current.Value )) return current;
        return default;
    }


}

// Enumerator/Peekable
public static class PeekableEnumeratorExtensions {


    // Take/While
    public static IEnumerable<T> TakeWhile<T>(this PeekableEnumerator<T> enumerator, Predicate<T> predicate) {
        // [true, true], break, false
        while (enumerator.TryTakeIf( predicate, out var current )) {
            yield return current;
        }
    }
    // Take/Until
    public static IEnumerable<T> TakeUntil<T>(this PeekableEnumerator<T> enumerator, Predicate<T> predicate) {
        // [false, false], break, true
        while (enumerator.TryTakeIfNot( predicate, out var current )) {
            yield return current;
        }
    }


    // Take/Try
    public static bool TryTakeIf<T>(this PeekableEnumerator<T> enumerator, Predicate<T> predicate, [MaybeNullWhen( false )] out T current) {
        return enumerator.TakeIf( predicate ).TryGetValue( out current );
    }
    public static bool TryTakeIfNot<T>(this PeekableEnumerator<T> enumerator, Predicate<T> predicate, [MaybeNullWhen( false )] out T current) {
        return enumerator.TakeIfNot( predicate ).TryGetValue( out current );
    }


    // Take
    public static Option<T> TakeIf<T>(this PeekableEnumerator<T> enumerator, Predicate<T> predicate) {
        var next = enumerator.Peek();
        if (next.HasValue && predicate( next.Value )) return enumerator.Take();
        return default;
    }
    public static Option<T> TakeIfNot<T>(this PeekableEnumerator<T> enumerator, Predicate<T> predicate) {
        var next = enumerator.Peek();
        if (next.HasValue && !predicate( next.Value )) return enumerator.Take();
        return default;
    }


}