namespace System.Linq;
using System;
using System.Collections.Generic;
using System.Text;

public static class EnumerableExtensions {


    // CompareTo
    public static void CompareTo<T>(this IEnumerable<T> first, IEnumerable<T> second, out T[] missing, out T[] extra) {
        var second_ = new LinkedList<T>( second );
        extra = first.Where( i => !second_.Remove( i ) ).ToArray();
        missing = second_.ToArray();
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


}
