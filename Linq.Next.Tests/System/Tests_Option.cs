namespace System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using static NUnit.Framework.Utils;

public class Tests_Option {


    // Constructor
    [Test]
    public void Constructor() {
        _ = new Option<object?>( new object() );
    }


    // Value
    [Test]
    public void Value() {
        Value( Option() );
        Value( Option( null ), null );
        Value( Option( this ), this );
        Value( Option( 777 ), 777 );
    }
    private static void Value<T>(Option<T> source) {
        Assert.That( source.HasValue, Is.False );
        Assert.Throws<InvalidOperationException>( () => _ = source.Value );
        Assert.That( source.ValueOrDefault, Is.EqualTo( default( T ) ) );
    }
    private static void Value<T>(Option<T> source, T expected_value) {
        Assert.That( source.HasValue, Is.True );
        Assert.That( source.Value, Is.EqualTo( expected_value ) );
        Assert.That( source.ValueOrDefault, Is.EqualTo( expected_value ) );
    }


}