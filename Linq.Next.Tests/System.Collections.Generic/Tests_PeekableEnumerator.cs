namespace System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using static NUnit.Framework.Utils;

public class Tests_PeekableEnumerator {


    // Constructor
    [Test]
    public void Constructor() {
        using var source = PeekableEnumerator( 0, 1, 2 );
        Assert.That( source.IsStarted, Is.False );
        Assert.That( source.IsFinished, Is.False );
        Assert.That( source.Current, Is.EqualTo( Default ) );
    }


    // Take
    [Test]
    public void Take_00() {
        using var source = PeekableEnumerator();
        // Peek
        Peek( source, false, false, Default, Default );
        // Take-Peek
        Take( source, true, true, Default );
        Peek( source, true, true, Default, Default );
    }
    [Test]
    public void Take_01() {
        using var source = PeekableEnumerator( 0, 1, 2 );
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
        source.Take();
        Assert.That( source.IsStarted, Is.EqualTo( expected_isStarted ) );
        Assert.That( source.IsFinished, Is.EqualTo( expected_isFinished ) );
        Assert.That( source.Current, Is.EqualTo( expected_current ) );
    }
    private static void Peek(PeekableEnumerator<int> source, bool expected_isStarted, bool expected_isFinished, Option<int> expected_current, Option<int> expected_next) {
        source.Peek();
        Assert.That( source.IsStarted, Is.EqualTo( expected_isStarted ) );
        Assert.That( source.IsFinished, Is.EqualTo( expected_isFinished ) );
        Assert.That( source.Current, Is.EqualTo( expected_current ) );
        Assert.That( source.Next, Is.EqualTo( expected_next ) );
    }


    // Reset
    [Test]
    public void Reset() {
        using var source = PeekableEnumerator( 0, 1, 2 );
        source.Take();

        source.Reset();
        Assert.That( source.IsStarted, Is.False );
        Assert.That( source.IsFinished, Is.False );
        Assert.That( source.Current, Is.EqualTo( Default ) );
    }


}