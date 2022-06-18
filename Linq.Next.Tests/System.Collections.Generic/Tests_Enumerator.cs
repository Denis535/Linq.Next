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

[TestFixture( TestName = "Tests_Enumerator" )]
public class Tests_EnumeratorExtensions {


    // Take
    [Test]
    public void Take() {
        using var source = Source.Enumerator( 0, 1, 2 );
        Assert.That( source.Take(), Is.EqualTo( 0 ) );
        Assert.That( source.Take(), Is.EqualTo( 1 ) );
        Assert.That( source.Take(), Is.EqualTo( 2 ) );
        Assert.That( source.Take(), Is.EqualTo( Default ) );
    }


}