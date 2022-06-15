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


    // Take/While
    [Test]
    public void TakeWhile() {
        var source = Enumerator( 0, 1, 2, 3 );
        Assert.That( source.TakeWhile( WhilePredicate ), Is.EquivalentTo( Array( 0, 1, 2 ) ) );
        Assert.That( source.Current, Is.EqualTo( 3 ) );
    }
    // Take/Until
    [Test]
    public void TakeUntil() {
        var source = Enumerator( 0, 1, 2, 3 );
        Assert.That( source.TakeUntil( UntilPredicate ), Is.EquivalentTo( Array( 0, 1, 2 ) ) );
        Assert.That( source.Current, Is.EqualTo( 3 ) );
    }


    // Take
    [Test]
    public void TakeIf() {
        var source = Enumerator( 0, 1, 2, 3 );
        Assert.That( source.TakeIf( i => false ), Is.EqualTo( Default ) ); // 0 is skipped
        Assert.That( source.TakeIf( i => true ), Is.EqualTo( 1 ) );        // 1
        Assert.That( source.TakeIf( i => false ), Is.EqualTo( Default ) ); // 2 is skipped
        Assert.That( source.TakeIf( i => true ), Is.EqualTo( 3 ) );        // 2
        Assert.That( source.TakeIf( i => true ), Is.EqualTo( Default ) );  // finish
    }
    [Test]
    public void TakeIfNot() {
        var source = Enumerator( 0, 1, 2, 3 );
        Assert.That( source.TakeIfNot( i => true ), Is.EqualTo( Default ) );  // 0 is skipped
        Assert.That( source.TakeIfNot( i => false ), Is.EqualTo( 1 ) );       // 1
        Assert.That( source.TakeIfNot( i => true ), Is.EqualTo( Default ) );  // 2 is skipped
        Assert.That( source.TakeIfNot( i => false ), Is.EqualTo( 3 ) );       // 2
        Assert.That( source.TakeIfNot( i => false ), Is.EqualTo( Default ) ); // finish
    }
    [Test]
    public void Take() {
        var source = Enumerator( 0, 1, 2, 3 );
        Assert.That( source.Take(), Is.EqualTo( 0 ) );
        Assert.That( source.Take(), Is.EqualTo( 1 ) );
        Assert.That( source.Take(), Is.EqualTo( 2 ) );
        Assert.That( source.Take(), Is.EqualTo( 3 ) );
        Assert.That( source.Take(), Is.EqualTo( Default ) );
    }


    // Helpers
    private static bool WhilePredicate(int value) => value <= 2;
    private static bool UntilPredicate(int value) => !(value <= 2);

}

// Enumerator/Stateful
[TestFixture( TestName = "Tests_Enumerator/Extensions/Stateful" )]
public class Tests_StatefulEnumeratorExtensions {


    // Take/While
    [Test]
    public void TakeWhile() {
        var source = Enumerator( 0, 1, 2, 3 ).AsStateful();
        Assert.That( source.TakeWhile( WhilePredicate ), Is.EquivalentTo( Array( 0, 1, 2 ) ) );
        Assert.That( source.Current, Is.EqualTo( 3 ) );
    }
    // Take/Until
    [Test]
    public void TakeUntil() {
        var source = Enumerator( 0, 1, 2, 3 ).AsStateful();
        Assert.That( source.TakeUntil( UntilPredicate ), Is.EquivalentTo( Array( 0, 1, 2 ) ) );
        Assert.That( source.Current, Is.EqualTo( 3 ) );
    }


    // Take
    [Test]
    public void TakeIf() {
        var source = Enumerator( 0, 1, 2, 3 ).AsStateful();
        Assert.That( source.TakeIf( i => false ), Is.EqualTo( Default ) ); // 0 is skipped
        Assert.That( source.TakeIf( i => true ), Is.EqualTo( 1 ) );        // 1
        Assert.That( source.TakeIf( i => false ), Is.EqualTo( Default ) ); // 2 is skipped
        Assert.That( source.TakeIf( i => true ), Is.EqualTo( 3 ) );        // 2
        Assert.That( source.TakeIf( i => true ), Is.EqualTo( Default ) );  // finish
    }
    [Test]
    public void TakeIfNot() {
        var source = Enumerator( 0, 1, 2, 3 ).AsStateful();
        Assert.That( source.TakeIfNot( i => true ), Is.EqualTo( Default ) );  // 0 is skipped
        Assert.That( source.TakeIfNot( i => false ), Is.EqualTo( 1 ) );       // 1
        Assert.That( source.TakeIfNot( i => true ), Is.EqualTo( Default ) );  // 2 is skipped
        Assert.That( source.TakeIfNot( i => false ), Is.EqualTo( 3 ) );       // 2
        Assert.That( source.TakeIfNot( i => false ), Is.EqualTo( Default ) ); // finish
    }
    [Test]
    public void Take() {
        var source = Enumerator( 0, 1, 2, 3 ).AsStateful();
        Assert.That( source.Take(), Is.EqualTo( 0 ) );
        Assert.That( source.Take(), Is.EqualTo( 1 ) );
        Assert.That( source.Take(), Is.EqualTo( 2 ) );
        Assert.That( source.Take(), Is.EqualTo( 3 ) );
        Assert.That( source.Take(), Is.EqualTo( Default ) );
    }


    // Helpers
    private static bool WhilePredicate(int value) => value <= 2;
    private static bool UntilPredicate(int value) => !(value <= 2);

}

// Enumerator/Peekable
[TestFixture( TestName = "Tests_Enumerator/Extensions/Peekable" )]
public class Tests_PeekableEnumeratorExtensions {


    // Take/While
    [Test]
    public void TakeWhile() {
        var source = Enumerator( 0, 1, 2, 3 ).AsPeekable();
        Assert.That( source.TakeWhile( WhilePredicate ), Is.EquivalentTo( Array( 0, 1, 2 ) ) );
        Assert.That( source.Current, Is.EqualTo( 2 ) );
    }
    // Take/Until
    [Test]
    public void TakeUntil() {
        var source = Enumerator( 0, 1, 2, 3 ).AsPeekable();
        Assert.That( source.TakeUntil( UntilPredicate ), Is.EquivalentTo( Array( 0, 1, 2 ) ) );
        Assert.That( source.Current, Is.EqualTo( 2 ) );
    }


    // Take
    [Test]
    public void TakeIf() {
        var source = Enumerator( 0, 1, 2, 3 ).AsPeekable();
        Assert.That( source.TakeIf( i => false ), Is.EqualTo( Default ) );
        Assert.That( source.TakeIf( i => true ), Is.EqualTo( 0 ) );
        Assert.That( source.TakeIf( i => true ), Is.EqualTo( 1 ) );
        Assert.That( source.TakeIf( i => true ), Is.EqualTo( 2 ) );
        Assert.That( source.TakeIf( i => true ), Is.EqualTo( 3 ) );
        Assert.That( source.TakeIf( i => true ), Is.EqualTo( Default ) );
    }
    [Test]
    public void TakeIfNot() {
        var source = Enumerator( 0, 1, 2, 3 ).AsPeekable();
        Assert.That( source.TakeIfNot( i => true ), Is.EqualTo( Default ) );
        Assert.That( source.TakeIfNot( i => false ), Is.EqualTo( 0 ) );
        Assert.That( source.TakeIfNot( i => false ), Is.EqualTo( 1 ) );
        Assert.That( source.TakeIfNot( i => false ), Is.EqualTo( 2 ) );
        Assert.That( source.TakeIfNot( i => false ), Is.EqualTo( 3 ) );
        Assert.That( source.TakeIfNot( i => false ), Is.EqualTo( Default ) );
    }


    // Helpers
    private static bool WhilePredicate(int value) => value <= 2;
    private static bool UntilPredicate(int value) => !(value <= 2);

}