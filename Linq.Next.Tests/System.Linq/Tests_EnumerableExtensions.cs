namespace System.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

public class Tests_EnumerableExtensions {


    // Compare
    [Test]
    public void Test_00_Compare() {
        var array = Array( 0, 1, 2 );
        var standard = Array( 2, 3, 4 );
        array.Compare( standard, out var actual_missing, out var actual_extra );
        var expected_missing = Array( 3, 4 );
        var expected_extra = Array( 0, 1 );
        Assert.That( actual_missing, Is.EqualTo( expected_missing ) );
        Assert.That( actual_extra, Is.EqualTo( expected_extra ) );
    }


    // Shuffle
    [Test]
    public void Test_01_Shuffle() {
        var array = Array( 0, 1, 2 );
        var actual = array.Shuffle( new Random( 0 ) );
        var expected = Array( 2, 0, 1 );
        Assert.That( actual, Is.EquivalentTo( expected ) );
    }


    // Slice
    [Test]
    public void Test_02_Slice() {
        var array = Array( 0, 1, 2, 3, 4, 5 );
        var actual = array.Slice( i => i is 2 or 4 );
        var expected = Array( Array( 0, 1, 2 ), Array( 3, 4 ), Array( 5 ) );
        Assert.That( actual, Is.EquivalentTo( expected ) );

        array = Array( 0, 1, 2, 3, 4, 5 );
        actual = array.Slice( i => i is 2 or 4 or 5 );
        expected = Array( Array( 0, 1, 2 ), Array( 3, 4 ), Array( 5 ) );
        Assert.That( actual, Is.EquivalentTo( expected ) );
    }


    // Tag/First-Last
    [Test]
    public static void Test_03_TagFirst() {
        var actual = Array().TagFirst().ToArray();
        var expected = default( (int, bool)[] );
        Assert.That( actual, Has.Length.Zero );

        actual = Array( 0 ).TagFirst().ToArray();
        expected = Array( (0, true) );
        Assert.That( actual, Is.EquivalentTo( expected ) );

        actual = Array( 0, 1, 2 ).TagFirst().ToArray();
        expected = Array( (0, true), (1, false), (2, false) );
        Assert.That( actual, Is.EquivalentTo( expected ) );
    }
    [Test]
    public static void Test_03_TagLast() {
        var actual = Array().TagLast().ToArray();
        var expected = default( (int, bool)[] );
        Assert.That( actual, Has.Length.Zero );

        actual = Array( 0 ).TagLast().ToArray();
        expected = Array( (0, true) );
        Assert.That( actual, Is.EquivalentTo( expected ) );

        actual = Array( 0, 1, 2 ).TagLast().ToArray();
        expected = Array( (0, false), (1, false), (2, true) );
        Assert.That( actual, Is.EquivalentTo( expected ) );
    }
    [Test]
    public static void Test_03_TagFirstLast() {
        var actual = Array().TagFirstLast().ToArray();
        var expected = default( (int, bool, bool)[] );
        Assert.That( actual, Has.Length.Zero );

        actual = Array( 0 ).TagFirstLast().ToArray();
        expected = Array( (0, true, true) );
        Assert.That( actual, Is.EquivalentTo( expected ) );

        actual = Array( 0, 1, 2 ).TagFirstLast().ToArray();
        expected = Array( (0, true, false), (1, false, false), (2, false, true) );
        Assert.That( actual, Is.EquivalentTo( expected ) );
    }


    // With/Prev-Next
    [Test]
    public static void Test_04_WithPrev() {
        var actual = Array().WithPrev().ToArray();
        var expected = default( (int, Option<int>)[] );
        Assert.That( actual, Has.Length.Zero );

        actual = Array( 0 ).WithPrev().ToArray();
        expected = Array( (0, Option()) );
        Assert.That( actual, Is.EquivalentTo( expected ) );

        actual = Array( 0, 1, 2 ).WithPrev().ToArray();
        expected = Array( (0, Option()), (1, Option( 0 )), (2, Option( 1 )) );
        Assert.That( actual, Is.EquivalentTo( expected ) );
    }
    [Test]
    public static void Test_04_WithNext() {
        var actual = Array().WithNext().ToArray();
        var expected = default( (int, Option<int>)[] );
        Assert.That( actual, Has.Length.Zero );

        actual = Array( 0 ).WithNext().ToArray();
        expected = Array( (0, Option()) );
        Assert.That( actual, Is.EquivalentTo( expected ) );

        actual = Array( 0, 1, 2 ).WithNext().ToArray();
        expected = Array( (0, Option( 1 )), (1, Option( 2 )), (2, Option()) );
        Assert.That( actual, Is.EquivalentTo( expected ) );
    }
    [Test]
    public static void Test_04_WithPrevNext() {
        var actual = Array().WithPrevNext().ToArray();
        var expected = default( (int, Option<int>, Option<int>)[] );
        Assert.That( actual, Has.Length.Zero );

        actual = Array( 0 ).WithPrevNext().ToArray();
        expected = Array( (0, Option(), Option()) );
        Assert.That( actual, Is.EquivalentTo( expected ) );

        actual = Array( 0, 1, 2 ).WithPrevNext().ToArray();
        expected = Array( (0, Option(), Option( 1 )), (1, Option( 0 ), Option( 2 )), (2, Option( 1 ), Option()) );
        Assert.That( actual, Is.EquivalentTo( expected ) );
    }


    // Helpers
    private static int[] Array(params int[] array) {
        return array;
    }
    private static T[] Array<T>(params T[] array) {
        return array;
    }
    // Helpers
    private static Option<int> Option() {
        return System.Option.Default<int>();
    }
    private static Option<int> Option(int value) {
        return System.Option.Create( value );
    }


}