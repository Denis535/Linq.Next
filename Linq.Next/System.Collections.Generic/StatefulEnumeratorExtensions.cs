namespace System.Collections.Generic;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;

public static class StatefulEnumeratorExtensions {


    // Take/While
    public static IEnumerable<T> TakeWhile<T>(this StatefulEnumerator<T> enumerator, Predicate<T> predicate) {
        // [true, true], break, false
        while (enumerator.TryTake( out var current )) {
            if (predicate( current )) {
                yield return current;
            } else {
                break;
            }
        }
    }
    // Take/Until
    public static IEnumerable<T> TakeUntil<T>(this StatefulEnumerator<T> enumerator, Predicate<T> predicate) {
        // [false, false], break, true
        while (enumerator.TryTake( out var current )) {
            if (!predicate( current )) {
                yield return current;
            } else {
                break;
            }
        }
    }


}