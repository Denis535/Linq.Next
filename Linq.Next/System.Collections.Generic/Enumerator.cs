namespace System.Collections.Generic;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;

// Usually iterating over an array looks like this:
// var i = 0;
// var v0 = array[i]; i++;
// var v1 = array[i]; i++;

// But iterating over an enumerator looks like this:
// var i = -1;
// Take:
//   i++;
//   var value = array[i];
// Peek:
//   var value = array[i+1];

// Maybe the desired behavior would be???:
// var i = -1;
// Take:
//   if (i == -1) i++;
//   var value = array[i]; i++;
// Peek:
//   var value = array[i+1];

// Enumerator/Stateful
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

// Enumerator/Peekable
// Note: Don't use try-catch-finally in Take, Peek, Reset (because enumerator can not be started, finished, reseted if there was exception)
public class PeekableEnumerator<T> : IEnumerator<T>, IDisposable {

    private Option<T> current, next;
    private IEnumerator<T> Source { get; }
    public bool IsStarted { get; private set; }
    public bool IsLast => !PeekInternal().HasValue;
    public bool IsFinished { get; private set; }
    public bool HasNext => PeekInternal().HasValue;
    public Option<T> Current => current;
    public Option<T> Next => PeekInternal();


    public PeekableEnumerator(IEnumerator<T> source!!) {
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
        (IsStarted, IsFinished) = (false, false);
        (current, next) = (default, default);
    }


    // Helpers
    private Option<T> TakeInternal() {
        if (next.HasValue) {
            (IsStarted, IsFinished) = (true, false);
            (current, next) = (next, default);
            return current;
        }
        if (Source.MoveNext()) {
            (IsStarted, IsFinished) = (true, false);
            (current, next) = (Source.Current, default);
            return current;
        }
        (IsStarted, IsFinished) = (true, true);
        (current, next) = (default, default);
        return current;
    }
    private Option<T> PeekInternal() {
        // It does not affect IsStarted
        // It does not affect IsFinished
        // It does not affect Current
        if (next.HasValue) {
            return next;
        }
        if (Source.MoveNext()) {
            next = Source.Current;
            return next;
        }
        next = default;
        return next;
    }


}