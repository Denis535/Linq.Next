namespace System.Collections.Generic;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;

// Note: Don't use try-catch-finally in Take, Peek, Reset (because enumerator can not be started, finished, reseted if there was exception)
public class PeekableEnumerator<T> : IEnumerator<T>, IDisposable {

    private IEnumerator<T> Source { get; }
    public Option<T> Current { get; set; }
    private Option<T> Next { get; set; }
    public bool HasNext => PeekInternal().HasValue;
    public bool IsStarted { get; private set; }
    public bool IsLast => !PeekInternal().HasValue;
    public bool IsFinished { get; private set; }


    public PeekableEnumerator(IEnumerator<T> source!!) {
        Source = source;
    }
    public void Dispose() {
        Source.Dispose();
    }


    // IEnumerator
    T IEnumerator<T>.Current => Current.Value;
    object? IEnumerator.Current => Current.Value;
    bool IEnumerator.MoveNext() => TakeInternal().HasValue;


    // Take
    public bool TryTake([MaybeNullWhen( false )] out T current) {
        return TakeInternal().TryGetValue( out current );
    }
    public Option<T> Take() {
        return TakeInternal();
    }
    // Peek
    public bool TryPeek([MaybeNullWhen( false )] out T next) {
        return PeekInternal().TryGetValue( out next );
    }
    public Option<T> Peek() {
        return PeekInternal();
    }
    // Reset
    public void Reset() {
        Source.Reset();
        Current = default;
        Next = default;
        IsStarted = false;
        IsFinished = false;
    }


    // Helpers
    private Option<T> TakeInternal() {
        var value = MoveNext( Next, Source );
        Current = value;
        Next = default;
        IsStarted = true;
        IsFinished = !Current.HasValue;
        return Current;
    }
    private Option<T> PeekInternal() {
        var value = MoveNext( Next, Source );
        // It does not affect Current
        Next = value;
        return Next;
        // It does not affect IsStarted
        // It does not affect IsFinished
    }
    private static Option<T> MoveNext(Option<T> next, IEnumerator<T> enumerator) {
        if (next.HasValue) return next;
        if (enumerator.MoveNext()) return enumerator.Current;
        return default;
    }


}