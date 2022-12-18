// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace System.Linq;
using System;
using System.Collections.Generic;
using System.Text;

public static class LinqNext {


    // LazyGroup
    public static IEnumerable<T[]> LazyGroup<T>(
        this IEnumerable<T> source,
        Func<T, bool> predicate) {
        return source.FastLazyGroup( (i, group) => predicate( i ), i => i ).Select( i => i.ToArray() );
    }
    public static IEnumerable<TResult[]> LazyGroup<T, TResult>(
        this IEnumerable<T> source,
        Func<T, bool> predicate,
        Func<T, TResult> resultSelector) {
        return source.FastLazyGroup( (i, group) => predicate( i ), resultSelector ).Select( i => i.ToArray() );
    }
    // LazyGroup
    public static IEnumerable<T[]> LazyGroup<T>(
        this IEnumerable<T> source,
        Func<T, IReadOnlyList<T>, bool> predicate) {
        return source.FastLazyGroup( predicate, i => i ).Select( i => i.ToArray() );
    }
    public static IEnumerable<TResult[]> LazyGroup<T, TResult>(
        this IEnumerable<T> source,
        Func<T, IReadOnlyList<TResult>, bool> predicate,
        Func<T, TResult> resultSelector) {
        return source.FastLazyGroup( predicate, resultSelector ).Select( i => i.ToArray() );
    }
    // LazyGroup/Fast
    public static IEnumerable<IReadOnlyList<TResult>> FastLazyGroup<T, TResult>(
        this IEnumerable<T> source,
        Func<T, IReadOnlyList<TResult>, bool> predicate,
        Func<T, TResult> resultSelector) {
        // Join adjacent items into groups
        // [true, true, true], [false, true, true]
        using var source_enumerator = source.GetEnumerator();
        var group = new List<TResult>();
        var item = source_enumerator.Take();
        while (item.HasValue) {
            { // Add the initial item to the group
                group.Add( resultSelector( item.Value ) );
                item = source_enumerator.Take();
            }
            while (item.HasValue && predicate( item.Value, group )) { // Add the next item to the group if this item belongs to this group
                group.Add( resultSelector( item.Value ) );
                item = source_enumerator.Take();
            }
            yield return group; // yield the filled group
            group.Clear();
        }
    }


    // Split
    public static IEnumerable<T[]> Split<T>(
        this IEnumerable<T> source,
        Func<T, bool> predicate) {
        return source.FastSplit( predicate, i => i ).Select( i => i.ToArray() );
    }
    public static IEnumerable<TResult[]> Split<T, TResult>(
        this IEnumerable<T> source,
        Func<T, bool> predicate,
        Func<T, TResult> resultSelector) {
        return source.FastSplit( predicate, resultSelector ).Select( i => i.ToArray() );
    }
    // Split/Fast
    public static IEnumerable<IReadOnlyList<TResult>> FastSplit<T, TResult>(
        this IEnumerable<T> source,
        Func<T, bool> predicate,
        Func<T, TResult> resultSelector) {
        // Split items by separator into slices (the separator is excluded)
        // [false, false, false], true, [false, false, false]
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
    public static IEnumerable<T[]> SplitBefore<T>(
        this IEnumerable<T> source,
        Func<T, bool> predicate) {
        return source.FastSplitBefore( predicate, i => i ).Select( i => i.ToArray() );
    }
    public static IEnumerable<TResult[]> SplitBefore<T, TResult>(
        this IEnumerable<T> source,
        Func<T, bool> predicate,
        Func<T, TResult> resultSelector) {
        return source.FastSplitBefore( predicate, resultSelector ).Select( i => i.ToArray() );
    }
    // Split/Before/Fast
    public static IEnumerable<IReadOnlyList<TResult>> FastSplitBefore<T, TResult>(
        this IEnumerable<T> source,
        Func<T, bool> predicate,
        Func<T, TResult> resultSelector) {
        // Split items by separator into slices (spliting before the separator)
        // [false, false, false], [true, false, false]
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
    public static IEnumerable<T[]> SplitAfter<T>(
        this IEnumerable<T> source,
        Func<T, bool> predicate) {
        return source.FastSplitAfter( predicate, i => i ).Select( i => i.ToArray() );
    }
    public static IEnumerable<TResult[]> SplitAfter<T, TResult>(
        this IEnumerable<T> source,
        Func<T, bool> predicate,
        Func<T, TResult> resultSelector) {
        return source.FastSplitAfter( predicate, resultSelector ).Select( i => i.ToArray() );
    }
    // Split/After/Fast
    public static IEnumerable<IReadOnlyList<TResult>> FastSplitAfter<T, TResult>(
        this IEnumerable<T> source,
        Func<T, bool> predicate,
        Func<T, TResult> resultSelector) {
        // Split items by separator into slices (spliting after the separator)
        // [false, false, true], [false, false, false]
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


    // CompareTo
    public static void CompareTo<T>(this IEnumerable<T> first, IEnumerable<T> second, out T[] missing, out T[] extra) {
        var second_ = new LinkedList<T>( second );
        extra = first.Where( i => !second_.Remove( i ) ).ToArray();
        missing = second_.ToArray();
    }


}
