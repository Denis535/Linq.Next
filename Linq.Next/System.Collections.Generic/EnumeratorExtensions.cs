namespace System.Collections.Generic;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;

public static class EnumeratorExtensions {


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
