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
        Take( Source_Empty, true, true, Default );
    }
    [Test]
    public void Take_01() {
        Take( Source, true, false, 0 );
        Take( Source, true, false, 1 );
        Take( Source, true, false, 2 );
        Take( Source, true, true, Default );
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
        ((IEnumerator<int>) Source).MoveNext();

        Source.Reset();
        Assert.That( Source.IsStarted, Is.False );
        Assert.That( Source.IsFinished, Is.False );
        Assert.That( Source.Current, Is.EqualTo( Default ) );
    }


}