// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

[TestFixture( TestName = "Tests_Enumerator/Peekable" )]
public class Tests_PeekableEnumerator {


    // Constructor
    [Test]
    public void Constructor() {
        using var source = Source.Peekable( 0, 1, 2 );
        Assert.That( source.IsStarted, Is.False );
        Assert.That( source.IsFinished, Is.False );
        Assert.That( source.Current, Is.EqualTo( Expected.Option( null ) ) );
    }


    // Take
    [Test]
    public void Take_00() {
        using var source = Source.Peekable();
        // Peek
        Peek( source, false, false, Expected.Option( null ), Expected.Option( null ) );
        // Take-Peek
        Take( source, true, true, Expected.Option( null ) );
        Peek( source, true, true, Expected.Option( null ), Expected.Option( null ) );
    }
    [Test]
    public void Take_01() {
        using var source = Source.Peekable( 0, 1, 2 );
        // Peek
        Peek( source, false, false, Expected.Option( null ), Expected.Option( 0 ) );
        // Take-Peek
        Take( source, true, false, Expected.Option( 0 ) );
        Peek( source, true, false, Expected.Option( 0 ), Expected.Option( 1 ) );
        // Take-Peek
        Take( source, true, false, Expected.Option( 1 ) );
        Peek( source, true, false, Expected.Option( 1 ), Expected.Option( 2 ) );
        // Take-Peek
        Take( source, true, false, Expected.Option( 2 ) );
        Peek( source, true, false, Expected.Option( 2 ), Expected.Option( null ) );
        // Take-Peek
        Take( source, true, true, Expected.Option( null ) );
        Peek( source, true, true, Expected.Option( null ), Expected.Option( null ) );
    }


    // Reset
    [Test]
    public void Reset() {
        using var source = Source.Peekable( 0, 1, 2 );
        ((IEnumerator<int>) source).MoveNext();

        source.Reset();
        Assert.That( source.IsStarted, Is.False );
        Assert.That( source.IsFinished, Is.False );
        Assert.That( source.Current, Is.EqualTo( Expected.Option( null ) ) );
    }


    // Helpers/Take
    private static void Take(PeekableEnumerator<int> source, bool expected_isStarted, bool expected_isFinished, Option<int> expected_current) {
        var current = source.Take();
        Assert.That( source.IsStarted, Is.EqualTo( expected_isStarted ) );
        Assert.That( source.IsFinished, Is.EqualTo( expected_isFinished ) );
        Assert.That( source.Current, Is.EqualTo( expected_current ) );
        Assert.That( source.Current, Is.EqualTo( current ) );
    }
    private static void Peek(PeekableEnumerator<int> source, bool expected_isStarted, bool expected_isFinished, Option<int> expected_current, Option<int> expected_next) {
        var next = source.Peek();
        Assert.That( source.IsStarted, Is.EqualTo( expected_isStarted ) );
        Assert.That( source.IsFinished, Is.EqualTo( expected_isFinished ) );
        Assert.That( source.Current, Is.EqualTo( expected_current ) );
        Assert.That( source.Next, Is.EqualTo( expected_next ) );
        Assert.That( source.Next, Is.EqualTo( next ) );
    }


}
[TestFixture( TestName = "Tests_Enumerator/Peekable" )]
public class Tests_PeekableEnumeratorExtensions {


    // Take/While
    [Test]
    public void TakeWhile() {
        using var source = Source.Peekable( 0, 1, 2 );
        var predicate = Source.Predicate( i => i <= 1 );
        Assert.That( source.TakeWhile( predicate ), Is.EqualTo( Expected.Array1D( 0, 1 ) ) );
        Assert.That( source.Current, Is.EqualTo( 1 ) );
    }
    // Take/Until
    [Test]
    public void TakeUntil() {
        using var source = Source.Peekable( 0, 1, 2 );
        var predicate = Source.Predicate( i => !(i <= 1) );
        Assert.That( source.TakeUntil( predicate ), Is.EqualTo( Expected.Array1D( 0, 1 ) ) );
        Assert.That( source.Current, Is.EqualTo( 1 ) );
    }


    // Take
    [Test]
    public void TakeIf() {
        using var source = Source.Peekable( 0, 1, 2 );
        Assert.That( source.TakeIf( i => true ), Is.EqualTo( Expected.Option( 0 ) ) );
        Assert.That( source.TakeIf( i => false ), Is.EqualTo( Expected.Option( null ) ) );
        Assert.That( source.TakeIf( i => true ), Is.EqualTo( Expected.Option( 1 ) ) );
        Assert.That( source.TakeIf( i => true ), Is.EqualTo( Expected.Option( 2 ) ) );
        Assert.That( source.TakeIf( i => true ), Is.EqualTo( Expected.Option( null ) ) );
    }
    [Test]
    public void TakeIfNot() {
        using var source = Source.Peekable( 0, 1, 2 );
        Assert.That( source.TakeIfNot( i => !true ), Is.EqualTo( Expected.Option( 0 ) ) );
        Assert.That( source.TakeIfNot( i => !false ), Is.EqualTo( Expected.Option( null ) ) );
        Assert.That( source.TakeIfNot( i => !true ), Is.EqualTo( Expected.Option( 1 ) ) );
        Assert.That( source.TakeIfNot( i => !true ), Is.EqualTo( Expected.Option( 2 ) ) );
        Assert.That( source.TakeIfNot( i => !true ), Is.EqualTo( Expected.Option( null ) ) );
    }


}