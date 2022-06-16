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
internal static class SourceFactory {
    public static int[] Array(params int[] array) {
        return array;
    }
    public static IEnumerator<int> Enumerator(params int[] array) {
        return array.AsEnumerable().GetEnumerator();
    }
}
internal static class ExpectedFactory {
    public static int[] Array(params int[] array) {
        return array;
    }

    public static int[][] Slices(params object[] array) {
        return array.Select( Slice ).ToArray();
    }
    private static int[] Slice(object @object) {
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

    public static (int, bool)[] TagFirst(params (int, bool)[] array) {
        return array;
    }
    public static (int, bool)[] TagLast(params (int, bool)[] array) {
        return array;
    }
    public static (int, bool, bool)[] TagFirstLast(params (int, bool, bool)[] array) {
        return array;
    }

    public static (int, Option<int>)[] WithPrev(params (int, Option<int>)[] array) {
        return array;
    }
    public static (int, Option<int>)[] WithNext(params (int, Option<int>)[] array) {
        return array;
    }
    public static (int, Option<int>, Option<int>)[] WithPrevNext(params (int, Option<int>, Option<int>)[] array) {
        return array;
    }

}