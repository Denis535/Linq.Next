namespace System.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

public static class PeekableEnumeratorExtensions {


    // Take/While
    public static IEnumerable<T> TakeWhile<T>(this PeekableEnumerator<T> enumerator, Predicate<T> predicate) {
        // [true, true], false
        while (enumerator.TryTakeIf( predicate, out var current )) {
            yield return current;
        }
    }
    // Take/Until
    public static IEnumerable<T> TakeUntil<T>(this PeekableEnumerator<T> enumerator, Predicate<T> predicate) {
        // [false, false], true
        while (enumerator.TryTakeIfNot( predicate, out var current )) {
            yield return current;
        }
    }


    // Take/If
    public static bool TryTakeIf<T>(this PeekableEnumerator<T> enumerator, Predicate<T> predicate, [MaybeNullWhen( false )] out T current) {
        return enumerator.TakeIf( predicate ).TryGetValue( out current );
    }
    public static bool TryTakeIfNot<T>(this PeekableEnumerator<T> enumerator, Predicate<T> predicate, [MaybeNullWhen( false )] out T current) {
        return enumerator.TakeIfNot( predicate ).TryGetValue( out current );
    }
    public static Option<T> TakeIf<T>(this PeekableEnumerator<T> enumerator, Predicate<T> predicate) {
        if (enumerator.TryPeek( out var next ) && predicate( next )) return enumerator.Take();
        return default;
    }
    public static Option<T> TakeIfNot<T>(this PeekableEnumerator<T> enumerator, Predicate<T> predicate) {
        if (enumerator.TryPeek( out var next ) && !predicate( next )) return enumerator.Take();
        return default;
    }


}