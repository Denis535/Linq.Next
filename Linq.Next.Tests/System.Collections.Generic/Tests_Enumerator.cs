﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace System.Collections.Generic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

[TestFixture( TestName = "Tests_Enumerator" )]
public class Tests_EnumeratorExtensions {


    // Take
    [Test]
    public void Take() {
        using var source = Helper.Enumerator<int>( 0, 1, 2 );
        Assert.That( source.Take(), Is.EqualTo( Helper.Option<int>( 0 ) ) );
        Assert.That( source.Take(), Is.EqualTo( Helper.Option<int>( 1 ) ) );
        Assert.That( source.Take(), Is.EqualTo( Helper.Option<int>( 2 ) ) );
        Assert.That( source.Take(), Is.EqualTo( Helper.Option<int>( null ) ) );
    }


}
