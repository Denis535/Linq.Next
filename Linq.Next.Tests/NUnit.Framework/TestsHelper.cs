namespace NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal static class TestsHelper {

    public static readonly Option<int> Default = default;


    public static int[] Array() {
        return System.Array.Empty<int>();
    }
    public static int[] Array(params int[] array) {
        return array;
    }
    public static int[][] Array2D(params int[][] array) {
        return array;
    }


    public static IEnumerator<int> Enumerator(params int[] array) {
        return array.AsEnumerable().GetEnumerator();
    }


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
