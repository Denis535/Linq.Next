namespace System.Linq;
using System;
using System.Collections.Generic;
using System.Text;

public static class EnumerableExtensions {


    // GetPeekableEnumerator
    //public static PeekableEnumerator<T> GetPeekableEnumerator<T>(this IEnumerable<T> enumerable) {
    //    return new PeekableEnumerator<T>( enumerable.GetEnumerator() );
    //}


    // Compare
    public static void Compare<T>(this IEnumerable<T> source, IEnumerable<T> standard, out T[] missing, out T[] extra) {
        var expected_ = new LinkedList<T>( standard );
        extra = source.Where( i => !expected_.Remove( i ) ).ToArray();
        missing = expected_.ToArray();
    }


    // Shuffle
    public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source) {
        return source.Shuffle( new Random() );
    }
    public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source, Random random) {
        var result = source.ToArray();
        for (var i = 0; i < result.Length; i++) {
            var i2 = i + random.Next( 0, result.Length - i );
            (result[ i2 ], result[ i ]) = (result[ i ], result[ i2 ]);
        }
        return result;
    }


    // Slice
    //public static IEnumerable<T[]> Slice<T>(this IEnumerable<T> source, Func<T, bool> predicate) {
    //    using var source_enumerator = source.GetPeekableEnumerator();
    //    var slice = new List<T>();
    //    while (source_enumerator.HasNext) {
    //        slice.Clear();
    //        slice.AddRange( source_enumerator.TakeSlice( predicate ) );
    //        yield return slice.ToArray();
    //    }
    //}
    //public static IEnumerable<T[]> Slice<T>(this IEnumerable<T> source, Func<T, T, bool> predicate) {
    //    using var source_enumerator = source.GetPeekableEnumerator();
    //    var slice = new List<T>();
    //    while (source_enumerator.HasNext) {
    //        slice.Clear();
    //        slice.AddRange( source_enumerator.TakeSlice( predicate ) );
    //        yield return slice.ToArray();
    //    }
    //}
    // Slice/TakeSlice
    //private static IEnumerable<T> TakeSlice<T>(this IEnumerator<T> enumerator, Func<T, bool> predicate) {
    //    // a
    //    // a,    [break], b
    //    // a, a, [break], b, b
    //    while (enumerator.TryTake( out var current )) {
    //        yield return current;
    //        if (predicate( current )) yield break;
    //    }
    //}
    //private static IEnumerable<T> TakeSlice<T>(this IEnumerator<T> enumerator, Func<T, T, bool> predicate) {
    //    // a
    //    // a,    [break], b
    //    // a, a, [break], b, b
    //    while (enumerator.TryTake( out var current )) {
    //        yield return current;
    //        if (enumerator.TryPeek( out var next ) && predicate( current, next )) yield break;
    //    }
    //}
    // Slice
    public static IEnumerable<T[]> Slice<T>(this IEnumerable<T> source, Predicate<T> predicate) {
        // [false, false, false, true], [false, false]
        using var source_enumerator = source.GetEnumerator();
        var slice = new List<T>();
        while (source_enumerator.TryTake( out var value )) {
            slice.Add( value );
            if (predicate( value )) {
                yield return slice.ToArray();
                slice.Clear();
            }
        }
        if (slice.Any()) {
            yield return slice.ToArray();
            slice.Clear();
        }
    }


    // Tag/First-Last
    //public static IEnumerable<(T Value, bool IsFirst)> TagFirst<T>(this IEnumerable<T> source) {
    //    using var source_enumerator = source.GetEnumerator();
    //    if (source_enumerator.TryTake( out var value_first )) {
    //        yield return (value_first, true);
    //    }
    //    while (source_enumerator.TryTake( out var value )) {
    //        yield return (value, false);
    //    }
    //}
    //public static IEnumerable<(T Value, bool IsLast)> TagLast<T>(this IEnumerable<T> source) {
    //    using var source_enumerator = source.GetPeekableEnumerator();
    //    while (source_enumerator.TryTake( out var value )) {
    //        yield return (value, source_enumerator.IsLast);
    //    }
    //}
    //public static IEnumerable<(T Value, bool IsFirst, bool IsLast)> TagFirstLast<T>(this IEnumerable<T> source) {
    //    using var source_enumerator = source.GetPeekableEnumerator();
    //    if (source_enumerator.TryTake( out var value_first )) {
    //        yield return (value_first, true, source_enumerator.IsLast);
    //    }
    //    while (source_enumerator.TryTake( out var value )) {
    //        yield return (value, false, source_enumerator.IsLast);
    //    }
    //}
    // Tag/First-Last
    public static IEnumerable<(T Value, bool IsFirst)> TagFirst<T>(this IEnumerable<T> source) {
        using var source_enumerator = source.GetEnumerator();
        if (source_enumerator.TryTake( out var first )) {
            yield return (first, true);
        }
        while (source_enumerator.TryTake( out var value )) {
            yield return (value, false);
        }
    }
    public static IEnumerable<(T Value, bool IsLast)> TagLast<T>(this IEnumerable<T> source) {
        using var source_enumerator = source.GetEnumerator();
        var value = source_enumerator.Take();
        var next = source_enumerator.Take();
        while (value.HasValue) {
            yield return (value.Value, !next.HasValue);
            (value, next) = (next, source_enumerator.Take());
        }
    }
    public static IEnumerable<(T Value, bool IsFirst, bool IsLast)> TagFirstLast<T>(this IEnumerable<T> source) {
        using var source_enumerator = source.GetEnumerator();
        var value = source_enumerator.Take();
        var next = source_enumerator.Take();
        if (value.HasValue) {
            yield return (value.Value, true, !next.HasValue);
            (value, next) = (next, source_enumerator.Take());
        }
        while (value.HasValue) {
            yield return (value.Value, false, !next.HasValue);
            (value, next) = (next, source_enumerator.Take());
        }
    }


    //// With/Prev-Next
    //public static IEnumerable<(T Current, Option<T> Prev)> WithPrev<T>(this IEnumerable<T> source) {
    //    using var source_enumerator = source.GetEnumerator();
    //    var prev = default( Option<T> );
    //    while (source_enumerator.TryTake( out var current )) {
    //        yield return (current, prev);
    //        prev = current;
    //    }
    //}
    //public static IEnumerable<(T Current, Option<T> Next)> WithNext<T>(this IEnumerable<T> source) {
    //    using var source_enumerator = source.GetPeekableEnumerator();
    //    while (source_enumerator.TryTake( out var current )) {
    //        yield return (current, source_enumerator.Peek());
    //    }
    //}
    //public static IEnumerable<(T Current, Option<T> Prev, Option<T> Next)> WithPrevNext<T>(this IEnumerable<T> source) {
    //    using var source_enumerator = source.GetPeekableEnumerator();
    //    var prev = default( Option<T> );
    //    while (source_enumerator.TryTake( out var current )) {
    //        yield return (current, prev, source_enumerator.Peek());
    //        prev = current;
    //    }
    //}
    // With/Prev-Next
    public static IEnumerable<(T Value, Option<T> Prev)> WithPrev<T>(this IEnumerable<T> source) {
        var prev = Option<T>.Default;
        foreach (var item in source) {
            yield return (item, prev);
            prev = item;
        }
    }
    public static IEnumerable<(T Value, Option<T> Next)> WithNext<T>(this IEnumerable<T> source) {
        using var source_enumerator = source.GetEnumerator();
        var value = source_enumerator.Take();
        var next = source_enumerator.Take();
        while (value.HasValue) {
            yield return (value.Value, next);
            (value, next) = (next, source_enumerator.Take());
        }
    }
    public static IEnumerable<(T Value, Option<T> Prev, Option<T> Next)> WithPrevNext<T>(this IEnumerable<T> source) {
        using var source_enumerator = source.GetEnumerator();
        var prev = default( Option<T> );
        var value = source_enumerator.Take();
        var next = source_enumerator.Take();
        while (value.HasValue) {
            yield return (value.Value, prev, next);
            (prev, value, next) = (value, next, source_enumerator.Take());
        }
    }


    // ToEnumerable
    public static IEnumerable<T> ToEnumerable<T>(this T element) {
        return new[] { element };
    }
    public static IEnumerable<T> ToEnumerable<T>(this T element, T second) {
        return new[] { element, second };
    }
    public static IEnumerable<T> ToEnumerable<T>(this T element, IEnumerable<T> seconds) {
        return seconds.Prepend( element );
    }


}
