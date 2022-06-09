namespace System.Linq;
using System;
using System.Collections.Generic;
using System.Text;

public static class EnumerableExtensions {


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


    // Split
    public static IEnumerable<T[]> Split<T>(this IEnumerable<T> source, Predicate<T> predicate) {
        return source.FastSplit( predicate, i => i ).Select( i => i.ToArray() );
    }
    public static IEnumerable<TResult[]> Split<T, TResult>(this IEnumerable<T> source, Predicate<T> predicate, Func<T, TResult> resultSelector) {
        return source.FastSplit( predicate, resultSelector ).Select( i => i.ToArray() );
    }
    public static IEnumerable<IList<TResult>> FastSplit<T, TResult>(this IEnumerable<T> source, Predicate<T> predicate, Func<T, TResult> resultSelector) {
        // [false, false, false], break, [false, false]
        var segment = new List<TResult>();
        foreach (var item in source) {
            if (predicate( item )) {
                if (segment.Any()) {
                    yield return segment;
                    segment.Clear();
                }
            } else {
                segment.Add( resultSelector( item ) );
            }
        }
        if (segment.Any()) {
            yield return segment;
            segment.Clear();
        }
    }
    // Split/Before
    public static IEnumerable<T[]> SplitBefore<T>(this IEnumerable<T> source, Predicate<T> predicate) {
        return source.FastSplitBefore( predicate, i => i ).Select( i => i.ToArray() );
    }
    public static IEnumerable<TResult[]> SplitBefore<T, TResult>(this IEnumerable<T> source, Predicate<T> predicate, Func<T, TResult> resultSelector) {
        return source.FastSplitBefore( predicate, resultSelector ).Select( i => i.ToArray() );
    }
    public static IEnumerable<IList<TResult>> FastSplitBefore<T, TResult>(this IEnumerable<T> source, Predicate<T> predicate, Func<T, TResult> resultSelector) {
        // [false, false, false], break, [true, false, false]
        var segment = new List<TResult>();
        foreach (var item in source) {
            if (predicate( item )) {
                if (segment.Any()) {
                    yield return segment;
                    segment.Clear();
                }
            }
            segment.Add( resultSelector( item ) );
        }
        if (segment.Any()) {
            yield return segment;
            segment.Clear();
        }
    }
    // Split/After
    public static IEnumerable<T[]> SplitAfter<T>(this IEnumerable<T> source, Predicate<T> predicate) {
        return source.FastSplitAfter( predicate, i => i ).Select( i => i.ToArray() );
    }
    public static IEnumerable<TResult[]> SplitAfter<T, TResult>(this IEnumerable<T> source, Predicate<T> predicate, Func<T, TResult> resultSelector) {
        return source.FastSplitAfter( predicate, resultSelector ).Select( i => i.ToArray() );
    }
    public static IEnumerable<IList<TResult>> FastSplitAfter<T, TResult>(this IEnumerable<T> source, Predicate<T> predicate, Func<T, TResult> resultSelector) {
        // [false, false, false, true], break, [false, false]
        var segment = new List<TResult>();
        foreach (var item in source) {
            segment.Add( resultSelector( item ) );
            if (predicate( item )) {
                if (segment.Any()) {
                    yield return segment;
                    segment.Clear();
                }
            }
        }
        if (segment.Any()) {
            yield return segment;
            segment.Clear();
        }
    }
    // Slice/By
    //public static IEnumerable<T[]> SliceBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> keySelector) {
    //    return source.SliceBy( keySelector, i => i, EqualityComparer<TKey>.Default );
    //}
    //public static IEnumerable<TResult[]> SliceBy<T, TKey, TResult>(this IEnumerable<T> source, Func<T, TKey> keySelector, Func<T, TResult> resultSelector) {
    //    return source.SliceBy( keySelector, resultSelector, EqualityComparer<TKey>.Default );
    //}
    //public static IEnumerable<TResult[]> SliceBy<T, TKey, TResult>(this IEnumerable<T> source, Func<T, TKey> keySelector, Func<T, TResult> resultSelector, IEqualityComparer<TKey> comparer) {
    //    return source.FastSliceBy( keySelector, resultSelector, comparer ).Select( i => i.ToArray() );
    //}
    //public static IEnumerable<IList<TResult>> FastSliceBy<T, TKey, TResult>(this IEnumerable<T> source, Func<T, TKey> keySelector, Func<T, TResult> resultSelector, IEqualityComparer<TKey> comparer) {
    //    // [0, 0, 0, 0], [1, 1], [2]
    //    var prev = Option<TKey>.Default;
    //    var segment = new List<TResult>();
    //    foreach (var item in source) {
    //        var key = keySelector( item );
    //        if (prev.HasValue && !comparer.Equals( prev.Value, key )) {
    //            yield return segment;
    //            segment.Clear();
    //        }
    //        segment.Add( resultSelector( item ) );
    //        prev = key;
    //    }
    //    if (segment.Any()) {
    //        yield return segment;
    //        segment.Clear();
    //    }
    //}


    // Tag/First
    public static IEnumerable<(T Value, bool IsFirst)> TagFirst<T>(this IEnumerable<T> source) {
        using var source_enumerator = source.GetEnumerator();
        if (source_enumerator.TryTake( out var first )) {
            yield return (first, true);
        }
        while (source_enumerator.TryTake( out var value )) {
            yield return (value, false);
        }
    }
    // Tag/Last
    public static IEnumerable<(T Value, bool IsLast)> TagLast<T>(this IEnumerable<T> source) {
        using var source_enumerator = source.GetEnumerator();
        var value = source_enumerator.Take();
        var next = source_enumerator.Take();
        while (value.HasValue) {
            yield return (value.Value, !next.HasValue);
            (value, next) = (next, source_enumerator.Take());
        }
    }
    // Tag/First-Last
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


    // With/Prev
    public static IEnumerable<(T Value, Option<T> Prev)> WithPrev<T>(this IEnumerable<T> source) {
        var prev = Option<T>.Default;
        foreach (var item in source) {
            yield return (item, prev);
            prev = item;
        }
    }
    // With/Next
    public static IEnumerable<(T Value, Option<T> Next)> WithNext<T>(this IEnumerable<T> source) {
        using var source_enumerator = source.GetEnumerator();
        var value = source_enumerator.Take();
        var next = source_enumerator.Take();
        while (value.HasValue) {
            yield return (value.Value, next);
            (value, next) = (next, source_enumerator.Take());
        }
    }
    // With/Prev-Next
    public static IEnumerable<(T Value, Option<T> Prev, Option<T> Next)> WithPrevNext<T>(this IEnumerable<T> source) {
        using var source_enumerator = source.GetEnumerator();
        var prev = Option<T>.Default;
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
