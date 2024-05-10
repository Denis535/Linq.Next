// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace System.Collections.Generic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

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

    // Constructor
    public PeekableEnumerator(IEnumerator<T> source) {
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
            (current, next) = (Source.Current.AsOption(), default);
            return current;
        }
        (IsStarted, IsFinished) = (true, true);
        (current, next) = (default, default);
        return current;
    }
    private Option<T> PeekInternal() {
        // It does not affect: IsStarted, IsFinished, Current
        if (next.HasValue) {
            return next;
        }
        if (Source.MoveNext()) {
            next = Source.Current.AsOption();
            return next;
        }
        next = default;
        return next;
    }

}
public static class PeekableEnumeratorExtensions {

    // Take/While
    public static IEnumerable<T> TakeWhile<T>(this PeekableEnumerator<T> enumerator, Func<T, bool> predicate) {
        // [true, true], false
        while (enumerator.TryPeek( out var next ) && predicate( next )) {
            yield return enumerator.Take().Value;
        }
    }
    // Take/Until
    public static IEnumerable<T> TakeUntil<T>(this PeekableEnumerator<T> enumerator, Func<T, bool> predicate) {
        // [false, false], true
        while (enumerator.TryPeek( out var next ) && !predicate( next )) {
            yield return enumerator.Take().Value;
        }
    }

    // Take/Try
    public static bool TryTakeIf<T>(this PeekableEnumerator<T> enumerator, Func<T, bool> predicate, [MaybeNullWhen( false )] out T current) {
        return enumerator.TakeIf( predicate ).TryGetValue( out current );
    }
    public static bool TryTakeIfNot<T>(this PeekableEnumerator<T> enumerator, Func<T, bool> predicate, [MaybeNullWhen( false )] out T current) {
        return enumerator.TakeIfNot( predicate ).TryGetValue( out current );
    }

    // Take
    public static Option<T> TakeIf<T>(this PeekableEnumerator<T> enumerator, Func<T, bool> predicate) {
        if (enumerator.TryPeek( out var next ) && predicate( next )) {
            return enumerator.Take();
        }
        return default;
    }
    public static Option<T> TakeIfNot<T>(this PeekableEnumerator<T> enumerator, Func<T, bool> predicate) {
        if (enumerator.TryPeek( out var next ) && !predicate( next )) {
            return enumerator.Take();
        }
        return default;
    }

}
