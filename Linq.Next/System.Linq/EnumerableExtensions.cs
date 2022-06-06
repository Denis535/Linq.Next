namespace System.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public static class EnumerableExtensions {


    // GetPeekableEnumerator
    public static PeekableEnumerator<T> GetPeekableEnumerator<T>(this IEnumerable<T> enumerable) {
        return new PeekableEnumerator<T>( enumerable.GetEnumerator() );
    }


    // Compare
    public static void Compare<T>(this IEnumerable<T> actual, IEnumerable<T> expected, out List<T> missing, out List<T> extra) {
        var expected_ = new LinkedList<T>( expected );
        extra = actual.Where( i => !expected_.Remove( i ) ).ToList();
        missing = expected_.ToList();
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


    // LazyDistinctBy
    //public static IEnumerable<T> LazyDistinctBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> keySelector) where T : class where TKey : struct {
    //    var comparer = EqualityComparer<TKey>.Default;
    //    var prev = default( T );
    //    foreach (var item in source) {
    //        if (prev == null || !comparer.Equals( keySelector( prev ), keySelector( item ) )) {
    //            yield return item;
    //        }
    //        prev = item;
    //    }
    //}


    // FastSlice
    //public static IEnumerable<(T First, T Last)> FastSlice<T>(this IEnumerable<T> source, Func<T, T, bool> predicate) where T : class {
    //    var source_list = (source as IList<T>) ?? source.ToList();
    //    var (original, count) = (default( T ), 0);

    //    for (var i = 0; i < source_list.Count; i++) {
    //        var hasNext = i < source_list.Count - 1;
    //        var item = source_list[ i ];
    //        var next = hasNext ? source_list[ i + 1 ] : null;

    //        if (count == 0) {
    //            (original, count) = (item, 1);
    //        } else {
    //            count++;
    //        }

    //        if (hasNext && predicate( item, next! )) {
    //            continue;
    //        }

    //        yield return (original!, item);
    //        (original, count) = (null, 0);
    //    }
    //}


    //// Slice
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
    //// Slice/TakeSlice
    //private static IEnumerable<T> TakeSlice<T>(this PeekableEnumerator<T> enumerator, Func<T, bool> predicate) {
    //    // a
    //    // a,    [break], b
    //    // a, a, [break], b, b
    //    while (enumerator.TryTake( out var current )) {
    //        yield return current;
    //        if (predicate( current )) yield break;
    //    }
    //}
    //private static IEnumerable<T> TakeSlice<T>(this PeekableEnumerator<T> enumerator, Func<T, T, bool> predicate) {
    //    // a
    //    // a,    [break], b
    //    // a, a, [break], b, b
    //    while (enumerator.TryTake( out var current )) {
    //        yield return current;
    //        if (enumerator.TryPeek( out var next ) && predicate( current, next )) yield break;
    //    }
    //}


    // Tag/First-Last
    public static IEnumerable<(T Value, bool IsFirst)> TagFirst<T>(this IEnumerable<T> source) {
        using var source_enumerator = source.GetEnumerator();
        if (source_enumerator.TryTake( out var value_first )) {
            yield return (value_first, true);
        }
        while (source_enumerator.TryTake( out var value )) {
            yield return (value, false);
        }
    }
    public static IEnumerable<(T Value, bool IsLast)> TagLast<T>(this IEnumerable<T> source) {
        using var source_enumerator = source.GetPeekableEnumerator();
        while (source_enumerator.TryTake( out var value )) {
            yield return (value, source_enumerator.IsLast);
        }
    }
    public static IEnumerable<(T Value, bool IsFirst, bool IsLast)> TagFirstLast<T>(this IEnumerable<T> source) {
        using var source_enumerator = source.GetPeekableEnumerator();
        if (source_enumerator.TryTake( out var value_first )) {
            yield return (value_first, true, source_enumerator.IsLast);
        }
        while (source_enumerator.TryTake( out var value )) {
            yield return (value, false, source_enumerator.IsLast);
        }
    }


    // With/Prev-Next
    //public static IEnumerable<(T Value, Option<T> Prev)> WithPrev<T>(this IEnumerable<T> source) {
    //    var prev = default( Option<T> );
    //    foreach (var item in source) {
    //        yield return (item, prev);
    //        prev = item;
    //    }
    //}
    //public static IEnumerable<(T Value, Option<T> Next)> WithNext<T>(this IEnumerable<T> source) {
    //    using var source_enumerator = source.GetEnumerator();
    //    var item = source_enumerator.MoveNext() ? source_enumerator.Current : default( Option<T> );
    //    while (source_enumerator.MoveNext()) {
    //        var next = source_enumerator.Current;
    //        yield return (item!.Value, next);
    //        item = next;
    //    }
    //    if (item != null) {
    //        yield return (item.Value, default);
    //        item = default;
    //    }
    //}
    //public static IEnumerable<(T Value, Option<T> Prev, Option<T> Next)> WithPrevNext<T>(this IEnumerable<T> source) {
    //    using var source_enumerator = source.GetEnumerator();
    //    var prev = default( Option<T> );
    //    var item = source_enumerator.MoveNext() ? source_enumerator.Current : default( Option<T> );
    //    while (source_enumerator.MoveNext()) {
    //        var next = source_enumerator.Current;
    //        yield return (item!.Value, prev, next);
    //        prev = item;
    //        item = next;
    //    }
    //    if (item != null) {
    //        yield return (item.Value, prev, default);
    //        prev = item;
    //        item = default;
    //    }
    //}


    // With/Prev-Next
    public static IEnumerable<(T Current, Option<T> Prev)> WithPrev<T>(this IEnumerable<T> source) {
        using var source_enumerator = source.GetEnumerator();
        var prev = default( Option<T> );
        while (source_enumerator.TryTake( out var current )) {
            yield return (current, prev);
            prev = current;
        }
    }
    public static IEnumerable<(T Current, Option<T> Next)> WithNext<T>(this IEnumerable<T> source) {
        using var source_enumerator = source.GetPeekableEnumerator();
        while (source_enumerator.TryTake( out var current )) {
            yield return (current, source_enumerator.Peek());
        }
    }
    public static IEnumerable<(T Current, Option<T> Prev, Option<T> Next)> WithPrevNext<T>(this IEnumerable<T> source) {
        using var source_enumerator = source.GetPeekableEnumerator();
        var prev = default( Option<T> );
        while (source_enumerator.TryTake( out var current )) {
            yield return (current, prev, source_enumerator.Peek());
            prev = current;
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
