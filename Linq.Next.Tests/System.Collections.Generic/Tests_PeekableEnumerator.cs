// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using static NUnit.Framework.TestsHelper;

[TestFixture( TestName = "Tests_Enumerator/Peekable" )]
public class Tests_PeekableEnumerator {


    // Tests/Constructor
    [Test]
    public void Constructor() {
        using var source = SourceFactory.Enumerator( 0, 1, 2 ).AsPeekable();
        Assert.That( source.IsStarted, Is.False );
        Assert.That( source.IsFinished, Is.False );
        Assert.That( source.Current, Is.EqualTo( Default ) );
    }


    // Tests/Take
    [Test]
    public void Take_00() {
        using var source = SourceFactory.Enumerator().AsPeekable();
        // Peek
        Peek( source, false, false, Default, Default );
        // Take-Peek
        Take( source, true, true, Default );
        Peek( source, true, true, Default, Default );
    }
    [Test]
    public void Take_01() {
        using var source = SourceFactory.Enumerator( 0, 1, 2 ).AsPeekable();
        // Peek
        Peek( source, false, false, Default, 0 );
        // Take-Peek
        Take( source, true, false, 0 );
        Peek( source, true, false, 0, 1 );
        // Take-Peek
        Take( source, true, false, 1 );
        Peek( source, true, false, 1, 2 );
        // Take-Peek
        Take( source, true, false, 2 );
        Peek( source, true, false, 2, Default );
        // Take-Peek
        Take( source, true, true, Default );
        Peek( source, true, true, Default, Default );
    }
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


    // Tests/Reset
    [Test]
    public void Reset() {
        using var source = SourceFactory.Enumerator( 0, 1, 2 ).AsPeekable();
        ((IEnumerator<int>) source).MoveNext();

        source.Reset();
        Assert.That( source.IsStarted, Is.False );
        Assert.That( source.IsFinished, Is.False );
        Assert.That( source.Current, Is.EqualTo( Default ) );
    }


}

[TestFixture( TestName = "Tests_Enumerator/Peekable" )]
public class Tests_PeekableEnumeratorExtensions {


    // Tests/Take/While
    [Test]
    public void TakeWhile() {
        using var source = SourceFactory.Enumerator( 0, 1, 2 ).AsPeekable();
        Assert.That( source.TakeWhile( WhilePredicate ), Is.EqualTo( ExpectedFactory.Array( 0, 1 ) ) );
        Assert.That( source.Current, Is.EqualTo( 1 ) );
    }
    // Tests/Take/Until
    [Test]
    public void TakeUntil() {
        using var source = SourceFactory.Enumerator( 0, 1, 2 ).AsPeekable();
        Assert.That( source.TakeUntil( UntilPredicate ), Is.EqualTo( ExpectedFactory.Array( 0, 1 ) ) );
        Assert.That( source.Current, Is.EqualTo( 1 ) );
    }


    // Tests/Take
    [Test]
    public void TakeIf() {
        using var source = SourceFactory.Enumerator( 0, 1, 2 ).AsPeekable();
        Assert.That( source.TakeIf( i => true ), Is.EqualTo( 0 ) );
        Assert.That( source.TakeIf( i => false ), Is.EqualTo( Default ) );
        Assert.That( source.TakeIf( i => true ), Is.EqualTo( 1 ) );
        Assert.That( source.TakeIf( i => true ), Is.EqualTo( 2 ) );
        Assert.That( source.TakeIf( i => true ), Is.EqualTo( Default ) );
    }
    [Test]
    public void TakeIfNot() {
        using var source = SourceFactory.Enumerator( 0, 1, 2 ).AsPeekable();
        Assert.That( source.TakeIfNot( i => false ), Is.EqualTo( 0 ) );
        Assert.That( source.TakeIfNot( i => true ), Is.EqualTo( Default ) );
        Assert.That( source.TakeIfNot( i => false ), Is.EqualTo( 1 ) );
        Assert.That( source.TakeIfNot( i => false ), Is.EqualTo( 2 ) );
        Assert.That( source.TakeIfNot( i => false ), Is.EqualTo( Default ) );
    }


    // Helpers
    private static bool WhilePredicate(int value) => value <= 1;
    private static bool UntilPredicate(int value) => !(value <= 1);

}