// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace System.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

public static class LinqNext {


    // Split
    public static IEnumerable<T[]> Split<T>(
        this IEnumerable<T> source,
        Func<T, bool> predicate
        ) {
        return source.FastSplit( predicate, i => i ).Select( i => i.ToArray() );
    }
    public static IEnumerable<TResult[]> Split<T, TResult>(
        this IEnumerable<T> source,
        Func<T, bool> predicate,
        Func<T, TResult> resultSelector
        ) {
        return source.FastSplit( predicate, resultSelector ).Select( i => i.ToArray() );
    }
    // Split/Fast
    // Split the items into segments (the separator is excluded)
    // [false, false, false], true, [false, false, false]
    public static IEnumerable<IList<TResult>> FastSplit<T, TResult>(
        this IEnumerable<T> source,
        Func<T, bool> predicate,
        Func<T, TResult> resultSelector
        ) {
        var segment = new List<TResult>();
        foreach (var item in source) {
            if (predicate( item )) {
                yield return segment;
                segment.Clear();
            } else {
                segment.Add( resultSelector( item ) );
            }
        }
        {
            yield return segment;
            segment.Clear();
        }
    }


    // Split/Before
    public static IEnumerable<T[]> SplitBefore<T>(
        this IEnumerable<T> source,
        Func<T, bool> predicate
        ) {
        return source.FastSplitBefore( predicate, i => i ).Select( i => i.ToArray() );
    }
    public static IEnumerable<TResult[]> SplitBefore<T, TResult>(
        this IEnumerable<T> source,
        Func<T, bool> predicate,
        Func<T, TResult> resultSelector
        ) {
        return source.FastSplitBefore( predicate, resultSelector ).Select( i => i.ToArray() );
    }
    // Split/Before/Fast
    // Split the items into segments (the separator is included at the beginning of segment)
    // [false, false, false], [true, false, false]
    public static IEnumerable<IList<TResult>> FastSplitBefore<T, TResult>(
        this IEnumerable<T> source,
        Func<T, bool> predicate,
        Func<T, TResult> resultSelector
        ) {
        var segment = new List<TResult>();
        foreach (var item in source) {
            if (predicate( item )) {
                yield return segment;
                segment.Clear();
            }
            segment.Add( resultSelector( item ) );
        }
        {
            yield return segment;
            segment.Clear();
        }
    }


    // Split/After
    public static IEnumerable<T[]> SplitAfter<T>(
        this IEnumerable<T> source,
        Func<T, bool> predicate
        ) {
        return source.FastSplitAfter( predicate, i => i ).Select( i => i.ToArray() );
    }
    public static IEnumerable<TResult[]> SplitAfter<T, TResult>(
        this IEnumerable<T> source,
        Func<T, bool> predicate,
        Func<T, TResult> resultSelector
        ) {
        return source.FastSplitAfter( predicate, resultSelector ).Select( i => i.ToArray() );
    }
    // Split/After/Fast
    // Split the items into segments (the separator is included at the end of segment)
    // [false, false, true], [false, false, false]
    public static IEnumerable<IList<TResult>> FastSplitAfter<T, TResult>(
        this IEnumerable<T> source,
        Func<T, bool> predicate,
        Func<T, TResult> resultSelector
        ) {
        var segment = new List<TResult>();
        foreach (var item in source) {
            segment.Add( resultSelector( item ) );
            if (predicate( item )) {
                yield return segment;
                segment.Clear();
            }
        }
        {
            yield return segment;
            segment.Clear();
        }
    }


    // Slice
    public static IEnumerable<T[]> Slice<T>(
        this IEnumerable<T> source,
        Func<T, IList<T>, bool> predicate
        ) {
        return source.FastSlice( predicate, i => i ).Select( i => i.ToArray() );
    }
    public static IEnumerable<TResult[]> Slice<T, TResult>(
        this IEnumerable<T> source,
        Func<T, IList<TResult>, bool> predicate,
        Func<T, TResult> resultSelector
        ) {
        return source.FastSlice( predicate, resultSelector ).Select( i => i.ToArray() );
    }
    // Slice/Fast
    // Slice the items into slices
    // [true, true, true], [false, true, true]
    public static IEnumerable<IList<TResult>> FastSlice<T, TResult>(
        this IEnumerable<T> source,
        Func<T, IList<TResult>, bool> predicate,
        Func<T, TResult> resultSelector
        ) {
        using var source_enumerator = source.GetEnumerator();
        var slice = new List<TResult>();
        foreach (var item in source) {
            if (slice.Any() && !predicate( item, slice )) {
                yield return slice;
                slice.Clear();
            }
            slice.Add( resultSelector( item ) );
        }
        if (slice.Any()) {
            yield return slice;
            slice.Clear();
        }
    }


    // Unflatten
    // Unflatten the items into key-values groups
    // true: [false, false, false], true: [false, false, false]
    // key: [value, value, value], key: [value, value, value]
    public static IEnumerable<(Option<T> Key, T[] Values)> Unflatten<T>(
        this IEnumerable<T> source,
        Func<T, bool> predicate
        ) {
        return source.FastUnflatten( predicate, i => i, i => i ).Select( i => (i.Key, i.Values.ToArray()) );
    }
    public static IEnumerable<(Option<TKey> Key, TValue[] Values)> Unflatten<T, TKey, TValue>(
        this IEnumerable<T> source,
        Func<T, bool> predicate,
        Func<T, TKey> keySelector,
        Func<T, TValue> valueSelector
        ) {
        return source.FastUnflatten( predicate, keySelector, valueSelector ).Select( i => (i.Key, i.Values.ToArray()) );
    }
    // Unflatten/Fast
    public static IEnumerable<(Option<TKey> Key, IList<TValue> Values)> FastUnflatten<T, TKey, TValue>(
        this IEnumerable<T> source,
        Func<T, bool> predicate,
        Func<T, TKey> keySelector,
        Func<T, TValue> valueSelector
        ) {
        using var source_enumerator = source.GetEnumerator();
        var key = default( Option<TKey> );
        var values = new List<TValue>();
        foreach (var item in source) {
            if (predicate( item )) {
                if (key.HasValue || values.Any()) {
                    yield return (key, values);
                    values.Clear();
                }
                key = keySelector( item ).AsOption();
            } else {
                values.Add( valueSelector( item ) );
            }
        }
        if (key.HasValue || values.Any()) {
            yield return (key, values);
            values.Clear();
        }
    }


    // With/Prev
    // Return the each item with prev item
    public static IEnumerable<(T Value, Option<T> Prev)> WithPrev<T>(this IEnumerable<T> source) {
        var prev = default( Option<T> );
        foreach (var item in source) {
            yield return (item, prev);
            prev = item.AsOption();
        }
    }
    // With/Next
    // Return the each item with next item
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
    // Return the each item with prev and next item
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
