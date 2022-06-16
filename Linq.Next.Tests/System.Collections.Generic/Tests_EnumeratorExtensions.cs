namespace System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using static NUnit.Framework.TestsHelper;

// Enumerator
[TestFixture( TestName = "Tests_Enumerator/Extensions" )]
public class Tests_EnumeratorExtensions {

    private IEnumerator<int> Source { get; set; } = default!;
    private IEnumerator<int> Source_Empty { get; set; } = default!;


    [SetUp]
    public void SetUp() {
        Source = SourceFactory.Enumerator( 0, 1, 2 );
        Source_Empty = SourceFactory.Enumerator();
    }
    [TearDown]
    public void TearDown() {
        Source.Dispose();
        Source_Empty.Dispose();
    }


    [Test]
    public void Take() {
        Assert.That( Source.Take(), Is.EqualTo( 0 ) );
        Assert.That( Source.Take(), Is.EqualTo( 1 ) );
        Assert.That( Source.Take(), Is.EqualTo( 2 ) );
        Assert.That( Source.Take(), Is.EqualTo( Default ) );
    }


}

// Enumerator/Stateful
[TestFixture( TestName = "Tests_Enumerator/Stateful/Extensions" )]
public class Tests_StatefulEnumeratorExtensions {

    private StatefulEnumerator<int> Source { get; set; } = default!;
    private StatefulEnumerator<int> Source_Empty { get; set; } = default!;


    [SetUp]
    public void SetUp() {
        Source = SourceFactory.Enumerator( 0, 1, 2 ).AsStateful();
        Source_Empty = SourceFactory.Enumerator().AsStateful();
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
        Assert.That( Source.Current, Is.EqualTo( 2 ) );
    }
    // Take/Until
    [Test]
    public void TakeUntil() {
        Assert.That( Source.TakeUntil( PredicateInverted ), Is.EquivalentTo( ExpectedFactory.Array( 0, 1 ) ) );
        Assert.That( Source.Current, Is.EqualTo( 2 ) );
    }


    // Take
    [Test]
    public void TakeIf() {
        Assert.That( Source.TakeIf( i => true ), Is.EqualTo( 0 ) );
        Assert.That( Source.TakeIf( i => false ), Is.EqualTo( Default ) );
        Assert.That( Source.TakeIf( i => true ), Is.EqualTo( 2 ) );
        Assert.That( Source.TakeIf( i => true ), Is.EqualTo( Default ) );
    }
    [Test]
    public void TakeIfNot() {
        Assert.That( Source.TakeIfNot( i => !true ), Is.EqualTo( 0 ) );
        Assert.That( Source.TakeIfNot( i => !false ), Is.EqualTo( Default ) );
        Assert.That( Source.TakeIfNot( i => !true ), Is.EqualTo( 2 ) );
        Assert.That( Source.TakeIfNot( i => !true ), Is.EqualTo( Default ) );
    }


    // Helpers
    private static bool Predicate(int value) => value <= 1;
    private static bool PredicateInverted(int value) => !(value <= 1);

}

// Enumerator/Peekable
[TestFixture( TestName = "Tests_Enumerator/Peekable/Extensions" )]
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