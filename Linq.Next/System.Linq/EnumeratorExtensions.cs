namespace System.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

public static class EnumeratorExtensions {


    // Take
    public static bool TryTake<T>(this IEnumerator<T> enumerator, [MaybeNullWhen( false )] out T current) {
        return enumerator.Take().TryGetValue( out current );
    }
    public static Option<T> Take<T>(this IEnumerator<T> enumerator) {
        if (enumerator.MoveNext()) return enumerator.Current;
        return default;
    }


}
