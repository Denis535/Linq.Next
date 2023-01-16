// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace NUnit.Framework;
using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

internal static class Expected {

    // Option
    public static readonly Option<int> Option = default;

    // Array
    public static int[] Array1D(params int[] array) {
        return array;
    }
    public static int[][] Array2D(params object?[] array) {
        return array.Select( ToArray ).ToArray();
    }
    private static int[] ToArray(object? @object) {
        if (@object is null) {
            return new int[ 0 ];
        }
        if (@object is int value) {
            return new int[ 1 ] { value };
        }
        if (@object is int[] array) {
            return array;
        }
        if (@object is ITuple values) {
            return Enumerable.Range( 0, values.Length ).Select( i => values[ i ] ).Cast<int>().ToArray();
        }
        throw new ArgumentException( $"Object '{@object}' is invalid" );
    }

    // Array
    public static (int, int[])[] Array_Unflatten(params (int, int[])[] array) {
        return array;
    }

    // Array
    public static (int, bool)[] Array_TagFirst(params (int, bool)[] array) {
        return array;
    }
    public static (int, bool)[] Array_TagLast(params (int, bool)[] array) {
        return array;
    }
    public static (int, bool, bool)[] Array_TagFirstLast(params (int, bool, bool)[] array) {
        return array;
    }

    // Array
    public static (int, Option<int>)[] Array_WithPrev(params (int, Option<int>)[] array) {
        return array;
    }
    public static (int, Option<int>)[] Array_WithNext(params (int, Option<int>)[] array) {
        return array;
    }
    public static (int, Option<int>, Option<int>)[] Array_WithPrevNext(params (int, Option<int>, Option<int>)[] array) {
        return array;
    }

}