namespace System.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using static NUnit.Framework.Utils;

public class Tests_EnumerableExtensions {


    // CompareTo
    [Test]
    public void CompareTo() {
        CompareTo( Array( 0, 1, 2 ), Array( 2, 3, 4 ), Array( 3, 4 ), Array( 0, 1 ) );
    }
    private static void CompareTo(int[] source, int[] standard, int[] expected_missing, int[] expected_extra) {
        source.CompareTo( standard, out var actual_missing, out var actual_extra );
        Assert.That( actual_missing, Is.EqualTo( expected_missing ) );
        Assert.That( actual_extra, Is.EqualTo( expected_extra ) );
    }


    // Split
    [Test]
    public void Split() {
        // Empty
        Split( Array(), i => true, Array2D() );
        // False
        Split( Array( 0, 1, 2, 3, 4, 5 ), i => false, Array2D( Array( 0, 1, 2, 3, 4, 5 ) ) );
        // True
        Split( Array( 0, 1, 2, 3, 4, 5 ), i => true, Array2D() );
        // 2, 3
        Split( Array( 0, 1, 2, 3, 4, 5 ), i => (i is 2 or 3), Array2D( Array( 0, 1 ), Array( 4, 5 ) ) );
        // 0, 2, 3, 5
        Split( Array( 0, 1, 2, 3, 4, 5 ), i => (i is 0 or 2 or 3 or 5), Array2D( Array( 1 ), Array( 4 ) ) );
    }
    private static void Split(int[] source, Predicate<int> predicate, int[][] expected) {
        var actual = source.Split( predicate ).ToArray();
        Assert.That( actual, Is.EquivalentTo( expected ) );
    }
    // Split/Before
    [Test]
    public void SplitBefore() {
        // Empty
        SplitBefore( Array(), i => true, Array2D() );
        // False
        SplitBefore( Array( 0, 1, 2, 3, 4, 5 ), i => false, Array2D( Array( 0, 1, 2, 3, 4, 5 ) ) );
        // True
        SplitBefore( Array( 0, 1, 2, 3, 4, 5 ), i => true, Array2D( Array( 0 ), Array( 1 ), Array( 2 ), Array( 3 ), Array( 4 ), Array( 5 ) ) );
        // 2, 3
        SplitBefore( Array( 0, 1, 2, 3, 4, 5 ), i => (i is 2 or 3), Array2D( Array( 0, 1 ), Array( 2 ), Array( 3, 4, 5 ) ) );
        // 0, 2, 3, 5
        SplitBefore( Array( 0, 1, 2, 3, 4, 5 ), i => (i is 0 or 2 or 3 or 5), Array2D( Array( 0, 1 ), Array( 2 ), Array( 3, 4 ), Array( 5 ) ) );
    }
    private static void SplitBefore(int[] source, Predicate<int> predicate, int[][] expected) {
        var actual = source.SplitBefore( predicate ).ToArray();
        Assert.That( actual, Is.EquivalentTo( expected ) );
    }
    // Split/After
    [Test]
    public void SplitAfter() {
        // Empty
        SplitAfter( Array(), i => true, Array2D() );
        // False
        SplitAfter( Array( 0, 1, 2, 3, 4, 5 ), i => false, Array2D( Array( 0, 1, 2, 3, 4, 5 ) ) );
        // True
        SplitAfter( Array( 0, 1, 2, 3, 4, 5 ), i => true, Array2D( Array( 0 ), Array( 1 ), Array( 2 ), Array( 3 ), Array( 4 ), Array( 5 ) ) );
        // 2, 3
        SplitAfter( Array( 0, 1, 2, 3, 4, 5 ), i => i is (2 or 3), Array2D( Array( 0, 1, 2 ), Array( 3 ), Array( 4, 5 ) ) );
        // 0, 2, 3, 5
        SplitAfter( Array( 0, 1, 2, 3, 4, 5 ), i => (i is 0 or 2 or 3 or 5), Array2D( Array( 0 ), Array( 1, 2 ), Array( 3 ), Array( 4, 5 ) ) );
    }
    private static void SplitAfter(int[] source, Predicate<int> predicate, int[][] expected) {
        var actual = source.SplitAfter( predicate ).ToArray();
        Assert.That( actual, Is.EquivalentTo( expected ) );
    }


    // Tag/First
    [Test]
    public static void TagFirst() {
        TagFirst( Array(), Array2D() );
        TagFirst( Array( 0 ), Array2D( (0, true) ) );
        TagFirst( Array( 0, 1, 2 ), Array2D( (0, true), (1, false), (2, false) ) );
        static (int, bool)[] Array2D(params (int, bool)[] array) => array;
    }
    private static void TagFirst(int[] source, (int, bool)[] expected) {
        var actual = source.TagFirst().ToArray();
        Assert.That( actual, Is.EquivalentTo( expected ) );
    }
    // Tag/Last
    [Test]
    public static void TagLast() {
        TagLast( Array(), Array2D() );
        TagLast( Array( 0 ), Array2D( (0, true) ) );
        TagLast( Array( 0, 1, 2 ), Array2D( (0, false), (1, false), (2, true) ) );
        static (int, bool)[] Array2D(params (int, bool)[] array) => array;
    }
    private static void TagLast(int[] source, (int, bool)[] expected) {
        var actual = source.TagLast().ToArray();
        Assert.That( actual, Is.EquivalentTo( expected ) );
    }
    // Tag/First-Last
    [Test]
    public static void TagFirstLast() {
        TagFirstLast( Array(), Array2D() );
        TagFirstLast( Array( 0 ), Array2D( (0, true, true) ) );
        TagFirstLast( Array( 0, 1, 2 ), Array2D( (0, true, false), (1, false, false), (2, false, true) ) );
        static (int, bool, bool)[] Array2D(params (int, bool, bool)[] array) => array;
    }
    private static void TagFirstLast(int[] source, (int, bool, bool)[] expected) {
        var actual = source.TagFirstLast().ToArray();
        Assert.That( actual, Is.EquivalentTo( expected ) );
    }


    // With/Prev
    [Test]
    public static void WithPrev() {
        WithPrev( Array(), Array2D() );
        WithPrev( Array( 0 ), Array2D( (0, default) ) );
        WithPrev( Array( 0, 1, 2 ), Array2D( (0, default), (1, 0), (2, 1) ) );
        static (int, Option<int>)[] Array2D(params (int, Option<int>)[] array) => array;
    }
    private static void WithPrev(int[] source, (int, Option<int>)[] expected) {
        var actual = source.WithPrev().ToArray();
        Assert.That( actual, Is.EquivalentTo( expected ) );
    }
    // With/Next
    [Test]
    public static void WithNext() {
        WithNext( Array(), Array2D() );
        WithNext( Array( 0 ), Array2D( (0, default) ) );
        WithNext( Array( 0, 1, 2 ), Array2D( (0, 1), (1, 2), (2, default) ) );
        static (int, Option<int>)[] Array2D(params (int, Option<int>)[] array) => array;
    }
    private static void WithNext(int[] source, (int, Option<int>)[] expected) {
        var actual = source.WithNext().ToArray();
        Assert.That( actual, Is.EquivalentTo( expected ) );
    }
    // With/Prev-Next
    [Test]
    public static void WithPrevNext() {
        WithPrevNext( Array(), Array2D() );
        WithPrevNext( Array( 0 ), Array2D( (0, default, default) ) );
        WithPrevNext( Array( 0, 1, 2 ), Array2D( (0, default, 1), (1, 0, 2), (2, 1, default) ) );
        static (int, Option<int>, Option<int>)[] Array2D(params (int, Option<int>, Option<int>)[] array) => array;
    }
    private static void WithPrevNext(int[] source, (int, Option<int>, Option<int>)[] expected) {
        var actual = source.WithPrevNext().ToArray();
        Assert.That( actual, Is.EquivalentTo( expected ) );
    }


}