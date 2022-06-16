namespace System.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using static NUnit.Framework.TestsHelper;

[TestFixture( TestName = "Tests_Enumerable/Extensions" )]
public class Tests_EnumerableExtensions {


    // CompareTo
    [Test]
    public void CompareTo() {
        CompareTo( SourceFactory.Array( 0, 1, 2 ), SourceFactory.Array( 2, 3, 4 ), ExpectedFactory.Array( 3, 4 ), ExpectedFactory.Array( 0, 1 ) );
    }
    private static void CompareTo(int[] first, int[] second, int[] expected_missing, int[] expected_extra) {
        first.CompareTo( second, out var actual_missing, out var actual_extra );
        Assert.That( actual_missing, Is.EqualTo( expected_missing ) );
        Assert.That( actual_extra, Is.EqualTo( expected_extra ) );
    }


    // Split
    [Test]
    public void Split() {
        // Empty
        Split( SourceFactory.Array(), i => true, ExpectedFactory.Array2D() );
        // False
        Split( SourceFactory.Array( 0, 1, 2, 3, 4, 5 ), i => false, ExpectedFactory.Array2D( (0, 1, 2, 3, 4, 5) ) );
        // True
        Split( SourceFactory.Array( 0, 1, 2, 3, 4, 5 ), i => true, ExpectedFactory.Array2D() );
        // 2, 3
        Split( SourceFactory.Array( 0, 1, 2, 3, 4, 5 ), i => (i is 2 or 3), ExpectedFactory.Array2D( (0, 1), (4, 5) ) );
        // 0, 2, 3, 5
        Split( SourceFactory.Array( 0, 1, 2, 3, 4, 5 ), i => (i is 0 or 2 or 3 or 5), ExpectedFactory.Array2D( 1, 4 ) );
    }
    private static void Split(int[] source, Predicate<int> predicate, int[][] expected) {
        var actual = source.Split( predicate ).ToArray();
        Assert.That( actual, Is.EquivalentTo( expected ) );
    }
    // Split/Before
    [Test]
    public void SplitBefore() {
        // Empty
        SplitBefore( SourceFactory.Array(), i => true, ExpectedFactory.Array2D() );
        // False
        SplitBefore( SourceFactory.Array( 0, 1, 2, 3, 4, 5 ), i => false, ExpectedFactory.Array2D( (0, 1, 2, 3, 4, 5) ) );
        // True
        SplitBefore( SourceFactory.Array( 0, 1, 2, 3, 4, 5 ), i => true, ExpectedFactory.Array2D( 0, 1, 2, 3, 4, 5 ) );
        // 2, 3
        SplitBefore( SourceFactory.Array( 0, 1, 2, 3, 4, 5 ), i => (i is 2 or 3), ExpectedFactory.Array2D( (0, 1), 2, (3, 4, 5) ) );
        // 0, 2, 3, 5
        SplitBefore( SourceFactory.Array( 0, 1, 2, 3, 4, 5 ), i => (i is 0 or 2 or 3 or 5), ExpectedFactory.Array2D( (0, 1), 2, (3, 4), 5 ) );
    }
    private static void SplitBefore(int[] source, Predicate<int> predicate, int[][] expected) {
        var actual = source.SplitBefore( predicate ).ToArray();
        Assert.That( actual, Is.EquivalentTo( expected ) );
    }
    // Split/After
    [Test]
    public void SplitAfter() {
        // Empty
        SplitAfter( SourceFactory.Array(), i => true, ExpectedFactory.Array2D() );
        // False
        SplitAfter( SourceFactory.Array( 0, 1, 2, 3, 4, 5 ), i => false, ExpectedFactory.Array2D( (0, 1, 2, 3, 4, 5) ) );
        // True
        SplitAfter( SourceFactory.Array( 0, 1, 2, 3, 4, 5 ), i => true, ExpectedFactory.Array2D( 0, 1, 2, 3, 4, 5 ) );
        // 2, 3
        SplitAfter( SourceFactory.Array( 0, 1, 2, 3, 4, 5 ), i => i is (2 or 3), ExpectedFactory.Array2D( (0, 1, 2), 3, (4, 5) ) );
        // 0, 2, 3, 5
        SplitAfter( SourceFactory.Array( 0, 1, 2, 3, 4, 5 ), i => (i is 0 or 2 or 3 or 5), ExpectedFactory.Array2D( 0, (1, 2), 3, (4, 5) ) );
    }
    private static void SplitAfter(int[] source, Predicate<int> predicate, int[][] expected) {
        var actual = source.SplitAfter( predicate ).ToArray();
        Assert.That( actual, Is.EquivalentTo( expected ) );
    }


    // Tag/First
    [Test]
    public static void TagFirst() {
        TagFirst( SourceFactory.Array(), Expected_Array2D() );
        TagFirst( SourceFactory.Array( 0 ), Expected_Array2D( (0, true) ) );
        TagFirst( SourceFactory.Array( 0, 1, 2 ), Expected_Array2D( (0, true), (1, false), (2, false) ) );
        static (int, bool)[] Expected_Array2D(params (int, bool)[] array) => array;
    }
    private static void TagFirst(int[] source, (int, bool)[] expected) {
        var actual = source.TagFirst().ToArray();
        Assert.That( actual, Is.EquivalentTo( expected ) );
    }
    // Tag/Last
    [Test]
    public static void TagLast() {
        TagLast( SourceFactory.Array(), Expected_Array2D() );
        TagLast( SourceFactory.Array( 0 ), Expected_Array2D( (0, true) ) );
        TagLast( SourceFactory.Array( 0, 1, 2 ), Expected_Array2D( (0, false), (1, false), (2, true) ) );
        static (int, bool)[] Expected_Array2D(params (int, bool)[] array) => array;
    }
    private static void TagLast(int[] source, (int, bool)[] expected) {
        var actual = source.TagLast().ToArray();
        Assert.That( actual, Is.EquivalentTo( expected ) );
    }
    // Tag/First-Last
    [Test]
    public static void TagFirstLast() {
        TagFirstLast( SourceFactory.Array(), Expected_Array2D() );
        TagFirstLast( SourceFactory.Array( 0 ), Expected_Array2D( (0, true, true) ) );
        TagFirstLast( SourceFactory.Array( 0, 1, 2 ), Expected_Array2D( (0, true, false), (1, false, false), (2, false, true) ) );
        static (int, bool, bool)[] Expected_Array2D(params (int, bool, bool)[] array) => array;
    }
    private static void TagFirstLast(int[] source, (int, bool, bool)[] expected) {
        var actual = source.TagFirstLast().ToArray();
        Assert.That( actual, Is.EquivalentTo( expected ) );
    }


    // With/Prev
    [Test]
    public static void WithPrev() {
        WithPrev( SourceFactory.Array(), Expected_Array2D() );
        WithPrev( SourceFactory.Array( 0 ), Expected_Array2D( (0, default) ) );
        WithPrev( SourceFactory.Array( 0, 1, 2 ), Expected_Array2D( (0, default), (1, 0), (2, 1) ) );
        static (int, Option<int>)[] Expected_Array2D(params (int, Option<int>)[] array) => array;
    }
    private static void WithPrev(int[] source, (int, Option<int>)[] expected) {
        var actual = source.WithPrev().ToArray();
        Assert.That( actual, Is.EquivalentTo( expected ) );
    }
    // With/Next
    [Test]
    public static void WithNext() {
        WithNext( SourceFactory.Array(), Expected_Array2D() );
        WithNext( SourceFactory.Array( 0 ), Expected_Array2D( (0, default) ) );
        WithNext( SourceFactory.Array( 0, 1, 2 ), Expected_Array2D( (0, 1), (1, 2), (2, default) ) );
        static (int, Option<int>)[] Expected_Array2D(params (int, Option<int>)[] array) => array;
    }
    private static void WithNext(int[] source, (int, Option<int>)[] expected) {
        var actual = source.WithNext().ToArray();
        Assert.That( actual, Is.EquivalentTo( expected ) );
    }
    // With/Prev-Next
    [Test]
    public static void WithPrevNext() {
        WithPrevNext( SourceFactory.Array(), Expected_Array2D() );
        WithPrevNext( SourceFactory.Array( 0 ), Expected_Array2D( (0, default, default) ) );
        WithPrevNext( SourceFactory.Array( 0, 1, 2 ), Expected_Array2D( (0, default, 1), (1, 0, 2), (2, 1, default) ) );
        static (int, Option<int>, Option<int>)[] Expected_Array2D(params (int, Option<int>, Option<int>)[] array) => array;
    }
    private static void WithPrevNext(int[] source, (int, Option<int>, Option<int>)[] expected) {
        var actual = source.WithPrevNext().ToArray();
        Assert.That( actual, Is.EquivalentTo( expected ) );
    }


}