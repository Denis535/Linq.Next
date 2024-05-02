// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal static class Helper {

    public static T Value<T>(T value) {
        return value;
    }
    public static Option<T> Option<T>(T value) {
        return value != null ? new Option<T>( value ) : default;
    }
    public static Option<T> Option<T>(T? value) where T : struct {
        return value != null ? new Option<T>( value.Value ) : default;
    }

    public static T[] Values<T>(params T[] values) {
        return values;
    }
    public static Option<T>[] Options<T>(params T[] values) {
        return values.Select( i => i != null ? new Option<T>( i ) : default ).ToArray();
    }
    public static Option<T>[] Options<T>(params T?[] values) where T : struct {
        return values.Select( i => i != null ? new Option<T>( i.Value ) : default ).ToArray();
    }

    public static IEnumerator<T> Enumerator<T>(params T[] values) {
        return values.AsEnumerable().GetEnumerator();
    }
    public static StatefulEnumerator<T> Stateful<T>(params T[] values) {
        return values.AsEnumerable().GetEnumerator().AsStateful();
    }
    public static PeekableEnumerator<T> Peekable<T>(params T[] values) {
        return values.AsEnumerable().GetEnumerator().AsPeekable();
    }

}
