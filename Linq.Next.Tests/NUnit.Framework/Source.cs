// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

internal static class Source {

    // Option
    public static readonly Option<int> Option = default;

    // Array
    public static int[] Array(params int[] array) {
        return array;
    }

    // Enumerator
    public static IEnumerator<int> Enumerator(params int[] array) {
        return array.AsEnumerable().GetEnumerator();
    }
    public static StatefulEnumerator<int> Stateful(params int[] array) {
        return array.AsEnumerable().GetEnumerator().AsStateful();
    }
    public static PeekableEnumerator<int> Peekable(params int[] array) {
        return array.AsEnumerable().GetEnumerator().AsPeekable();
    }

    // Predicate
    public static Func<int, bool> Predicate(bool value) {
        return i => value;
    }
    public static Func<int, bool> Predicate(Func<int, bool> predicate) {
        return predicate;
    }
    public static Func<int, IList<int>, bool> Predicate(Func<int, IList<int>, bool> predicate) {
        return predicate;
    }

}
