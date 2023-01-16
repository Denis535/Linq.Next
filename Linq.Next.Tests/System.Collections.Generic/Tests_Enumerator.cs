// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

[TestFixture( TestName = "Tests_Enumerator" )]
public class Tests_EnumeratorExtensions {


    // Take
    [Test]
    public void Take() {
        using var source = Source.Enumerator( 0, 1, 2 );
        Assert.That( source.Take(), Is.EqualTo( Expected.Option( 0 ) ) );
        Assert.That( source.Take(), Is.EqualTo( Expected.Option( 1 ) ) );
        Assert.That( source.Take(), Is.EqualTo( Expected.Option( 2 ) ) );
        Assert.That( source.Take(), Is.EqualTo( Expected.Option( null ) ) );
    }


}