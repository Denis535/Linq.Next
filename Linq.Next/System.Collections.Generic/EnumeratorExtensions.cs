namespace System.Collections.Generic;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;

public static class EnumeratorExtensions {


    //// Take/While
    //public static IEnumerable<T> TakeWhile<T>(this IEnumerator<T> enumerator, Predicate<T> predicate) {
    //    // [true, true], break, false
    //    while (enumerator.TryTake( out var current ) && predicate( current )) {
    //        yield return current;
    //    }
    //}
    //// Take/Until
    //public static IEnumerable<T> TakeUntil<T>(this IEnumerator<T> enumerator, Predicate<T> predicate) {
    //    // [false, false], break, true
    //    while (enumerator.TryTake( out var current ) && !predicate( current )) {
    //        yield return current;
    //    }
    //}


    // Take/Try
    public static bool TryTake<T>(this IEnumerator<T> enumerator, [MaybeNullWhen( false )] out T current) {
        return enumerator.Take().TryGetValue( out current );
    }
    // Take
    public static Option<T> Take<T>(this IEnumerator<T> enumerator) {
        if (enumerator.MoveNext()) return enumerator.Current;
        return default;
    }


}
