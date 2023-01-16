// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal static class Expected<T> {

    // Value
    public static T Value(T value) {
        return value;
    }

    // Option
    public static Option<T> Option() {
        return default;
    }
    public static Option<T> Option(T value) {
        return new Option<T>( value );
    }

    // Array
    public static T[] Array(params T[] array) {
        return array;
    }

    // Helpers
    //private static T[] ToArray(object? @object) {
    //    if (@object is null) {
    //        return new T[ 0 ];
    //    }
    //    if (@object is T value) {
    //        return new T[ 1 ] { value };
    //    }
    //    if (@object is T[] array) {
    //        return array;
    //    }
    //    if (@object is ITuple tuple) {
    //        return Enumerable.Range( 0, tuple.Length ).Select( i => tuple[ i ] ).Cast<T>().ToArray();
    //    }
    //    throw new ArgumentException( $"Object '{@object}' is invalid" );
    //}

}
internal static class Expected<T1, T2> {

    // Value
    public static (T1, T2) Value(T1 value1, T2 value2) {
        return (value1, value2);
    }

    // Option
    public static Option<(T1, T2)> Option() {
        return default;
    }
    public static Option<(T1, T2)> Option(T1 value1, T2 value2) {
        return new Option<(T1, T2)>( (value1, value2) );
    }

    // Array
    public static (T1, T2)[] Array(params (T1, T2)[] array) {
        return array;
    }

}
internal static class Expected<T1, T2, T3> {

    // Value
    public static (T1, T2, T3) Value(T1 value1, T2 value2, T3 value3) {
        return (value1, value2, value3);
    }

    // Option
    public static Option<(T1, T2, T3)> Option() {
        return default;
    }
    public static Option<(T1, T2, T3)> Option(T1 value1, T2 value2, T3 value3) {
        return new Option<(T1, T2, T3)>( (value1, value2, value3) );
    }

    // Array
    public static (T1, T2, T3)[] Array(params (T1, T2, T3)[] array) {
        return array;
    }

}