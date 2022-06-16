namespace System.Collections.Generic;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;

// Note: Don't use try-catch-finally in Take, Reset (because enumerator can not be started, finished, reseted if there was exception)
public class StatefulEnumerator<T> : IEnumerator<T>, IDisposable {

    private Option<T> current;
    private IEnumerator<T> Source { get; }
    public bool IsStarted { get; private set; }
    public bool IsFinished { get; private set; }
    public Option<T> Current => current;


    public StatefulEnumerator(IEnumerator<T> source!!) {
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
            current = Source.Current;
            return current;
        }
        (IsStarted, IsFinished) = (true, true);
        current = default;
        return current;
    }


}