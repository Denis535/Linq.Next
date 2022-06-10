namespace System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

public class Tests_PeekableEnumeratorExtensions {

    private static readonly Option<int> Default = default;


    // Take/While
    [Test]
    public void TakeWhile() {
        var source = Enumerator( 0, 1, 2 );
        var actual = source.TakeWhile( i => i <= 1 ).ToArray();
        var expected = Array( 0, 1 );
        Assert.That( actual, Is.EquivalentTo( expected ) );
    }


    // Take/Until
    [Test]
    public void TakeUntil() {
        var source = Enumerator( 0, 1, 2 );
        var actual = source.TakeUntil( i => !(i <= 1) ).ToArray();
        var expected = Array( 0, 1 );
        Assert.That( actual, Is.EquivalentTo( expected ) );
    }


    // Take/If
    [Test]
    public void TakeIf() {
        var source = Enumerator( 0, 1, 2 );
        Assert.That( source.TakeIf( i => false ), Is.EqualTo( Default ) );
        Assert.That( source.TakeIf( i => true ), Is.EqualTo( 0 ) );
        Assert.That( source.TakeIf( i => true ), Is.EqualTo( 1 ) );
        Assert.That( source.TakeIf( i => true ), Is.EqualTo( 2 ) );
        Assert.That( source.TakeIf( i => true ), Is.EqualTo( Default ) );
    }


    // Take/If/Not
    [Test]
    public void TakeIfNot() {
        var source = Enumerator( 0, 1, 2 );
        Assert.That( source.TakeIfNot( i => true ), Is.EqualTo( Default ) );
        Assert.That( source.TakeIfNot( i => false ), Is.EqualTo( 0 ) );
        Assert.That( source.TakeIfNot( i => false ), Is.EqualTo( 1 ) );
        Assert.That( source.TakeIfNot( i => false ), Is.EqualTo( 2 ) );
        Assert.That( source.TakeIfNot( i => false ), Is.EqualTo( Default ) );
    }


    // Helpers
    private static PeekableEnumerator<int> Enumerator(params int[] array) {
        return new PeekableEnumerator<int>( array.AsEnumerable().GetEnumerator() );
    }
    private static int[] Array(params int[] array) {
        return array;
    }

}
