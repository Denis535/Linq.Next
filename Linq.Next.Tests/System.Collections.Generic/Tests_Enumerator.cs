namespace System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using static NUnit.Framework.TestsHelper;

// Enumerator/Extensions
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