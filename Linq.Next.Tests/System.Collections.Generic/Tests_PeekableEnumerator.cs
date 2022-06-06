namespace System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

public class Tests_PeekableEnumerator {


    [Test]
    public void Test_00_Constructor() {
        using var enumerator = new PeekableEnumerator<int>( Enumerable.Empty<int>().GetEnumerator() );
        Assert.That( enumerator.IsStarted, Is.False );
        Assert.That( enumerator.IsFinished, Is.False );
        Assert.That( enumerator.Current, Is.EqualTo( Option<int>.Default ) );
    }


    // Take
    [TestCase]
    public void Test_01_Take() {
        using var enumerator = new PeekableEnumerator<int>( Enumerable.Empty<int>().GetEnumerator() );
        // Take/Finish
        Assert.That( enumerator.Take(), Is.EqualTo( Option<int>.Default ) );
        Assert.That( enumerator.IsStarted, Is.True );
        Assert.That( enumerator.IsFinished, Is.True );
        Assert.That( enumerator.Current, Is.EqualTo( Option<int>.Default ) );
    }
    [TestCase( 1, 2 )]
    public void Test_01_Take(params int[] array) {
        using var enumerator = new PeekableEnumerator<int>( array.AsEnumerable().GetEnumerator() );
        // Take/First
        Assert.That( enumerator.Take(), Is.EqualTo( Option<int>.Create( 1 ) ) );
        Assert.That( enumerator.IsStarted, Is.True );
        Assert.That( enumerator.IsFinished, Is.False );
        Assert.That( enumerator.Current, Is.EqualTo( Option<int>.Create( 1 ) ) );

        // Take/Second
        Assert.That( enumerator.Take(), Is.EqualTo( Option<int>.Create( 2 ) ) );
        Assert.That( enumerator.IsStarted, Is.True );
        Assert.That( enumerator.IsFinished, Is.False );
        Assert.That( enumerator.Current, Is.EqualTo( Option<int>.Create( 2 ) ) );

        // Take/Finish
        Assert.That( enumerator.Take(), Is.EqualTo( Option<int>.Default ) );
        Assert.That( enumerator.IsStarted, Is.True );
        Assert.That( enumerator.IsFinished, Is.True );
        Assert.That( enumerator.Current, Is.EqualTo( Option<int>.Default ) );
    }


    // Peek
    [TestCase]
    public void Test_02_Peek() {
        using var enumerator = new PeekableEnumerator<int>( Enumerable.Empty<int>().GetEnumerator() );
        // Next/Finish
        Assert.That( enumerator.Peek(), Is.EqualTo( Option<int>.Default ) );
        Assert.That( enumerator.IsStarted, Is.False );
        Assert.That( enumerator.IsFinished, Is.False );
        Assert.That( enumerator.Current, Is.EqualTo( Option<int>.Default ) );
        // Take/Finish
        Assert.That( enumerator.Take(), Is.EqualTo( Option<int>.Default ) );
        Assert.That( enumerator.IsStarted, Is.True );
        Assert.That( enumerator.IsFinished, Is.True );
        Assert.That( enumerator.Current, Is.EqualTo( Option<int>.Default ) );
    }
    [TestCase( 1, 2 )]
    public void Test_02_Peek(params int[] array) {
        using var enumerator = new PeekableEnumerator<int>( array.AsEnumerable().GetEnumerator() );
        // Next/First
        Assert.That( enumerator.Peek(), Is.EqualTo( Option<int>.Create( 1 ) ) );
        Assert.That( enumerator.IsStarted, Is.False );
        Assert.That( enumerator.IsFinished, Is.False );
        Assert.That( enumerator.Current, Is.EqualTo( Option<int>.Default ) );
        // Take/First
        Assert.That( enumerator.Take(), Is.EqualTo( Option<int>.Create( 1 ) ) );
        Assert.That( enumerator.IsStarted, Is.True );
        Assert.That( enumerator.IsFinished, Is.False );
        Assert.That( enumerator.Current, Is.EqualTo( Option<int>.Create( 1 ) ) );

        // Next/Second
        Assert.That( enumerator.Peek(), Is.EqualTo( Option<int>.Create( 2 ) ) );
        Assert.That( enumerator.IsStarted, Is.True );
        Assert.That( enumerator.IsFinished, Is.False );
        Assert.That( enumerator.Current, Is.EqualTo( Option<int>.Create( 1 ) ) );
        // Take/Second
        Assert.That( enumerator.Take(), Is.EqualTo( Option<int>.Create( 2 ) ) );
        Assert.That( enumerator.IsStarted, Is.True );
        Assert.That( enumerator.IsFinished, Is.False );
        Assert.That( enumerator.Current, Is.EqualTo( Option<int>.Create( 2 ) ) );

        // Next/Finish
        Assert.That( enumerator.Peek(), Is.EqualTo( Option<int>.Default ) );
        Assert.That( enumerator.IsStarted, Is.True );
        Assert.That( enumerator.IsFinished, Is.False );
        Assert.That( enumerator.Current, Is.EqualTo( Option<int>.Create( 2 ) ) );
        // Take/Finish
        Assert.That( enumerator.Take(), Is.EqualTo( Option<int>.Default ) );
        Assert.That( enumerator.IsStarted, Is.True );
        Assert.That( enumerator.IsFinished, Is.True );
        Assert.That( enumerator.Current, Is.EqualTo( Option<int>.Default ) );
    }


    [Test]
    public void Test_03_Reset() {
        using var enumerator = new PeekableEnumerator<int>( Enumerable.Empty<int>().GetEnumerator() );
        _ = enumerator.Take();

        // Reset
        enumerator.Reset();
        Assert.That( enumerator.IsStarted, Is.False );
        Assert.That( enumerator.IsFinished, Is.False );
        Assert.That( enumerator.Current, Is.EqualTo( Option<int>.Default ) );
    }


}