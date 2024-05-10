// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace System.Collections.Generic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

[TestFixture( TestName = "Tests_Enumerator/Peekable" )]
public class Tests_PeekableEnumerator {


    // Constructor
    [Test]
    public void Constructor() {
        using var source = Helper.Peekable<int>( 0, 1, 2 );
        Assert.That( source.IsStarted, Is.False );
        Assert.That( source.IsFinished, Is.False );
        Assert.That( source.Current, Is.EqualTo( Helper.Option<int>( null ) ) );
    }


    // Take
    [Test]
    public void Take_00() {
        using var source = Helper.Peekable<int>();
        // Peek
        Peek( source, false, false, Helper.Option<int>( null ), Helper.Option<int>( null ) );
        // Take-Peek
        Take( source, true, true, Helper.Option<int>( null ) );
        Peek( source, true, true, Helper.Option<int>( null ), Helper.Option<int>( null ) );
    }
    [Test]
    public void Take_01() {
        using var source = Helper.Peekable<int>( 0, 1, 2 );
        // Peek
        Peek( source, false, false, Helper.Option<int>( null ), Helper.Option<int>( 0 ) );
        // Take-Peek
        Take( source, true, false, Helper.Option<int>( 0 ) );
        Peek( source, true, false, Helper.Option<int>( 0 ), Helper.Option<int>( 1 ) );
        // Take-Peek
        Take( source, true, false, Helper.Option<int>( 1 ) );
        Peek( source, true, false, Helper.Option<int>( 1 ), Helper.Option<int>( 2 ) );
        // Take-Peek
        Take( source, true, false, Helper.Option<int>( 2 ) );
        Peek( source, true, false, Helper.Option<int>( 2 ), Helper.Option<int>( null ) );
        // Take-Peek
        Take( source, true, true, Helper.Option<int>( null ) );
        Peek( source, true, true, Helper.Option<int>( null ), Helper.Option<int>( null ) );
    }


    // Reset
    [Test]
    public void Reset() {
        using var source = Helper.Peekable<int>( 0, 1, 2 );
        ((IEnumerator<int>) source).MoveNext();

        source.Reset();
        Assert.That( source.IsStarted, Is.False );
        Assert.That( source.IsFinished, Is.False );
        Assert.That( source.Current, Is.EqualTo( Helper.Option<int>( null ) ) );
    }


    // Helpers/Take
    private static void Take(PeekableEnumerator<int> source, bool expected_isStarted, bool expected_isFinished, Option<int> expected_current) {
        var current = source.Take();
        Assert.That( source.IsStarted, Is.EqualTo( expected_isStarted ) );
        Assert.That( source.IsFinished, Is.EqualTo( expected_isFinished ) );
        Assert.That( source.Current, Is.EqualTo( expected_current ) );
        Assert.That( source.Current, Is.EqualTo( current ) );
    }
    // Helpers/Peek
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
        using var source = Helper.Peekable<int>( 0, 1, 2 );
        Assert.That( source.TakeWhile( i => i <= 1 ), Is.EqualTo( Helper.Values<int>( 0, 1 ) ) );
        Assert.That( source.Current, Is.EqualTo( 1 ) );
    }
    // Take/Until
    [Test]
    public void TakeUntil() {
        using var source = Helper.Peekable<int>( 0, 1, 2 );
        Assert.That( source.TakeUntil( i => !(i <= 1) ), Is.EqualTo( Helper.Values<int>( 0, 1 ) ) );
        Assert.That( source.Current, Is.EqualTo( 1 ) );
    }


    // Take/If
    [Test]
    public void TakeIf() {
        using var source = Helper.Peekable<int>( 0, 1, 2 );
        Assert.That( source.TakeIf( i => true ), Is.EqualTo( Helper.Option<int>( 0 ) ) );
        Assert.That( source.TakeIf( i => false ), Is.EqualTo( Helper.Option<int>( null ) ) );
        Assert.That( source.TakeIf( i => true ), Is.EqualTo( Helper.Option<int>( 1 ) ) );
        Assert.That( source.TakeIf( i => true ), Is.EqualTo( Helper.Option<int>( 2 ) ) );
        Assert.That( source.TakeIf( i => true ), Is.EqualTo( Helper.Option<int>( null ) ) );
    }
    // Take/If/Not
    [Test]
    public void TakeIfNot() {
        using var source = Helper.Peekable<int>( 0, 1, 2 );
        Assert.That( source.TakeIfNot( i => !true ), Is.EqualTo( Helper.Option<int>( 0 ) ) );
        Assert.That( source.TakeIfNot( i => !false ), Is.EqualTo( Helper.Option<int>( null ) ) );
        Assert.That( source.TakeIfNot( i => !true ), Is.EqualTo( Helper.Option<int>( 1 ) ) );
        Assert.That( source.TakeIfNot( i => !true ), Is.EqualTo( Helper.Option<int>( 2 ) ) );
        Assert.That( source.TakeIfNot( i => !true ), Is.EqualTo( Helper.Option<int>( null ) ) );
    }


}
