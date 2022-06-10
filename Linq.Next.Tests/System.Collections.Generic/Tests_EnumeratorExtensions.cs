namespace System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

public class Tests_EnumeratorExtensions {

    private static readonly Option<int> Default = default;


    // Take
    [Test]
    public void Take() {
        var source = Enumerator( 0, 1, 2 );
        Assert.That( source.Take(), Is.EqualTo( 0 ) );
        Assert.That( source.Take(), Is.EqualTo( 1 ) );
        Assert.That( source.Take(), Is.EqualTo( 2 ) );
        Assert.That( source.Take(), Is.EqualTo( Default ) );
    }


    // Helpers
    private static IEnumerator<int> Enumerator(params int[] array) {
        return array.AsEnumerable().GetEnumerator();
    }

}
