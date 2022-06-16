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
    public static int[] Array() {
        return System.Array.Empty<int>();
    }
    public static int[] Array(params int[] array) {
        return array;
    }
    public static IEnumerator<int> Enumerator() {
        return System.Array.Empty<int>().AsEnumerable().GetEnumerator();
    }
    public static IEnumerator<int> Enumerator(params int[] array) {
        return array.AsEnumerable().GetEnumerator();
    }
}
internal static class ExpectedFactory {
    public static int[] Array() {
        return System.Array.Empty<int>();
    }
    public static int[] Array(params int[] array) {
        return array;
    }
    public static int[][] Array2D(params object[] array) {
        return array.Select( Array1D ).ToArray();
    }
    private static int[] Array1D(object @object) {
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