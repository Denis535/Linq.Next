namespace System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

public class Tests_Option {


    [Test]
    public void Test_00_Constructor() {
        _ = new Option<object>();
    }
    [TestCase( null )]
    public void Test_00_Constructor(object value) {
        _ = new Option<object>( value );
    }
    [TestCase( 777 )]
    public void Test_00_Constructor<T>(T value) {
        _ = new Option<T>( value );
    }


    [Test]
    public void Test_01_Value() {
        var option = new Option<object>();

        Assert.That( option.HasValue, Is.False );
        Assert.Throws<InvalidOperationException>( () => _ = option.Value );
        Assert.That( option.ValueOrDefault, Is.Null );

        Assert.That( option.TryGetValue( out var val ), Is.False );
        Assert.That( val, Is.Null );
    }
    [TestCase( null )]
    public void Test_01_Value(object value) {
        var option = new Option<object?>( value );

        Assert.That( option.HasValue, Is.True );
        Assert.That( option.Value, Is.EqualTo( value ) );
        Assert.That( option.ValueOrDefault, Is.EqualTo( value ) );

        var hasVal = option.TryGetValue( out var val );
        Assert.That( hasVal, Is.True );
        Assert.That( val, Is.EqualTo( value ) );
    }
    [TestCase( 777 )]
    public void Test_01_Value<T>(T value) {
        var option = new Option<T>( value );

        Assert.That( option.HasValue, Is.True );
        Assert.That( option.Value, Is.EqualTo( value ) );
        Assert.That( option.ValueOrDefault, Is.EqualTo( value ) );

        var hasVal = option.TryGetValue( out var val );
        Assert.That( hasVal, Is.True );
        Assert.That( val, Is.EqualTo( value ) );
    }


}