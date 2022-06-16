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

    private PeekableEnumerator<int> Source { get; set; } = default!;
    private PeekableEnumerator<int> Source_Empty { get; set; } = default!;


    [SetUp]
    public void SetUp() {
        Source = SourceFactory.Enumerator( 0, 1, 2 ).AsPeekable();
        Source_Empty = SourceFactory.Enumerator().AsPeekable();
    }
    [TearDown]
    public void TearDown() {
        Source.Dispose();
        Source_Empty.Dispose();
    }


    // Constructor
    [Test]
    public void Constructor() {
        Assert.That( Source.IsStarted, Is.False );
        Assert.That( Source.IsFinished, Is.False );
        Assert.That( Source.Current, Is.EqualTo( Default ) );
    }


    // Take
    [Test]
    public void Take_00() {
        // Peek
        Peek( Source_Empty, false, false, Default, Default );
        // Take-Peek
        Take( Source_Empty, true, true, Default );
        Peek( Source_Empty, true, true, Default, Default );
    }
    [Test]
    public void Take_01() {
        // Peek
        Peek( Source, false, false, Default, 0 );
        // Take-Peek
        Take( Source, true, false, 0 );
        Peek( Source, true, false, 0, 1 );
        // Take-Peek
        Take( Source, true, false, 1 );
        Peek( Source, true, false, 1, 2 );
        // Take-Peek
        Take( Source, true, false, 2 );
        Peek( Source, true, false, 2, Default );
        // Take-Peek
        Take( Source, true, true, Default );
        Peek( Source, true, true, Default, Default );
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


    // Reset
    [Test]
    public void Reset() {
        ((IEnumerator<int>) Source).MoveNext();

        Source.Reset();
        Assert.That( Source.IsStarted, Is.False );
        Assert.That( Source.IsFinished, Is.False );
        Assert.That( Source.Current, Is.EqualTo( Default ) );
    }


}

[TestFixture( TestName = "Tests_Enumerator/Peekable" )]
public class Tests_PeekableEnumeratorExtensions {

    private PeekableEnumerator<int> Source { get; set; } = default!;
    private PeekableEnumerator<int> Source_Empty { get; set; } = default!;


    [SetUp]
    public void SetUp() {
        Source = SourceFactory.Enumerator( 0, 1, 2 ).AsPeekable();
        Source_Empty = SourceFactory.Enumerator().AsPeekable();
    }
    [TearDown]
    public void TearDown() {
        Source.Dispose();
        Source_Empty.Dispose();
    }


    // Take/While
    [Test]
    public void TakeWhile() {
        Assert.That( Source.TakeWhile( Predicate ), Is.EquivalentTo( ExpectedFactory.Array( 0, 1 ) ) );
        Assert.That( Source.Current, Is.EqualTo( 1 ) );
    }
    // Take/Until
    [Test]
    public void TakeUntil() {
        Assert.That( Source.TakeUntil( PredicateInverted ), Is.EquivalentTo( ExpectedFactory.Array( 0, 1 ) ) );
        Assert.That( Source.Current, Is.EqualTo( 1 ) );
    }


    // Take
    [Test]
    public void TakeIf() {
        Assert.That( Source.TakeIf( i => true ), Is.EqualTo( 0 ) );
        Assert.That( Source.TakeIf( i => false ), Is.EqualTo( Default ) );
        Assert.That( Source.TakeIf( i => true ), Is.EqualTo( 1 ) );
        Assert.That( Source.TakeIf( i => true ), Is.EqualTo( 2 ) );
        Assert.That( Source.TakeIf( i => true ), Is.EqualTo( Default ) );
    }
    [Test]
    public void TakeIfNot() {
        Assert.That( Source.TakeIfNot( i => false ), Is.EqualTo( 0 ) );
        Assert.That( Source.TakeIfNot( i => true ), Is.EqualTo( Default ) );
        Assert.That( Source.TakeIfNot( i => false ), Is.EqualTo( 1 ) );
        Assert.That( Source.TakeIfNot( i => false ), Is.EqualTo( 2 ) );
        Assert.That( Source.TakeIfNot( i => false ), Is.EqualTo( Default ) );
    }


    // Helpers
    private static bool Predicate(int value) => value <= 1;
    private static bool PredicateInverted(int value) => !(value <= 1);

}