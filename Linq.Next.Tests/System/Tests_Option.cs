namespace System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using static NUnit.Framework.TestsHelper;

public class Tests_Option {


    // Constructor
    [Test]
    public void Constructor() {
        _ = new Option<object?>( new object() );
    }


    // Value
    [Test]
    public void Value() {
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