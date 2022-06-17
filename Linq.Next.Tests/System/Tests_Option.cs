// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using static NUnit.Framework.TestsHelper;

public class Tests_Option {


    // Tests/Constructor
    [Test]
    public void Constructor() {
        {
            var source = new Option<object?>();
            Assert.That( source.HasValue, Is.False );
            Assert.Throws<InvalidOperationException>( () => _ = source.Value );
            Assert.That( source.ValueOrDefault, Is.EqualTo( null ) );
        }
        {
            var source = new Option<object?>( "Hello World !!!" );
            Assert.That( source.HasValue, Is.True );
            Assert.That( source.Value, Is.EqualTo( "Hello World !!!" ) );
            Assert.That( source.ValueOrDefault, Is.EqualTo( "Hello World !!!" ) );
        }
        {
            var source = new Option<object?>( null );
            Assert.That( source.HasValue, Is.True );
            Assert.That( source.Value, Is.EqualTo( null ) );
            Assert.That( source.ValueOrDefault, Is.EqualTo( null ) );
        }
    }


}