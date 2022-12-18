// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace System.Collections.Generic;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;

public static class EnumeratorExtensions {

    // As/Stateful
    public static StatefulEnumerator<T> AsStateful<T>(this IEnumerator<T> enumerator) {
        return new StatefulEnumerator<T>( enumerator );
    }
    // As/Peekable
    public static PeekableEnumerator<T> AsPeekable<T>(this IEnumerator<T> enumerator) {
        return new PeekableEnumerator<T>( enumerator );
    }

    // Take/Try
    public static bool TryTake<T>(this IEnumerator<T> enumerator, [MaybeNullWhen( false )] out T current) {
        return enumerator.Take().TryGetValue( out current );
    }
    // Take
    public static Option<T> Take<T>(this IEnumerator<T> enumerator) {
        if (enumerator.MoveNext()) return enumerator.Current;
        return default;
    }

}