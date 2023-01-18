// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal static class Source<T> {

    // Array
    public static T[] Array(params T[] array) {
        return array;
    }
    // Array/Enumerator
    public static IEnumerator<T> Enumerator(params T[] array) {
        return array.AsEnumerable().GetEnumerator();
    }
    public static StatefulEnumerator<T> Stateful(params T[] array) {
        return array.AsEnumerable().GetEnumerator().AsStateful();
    }
    public static PeekableEnumerator<T> Peekable(params T[] array) {
        return array.AsEnumerable().GetEnumerator().AsPeekable();
    }

    // Predicate
    public static Func<bool> Predicate(Func<bool> predicate) {
        return predicate;
    }
    public static Func<T, bool> Predicate(Func<T, bool> predicate) {
        return predicate;
    }
    //public static Func<IList<T>, T, bool> Predicate(Func<IList<T>, T, bool> predicate) {
    //    return predicate;
    //}
    //public static Func<T[], T, bool> Predicate(Func<T[], T, bool> predicate) {
    //    return predicate;
    //}
    public static Func<T, IList<T>, bool> Predicate(Func<T, IList<T>, bool> predicate) {
        return predicate;
    }
    public static Func<T, T[], bool> Predicate(Func<T, T[], bool> predicate) {
        return predicate;
    }

}
