// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

internal static class TestsHelper {

    public static Option<int> Default => default;


    public static Option<object> Option() {
        return new Option<object>();
    }
    public static Option<object?> Option(object? value) {
        return new Option<object?>( value );
    }
    public static Option<int> Option(int value) {
        return new Option<int>( value );
    }


}
internal static class Source {

    public static int[] Array(params int[] array) {
        return array;
    }
    public static IEnumerator<int> Enumerator(params int[] array) {
        return array.AsEnumerable().GetEnumerator();
    }
    public static StatefulEnumerator<int> Stateful(params int[] array) {
        return array.AsEnumerable().GetEnumerator().AsStateful();
    }
    public static PeekableEnumerator<int> Peekable(params int[] array) {
        return array.AsEnumerable().GetEnumerator().AsPeekable();
    }

    public static Func<int, bool> Predicate(bool value) {
        return i => value;
    }
    public static Func<int, bool> Predicate(Func<int, bool> predicate) {
        return predicate;
    }
    public static Func<int, IReadOnlyList<int>, bool> Predicate(Func<int, IReadOnlyList<int>, bool> predicate) {
        return predicate;
    }

}
internal static class Expected {

    public static int[] Array(params int[] array) {
        return array;
    }

    public static int[][] Array2D(params object[] array) {
        return array.Select( GetValues ).ToArray();
    }

    public static (int, bool)[] Array_TagFirst(params (int, bool)[] array) {
        return array;
    }
    public static (int, bool)[] Array_TagLast(params (int, bool)[] array) {
        return array;
    }
    public static (int, bool, bool)[] Array_TagFirstLast(params (int, bool, bool)[] array) {
        return array;
    }

    public static (int, Option<int>)[] Array_WithPrev(params (int, Option<int>)[] array) {
        return array;
    }
    public static (int, Option<int>)[] Array_WithNext(params (int, Option<int>)[] array) {
        return array;
    }
    public static (int, Option<int>, Option<int>)[] Array_WithPrevNext(params (int, Option<int>, Option<int>)[] array) {
        return array;
    }

    // Helpers
    private static int[] GetValues(object @object) {
        if (@object is ITuple values) {
            return Enumerable.Range( 0, values.Length ).Select( i => values[ i ] ).Cast<int>().ToArray();
        }
        if (@object is int value) {
            return new int[ 1 ] { value };
        }
        if (@object is null) {
            return new int[ 0 ];
        }
        throw new ArgumentException( $"Object '{@object}' is invalid" );
    }

}