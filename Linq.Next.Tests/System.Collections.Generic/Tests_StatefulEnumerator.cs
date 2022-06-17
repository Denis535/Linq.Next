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

[TestFixture( TestName = "Tests_Enumerator/Stateful" )]
public class Tests_StatefulEnumerator {


    // Constructor
    [Test]
    public void Constructor() {
        using var source = SourceFactory.Enumerator( 0, 1, 2 ).AsStateful();
        Assert.That( source.IsStarted, Is.False );
        Assert.That( source.IsFinished, Is.False );
        Assert.That( source.Current, Is.EqualTo( Default ) );
    }


    // Take
    [Test]
    public void Take_00() {
        using var source = SourceFactory.Enumerator( 0, 1, 2 ).AsStateful();
        Take( source, true, true, Default );
    }
    [Test]
    public void Take_01() {
        using var source = SourceFactory.Enumerator( 0, 1, 2 ).AsStateful();
        Take( source, true, false, 0 );
        Take( source, true, false, 1 );
        Take( source, true, false, 2 );
        Take( source, true, true, Default );
    }
    private static void Take(StatefulEnumerator<int> source, bool expected_isStarted, bool expected_isFinished, Option<int> expected_current) {
        var current = source.Take();
        Assert.That( source.IsStarted, Is.EqualTo( expected_isStarted ) );
        Assert.That( source.IsFinished, Is.EqualTo( expected_isFinished ) );
        Assert.That( source.Current, Is.EqualTo( expected_current ) );
        Assert.That( source.Current, Is.EqualTo( current ) );
    }


    // Reset
    [Test]
    public void Reset() {
        using var source = SourceFactory.Enumerator( 0, 1, 2 ).AsStateful();
        ((IEnumerator<int>) source).MoveNext();

        source.Reset();
        Assert.That( source.IsStarted, Is.False );
        Assert.That( source.IsFinished, Is.False );
        Assert.That( source.Current, Is.EqualTo( Default ) );
    }


}