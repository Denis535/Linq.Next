// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace System.Collections.Generic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

// Note: Don't use try-catch-finally in Take, Reset (because enumerator can not be started, finished, reseted if there was exception)
public class StatefulEnumerator<T> : IEnumerator<T>, IDisposable {

    private Option<T> current;
    private IEnumerator<T> Source { get; }
    public bool IsStarted { get; private set; }
    public bool IsFinished { get; private set; }
    public Option<T> Current => current;

    // Constructor
    public StatefulEnumerator(IEnumerator<T> source) {
        Source = source;
    }
    public void Dispose() {
        Source.Dispose();
    }

    // IEnumerator
    T IEnumerator<T>.Current => current.Value;
    object? IEnumerator.Current => current.Value;
    bool IEnumerator.MoveNext() => TakeInternal().HasValue;

    // Take
    public bool TryTake([MaybeNullWhen( false )] out T current) {
        return TakeInternal().TryGetValue( out current );
    }
    public Option<T> Take() {
        return TakeInternal();
    }

    // Reset
    public void Reset() {
        Source.Reset();
        (IsStarted, IsFinished) = (false, false);
        current = default;
    }

    // Helpers
    private Option<T> TakeInternal() {
        if (Source.MoveNext()) {
            (IsStarted, IsFinished) = (true, false);
            current = Source.Current.AsOption();
            return current;
        }
        (IsStarted, IsFinished) = (true, true);
        current = default;
        return current;
    }

}
