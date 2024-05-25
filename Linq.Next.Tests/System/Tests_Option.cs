// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace System;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

public class Tests_Option {


    // Constructor
    [Test]
    public void Constructor() {
        // Empty
        var source = new Option<object?>();
        Assert.That( source.HasValue, Is.False );
        Assert.Throws<InvalidOperationException>( () => _ = source.Value );
        Assert.That( source.ValueOrDefault, Is.EqualTo( null ) );
        // Value/Null
        source = new Option<object?>( null );
        Assert.That( source.HasValue, Is.True );
        Assert.That( source.Value, Is.EqualTo( null ) );
        Assert.That( source.ValueOrDefault, Is.EqualTo( null ) );
        // Value/Object
        source = new Option<object?>( "Hello World !!!" );
        Assert.That( source.HasValue, Is.True );
        Assert.That( source.Value, Is.EqualTo( "Hello World !!!" ) );
        Assert.That( source.ValueOrDefault, Is.EqualTo( "Hello World !!!" ) );
    }


}
